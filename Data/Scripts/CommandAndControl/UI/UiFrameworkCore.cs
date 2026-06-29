using Sandbox.Game.GameSystems.TextSurfaceScripts;
using VRage.Game.GUI.TextPanel;
using VRageMath;

namespace AGS
{
    public enum UiVisibility
    {
        Visible,
        Collapsed
    }

    public enum UiOrientation
    {
        Horizontal,
        Vertical
    }

    public enum UiDock
    {
        Left,
        Top,
        Right,
        Bottom
    }

    public enum UiControlState
    {
        Normal,
        Hover,
        Pressed,
        Focused,
        Disabled,
        Selected
    }

    public enum GridUnitType
    {
        Auto,
        Pixel,
        Star
    }

    public enum IconKind
    {
        Ring,
        Power,
        Gear
    }

    public struct UiLength
    {
        public float Value;
        public GridUnitType UnitType;

        public UiLength(float value, GridUnitType unitType)
        {
            Value = value;
            UnitType = unitType;
        }

        public static UiLength Auto() { return new UiLength(0f, GridUnitType.Auto); }
        public static UiLength Pixel(float value) { return new UiLength(value, GridUnitType.Pixel); }
        public static UiLength Star(float value) { return new UiLength(value, GridUnitType.Star); }
    }

    public sealed class GridDefinition
    {
        public GridDefinition(UiLength length)
        {
            Length = length;
        }

        public UiLength Length { get; set; }
        public float ActualSize { get; set; }
    }

    public sealed class UiCommand
    {
        public UiCommand(UiIntentType intentType, string appId, int value, string data)
        {
            IntentType = intentType;
            AppId = appId ?? string.Empty;
            Value = value;
            Data = data ?? string.Empty;
        }

        public UiIntentType IntentType { get; private set; }
        public string AppId { get; private set; }
        public int Value { get; private set; }
        public string Data { get; private set; }

        public UiIntent ToIntent()
        {
            return new UiIntent(IntentType, AppId, Value, Data);
        }

        public static UiCommand Create(UiIntentType intentType)
        {
            return new UiCommand(intentType, string.Empty, 0, string.Empty);
        }

        public static UiCommand Create(UiIntentType intentType, string appId)
        {
            return new UiCommand(intentType, appId, 0, string.Empty);
        }

        public static UiCommand Create(UiIntentType intentType, string appId, int value)
        {
            return new UiCommand(intentType, appId, value, string.Empty);
        }

        public static UiCommand Create(UiIntentType intentType, string appId, int value, string data)
        {
            return new UiCommand(intentType, appId, value, data);
        }
    }

    public sealed class FocusManager
    {
        public string FocusKey { get; private set; }

        public void Focus(string focusKey)
        {
            FocusKey = focusKey ?? string.Empty;
        }

        public bool IsFocused(string focusKey)
        {
            return !string.IsNullOrEmpty(focusKey) && FocusKey == focusKey;
        }

        public void Clear()
        {
            FocusKey = string.Empty;
        }
    }

    public sealed class HitTestResult
    {
        public UiElement Element { get; set; }
    }

    public sealed class UiContext
    {
        public UiFrame Frame { get; set; }
        public UiTheme Theme { get; set; }
        public UiRect Viewport { get; set; }
        public PointerState Pointer { get; set; }
        public Vector2 PointerLocal { get; set; }
        public bool PointerActive { get; set; }
        public bool PointerPressed { get; set; }
        public bool PointerClicked { get; set; }
        public int ScrollDelta { get; set; }
        public FocusManager Focus { get; set; }
        public Vector2 SurfaceSize { get; set; }
    }

    public abstract class UiBuilder
    {
        public abstract UiView Build(UiContext context);
    }

    public sealed class UiView
    {
        public UiView(UiElement root)
        {
            Root = root;
        }

        public UiElement Root { get; private set; }

        public void Layout(UiContext context)
        {
            if (Root == null)
            {
                return;
            }

            Root.Measure(context.Viewport.Size, context);
            Root.Arrange(context.Viewport, context);
            Root.ApplyFocus(context.Focus);
        }

        public void Render(MySpriteDrawFrame frame, Vector2 origin, UiContext context)
        {
            if (Root == null)
            {
                return;
            }

            Root.Render(frame, origin, context);
        }

        public UiCommand HandleInput(UiContext context)
        {
            if (Root == null)
            {
                return null;
            }

            Root.ResetInputState();
            Root.ApplyFocus(context.Focus);

            if (!context.PointerActive)
            {
                return null;
            }

            var hit = Root.HitTest(context.PointerLocal);
            if (hit != null)
            {
                hit.MarkHovered();
                if (context.PointerPressed)
                {
                    hit.MarkPressed();
                }
            }

            if (context.ScrollDelta != 0)
            {
                var wheelTarget = hit;
                while (wheelTarget != null)
                {
                    var wheelCommand = wheelTarget.OnScroll(context.ScrollDelta, context);
                    if (wheelCommand != null)
                    {
                        return wheelCommand;
                    }
                    wheelTarget = wheelTarget.Parent;
                }
            }

            if (!context.PointerClicked)
            {
                return null;
            }

            if (hit == null)
            {
                context.Focus.Clear();
                return null;
            }

            if (hit.Focusable)
            {
                context.Focus.Focus(hit.GetFocusKey());
            }
            else
            {
                context.Focus.Clear();
            }

            var target = hit;
            while (target != null)
            {
                var command = target.OnClick(context);
                if (command != null)
                {
                    return command;
                }
                target = target.Parent;
            }

            return null;
        }
    }
}
