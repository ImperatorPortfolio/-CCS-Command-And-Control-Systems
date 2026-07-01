using System.Collections.Generic;
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
        public string CaptureKey { get; private set; }

        public bool IsCapturing { get { return !string.IsNullOrEmpty(CaptureKey); } }

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

        // Pointer capture lives here (not on the view) because the view tree is
        // rebuilt every frame while this manager persists across frames.
        public void Capture(string captureKey)
        {
            CaptureKey = captureKey ?? string.Empty;
        }

        public void ReleaseCapture()
        {
            CaptureKey = string.Empty;
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
        // Floating layers (dialogs, popups, tooltips, notifications) arranged to the full
        // viewport, drawn after Root and hit-tested before it. Last added = top-most.
        private readonly List<UiElement> _overlays = new List<UiElement>();

        public UiView(UiElement root)
        {
            Root = root;
        }

        public UiElement Root { get; private set; }
        public IList<UiElement> Overlays { get { return _overlays; } }

        public void AddOverlay(UiElement overlay)
        {
            if (overlay != null)
            {
                _overlays.Add(overlay);
            }
        }

        public void Layout(UiContext context)
        {
            if (Root != null)
            {
                Root.Measure(context.Viewport.Size, context);
                Root.Arrange(context.Viewport, context);
                Root.ApplyFocus(context.Focus);
            }

            for (var i = 0; i < _overlays.Count; i++)
            {
                _overlays[i].Measure(context.Viewport.Size, context);
                _overlays[i].Arrange(context.Viewport, context);
                _overlays[i].ApplyFocus(context.Focus);
            }
        }

        public void Render(MySpriteDrawFrame frame, Vector2 origin, UiContext context)
        {
            if (Root != null)
            {
                Root.Render(frame, origin, context);
            }

            for (var i = 0; i < _overlays.Count; i++)
            {
                _overlays[i].Render(frame, origin, context);
            }
        }

        public UiCommand HandleInput(UiContext context)
        {
            if (Root == null && _overlays.Count == 0)
            {
                return null;
            }

            if (Root != null)
            {
                Root.ResetInputState();
                Root.ApplyFocus(context.Focus);
            }

            for (var i = 0; i < _overlays.Count; i++)
            {
                _overlays[i].ResetInputState();
                _overlays[i].ApplyFocus(context.Focus);
            }

            var focus = context.Focus;

            // End an active drag capture as soon as the pointer is released.
            if (focus != null && focus.IsCapturing && !context.PointerPressed)
            {
                focus.ReleaseCapture();
            }

            // While a control holds capture, route the pointer straight to it so a
            // drag keeps tracking even if the pointer briefly leaves its bounds.
            if (focus != null && focus.IsCapturing && context.PointerPressed)
            {
                var captured = FindCaptured(focus.CaptureKey);
                if (captured != null)
                {
                    captured.MarkPressed();
                    return captured.OnDrag(context);
                }

                focus.ReleaseCapture();
            }

            if (!context.PointerActive)
            {
                return null;
            }

            var hit = HitTestLayers(context.PointerLocal);
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

            // Start pointer capture for draggable controls (slider, scroll bar, ...).
            var captureTarget = hit;
            while (captureTarget != null)
            {
                if (captureTarget.CapturesPointer && captureTarget.Focusable)
                {
                    context.Focus.Capture(captureTarget.GetFocusKey());
                    break;
                }

                captureTarget = captureTarget.Parent;
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

        // Overlays are top-most (last added first), then Root.
        private UiElement HitTestLayers(Vector2 point)
        {
            for (var i = _overlays.Count - 1; i >= 0; i--)
            {
                var hit = _overlays[i].HitTest(point);
                if (hit != null)
                {
                    return hit;
                }
            }

            return Root != null ? Root.HitTest(point) : null;
        }

        private UiElement FindCaptured(string focusKey)
        {
            for (var i = _overlays.Count - 1; i >= 0; i--)
            {
                var found = _overlays[i].FindByFocusKey(focusKey);
                if (found != null)
                {
                    return found;
                }
            }

            return Root != null ? Root.FindByFocusKey(focusKey) : null;
        }
    }
}
