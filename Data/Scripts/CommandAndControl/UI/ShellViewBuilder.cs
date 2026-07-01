using System;
using VRage.Game.GUI.TextPanel;
using VRageMath;

namespace AGS
{
    public sealed class ShellViewBuilder : UiBuilder
    {
        private const int MinVisibleProgramSlots = 6;
        private const int MaxVisibleProgramSlots = 9;

        public override UiView Build(UiContext context)
        {
            var root = new GridPanel
            {
                Width = context.Viewport.Width,
                Height = context.Viewport.Height,
                ClipToBounds = true
            };
            root.Rows.Add(new GridDefinition(UiLength.Star(1f)));
            root.Columns.Add(new GridDefinition(UiLength.Star(1f)));

            root.AddChild(BuildShellLayer(context));

            var view = new UiView(root);

            // Modals live on the overlay layer: drawn above the page and hit-tested first,
            // with a full-viewport backdrop that dismisses on an outside click.
            if (context.Frame.StartMenuOpen)
            {
                view.AddOverlay(BuildBackdrop(UiCommand.Create(UiIntentType.ToggleStart)));
                view.AddOverlay(BuildStartMenu(context));
            }

            if (context.Frame.SettingsOpen)
            {
                view.AddOverlay(BuildBackdrop(UiCommand.Create(UiIntentType.CloseSettings)));
                view.AddOverlay(BuildSettingsWindow(context));
            }

            return view;
        }

        private UiElement BuildShellLayer(UiContext context)
        {
            var dock = new DockPanel();
            var headerHeight = GetHeaderHeight(context);
            var footerHeight = GetFooterHeight(context);

            var header = new Border
            {
                Dock = UiDock.Top,
                Height = headerHeight,
                Background = context.Theme.Background1,
                BorderBrush = new Color(0, 0, 0, 0),
                BorderThickness = 0f,
                Padding = new UiThickness(GetSideInset(context), 0f, GetSideInset(context), 0f)
            };
            header.Content = BuildHeaderBar(context, headerHeight);
            dock.AddChild(header);

            var footer = new Border
            {
                Dock = UiDock.Bottom,
                Height = footerHeight,
                Background = context.Theme.Background1,
                BorderBrush = new Color(0, 0, 0, 0),
                BorderThickness = 0f
            };
            dock.AddChild(footer);

            var body = new Border
            {
                Background = context.Theme.Background0,
                BorderBrush = new Color(0, 0, 0, 0),
                BorderThickness = 0f,
                Padding = new UiThickness(GetContentPadding(context))
            };
            body.Content = BuildBody(context);
            dock.AddChild(body);

            return dock;
        }

        private UiElement BuildHeaderBar(UiContext context, float headerHeight)
        {
            var grid = new GridPanel();
            var ringSide = MathHelper.Clamp(headerHeight * 0.9f, 16f * context.Theme.LayoutScale, 30f * context.Theme.LayoutScale);
            var squareSide = MathHelper.Clamp(headerHeight * 0.54f, 9f * context.Theme.LayoutScale, 16f * context.Theme.LayoutScale);
            var rightClusterWidth = MathHelper.Clamp(context.Viewport.Width * 0.14f, 56f * context.Theme.LayoutScale, 120f * context.Theme.LayoutScale);

            grid.Columns.Add(new GridDefinition(UiLength.Pixel(headerHeight)));
            grid.Columns.Add(new GridDefinition(UiLength.Star(1f)));
            grid.Columns.Add(new GridDefinition(UiLength.Pixel(rightClusterWidth)));
            grid.Rows.Add(new GridDefinition(UiLength.Star(1f)));

            var startButton = new IconButton
            {
                GridColumn = 0,
                Width = ringSide,
                Height = ringSide,
                HorizontalAlignment = UiHorizontalAlignment.Left,
                VerticalAlignment = UiVerticalAlignment.Center,
                Icon = IconKind.Ring,
                Active = context.Frame.StartMenuOpen,
                Command = UiCommand.Create(UiIntentType.ToggleStart),
                Name = "startButton"
            };
            grid.AddChild(startButton);

            var rightCluster = new GridPanel
            {
                GridColumn = 2,
                VerticalAlignment = UiVerticalAlignment.Center,
                Height = headerHeight
            };
            rightCluster.Columns.Add(new GridDefinition(UiLength.Star(1f)));
            rightCluster.Columns.Add(new GridDefinition(UiLength.Pixel(squareSide + Math.Max(4f, context.Theme.SpacingSm))));
            rightCluster.Rows.Add(new GridDefinition(UiLength.Star(1f)));

            rightCluster.AddChild(new TextBlock
            {
                GridColumn = 0,
                Text = DateTime.Now.ToString("HH:mm"),
                Scale = Math.Max(context.Theme.TextMd, GetHeaderTextScale(context, headerHeight)),
                Color = context.Theme.ForegroundPrimary,
                Alignment = TextAlignment.RIGHT,
                TextVerticalAlignment = UiVerticalAlignment.Center,
                VerticalAlignment = UiVerticalAlignment.Center
            });

            rightCluster.AddChild(new Button
            {
                GridColumn = 1,
                Width = squareSide,
                Height = squareSide,
                HorizontalAlignment = UiHorizontalAlignment.Right,
                VerticalAlignment = UiVerticalAlignment.Center,
                Margin = new UiThickness(Math.Max(2f, context.Theme.SpacingSm), 0f, 0f, 0f),
                Text = string.Empty,
                Style = context.Theme.ShellButtonStyle,
                Command = UiCommand.Create(UiIntentType.OpenDesktop),
                Name = "desktopButton"
            });

            grid.AddChild(rightCluster);
            return grid;
        }

        private UiElement BuildBody(UiContext context)
        {
            if (context.Frame.IsStandby)
            {
                return BuildStandbyBody(context);
            }

            if (context.Frame.ActiveAppId == DesktopApp.IdValue)
            {
                return new Border
                {
                    Background = context.Theme.Background0,
                    BorderBrush = new Color(0, 0, 0, 0),
                    BorderThickness = 0f
                };
            }

            if (context.Frame.ActiveAppId == SecurityApp.IdValue)
            {
                return SecurityView.Build(context);
            }

            if (context.Frame.ActiveAppId == EngineeringApp.IdValue)
            {
                return EngineeringView.Build(context);
            }

            if (context.Frame.ActiveAppId == GalleryApp.IdValue)
            {
                return GalleryView.Build(context);
            }

            // Program-API apps render from a serialized ProgramFrame through ProgramHost,
            // rather than a hardcoded view. (Step 2: this branch replaces the per-app switch
            // as views migrate onto the contract.)
            if (context.Frame.ActiveAppId == DemoProgram.IdValue)
            {
                return ProgramHost.Render(context, DemoProgram.Manifest, DemoProgram.BuildFrame(context));
            }

            return BuildTextStation(context);
        }

        private UiElement BuildStandbyBody(UiContext context)
        {
            var grid = new GridPanel();
            grid.AddChild(new TextBlock
            {
                Text = "Standby",
                Scale = context.Theme.TextMd,
                Color = context.Theme.ForegroundMuted,
                Alignment = TextAlignment.CENTER,
                TextVerticalAlignment = UiVerticalAlignment.Center,
                HorizontalAlignment = UiHorizontalAlignment.Center,
                VerticalAlignment = UiVerticalAlignment.Center,
                Width = context.Viewport.Width * 0.26f,
                Height = MathHelper.Clamp(context.Viewport.Height * 0.08f, 20f * context.Theme.LayoutScale, 40f * context.Theme.LayoutScale)
            });
            return grid;
        }

        private UiElement BuildTextStation(UiContext context)
        {
            var stack = new StackPanel
            {
                Orientation = UiOrientation.Vertical,
                Spacing = context.Theme.SpacingSm
            };

            stack.AddChild(new TextBlock
            {
                Text = context.Frame.ActiveAppTitle,
                Scale = context.Theme.TextLg,
                Color = context.Theme.ForegroundPrimary,
                Height = 20f * context.Theme.LayoutScale
            });

            for (var i = 0; i < context.Frame.Lines.Count; i++)
            {
                stack.AddChild(new TextBlock
                {
                    Text = context.Frame.Lines[i],
                    Scale = context.Theme.TextSm,
                    Color = new Color(192, 202, 222),
                    Height = 16f * context.Theme.LayoutScale
                });
            }

            return stack;
        }

        private UiElement BuildStartMenu(UiContext context)
        {
            var menuWidth = MathHelper.Clamp(context.Viewport.Width * 0.4f, 180f * context.Theme.LayoutScale, 380f * context.Theme.LayoutScale);
            var menuHeight = MathHelper.Clamp(context.Viewport.Height * 0.76f, 200f * context.Theme.LayoutScale, 440f * context.Theme.LayoutScale);
            var headerHeight = Math.Max(30f * context.Theme.LayoutScale, context.Viewport.Height * 0.07f);
            var actionHeight = Math.Max(30f * context.Theme.LayoutScale, context.Viewport.Height * 0.09f);
            var rowHeight = Math.Max(22f * context.Theme.LayoutScale, context.Viewport.Height * 0.048f);
            var menuPadding = Math.Max(context.Theme.SpacingSm, 5f * context.Theme.LayoutScale);
            var rowGap = Math.Max(context.Theme.SpacingXs, 2f * context.Theme.LayoutScale);
            var visibleSlots = GetVisibleProgramSlots(menuHeight, headerHeight, actionHeight, rowHeight, menuPadding, rowGap);

            var menu = new Border
            {
                HorizontalAlignment = UiHorizontalAlignment.Left,
                VerticalAlignment = UiVerticalAlignment.Top,
                Margin = new UiThickness(GetSideInset(context) * 0.5f, GetHeaderHeight(context) + Math.Max(2f * context.Theme.LayoutScale, context.Viewport.Height * 0.01f), 0f, 0f),
                Width = menuWidth,
                Height = menuHeight,
                Background = new Color(10, 10, 10),
                BorderBrush = context.Theme.Border,
                BorderThickness = context.Theme.StrokeThin,
                Padding = new UiThickness(menuPadding)
            };

            var grid = new GridPanel();
            grid.Rows.Add(new GridDefinition(UiLength.Pixel(headerHeight)));
            grid.Rows.Add(new GridDefinition(UiLength.Star(1f)));
            grid.Rows.Add(new GridDefinition(UiLength.Pixel(actionHeight)));
            grid.Columns.Add(new GridDefinition(UiLength.Star(1f)));
            menu.Content = grid;

            var pageCount = GetProgramPageCount(context.Frame.StartItems.Count, visibleSlots);
            var page = ClampPage(context.Frame.StartMenuPage, pageCount);

            grid.AddChild(new SectionHeader
            {
                Title = "PROGRAMS",
                Detail = pageCount > 1 ? ("Wheel " + (page + 1) + "/" + pageCount) : "Available apps",
                Height = headerHeight,
                Style = context.Theme.SectionStyle
            });

            var scrollViewer = new ScrollViewer
            {
                GridRow = 1,
                Margin = new UiThickness(0f, Math.Max(context.Theme.SpacingSm, 4f * context.Theme.LayoutScale), 0f, Math.Max(context.Theme.SpacingSm, 4f * context.Theme.LayoutScale)),
                ScrollUpCommand = pageCount > 1 ? UiCommand.Create(UiIntentType.PrevStartPage) : null,
                ScrollDownCommand = pageCount > 1 ? UiCommand.Create(UiIntentType.NextStartPage) : null
            };

            var list = new ListView();
            for (var slot = 0; slot < visibleSlots; slot++)
            {
                var itemIndex = (page * visibleSlots) + slot;
                if (itemIndex >= context.Frame.StartItems.Count)
                {
                    break;
                }

                var item = context.Frame.StartItems[itemIndex];
                list.AddChild(new ListItem
                {
                    Height = rowHeight,
                    Margin = new UiThickness(0f, 0f, 0f, rowGap),
                    Text = item.Title,
                    BadgeText = GetAppBadge(item.Id),
                    Scale = Math.Max(context.Theme.TextMd + 0.02f, 0.42f),
                    Selected = context.Frame.ActiveAppId == item.Id,
                    Style = context.Theme.ShellButtonStyle,
                    Command = UiCommand.Create(UiIntentType.LaunchApp, item.Id)
                });
            }

            scrollViewer.Content = list;
            grid.AddChild(scrollViewer);

            var actionGrid = new GridPanel { GridRow = 2, Height = actionHeight };
            actionGrid.Columns.Add(new GridDefinition(UiLength.Star(1f)));
            actionGrid.Columns.Add(new GridDefinition(UiLength.Star(1f)));
            actionGrid.Rows.Add(new GridDefinition(UiLength.Star(1f)));
            actionGrid.AddChild(new IconButton
            {
                GridColumn = 0,
                Margin = new UiThickness(0f, 0f, context.Theme.SpacingSm, 0f),
                Icon = IconKind.Power,
                Caption = "STBY",
                Text = string.Empty,
                Scale = Math.Max(context.Theme.TextSm, 0.32f),
                Style = context.Theme.ShellButtonStyle,
                Command = UiCommand.Create(UiIntentType.EnterStandby)
            });
            actionGrid.AddChild(new IconButton
            {
                GridColumn = 1,
                Margin = new UiThickness(context.Theme.SpacingSm, 0f, 0f, 0f),
                Icon = IconKind.Gear,
                Caption = "CFG",
                Text = string.Empty,
                Scale = Math.Max(context.Theme.TextSm, 0.32f),
                Style = context.Theme.ShellButtonStyle,
                Command = UiCommand.Create(UiIntentType.ToggleSettings)
            });
            grid.AddChild(actionGrid);

            return menu;
        }

        private UiElement BuildSettingsWindow(UiContext context)
        {
            var width = MathHelper.Clamp(context.Viewport.Width * 0.6f, 160f * context.Theme.LayoutScale, 460f);
            var height = MathHelper.Clamp(context.Viewport.Height * 0.74f, 150f * context.Theme.LayoutScale, 560f);
            var panel = new Border
            {
                Width = width,
                Height = height,
                HorizontalAlignment = UiHorizontalAlignment.Center,
                VerticalAlignment = UiVerticalAlignment.Center,
                Background = new Color(8, 8, 8),
                BorderBrush = context.Theme.Border,
                BorderThickness = context.Theme.StrokeThin,
                Padding = new UiThickness(GetContentPadding(context))
            };

            var stack = new StackPanel { Orientation = UiOrientation.Vertical, Spacing = context.Theme.SpacingMd };
            panel.Content = stack;

            stack.AddChild(new TextBlock { Text = "Settings", Scale = context.Theme.TextLg, Color = context.Theme.ForegroundPrimary, Height = 20f * context.Theme.LayoutScale });
            stack.AddChild(new TextBlock { Text = "Display", Scale = context.Theme.TextSm, Color = context.Theme.ForegroundMuted, Height = 14f * context.Theme.LayoutScale });
            stack.AddChild(new ToggleButton
            {
                Text = context.Frame.PendingAlwaysOn ? "Always On" : "Timeout Mode",
                Scale = context.Theme.TextMd,
                Height = 24f * context.Theme.LayoutScale,
                IsChecked = context.Frame.PendingAlwaysOn,
                Style = context.Theme.ShellButtonStyle,
                Command = UiCommand.Create(UiIntentType.ToggleAlwaysOn)
            });
            stack.AddChild(new TextBlock { Text = "Timeout", Scale = context.Theme.TextSm, Color = context.Theme.ForegroundMuted, Height = 14f * context.Theme.LayoutScale });

            var timeoutGrid = new GridPanel { Height = 24f * context.Theme.LayoutScale };
            timeoutGrid.Columns.Add(new GridDefinition(UiLength.Pixel(22f * context.Theme.LayoutScale)));
            timeoutGrid.Columns.Add(new GridDefinition(UiLength.Star(1f)));
            timeoutGrid.Columns.Add(new GridDefinition(UiLength.Pixel(22f * context.Theme.LayoutScale)));
            timeoutGrid.Rows.Add(new GridDefinition(UiLength.Star(1f)));
            timeoutGrid.AddChild(new Button { GridColumn = 0, Text = "-", Scale = context.Theme.TextMd, Style = context.Theme.ShellButtonStyle, Command = UiCommand.Create(UiIntentType.AdjustTimeout, string.Empty, -1) });
            timeoutGrid.AddChild(new Slider { GridColumn = 1, Margin = new UiThickness(context.Theme.SpacingSm, 0f), Label = string.Empty, ValueText = string.Empty, Ratio = Math.Min(1f, Math.Max(0f, (context.Frame.PendingTimeoutMinutes - 1f) / 14f)), FillColor = context.Frame.PendingAlwaysOn ? context.Theme.AccentSoft : context.Theme.Accent });
            timeoutGrid.AddChild(new Button { GridColumn = 2, Text = "+", Scale = context.Theme.TextMd, Style = context.Theme.ShellButtonStyle, Command = UiCommand.Create(UiIntentType.AdjustTimeout, string.Empty, 1) });
            stack.AddChild(timeoutGrid);
            stack.AddChild(new TextBlock { Text = context.Frame.PendingTimeoutMinutes + " min", Scale = context.Theme.TextMd, Color = context.Theme.ForegroundPrimary, Alignment = TextAlignment.CENTER, Height = 18f * context.Theme.LayoutScale });

            // Numeric input demo: a display-only TextBox shows the buffer the Keypad edits.
            stack.AddChild(new TextBlock { Text = "Numeric input (demo)", Scale = context.Theme.TextSm, Color = context.Theme.ForegroundMuted, Height = 14f * context.Theme.LayoutScale });
            stack.AddChild(new TextBox
            {
                Text = context.Frame.InputBuffer,
                Placeholder = "0",
                Scale = context.Theme.TextMd,
                Height = 22f * context.Theme.LayoutScale
            });
            stack.AddChild(new Keypad
            {
                Height = 96f * context.Theme.LayoutScale,
                Command = UiCommand.Create(UiIntentType.InputKey)
            });

            var actions = new GridPanel { Height = 28f * context.Theme.LayoutScale };
            actions.Columns.Add(new GridDefinition(UiLength.Star(1f)));
            actions.Columns.Add(new GridDefinition(UiLength.Star(1f)));
            actions.Rows.Add(new GridDefinition(UiLength.Star(1f)));
            actions.AddChild(new Button { GridColumn = 0, Margin = new UiThickness(0f, 0f, context.Theme.SpacingSm, 0f), Text = "Back", Scale = context.Theme.TextMd, Style = context.Theme.ShellButtonStyle, Command = UiCommand.Create(UiIntentType.CloseSettings) });
            actions.AddChild(new Button { GridColumn = 1, Margin = new UiThickness(context.Theme.SpacingSm, 0f, 0f, 0f), Text = "Apply", Scale = context.Theme.TextMd, Style = context.Theme.ShellButtonStyle, Command = UiCommand.Create(UiIntentType.ApplySettings) });
            stack.AddChild(actions);

            return panel;
        }

        private UiElement BuildBackdrop(UiCommand command)
        {
            return new Button
            {
                Text = string.Empty,
                Style = new UiStyle
                {
                    Background = new Color(0, 0, 0, 0),
                    HoverBackground = new Color(0, 0, 0, 0),
                    PressedBackground = new Color(0, 0, 0, 0),
                    Border = new Color(0, 0, 0, 0),
                    Foreground = new Color(0, 0, 0, 0)
                },
                Command = command
            };
        }

        private static string GetAppBadge(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return "--";
            }

            if (id.Length == 1)
            {
                return id.ToUpperInvariant();
            }

            return id.Substring(0, 2).ToUpperInvariant();
        }

        private static int GetProgramPageCount(int count, int visibleSlots)
        {
            return Math.Max(1, (int)Math.Ceiling(count / (float)Math.Max(1, visibleSlots)));
        }

        private static int ClampPage(int page, int pageCount)
        {
            return Math.Max(0, Math.Min(page, pageCount - 1));
        }

        private static int GetVisibleProgramSlots(float menuHeight, float headerHeight, float actionHeight, float rowHeight, float menuPadding, float rowGap)
        {
            var listHeight = Math.Max(1f, menuHeight - headerHeight - actionHeight - (menuPadding * 2f) - (rowGap * 2f));
            var slotHeight = Math.Max(1f, rowHeight + rowGap);
            var slots = (int)Math.Floor((listHeight + rowGap) / slotHeight);
            return Math.Max(MinVisibleProgramSlots, Math.Min(MaxVisibleProgramSlots, slots));
        }

        private static float GetHeaderHeight(UiContext context)
        {
            return MathHelper.Clamp(context.Viewport.Height * 0.052f, 16f * context.Theme.LayoutScale, 34f * context.Theme.LayoutScale);
        }

        private static float GetFooterHeight(UiContext context)
        {
            return MathHelper.Clamp(context.Viewport.Height * 0.042f, 12f * context.Theme.LayoutScale, 26f * context.Theme.LayoutScale);
        }

        private static float GetContentPadding(UiContext context)
        {
            return MathHelper.Clamp(context.Viewport.Height * 0.018f, 5f * context.Theme.LayoutScale, 14f * context.Theme.LayoutScale);
        }

        private static float GetSideInset(UiContext context)
        {
            return MathHelper.Clamp(context.Viewport.Width * 0.016f, 5f * context.Theme.LayoutScale, 18f * context.Theme.LayoutScale);
        }

        private static float GetHeaderTextScale(UiContext context, float headerHeight)
        {
            return MathHelper.Clamp(headerHeight / 28f, context.Theme.TextSm, context.Theme.TextLg);
        }
    }
}
