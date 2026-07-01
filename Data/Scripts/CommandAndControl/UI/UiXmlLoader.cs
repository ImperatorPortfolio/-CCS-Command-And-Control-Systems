using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Sandbox.ModAPI;
using VRage.Game.GUI.TextPanel;
using VRageMath;

namespace AGS
{
    public static class UiXmlLoader
    {
        private sealed class UiMarkupNode
        {
            public string Name;
            public readonly Dictionary<string, string> Attributes = new Dictionary<string, string>();
            public readonly List<UiMarkupNode> Children = new List<UiMarkupNode>();
        }

        // Parsed view documents are immutable and rebuilt into fresh elements on every
        // call, so the disk read + parse only needs to happen once per view path.
        private static readonly Dictionary<string, UiMarkupNode> DocumentCache = new Dictionary<string, UiMarkupNode>();

        public static void ClearCache()
        {
            DocumentCache.Clear();
        }

        public static UiElement Build(string relativePath, UiContext context, UiXmlBindings bindings, Dictionary<string, Action<MySpriteDrawFrame, Vector2, UiRect, UiContext>> visuals)
        {
            try
            {
                UiMarkupNode document;
                if (!DocumentCache.TryGetValue(relativePath, out document))
                {
                    document = LoadDocument(relativePath);
                    if (document != null)
                    {
                        DocumentCache[relativePath] = document;
                    }
                }

                if (document == null)
                {
                    return BuildError("Missing XML view: " + relativePath, context);
                }

                var root = string.Equals(document.Name, "View", StringComparison.OrdinalIgnoreCase) ? FirstChild(document) : document;
                if (root == null)
                {
                    return BuildError("Empty XML view: " + relativePath, context);
                }

                return BuildElement(root, context, bindings, visuals);
            }
            catch (Exception exception)
            {
                return BuildError("XML view error: " + exception.Message, context);
            }
        }

        // Builds an element tree from a markup string rather than a file. Used to render
        // a program's serialized ProgramFrame (markup arrives over the wire, not from the
        // mod's own Data folder). When cacheKey is supplied the parsed document is reused
        // across frames; pass null for one-off markup to skip caching. External programs
        // supply no custom-visual delegates, so visuals defaults to none.
        public static UiElement BuildFromMarkup(string cacheKey, string markup, UiContext context, UiXmlBindings bindings, Dictionary<string, Action<MySpriteDrawFrame, Vector2, UiRect, UiContext>> visuals = null)
        {
            try
            {
                if (string.IsNullOrEmpty(markup))
                {
                    return BuildError("Empty program markup", context);
                }

                UiMarkupNode document = null;
                var key = string.IsNullOrEmpty(cacheKey) ? null : "markup:" + cacheKey;
                if (key == null || !DocumentCache.TryGetValue(key, out document))
                {
                    document = Parse(markup);
                    if (document != null && key != null)
                    {
                        DocumentCache[key] = document;
                    }
                }

                if (document == null)
                {
                    return BuildError("Unparseable program markup", context);
                }

                var root = string.Equals(document.Name, "View", StringComparison.OrdinalIgnoreCase) ? FirstChild(document) : document;
                if (root == null)
                {
                    return BuildError("Empty program markup", context);
                }

                return BuildElement(root, context, bindings, visuals);
            }
            catch (Exception exception)
            {
                return BuildError("Program markup error: " + exception.Message, context);
            }
        }

        private static UiMarkupNode LoadDocument(string relativePath)
        {
            var utilities = MyAPIGateway.Utilities;
            if (utilities == null || !Session.ModItem.HasValue)
            {
                return null;
            }

            using (TextReader reader = utilities.ReadFileInModLocation(relativePath, Session.ModItem.Value))
            {
                return Parse(reader.ReadToEnd());
            }
        }

        private static UiMarkupNode Parse(string text)
        {
            UiMarkupNode root = null;
            var stack = new List<UiMarkupNode>();
            var index = 0;

            while (index < text.Length)
            {
                var open = text.IndexOf('<', index);
                if (open < 0)
                {
                    break;
                }

                if (open + 1 < text.Length && text[open + 1] == '!')
                {
                    var ignoredClose = text.IndexOf('>', open + 1);
                    if (ignoredClose < 0)
                    {
                        break;
                    }

                    index = ignoredClose + 1;
                    continue;
                }

                var close = FindTagClose(text, open + 1);
                if (close < 0)
                {
                    break;
                }

                var tag = text.Substring(open + 1, close - open - 1).Trim();
                index = close + 1;
                if (tag.Length == 0)
                {
                    continue;
                }

                if (tag[0] == '/')
                {
                    if (stack.Count > 0)
                    {
                        stack.RemoveAt(stack.Count - 1);
                    }
                    continue;
                }

                var selfClosing = tag[tag.Length - 1] == '/';
                if (selfClosing)
                {
                    tag = tag.Substring(0, tag.Length - 1).TrimEnd();
                }

                var node = ParseNode(tag);
                if (node == null)
                {
                    continue;
                }

                if (stack.Count > 0)
                {
                    stack[stack.Count - 1].Children.Add(node);
                }
                else
                {
                    root = node;
                }

                if (!selfClosing)
                {
                    stack.Add(node);
                }
            }

            return root;
        }

        private static int FindTagClose(string text, int start)
        {
            var inQuote = false;
            for (var i = start; i < text.Length; i++)
            {
                var ch = text[i];
                if (ch == '"')
                {
                    inQuote = !inQuote;
                }
                else if (ch == '>' && !inQuote)
                {
                    return i;
                }
            }

            return -1;
        }

        private static UiMarkupNode ParseNode(string tag)
        {
            var index = 0;
            SkipWhitespace(tag, ref index);
            var nameStart = index;
            while (index < tag.Length && !char.IsWhiteSpace(tag[index]))
            {
                index++;
            }

            if (index <= nameStart)
            {
                return null;
            }

            var node = new UiMarkupNode();
            node.Name = tag.Substring(nameStart, index - nameStart);

            while (index < tag.Length)
            {
                SkipWhitespace(tag, ref index);
                if (index >= tag.Length)
                {
                    break;
                }

                var keyStart = index;
                while (index < tag.Length && tag[index] != '=' && !char.IsWhiteSpace(tag[index]))
                {
                    index++;
                }

                if (index <= keyStart)
                {
                    break;
                }

                var key = tag.Substring(keyStart, index - keyStart);
                SkipWhitespace(tag, ref index);
                if (index >= tag.Length || tag[index] != '=')
                {
                    node.Attributes[key] = string.Empty;
                    continue;
                }

                index++;
                SkipWhitespace(tag, ref index);
                if (index >= tag.Length)
                {
                    node.Attributes[key] = string.Empty;
                    break;
                }

                string value;
                if (tag[index] == '"')
                {
                    index++;
                    var valueStart = index;
                    while (index < tag.Length && tag[index] != '"')
                    {
                        index++;
                    }
                    value = tag.Substring(valueStart, index - valueStart);
                    if (index < tag.Length && tag[index] == '"')
                    {
                        index++;
                    }
                }
                else
                {
                    var valueStart = index;
                    while (index < tag.Length && !char.IsWhiteSpace(tag[index]))
                    {
                        index++;
                    }
                    value = tag.Substring(valueStart, index - valueStart);
                }

                node.Attributes[key] = value;
            }

            return node;
        }

        private static UiElement BuildElement(UiMarkupNode node, UiContext context, UiXmlBindings bindings, Dictionary<string, Action<MySpriteDrawFrame, Vector2, UiRect, UiContext>> visuals)
        {
            if (node == null)
            {
                return null;
            }

            UiElement element;
            switch ((node.Name ?? string.Empty).ToLowerInvariant())
            {
                case "border":
                    element = BuildBorder(node, context, bindings, visuals);
                    break;
                case "grid":
                    element = BuildGrid(node, context, bindings, visuals);
                    break;
                case "stack":
                case "stackpanel":
                    element = BuildStack(node, context, bindings, visuals);
                    break;
                case "dock":
                case "dockpanel":
                    element = BuildDock(node, context, bindings, visuals);
                    break;
                case "canvas":
                case "canvaspanel":
                    element = BuildCanvas(node, context, bindings, visuals);
                    break;
                case "sectionheader":
                    element = BuildSectionHeader(node, bindings);
                    break;
                case "valuetile":
                    element = BuildValueTile(node, bindings);
                    break;
                case "statusbadge":
                    element = BuildStatusBadge(node, bindings);
                    break;
                case "metricbar":
                    element = BuildMetricBar(node, bindings);
                    break;
                case "text":
                case "textblock":
                    element = BuildText(node, bindings);
                    break;
                case "button":
                    element = BuildButton(node, bindings);
                    break;
                case "iconbutton":
                    element = BuildIconButton(node, bindings);
                    break;
                case "textbox":
                    element = BuildTextBox(node, bindings);
                    break;
                case "togglebutton":
                    element = BuildToggleButton(node, bindings);
                    break;
                case "checkbox":
                    element = BuildCheckbox(node, bindings);
                    break;
                case "slider":
                    element = BuildSlider(node, bindings);
                    break;
                case "scrollbar":
                    element = BuildScrollBar(node, bindings);
                    break;
                case "scrollviewer":
                    element = BuildScrollViewer(node, bindings, visuals, context);
                    break;
                case "radiobutton":
                    element = BuildRadioButton(node, bindings);
                    break;
                case "stepper":
                    element = BuildStepper(node, bindings);
                    break;
                case "progressbar":
                    element = BuildProgressBar(node, bindings);
                    break;
                case "separator":
                    element = BuildSeparator(node, bindings);
                    break;
                case "image":
                    element = BuildImage(node, bindings);
                    break;
                case "keypad":
                    element = BuildKeypad(node, bindings);
                    break;
                case "tabstrip":
                    element = BuildTabStrip(node, bindings);
                    break;
                case "dropdown":
                    element = BuildDropdown(node, bindings);
                    break;
                case "uniformgrid":
                    element = BuildUniformGrid(node, context, bindings, visuals);
                    break;
                case "wrappanel":
                case "wrap":
                    element = BuildWrapPanel(node, context, bindings, visuals);
                    break;
                case "repeatbutton":
                    element = BuildRepeatButton(node, bindings);
                    break;
                case "rectangle":
                    element = BuildRectangle(node, bindings);
                    break;
                case "ellipse":
                    element = BuildEllipse(node, bindings);
                    break;
                case "line":
                    element = BuildLine(node, bindings);
                    break;
                case "chart":
                    element = BuildChart(node, bindings);
                    break;
                case "table":
                    element = BuildTable(node, bindings);
                    break;
                case "detailsview":
                    element = BuildDetailsView(node, bindings);
                    break;
                case "groupbox":
                    element = BuildGroupBox(node, context, bindings, visuals);
                    break;
                case "expander":
                    element = BuildExpander(node, context, bindings, visuals);
                    break;
                case "tabcontrol":
                    element = BuildTabControl(node, context, bindings, visuals);
                    break;
                case "tabitem":
                    element = BuildTabItem(node, context, bindings, visuals);
                    break;
                case "dialog":
                    element = BuildDialog(node, context, bindings, visuals);
                    break;
                case "customvisual":
                    element = BuildCustomVisual(node, bindings, visuals);
                    break;
                default:
                    element = BuildUnknown(node, context);
                    break;
            }

            ApplySharedProperties(element, node, bindings);
            return element;
        }

        private static UiElement BuildBorder(UiMarkupNode node, UiContext context, UiXmlBindings bindings, Dictionary<string, Action<MySpriteDrawFrame, Vector2, UiRect, UiContext>> visuals)
        {
            var border = new Border
            {
                Background = ResolveColor(node, bindings, "background", Color.Transparent),
                BorderBrush = ResolveColor(node, bindings, "border", Color.Transparent),
                BorderThickness = ResolveFloat(node, bindings, "borderThickness", 0f),
                Padding = ResolveThickness(node, bindings, "padding", new UiThickness(0f))
            };
            border.Content = BuildSingleChild(node, context, bindings, visuals);
            return border;
        }

        private static UiElement BuildGrid(UiMarkupNode node, UiContext context, UiXmlBindings bindings, Dictionary<string, Action<MySpriteDrawFrame, Vector2, UiRect, UiContext>> visuals)
        {
            var grid = new GridPanel();
            ApplyGridDefinitions(grid.Rows, GetAttribute(node, "rows"));
            ApplyGridDefinitions(grid.Columns, GetAttribute(node, "columns"));
            AddChildren(grid, node, context, bindings, visuals);
            return grid;
        }

        private static UiElement BuildStack(UiMarkupNode node, UiContext context, UiXmlBindings bindings, Dictionary<string, Action<MySpriteDrawFrame, Vector2, UiRect, UiContext>> visuals)
        {
            var stack = new StackPanel
            {
                Orientation = ResolveOrientation(node, bindings, "orientation", UiOrientation.Vertical),
                Spacing = ResolveFloat(node, bindings, "spacing", 0f)
            };
            AddChildren(stack, node, context, bindings, visuals);
            return stack;
        }

        private static UiElement BuildDock(UiMarkupNode node, UiContext context, UiXmlBindings bindings, Dictionary<string, Action<MySpriteDrawFrame, Vector2, UiRect, UiContext>> visuals)
        {
            var dock = new DockPanel();
            AddChildren(dock, node, context, bindings, visuals);
            return dock;
        }

        private static UiElement BuildCanvas(UiMarkupNode node, UiContext context, UiXmlBindings bindings, Dictionary<string, Action<MySpriteDrawFrame, Vector2, UiRect, UiContext>> visuals)
        {
            var canvas = new CanvasPanel();
            AddChildren(canvas, node, context, bindings, visuals);
            return canvas;
        }

        private static UiElement BuildSectionHeader(UiMarkupNode node, UiXmlBindings bindings)
        {
            return new SectionHeader
            {
                Title = ResolveString(node, bindings, "title", string.Empty),
                Detail = ResolveString(node, bindings, "detail", string.Empty)
            };
        }

        private static UiElement BuildValueTile(UiMarkupNode node, UiXmlBindings bindings)
        {
            return new ValueTile
            {
                Caption = ResolveString(node, bindings, "caption", string.Empty),
                Value = ResolveString(node, bindings, "value", string.Empty),
                ValueColor = ResolveColor(node, bindings, "valueColor", Color.Transparent)
            };
        }

        private static UiElement BuildStatusBadge(UiMarkupNode node, UiXmlBindings bindings)
        {
            return new StatusBadge
            {
                Label = ResolveString(node, bindings, "label", string.Empty),
                Value = ResolveString(node, bindings, "value", string.Empty),
                ValueColor = ResolveColor(node, bindings, "valueColor", Color.Transparent)
            };
        }

        private static UiElement BuildMetricBar(UiMarkupNode node, UiXmlBindings bindings)
        {
            return new MetricBar
            {
                Label = ResolveString(node, bindings, "label", string.Empty),
                ValueText = ResolveString(node, bindings, "valueText", string.Empty),
                Ratio = ResolveFloat(node, bindings, "ratio", 0f),
                FillColor = ResolveColor(node, bindings, "fillColor", Color.Transparent)
            };
        }

        private static UiElement BuildText(UiMarkupNode node, UiXmlBindings bindings)
        {
            return new TextBlock
            {
                Text = ResolveString(node, bindings, "text", string.Empty),
                Font = ResolveString(node, bindings, "font", "Monospace"),
                Scale = ResolveFloat(node, bindings, "scale", 0.42f),
                Color = ResolveColor(node, bindings, "color", Color.White),
                Alignment = ResolveTextAlignment(node, bindings, "alignment", TextAlignment.LEFT),
                TextVerticalAlignment = ResolveVerticalAlignment(node, bindings, "textVerticalAlignment", UiVerticalAlignment.Center),
                Padding = ResolveThickness(node, bindings, "padding", new UiThickness(0f))
            };
        }

        private static UiElement BuildButton(UiMarkupNode node, UiXmlBindings bindings)
        {
            return new Button
            {
                Text = ResolveString(node, bindings, "text", string.Empty),
                Font = ResolveString(node, bindings, "font", "Monospace"),
                Scale = ResolveFloat(node, bindings, "scale", 0.42f),
                Padding = ResolveThickness(node, bindings, "padding", new UiThickness(2f)),
                Alignment = ResolveTextAlignment(node, bindings, "alignment", TextAlignment.CENTER),
                Command = ResolveCommand(node, bindings)
            };
        }

        private static UiElement BuildIconButton(UiMarkupNode node, UiXmlBindings bindings)
        {
            return new IconButton
            {
                Icon = ResolveIcon(node, bindings, "icon", IconKind.Gear),
                Caption = ResolveString(node, bindings, "caption", string.Empty),
                Active = ResolveBool(node, bindings, "active", false),
                Text = ResolveString(node, bindings, "text", string.Empty),
                Font = ResolveString(node, bindings, "font", "Monospace"),
                Scale = ResolveFloat(node, bindings, "scale", 0.42f),
                Padding = ResolveThickness(node, bindings, "padding", new UiThickness(2f)),
                Alignment = ResolveTextAlignment(node, bindings, "alignment", TextAlignment.CENTER),
                Command = ResolveCommand(node, bindings)
            };
        }

        private static UiElement BuildTextBox(UiMarkupNode node, UiXmlBindings bindings)
        {
            return new TextBox
            {
                Text = ResolveString(node, bindings, "text", string.Empty),
                Placeholder = ResolveString(node, bindings, "placeholder", string.Empty),
                Scale = ResolveFloat(node, bindings, "scale", 0.42f),
                Padding = ResolveThickness(node, bindings, "padding", new UiThickness(4f, 2f))
            };
        }

        private static UiElement BuildToggleButton(UiMarkupNode node, UiXmlBindings bindings)
        {
            return new ToggleButton
            {
                Text = ResolveString(node, bindings, "text", string.Empty),
                Font = ResolveString(node, bindings, "font", "Monospace"),
                Scale = ResolveFloat(node, bindings, "scale", 0.42f),
                Padding = ResolveThickness(node, bindings, "padding", new UiThickness(2f)),
                Alignment = ResolveTextAlignment(node, bindings, "alignment", TextAlignment.CENTER),
                IsChecked = ResolveBool(node, bindings, "checked", false),
                Command = ResolveCommand(node, bindings)
            };
        }

        private static UiElement BuildCheckbox(UiMarkupNode node, UiXmlBindings bindings)
        {
            return new Checkbox
            {
                Text = ResolveString(node, bindings, "text", string.Empty),
                IsChecked = ResolveBool(node, bindings, "checked", false),
                Command = ResolveCommand(node, bindings)
            };
        }

        private static UiElement BuildSlider(UiMarkupNode node, UiXmlBindings bindings)
        {
            return new Slider
            {
                Label = ResolveString(node, bindings, "label", string.Empty),
                ValueText = ResolveString(node, bindings, "valueText", string.Empty),
                Ratio = ResolveFloat(node, bindings, "ratio", 0f),
                FillColor = ResolveColor(node, bindings, "fillColor", Color.Transparent),
                MinValue = ResolveInt(node, bindings, "minValue", 0),
                MaxValue = ResolveInt(node, bindings, "maxValue", 100),
                Command = ResolveCommand(node, bindings)
            };
        }

        private static UiElement BuildScrollBar(UiMarkupNode node, UiXmlBindings bindings)
        {
            return new ScrollBar
            {
                Value = ResolveInt(node, bindings, "value", 0),
                MaxValue = ResolveInt(node, bindings, "maxValue", 0),
                ViewSize = ResolveInt(node, bindings, "viewSize", 1),
                Vertical = ResolveBool(node, bindings, "vertical", true),
                Command = ResolveCommand(node, bindings)
            };
        }

        private static UiElement BuildScrollViewer(UiMarkupNode node, UiXmlBindings bindings, Dictionary<string, Action<MySpriteDrawFrame, Vector2, UiRect, UiContext>> visuals, UiContext context)
        {
            var viewer = new ScrollViewer
            {
                ScrollUpCommand = ResolveCommand(node, bindings, "scrollUpCommand", "scrollUpValue", "scrollUpData"),
                ScrollDownCommand = ResolveCommand(node, bindings, "scrollDownCommand", "scrollDownValue", "scrollDownData")
            };
            viewer.Content = BuildSingleChild(node, context, bindings, visuals);
            return viewer;
        }

        private static UiElement BuildRadioButton(UiMarkupNode node, UiXmlBindings bindings)
        {
            return new RadioButton
            {
                Text = ResolveString(node, bindings, "text", string.Empty),
                IsSelected = ResolveBool(node, bindings, "selected", false),
                GroupValue = ResolveInt(node, bindings, "groupValue", 0),
                Command = ResolveCommand(node, bindings)
            };
        }

        private static UiElement BuildStepper(UiMarkupNode node, UiXmlBindings bindings)
        {
            return new Stepper
            {
                Label = ResolveString(node, bindings, "label", string.Empty),
                ValueSuffix = ResolveString(node, bindings, "valueSuffix", string.Empty),
                Value = ResolveInt(node, bindings, "value", 0),
                MinValue = ResolveInt(node, bindings, "minValue", 0),
                MaxValue = ResolveInt(node, bindings, "maxValue", 100),
                Step = ResolveInt(node, bindings, "step", 1),
                Command = ResolveCommand(node, bindings)
            };
        }

        private static UiElement BuildProgressBar(UiMarkupNode node, UiXmlBindings bindings)
        {
            return new ProgressBar
            {
                Ratio = ResolveFloat(node, bindings, "ratio", 0f),
                ValueText = ResolveString(node, bindings, "valueText", string.Empty),
                FillColor = ResolveColor(node, bindings, "fillColor", Color.Transparent)
            };
        }

        private static UiElement BuildSeparator(UiMarkupNode node, UiXmlBindings bindings)
        {
            return new Separator
            {
                Vertical = ResolveBool(node, bindings, "vertical", false),
                Color = ResolveColor(node, bindings, "color", Color.Transparent),
                Thickness = ResolveFloat(node, bindings, "thickness", 1f)
            };
        }

        private static UiElement BuildImage(UiMarkupNode node, UiXmlBindings bindings)
        {
            return new ImageControl
            {
                Sprite = ResolveString(node, bindings, "sprite", string.Empty),
                Tint = ResolveColor(node, bindings, "tint", Color.White),
                Command = ResolveCommand(node, bindings)
            };
        }

        private static UiElement BuildKeypad(UiMarkupNode node, UiXmlBindings bindings)
        {
            return new Keypad
            {
                Command = ResolveCommand(node, bindings)
            };
        }

        private static UiElement BuildTabStrip(UiMarkupNode node, UiXmlBindings bindings)
        {
            var tabStrip = new TabStrip
            {
                SelectedIndex = ResolveInt(node, bindings, "selectedIndex", 0),
                Command = ResolveCommand(node, bindings)
            };
            tabStrip.SetTabs(ResolveString(node, bindings, "tabs", string.Empty));
            return tabStrip;
        }

        private static UiElement BuildDropdown(UiMarkupNode node, UiXmlBindings bindings)
        {
            var dropdown = new Dropdown
            {
                SelectedIndex = ResolveInt(node, bindings, "selectedIndex", -1),
                IsOpen = ResolveBool(node, bindings, "open", false),
                Placeholder = ResolveString(node, bindings, "placeholder", string.Empty),
                ToggleCommand = ResolveCommand(node, bindings, "toggleCommand", "toggleValue", "toggleData"),
                SelectCommand = ResolveCommand(node, bindings, "selectCommand", "selectValue", "selectData")
            };
            dropdown.SetOptions(ResolveString(node, bindings, "options", string.Empty));
            return dropdown;
        }

        private static UiElement BuildUniformGrid(UiMarkupNode node, UiContext context, UiXmlBindings bindings, Dictionary<string, Action<MySpriteDrawFrame, Vector2, UiRect, UiContext>> visuals)
        {
            var grid = new UniformGrid
            {
                Rows = ResolveInt(node, bindings, "rows", 0),
                Columns = ResolveInt(node, bindings, "columns", 0),
                Spacing = ResolveFloat(node, bindings, "spacing", 0f)
            };
            AddChildren(grid, node, context, bindings, visuals);
            return grid;
        }

        private static UiElement BuildWrapPanel(UiMarkupNode node, UiContext context, UiXmlBindings bindings, Dictionary<string, Action<MySpriteDrawFrame, Vector2, UiRect, UiContext>> visuals)
        {
            var wrap = new WrapPanel
            {
                Orientation = ResolveOrientation(node, bindings, "orientation", UiOrientation.Horizontal),
                ItemSpacing = ResolveFloat(node, bindings, "itemSpacing", 0f),
                LineSpacing = ResolveFloat(node, bindings, "lineSpacing", 0f)
            };
            AddChildren(wrap, node, context, bindings, visuals);
            return wrap;
        }

        private static UiElement BuildRepeatButton(UiMarkupNode node, UiXmlBindings bindings)
        {
            return new RepeatButton
            {
                Text = ResolveString(node, bindings, "text", string.Empty),
                Font = ResolveString(node, bindings, "font", "Monospace"),
                Scale = ResolveFloat(node, bindings, "scale", 0.42f),
                Padding = ResolveThickness(node, bindings, "padding", new UiThickness(2f)),
                Alignment = ResolveTextAlignment(node, bindings, "alignment", TextAlignment.CENTER),
                Command = ResolveCommand(node, bindings)
            };
        }

        private static UiElement BuildRectangle(UiMarkupNode node, UiXmlBindings bindings)
        {
            return new RectangleShape
            {
                Fill = ResolveColor(node, bindings, "fill", Color.Transparent),
                Stroke = ResolveColor(node, bindings, "stroke", Color.Transparent),
                StrokeThickness = ResolveFloat(node, bindings, "strokeThickness", 0f)
            };
        }

        private static UiElement BuildEllipse(UiMarkupNode node, UiXmlBindings bindings)
        {
            return new EllipseShape
            {
                Fill = ResolveColor(node, bindings, "fill", Color.Transparent),
                Stroke = ResolveColor(node, bindings, "stroke", Color.Transparent),
                StrokeThickness = ResolveFloat(node, bindings, "strokeThickness", 0f)
            };
        }

        private static UiElement BuildLine(UiMarkupNode node, UiXmlBindings bindings)
        {
            return new LineShape
            {
                Stroke = ResolveColor(node, bindings, "stroke", Color.Transparent),
                StrokeThickness = ResolveFloat(node, bindings, "strokeThickness", 1f),
                X1 = ResolveFloat(node, bindings, "x1", 0f),
                Y1 = ResolveFloat(node, bindings, "y1", 0.5f),
                X2 = ResolveFloat(node, bindings, "x2", 1f),
                Y2 = ResolveFloat(node, bindings, "y2", 0.5f)
            };
        }

        private static UiElement BuildChart(UiMarkupNode node, UiXmlBindings bindings)
        {
            var chart = new Chart
            {
                Kind = ResolveChartKind(node, bindings, "kind", ChartKind.Line),
                Minimum = ResolveFloat(node, bindings, "minimum", 0f),
                Maximum = ResolveFloat(node, bindings, "maximum", 0f),
                LineColor = ResolveColor(node, bindings, "lineColor", Color.Transparent),
                FillColor = ResolveColor(node, bindings, "fillColor", Color.Transparent),
                ShowFrame = ResolveBool(node, bindings, "showFrame", true)
            };
            chart.SetValues(ResolveString(node, bindings, "values", string.Empty));
            return chart;
        }

        private static UiElement BuildTable(UiMarkupNode node, UiXmlBindings bindings)
        {
            var table = new Table
            {
                ShowHeader = ResolveBool(node, bindings, "showHeader", true),
                RowHeight = ResolveFloat(node, bindings, "rowHeight", 22f)
            };
            table.SetColumns(ResolveString(node, bindings, "columns", string.Empty));
            table.SetRows(ResolveString(node, bindings, "rows", string.Empty));
            return table;
        }

        private static UiElement BuildDetailsView(UiMarkupNode node, UiXmlBindings bindings)
        {
            var details = new DetailsView
            {
                RowHeight = ResolveFloat(node, bindings, "rowHeight", 20f),
                LabelWeight = ResolveFloat(node, bindings, "labelWeight", 0.45f)
            };
            details.SetRows(ResolveString(node, bindings, "rows", string.Empty));
            return details;
        }

        private static UiElement BuildGroupBox(UiMarkupNode node, UiContext context, UiXmlBindings bindings, Dictionary<string, Action<MySpriteDrawFrame, Vector2, UiRect, UiContext>> visuals)
        {
            var group = new GroupBox
            {
                Header = ResolveString(node, bindings, "header", string.Empty),
                Background = ResolveColor(node, bindings, "background", Color.Transparent),
                BorderColor = ResolveColor(node, bindings, "border", Color.Transparent),
                Padding = ResolveThickness(node, bindings, "padding", new UiThickness(6f))
            };
            group.Content = BuildSingleChild(node, context, bindings, visuals);
            return group;
        }

        private static UiElement BuildExpander(UiMarkupNode node, UiContext context, UiXmlBindings bindings, Dictionary<string, Action<MySpriteDrawFrame, Vector2, UiRect, UiContext>> visuals)
        {
            var expander = new Expander
            {
                Header = ResolveString(node, bindings, "header", string.Empty),
                IsExpanded = ResolveBool(node, bindings, "expanded", false),
                ToggleCommand = ResolveCommand(node, bindings)
            };
            expander.Content = BuildSingleChild(node, context, bindings, visuals);
            return expander;
        }

        private static UiElement BuildTabControl(UiMarkupNode node, UiContext context, UiXmlBindings bindings, Dictionary<string, Action<MySpriteDrawFrame, Vector2, UiRect, UiContext>> visuals)
        {
            var tabs = new TabControl
            {
                SelectedIndex = ResolveInt(node, bindings, "selectedIndex", 0),
                Command = ResolveCommand(node, bindings)
            };
            for (var i = 0; i < node.Children.Count; i++)
            {
                var child = BuildElement(node.Children[i], context, bindings, visuals);
                var item = child as TabItem;
                if (item != null)
                {
                    tabs.AddChild(item);
                }
            }
            return tabs;
        }

        private static UiElement BuildTabItem(UiMarkupNode node, UiContext context, UiXmlBindings bindings, Dictionary<string, Action<MySpriteDrawFrame, Vector2, UiRect, UiContext>> visuals)
        {
            var item = new TabItem
            {
                Header = ResolveString(node, bindings, "header", string.Empty)
            };
            item.Content = BuildSingleChild(node, context, bindings, visuals);
            return item;
        }

        private static UiElement BuildDialog(UiMarkupNode node, UiContext context, UiXmlBindings bindings, Dictionary<string, Action<MySpriteDrawFrame, Vector2, UiRect, UiContext>> visuals)
        {
            var dialog = new Dialog
            {
                Title = ResolveString(node, bindings, "title", string.Empty),
                ShowClose = ResolveBool(node, bindings, "showClose", true),
                DismissOnBackdrop = ResolveBool(node, bindings, "dismissOnBackdrop", true),
                PanelWidth = ResolveFloat(node, bindings, "panelWidth", -1f),
                PanelHeight = ResolveFloat(node, bindings, "panelHeight", -1f),
                Background = ResolveColor(node, bindings, "background", Color.Transparent),
                BorderColor = ResolveColor(node, bindings, "border", Color.Transparent),
                BackdropColor = ResolveColor(node, bindings, "backdropColor", Color.Transparent),
                Padding = ResolveThickness(node, bindings, "padding", new UiThickness(8f)),
                CloseCommand = ResolveCommand(node, bindings, "closeCommand", "closeValue", "closeData")
            };
            dialog.Content = BuildSingleChild(node, context, bindings, visuals);
            return dialog;
        }

        private static ChartKind ResolveChartKind(UiMarkupNode node, UiXmlBindings bindings, string key, ChartKind fallback)
        {
            var raw = ResolveString(node, bindings, key, string.Empty);
            switch ((raw ?? string.Empty).ToLowerInvariant())
            {
                case "line":
                    return ChartKind.Line;
                case "bar":
                    return ChartKind.Bar;
                default:
                    return fallback;
            }
        }

        private static UiElement BuildCustomVisual(UiMarkupNode node, UiXmlBindings bindings, Dictionary<string, Action<MySpriteDrawFrame, Vector2, UiRect, UiContext>> visuals)
        {
            var visualKey = ResolveString(node, bindings, "visual", string.Empty);
            Action<MySpriteDrawFrame, Vector2, UiRect, UiContext> renderAction = null;
            if (!string.IsNullOrEmpty(visualKey) && visuals != null)
            {
                visuals.TryGetValue(visualKey, out renderAction);
            }

            return new CustomVisual
            {
                RenderAction = renderAction,
                ClickCommand = ResolveCommand(node, bindings)
            };
        }

        private static UiElement BuildUnknown(UiMarkupNode node, UiContext context)
        {
            return BuildError("Unknown XML element: " + node.Name, context);
        }

        private static UiElement BuildError(string message, UiContext context)
        {
            return new Border
            {
                Background = context.Theme.Background1,
                BorderBrush = context.Theme.Danger,
                BorderThickness = 1f,
                Padding = new UiThickness(8f),
                Content = new TextBlock
                {
                    Text = message,
                    Font = "Monospace",
                    Scale = Math.Max(context.Theme.TextSm, 0.36f),
                    Color = context.Theme.Danger,
                    Alignment = TextAlignment.LEFT,
                    TextVerticalAlignment = UiVerticalAlignment.Top
                }
            };
        }

        private static UiElement BuildSingleChild(UiMarkupNode node, UiContext context, UiXmlBindings bindings, Dictionary<string, Action<MySpriteDrawFrame, Vector2, UiRect, UiContext>> visuals)
        {
            if (node.Children.Count == 0)
            {
                return null;
            }

            return BuildElement(node.Children[0], context, bindings, visuals);
        }

        private static void AddChildren(UiElement parent, UiMarkupNode node, UiContext context, UiXmlBindings bindings, Dictionary<string, Action<MySpriteDrawFrame, Vector2, UiRect, UiContext>> visuals)
        {
            for (var i = 0; i < node.Children.Count; i++)
            {
                var child = BuildElement(node.Children[i], context, bindings, visuals);
                if (child != null)
                {
                    parent.AddChild(child);
                }
            }
        }

        private static void ApplySharedProperties(UiElement element, UiMarkupNode node, UiXmlBindings bindings)
        {
            if (element == null || node == null)
            {
                return;
            }

            element.Name = ResolveString(node, bindings, "name", element.Name);
            element.Margin = ResolveThickness(node, bindings, "margin", element.Margin);
            element.Width = ResolveFloat(node, bindings, "width", element.Width);
            element.Height = ResolveFloat(node, bindings, "height", element.Height);
            element.MinWidth = ResolveFloat(node, bindings, "minWidth", element.MinWidth);
            element.MinHeight = ResolveFloat(node, bindings, "minHeight", element.MinHeight);
            element.HorizontalAlignment = ResolveHorizontalAlignment(node, bindings, "horizontalAlignment", element.HorizontalAlignment);
            element.VerticalAlignment = ResolveVerticalAlignment(node, bindings, "verticalAlignment", element.VerticalAlignment);
            element.Visibility = ResolveBool(node, bindings, "collapsed", false) ? UiVisibility.Collapsed : UiVisibility.Visible;
            element.ClipToBounds = ResolveBool(node, bindings, "clip", element.ClipToBounds);
            element.Focusable = ResolveBool(node, bindings, "focusable", element.Focusable);
            element.ZIndex = ResolveInt(node, bindings, "zIndex", element.ZIndex);
            element.GridRow = ResolveInt(node, bindings, "row", element.GridRow);
            element.GridColumn = ResolveInt(node, bindings, "column", element.GridColumn);
            element.GridRowSpan = Math.Max(1, ResolveInt(node, bindings, "rowSpan", element.GridRowSpan));
            element.GridColumnSpan = Math.Max(1, ResolveInt(node, bindings, "columnSpan", element.GridColumnSpan));
            element.Dock = ResolveDock(node, bindings, "dock", element.Dock);
            element.CanvasLeft = ResolveFloat(node, bindings, "left", element.CanvasLeft);
            element.CanvasTop = ResolveFloat(node, bindings, "top", element.CanvasTop);
            element.CanvasWidth = ResolveFloat(node, bindings, "canvasWidth", element.CanvasWidth);
            element.CanvasHeight = ResolveFloat(node, bindings, "canvasHeight", element.CanvasHeight);
        }

        private static void ApplyGridDefinitions(IList<GridDefinition> definitions, string raw)
        {
            definitions.Clear();
            if (string.IsNullOrEmpty(raw))
            {
                definitions.Add(new GridDefinition(UiLength.Star(1f)));
                return;
            }

            var parts = raw.Split(',');
            for (var i = 0; i < parts.Length; i++)
            {
                var token = parts[i].Trim();
                if (token.Length == 0)
                {
                    continue;
                }

                definitions.Add(new GridDefinition(ParseGridLength(token)));
            }

            if (definitions.Count == 0)
            {
                definitions.Add(new GridDefinition(UiLength.Star(1f)));
            }
        }

        private static UiLength ParseGridLength(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return UiLength.Star(1f);
            }

            if (string.Equals(token, "auto", StringComparison.OrdinalIgnoreCase))
            {
                return UiLength.Auto();
            }

            if (token[token.Length - 1] == '*')
            {
                var factor = token.Length == 1 ? 1f : ParseFloat(token.Substring(0, token.Length - 1), 1f);
                return UiLength.Star(factor);
            }

            return UiLength.Pixel(ParseFloat(token, 0f));
        }

        private static UiMarkupNode FirstChild(UiMarkupNode node)
        {
            return node != null && node.Children.Count > 0 ? node.Children[0] : null;
        }

        private static void SkipWhitespace(string text, ref int index)
        {
            while (index < text.Length && char.IsWhiteSpace(text[index]))
            {
                index++;
            }
        }

        private static string GetAttribute(UiMarkupNode node, string key)
        {
            string value;
            return node.Attributes.TryGetValue(key, out value) ? value : null;
        }

        private static string ResolveString(UiMarkupNode node, UiXmlBindings bindings, string key, string fallback)
        {
            var raw = GetAttribute(node, key);
            return string.IsNullOrEmpty(raw) ? fallback : bindings.ResolveString(raw);
        }

        private static float ResolveFloat(UiMarkupNode node, UiXmlBindings bindings, string key, float fallback)
        {
            var raw = GetAttribute(node, key);
            return string.IsNullOrEmpty(raw) ? fallback : bindings.ResolveFloat(raw, fallback);
        }

        private static int ResolveInt(UiMarkupNode node, UiXmlBindings bindings, string key, int fallback)
        {
            var raw = GetAttribute(node, key);
            return string.IsNullOrEmpty(raw) ? fallback : bindings.ResolveInt(raw, fallback);
        }

        private static bool ResolveBool(UiMarkupNode node, UiXmlBindings bindings, string key, bool fallback)
        {
            var raw = GetAttribute(node, key);
            return string.IsNullOrEmpty(raw) ? fallback : bindings.ResolveBool(raw, fallback);
        }

        private static Color ResolveColor(UiMarkupNode node, UiXmlBindings bindings, string key, Color fallback)
        {
            var raw = GetAttribute(node, key);
            return string.IsNullOrEmpty(raw) ? fallback : bindings.ResolveColor(raw, fallback);
        }

        private static UiThickness ResolveThickness(UiMarkupNode node, UiXmlBindings bindings, string key, UiThickness fallback)
        {
            var raw = GetAttribute(node, key);
            return string.IsNullOrEmpty(raw) ? fallback : ParseThickness(bindings.ResolveString(raw), fallback);
        }

        private static UiCommand ResolveCommand(UiMarkupNode node, UiXmlBindings bindings)
        {
            return ResolveCommand(node, bindings, "command", "commandValue", "commandData");
        }

        private static UiCommand ResolveCommand(UiMarkupNode node, UiXmlBindings bindings, string commandKey, string valueKey, string dataKey)
        {
            var raw = GetAttribute(node, commandKey);
            if (string.IsNullOrEmpty(raw))
            {
                return null;
            }

            UiIntentType intentType;
            if (!Enum.TryParse(bindings.ResolveString(raw), true, out intentType))
            {
                return null;
            }

            var appId = ResolveString(node, bindings, "appId", string.Empty);
            var value = ResolveInt(node, bindings, valueKey, 0);
            var data = ResolveString(node, bindings, dataKey, string.Empty);
            return UiCommand.Create(intentType, appId, value, data);
        }

        private static UiThickness ParseThickness(string value, UiThickness fallback)
        {
            if (string.IsNullOrEmpty(value))
            {
                return fallback;
            }

            var parts = value.Split(',');
            if (parts.Length == 1)
            {
                var uniform = ParseFloat(parts[0], fallback.Left);
                return new UiThickness(uniform);
            }
            if (parts.Length == 2)
            {
                return new UiThickness(ParseFloat(parts[0], fallback.Left), ParseFloat(parts[1], fallback.Top));
            }
            if (parts.Length == 4)
            {
                return new UiThickness(
                    ParseFloat(parts[0], fallback.Left),
                    ParseFloat(parts[1], fallback.Top),
                    ParseFloat(parts[2], fallback.Right),
                    ParseFloat(parts[3], fallback.Bottom));
            }

            return fallback;
        }

        private static UiOrientation ResolveOrientation(UiMarkupNode node, UiXmlBindings bindings, string key, UiOrientation fallback)
        {
            var raw = ResolveString(node, bindings, key, string.Empty);
            if (string.Equals(raw, "horizontal", StringComparison.OrdinalIgnoreCase))
            {
                return UiOrientation.Horizontal;
            }
            if (string.Equals(raw, "vertical", StringComparison.OrdinalIgnoreCase))
            {
                return UiOrientation.Vertical;
            }
            return fallback;
        }

        private static UiHorizontalAlignment ResolveHorizontalAlignment(UiMarkupNode node, UiXmlBindings bindings, string key, UiHorizontalAlignment fallback)
        {
            var raw = ResolveString(node, bindings, key, string.Empty);
            switch ((raw ?? string.Empty).ToLowerInvariant())
            {
                case "left":
                    return UiHorizontalAlignment.Left;
                case "center":
                    return UiHorizontalAlignment.Center;
                case "right":
                    return UiHorizontalAlignment.Right;
                case "stretch":
                    return UiHorizontalAlignment.Stretch;
                default:
                    return fallback;
            }
        }

        private static UiVerticalAlignment ResolveVerticalAlignment(UiMarkupNode node, UiXmlBindings bindings, string key, UiVerticalAlignment fallback)
        {
            var raw = ResolveString(node, bindings, key, string.Empty);
            switch ((raw ?? string.Empty).ToLowerInvariant())
            {
                case "top":
                    return UiVerticalAlignment.Top;
                case "center":
                    return UiVerticalAlignment.Center;
                case "bottom":
                    return UiVerticalAlignment.Bottom;
                case "stretch":
                    return UiVerticalAlignment.Stretch;
                default:
                    return fallback;
            }
        }

        private static TextAlignment ResolveTextAlignment(UiMarkupNode node, UiXmlBindings bindings, string key, TextAlignment fallback)
        {
            var raw = ResolveString(node, bindings, key, string.Empty);
            switch ((raw ?? string.Empty).ToLowerInvariant())
            {
                case "center":
                    return TextAlignment.CENTER;
                case "right":
                    return TextAlignment.RIGHT;
                case "left":
                    return TextAlignment.LEFT;
                default:
                    return fallback;
            }
        }

        private static UiDock ResolveDock(UiMarkupNode node, UiXmlBindings bindings, string key, UiDock fallback)
        {
            var raw = ResolveString(node, bindings, key, string.Empty);
            switch ((raw ?? string.Empty).ToLowerInvariant())
            {
                case "top":
                    return UiDock.Top;
                case "right":
                    return UiDock.Right;
                case "bottom":
                    return UiDock.Bottom;
                case "left":
                    return UiDock.Left;
                default:
                    return fallback;
            }
        }

        private static IconKind ResolveIcon(UiMarkupNode node, UiXmlBindings bindings, string key, IconKind fallback)
        {
            var raw = ResolveString(node, bindings, key, string.Empty);
            switch ((raw ?? string.Empty).ToLowerInvariant())
            {
                case "ring":
                    return IconKind.Ring;
                case "power":
                    return IconKind.Power;
                case "gear":
                    return IconKind.Gear;
                default:
                    return fallback;
            }
        }

        private static float ParseFloat(string value, float fallback)
        {
            float parsed;
            return float.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out parsed) ? parsed : fallback;
        }
    }
}

