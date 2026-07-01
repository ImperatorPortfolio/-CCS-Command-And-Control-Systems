using System;
using System.Text;
using Sandbox.Game.GameSystems.TextSurfaceScripts;
using Sandbox.ModAPI;
using VRage.Game.GUI.TextPanel;
using VRage.Game.ModAPI;
using VRageMath;

namespace AGS
{
    [MyTextSurfaceScript("Shell", "Command Shell")]
    public sealed class ShellScript : MyTSSCommon
    {
        private readonly IMyCubeBlock _block;
        private readonly IMyTextSurface _surface;
        private readonly Vector2 _viewportSize;
        private readonly Vector2 _viewportOrigin;
        private readonly UiRect _viewportRect;
        private readonly float _layoutScale;
        private readonly FocusManager _focus;
        private readonly ShellViewBuilder _builder;
        private readonly StringBuilder _measureBuilder = new StringBuilder();

        private bool _registered;
        private bool _lowPowerMode;

        public override ScriptUpdate NeedsUpdate
        {
            get { return _lowPowerMode ? ScriptUpdate.Update100 : ScriptUpdate.Update10; }
        }

        public ShellScript(IMyTextSurface surface, IMyCubeBlock block, Vector2 size) : base(surface, block, size)
        {
            _block = block;
            _surface = surface;
            _viewportSize = surface.SurfaceSize;
            _viewportOrigin = (surface.TextureSize - surface.SurfaceSize) * 0.5f;
            _viewportRect = new UiRect(0f, 0f, _viewportSize.X, _viewportSize.Y);
            _layoutScale = MathHelper.Clamp(Math.Min(_viewportSize.X / 960f, _viewportSize.Y / 540f), 0.78f, 1.45f);
            _focus = new FocusManager();
            _builder = new ShellViewBuilder();

            _surface.ScriptBackgroundColor = Color.Black;
            _surface.ScriptForegroundColor = Color.White;
        }

        public override void Run()
        {
            base.Run();
            RegisterIfNeeded();

            // Point the renderer's text measurer at this surface so wrapping and auto-fit use
            // real proportional glyph widths. Set per Run (TSS draws run sequentially on the
            // main thread, so the shared static hook is safe to reassign here).
            UiRenderer.MeasureText = MeasureSurfaceText;

            var frameModel = Session.Program != null ? Session.Program.GetFrame(_block, _surface) : UiFrame.CreateOffline();
            if (frameModel == null)
            {
                frameModel = UiFrame.CreateOffline();
            }

            _lowPowerMode = frameModel.IsStandby;

            using (var frame = _surface.DrawFrame())
            {
                DrawBackground(frame);

                switch (frameModel.BootPhase)
                {
                    case BootPhase.Bios:
                        ResetPointerClick(frameModel);
                        DrawBios(frame, frameModel);
                        break;
                    case BootPhase.Logo:
                        ResetPointerClick(frameModel);
                        DrawLogo(frame);
                        break;
                    case BootPhase.Desktop:
                        DrawDesktop(frame, frameModel);
                        break;
                    default:
                        ResetPointerClick(frameModel);
                        DrawMissingController(frame);
                        break;
                }

                if (!frameModel.IsStandby)
                {
                    DrawPointer(frame, frameModel.Pointer);
                }
            }
        }

        public override void Dispose()
        {
            if (_registered && Session.Program != null)
            {
                Session.Program.UnregisterScreen(_block, _surface);
            }

            _registered = false;
            base.Dispose();
        }

        private void RegisterIfNeeded()
        {
            if (_registered || Session.Program == null)
            {
                return;
            }

            Session.Program.RegisterScreen(_block, _surface);
            _registered = true;
        }

        private Vector2 MeasureSurfaceText(string text, string font, float scale)
        {
            if (string.IsNullOrEmpty(text))
            {
                return Vector2.Zero;
            }

            _measureBuilder.Clear();
            _measureBuilder.Append(text);
            return _surface.MeasureStringInPixels(_measureBuilder, font ?? "Monospace", scale);
        }

        private void DrawBackground(MySpriteDrawFrame frame)
        {
            UiRenderer.DrawPanel(frame, _viewportOrigin, new UiPanel
            {
                Bounds = _viewportRect,
                Background = new Color(16, 16, 16)
            });
        }

        private void DrawDesktop(MySpriteDrawFrame frame, UiFrame frameModel)
        {
            var context = CreateContext(frameModel);
            var view = _builder.Build(context);
            view.Layout(context);

            if (frameModel.IsStandby)
            {
                if (context.PointerClicked)
                {
                    Session.Program.SubmitIntent(_block, _surface, new UiIntent(UiIntentType.WakeScreen));
                }

                ResetPointerClick(frameModel);
            }
            else
            {
                var command = view.HandleInput(context);
                if (command != null && Session.Program != null)
                {
                    Session.Program.SubmitIntent(_block, _surface, command.ToIntent());
                }

                ResetPointerClick(frameModel);
            }

            view.Render(frame, _viewportOrigin, context);
        }

        private UiContext CreateContext(UiFrame frameModel)
        {
            var pointer = frameModel.Pointer;
            var localPointer = pointer != null ? pointer.Position - _viewportOrigin : Vector2.Zero;
            var input = MyAPIGateway.Input;

            return new UiContext
            {
                Frame = frameModel,
                Theme = UiTheme.Create(frameModel, _layoutScale),
                Viewport = _viewportRect,
                Pointer = pointer,
                PointerLocal = localPointer,
                PointerActive = pointer != null && pointer.IsOnScreen,
                PointerPressed = pointer != null && pointer.IsPressed,
                PointerClicked = pointer != null && pointer.WasPressed,
                ScrollDelta = input != null ? input.DeltaMouseScrollWheelValue() : 0,
                Focus = _focus,
                SurfaceSize = _viewportSize
            };
        }

        private void DrawBios(MySpriteDrawFrame frame, UiFrame model)
        {
            var content = _viewportRect.Deflate(new UiThickness(GetContentPadding()));
            var lineHeight = MathHelper.Clamp(content.Height * 0.055f, 18f, 34f);
            var scale = GetBodyTextScale(0.72f);

            DrawLabel(frame, new UiRect(content.X, content.Y, content.Width, lineHeight), "AGS BIOS", GetBodyTextScale(1.1f), Color.Lime, TextAlignment.LEFT);
            DrawLabel(frame, new UiRect(content.X, content.Y + (lineHeight * 2f), content.Width, lineHeight), "controller scan online", scale, Color.Lime, TextAlignment.LEFT);
            DrawLabel(frame, new UiRect(content.X, content.Y + (lineHeight * 3.5f), content.Width, lineHeight), "construct: " + model.ConstructId, scale, Color.Lime, TextAlignment.LEFT);
            DrawLabel(frame, new UiRect(content.X, content.Y + (lineHeight * 4.75f), content.Width, lineHeight), "controllers: " + model.ControllerCount, scale, Color.Lime, TextAlignment.LEFT);
            DrawLabel(frame, new UiRect(content.X, content.Y + (lineHeight * 6.5f), content.Width, lineHeight), "booting command shell", scale, Color.Lime, TextAlignment.LEFT);
        }

        private void DrawLogo(MySpriteDrawFrame frame)
        {
            var title = CenterRect(_viewportRect.Width * 0.5f, MathHelper.Clamp(_viewportRect.Height * 0.1f, 30f, 80f), 0f, -(_viewportRect.Height * 0.03f));
            DrawLabel(frame, title, "COMMAND", GetBodyTextScale(1.45f), Color.White, TextAlignment.CENTER);
            DrawLabel(frame, new UiRect(title.X, title.Bottom, title.Width, title.Height * 0.55f), "SHELL", GetBodyTextScale(0.9f), new Color(170, 190, 225), TextAlignment.CENTER);
        }

        private void DrawMissingController(MySpriteDrawFrame frame)
        {
            var messageRect = CenterRect(_viewportRect.Width * 0.55f, MathHelper.Clamp(_viewportRect.Height * 0.16f, 48f, 120f));
            DrawLabel(frame, messageRect, "Command Core required", GetBodyTextScale(1f), Color.White, TextAlignment.CENTER);
            DrawLabel(frame, new UiRect(messageRect.X, messageRect.Y + (messageRect.Height * 0.55f), messageRect.Width, messageRect.Height * 0.4f), "place a core on this construct", GetBodyTextScale(0.72f), new Color(180, 180, 180), TextAlignment.CENTER);
        }

        private void DrawPointer(MySpriteDrawFrame frame, PointerState pointer)
        {
            if (pointer == null || !pointer.IsOnScreen || !pointer.IsActive)
            {
                return;
            }

            var size = MathHelper.Clamp(3f * pointer.Scale, 2f, 5f);
            var arm = size * 1.6f;
            var color = Color.White;
            var x = pointer.Position.X;
            var y = pointer.Position.Y;

            // Sprites anchor at (left-edge X, centre Y), so to centre each piece on the
            // cursor point its X must be shifted left by half its width (Y is already the
            // vertical centre). Without this the crosshair sits to the right of the point.
            frame.Add(new MySprite
            {
                Type = SpriteType.TEXTURE,
                Data = "SquareSimple",
                Position = new Vector2(x - (size * 0.5f), y),
                Size = new Vector2(size, size),
                Color = color
            });

            frame.Add(new MySprite
            {
                Type = SpriteType.TEXTURE,
                Data = "SquareSimple",
                Position = new Vector2(x - (arm * 0.5f), y),
                Size = new Vector2(arm, 1f),
                Color = color
            });

            frame.Add(new MySprite
            {
                Type = SpriteType.TEXTURE,
                Data = "SquareSimple",
                Position = new Vector2(x - 0.5f, y),
                Size = new Vector2(1f, arm),
                Color = color
            });
        }

        private void DrawLabel(MySpriteDrawFrame frame, UiRect rect, string text, float scale, Color color, TextAlignment alignment)
        {
            UiRenderer.DrawLabel(frame, _viewportOrigin, new UiLabel
            {
                Bounds = rect,
                Padding = new UiThickness(0f),
                Text = text,
                Font = "Monospace",
                Scale = scale,
                Color = color,
                Alignment = alignment,
                VerticalAlignment = UiVerticalAlignment.Center
            });
        }

        private UiRect CenterRect(float width, float height, float offsetX = 0f, float offsetY = 0f)
        {
            return new UiRect((_viewportRect.Width - width) * 0.5f + offsetX, (_viewportRect.Height - height) * 0.5f + offsetY, width, height);
        }

        private float GetContentPadding()
        {
            return MathHelper.Clamp(_viewportRect.Height * 0.03f, 8f, 18f);
        }

        private float GetBodyTextScale(float multiplier)
        {
            return MathHelper.Clamp((_viewportRect.Height / 512f) * 0.52f * multiplier, 0.22f, 1.1f);
        }

        private static void ResetPointerClick(UiFrame frameModel)
        {
            if (frameModel.Pointer != null)
            {
                frameModel.Pointer.WasPressed = false;
            }
        }
    }
}

