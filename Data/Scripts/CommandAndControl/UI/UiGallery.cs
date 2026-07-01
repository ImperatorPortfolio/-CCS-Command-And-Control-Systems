using VRage.Game.GUI.TextPanel;
using VRageMath;

namespace AGS
{
    // Session-local interaction state for the UIGallery test app.
    public sealed class GalleryState
    {
        public int Tab;
        public bool Expanded;
        public bool DropdownOpen;
        public int DropdownIndex;
        public int Slider = 50;
        public int Stepper = 5;
        public bool Check;
        public int Radio;
        public string Input = string.Empty;
        public bool DialogOpen;
    }

    // Code-built test page that instantiates every library control so each can be
    // exercised on a real surface without touching production station views.
    public static class GalleryView
    {
        public static UiElement Build(UiContext context)
        {
            var g = context.Frame.Gallery ?? new GalleryState();

            var root = new GridPanel { ClipToBounds = true };
            root.Rows.Add(new GridDefinition(UiLength.Star(1f)));
            root.Columns.Add(new GridDefinition(UiLength.Star(1f)));

            var tabs = new TabControl { SelectedIndex = g.Tab, Command = Cmd("tab") };
            tabs.AddTab(new TabItem { Header = "INPUTS", Content = BuildInputsTab(context, g) });
            tabs.AddTab(new TabItem { Header = "DATA", Content = BuildDataTab(context, g) });
            tabs.AddTab(new TabItem { Header = "SHAPES", Content = BuildShapesTab(context, g) });
            root.AddChild(tabs);

            if (g.DialogOpen)
            {
                var dialog = BuildDialog(context);
                dialog.ZIndex = 200;
                root.AddChild(dialog);
            }

            return root;
        }

        private static UiCommand Cmd(string action)
        {
            return UiCommand.Create(UiIntentType.GalleryAction, GalleryApp.IdValue, 0, action);
        }

        private static UiElement BuildInputsTab(UiContext context, GalleryState g)
        {
            var grid = new UniformGrid { Columns = 3, Spacing = context.Theme.SpacingSm };

            var buttons = new StackPanel { Orientation = UiOrientation.Vertical, Spacing = context.Theme.SpacingSm };
            buttons.AddChild(new Button { Text = "Action", Height = 26f, Style = context.Theme.ShellButtonStyle });
            buttons.AddChild(new ToggleButton { Text = g.Check ? "ON" : "OFF", Height = 26f, IsChecked = g.Check, Style = context.Theme.ShellButtonStyle, Command = Cmd("toggle") });
            buttons.AddChild(new Button { Text = "Open Dialog", Height = 26f, Style = context.Theme.ShellButtonStyle, Command = Cmd("dialog") });
            buttons.AddChild(new Button { Text = "Disabled", Height = 26f, IsEnabled = false, Style = context.Theme.ShellButtonStyle });
            grid.AddChild(Group(context, "BUTTONS", buttons));

            var toggles = new StackPanel { Orientation = UiOrientation.Vertical, Spacing = context.Theme.SpacingXs };
            toggles.AddChild(new Checkbox { Text = "Enabled", Height = 22f, IsChecked = g.Check, Command = Cmd("check") });
            toggles.AddChild(new RadioButton { Text = "Alpha", Height = 22f, GroupValue = 0, IsSelected = g.Radio == 0, Command = Cmd("radio") });
            toggles.AddChild(new RadioButton { Text = "Beta", Height = 22f, GroupValue = 1, IsSelected = g.Radio == 1, Command = Cmd("radio") });
            toggles.AddChild(new RadioButton { Text = "Gamma", Height = 22f, GroupValue = 2, IsSelected = g.Radio == 2, Command = Cmd("radio") });
            grid.AddChild(Group(context, "CHECK / RADIO", toggles));

            var range = new StackPanel { Orientation = UiOrientation.Vertical, Spacing = context.Theme.SpacingSm };
            range.AddChild(new Slider { Height = 34f, Label = "LEVEL", ValueText = g.Slider + "%", Ratio = g.Slider / 100f, MinValue = 0, MaxValue = 100, Command = Cmd("slider") });
            range.AddChild(new ProgressBar { Height = 14f, Ratio = g.Slider / 100f, ValueText = g.Slider + "%" });
            var repeats = new GridPanel { Height = 26f };
            repeats.Columns.Add(new GridDefinition(UiLength.Star(1f)));
            repeats.Columns.Add(new GridDefinition(UiLength.Star(1f)));
            repeats.Rows.Add(new GridDefinition(UiLength.Star(1f)));
            repeats.AddChild(new RepeatButton { GridColumn = 0, Text = "- hold", Margin = new UiThickness(0f, 0f, 2f, 0f), Style = context.Theme.ShellButtonStyle, Command = Cmd("repeatDec") });
            repeats.AddChild(new RepeatButton { GridColumn = 1, Text = "+ hold", Margin = new UiThickness(2f, 0f, 0f, 0f), Style = context.Theme.ShellButtonStyle, Command = Cmd("repeatInc") });
            range.AddChild(repeats);
            grid.AddChild(Group(context, "RANGE", range));

            var stepperBox = new StackPanel { Orientation = UiOrientation.Vertical, Spacing = context.Theme.SpacingSm };
            stepperBox.AddChild(new Stepper { Height = 30f, Label = "QTY", Value = g.Stepper, MinValue = 0, MaxValue = 20, Step = 1, Command = Cmd("stepper") });
            stepperBox.AddChild(new TextBlock { Text = "Wraps long text across multiple lines inside its bounds without overflowing.", Wrap = true, Scale = context.Theme.TextXs, Color = context.Theme.ForegroundMuted });
            grid.AddChild(Group(context, "STEPPER / TEXT", stepperBox));

            var entry = new StackPanel { Orientation = UiOrientation.Vertical, Spacing = context.Theme.SpacingXs };
            entry.AddChild(new TextBox { Text = g.Input, Placeholder = "0", Height = 22f });
            entry.AddChild(new Keypad { Height = 110f, Command = Cmd("key") });
            var expander = new Expander { Header = "More options", IsExpanded = g.Expanded, ToggleCommand = Cmd("expander") };
            expander.Content = new TextBlock { Text = "Hidden content revealed.", Scale = context.Theme.TextXs, Color = context.Theme.ForegroundMuted, Height = 18f };
            entry.AddChild(expander);
            grid.AddChild(Group(context, "ENTRY", entry));

            // Dropdown last so its open popup is not overdrawn by later cells.
            var ddBox = new StackPanel { Orientation = UiOrientation.Vertical, Spacing = context.Theme.SpacingSm };
            var dropdown = new Dropdown
            {
                Height = 26f,
                Placeholder = "Select system",
                SelectedIndex = g.DropdownIndex,
                IsOpen = g.DropdownOpen,
                ToggleCommand = Cmd("ddToggle"),
                SelectCommand = Cmd("ddSelect")
            };
            dropdown.SetOptions("Power,Hydrogen,Batteries,Reactors");
            ddBox.AddChild(dropdown);
            grid.AddChild(Group(context, "DROPDOWN", ddBox));

            return grid;
        }

        private static UiElement BuildDataTab(UiContext context, GalleryState g)
        {
            var grid = new UniformGrid { Columns = 2, Spacing = context.Theme.SpacingSm };

            var line = new Chart { Kind = ChartKind.Line, LineColor = context.Theme.Accent };
            line.SetValues("3,7,4,8,6,9,5,10,7,11,8,12");
            grid.AddChild(Group(context, "LINE CHART", line));

            var bar = new Chart { Kind = ChartKind.Bar, FillColor = context.Theme.Success };
            bar.SetValues("5,8,4,9,6,11,7,10");
            grid.AddChild(Group(context, "BAR CHART", bar));

            var table = new Table { RowHeight = 20f };
            table.SetColumns("Block,State,Load");
            table.SetRows("Reactor 1|ON|82%;Battery A|CHG|54%;H2 Tank|OK|73%;Thruster|FAULT|0%");
            grid.AddChild(Group(context, "TABLE", table));

            var details = new DetailsView { RowHeight = 20f };
            details.SetRows("Grid|CCS-01;Power|ONLINE;Crew|4;Alert|GREEN;Uptime|03:14");
            grid.AddChild(Group(context, "DETAILS", details));

            return grid;
        }

        private static UiElement BuildShapesTab(UiContext context, GalleryState g)
        {
            var grid = new UniformGrid { Columns = 3, Spacing = context.Theme.SpacingSm };

            grid.AddChild(Group(context, "RECTANGLE", new RectangleShape { Fill = context.Theme.AccentSoft, Stroke = context.Theme.Accent, StrokeThickness = 2f }));
            grid.AddChild(Group(context, "ELLIPSE", new EllipseShape { Fill = context.Theme.Success, Stroke = context.Theme.Border, StrokeThickness = 2f }));
            grid.AddChild(Group(context, "LINE", new LineShape { Stroke = context.Theme.Warning, StrokeThickness = 2f, X1 = 0f, Y1 = 0f, X2 = 1f, Y2 = 1f }));

            var swatches = new UniformGrid { Columns = 4, Spacing = 3f };
            var colors = new[] { context.Theme.Accent, context.Theme.Success, context.Theme.Warning, context.Theme.Danger, context.Theme.AccentSoft, context.Theme.Border, context.Theme.ForegroundMuted, context.Theme.ForegroundDim };
            for (var i = 0; i < colors.Length; i++)
            {
                swatches.AddChild(new RectangleShape { Fill = colors[i] });
            }
            grid.AddChild(Group(context, "UNIFORMGRID", swatches));

            var wrap = new WrapPanel { Orientation = UiOrientation.Horizontal, ItemSpacing = 4f, LineSpacing = 4f };
            for (var i = 0; i < 8; i++)
            {
                wrap.AddChild(new Button { Text = "Tag " + (i + 1), Width = 56f, Height = 22f, Style = context.Theme.ShellButtonStyle });
            }
            grid.AddChild(Group(context, "WRAPPANEL", wrap));

            grid.AddChild(Group(context, "WRAP TEXT", new TextBlock
            {
                Text = "The quick brown fox jumps over the lazy dog while the navigation computer recalculates the jump vector.",
                Wrap = true,
                Scale = context.Theme.TextXs,
                Color = context.Theme.ForegroundPrimary
            }));

            return grid;
        }

        private static UiElement BuildDialog(UiContext context)
        {
            var body = new StackPanel { Orientation = UiOrientation.Vertical, Spacing = context.Theme.SpacingSm };
            body.AddChild(new TextBlock { Text = "Confirm the action?", Scale = context.Theme.TextMd, Color = context.Theme.ForegroundPrimary, Height = 22f });
            body.AddChild(new TextBlock { Text = "This dialog is rendered over the page via ZIndex layering.", Wrap = true, Scale = context.Theme.TextXs, Color = context.Theme.ForegroundMuted });
            var actions = new GridPanel { Height = 26f };
            actions.Columns.Add(new GridDefinition(UiLength.Star(1f)));
            actions.Columns.Add(new GridDefinition(UiLength.Star(1f)));
            actions.Rows.Add(new GridDefinition(UiLength.Star(1f)));
            actions.AddChild(new Button { GridColumn = 0, Text = "Cancel", Margin = new UiThickness(0f, 0f, 3f, 0f), Style = context.Theme.ShellButtonStyle, Command = Cmd("dialog") });
            actions.AddChild(new Button { GridColumn = 1, Text = "Confirm", Margin = new UiThickness(3f, 0f, 0f, 0f), Style = context.Theme.ShellButtonStyle, Command = Cmd("dialog") });
            body.AddChild(actions);

            return new Dialog
            {
                Title = "Dialog",
                CloseCommand = Cmd("dialog"),
                PanelWidth = 240f,
                PanelHeight = 150f,
                Content = body
            };
        }

        private static GroupBox Group(UiContext context, string header, UiElement content)
        {
            return new GroupBox
            {
                Header = header,
                Margin = new UiThickness(0f),
                Content = content
            };
        }
    }
}
