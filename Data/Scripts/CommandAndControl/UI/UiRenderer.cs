using System;
using System.Collections.Generic;
using Sandbox.Game.GameSystems.TextSurfaceScripts;
using VRage.Game.GUI.TextPanel;
using VRageMath;

namespace AGS
{
    public static class UiRenderer
    {
        // Set once per surface (see ShellScript) so wrapping and auto-fit can measure real
        // proportional glyph widths instead of guessing a fixed character width. Args are
        // (text, font, scale) -> pixel size. Falls back to an estimate when unset.
        public static Func<string, string, float, Vector2> MeasureText;

        private static float MeasureWidth(string text, string font, float scale)
        {
            if (string.IsNullOrEmpty(text))
            {
                return 0f;
            }

            var measure = MeasureText;
            if (measure != null)
            {
                var size = measure(text, font, scale);
                if (size.X > 0f)
                {
                    return size.X;
                }
            }

            // Fallback when no surface measurer is wired: rough monospace estimate.
            return text.Length * Math.Max(4f, 9f * scale);
        }

        public static void DrawPanel(MySpriteDrawFrame frame, Vector2 origin, UiPanel panel)
        {
            var topLeft = origin + panel.Bounds.Position;
            var spriteTopLeft = new Vector2(topLeft.X, topLeft.Y + (panel.Bounds.Size.Y * 0.5f));
            frame.Add(SpriteRect(spriteTopLeft, panel.Bounds.Size, panel.Background));
        }

        public static void DrawLabel(MySpriteDrawFrame frame, Vector2 origin, UiLabel label)
        {
            var text = label.Text ?? string.Empty;
            var textRect = label.Bounds.Deflate(label.Padding);
            var requestedScale = label.Scale < 0.34f ? 0.34f : label.Scale;
            var fittedScale = FitTextScale(text, label.Font, requestedScale, textRect.Width, textRect.Height);
            var position = GetTextPosition(textRect, label.Alignment, label.VerticalAlignment, fittedScale);
            var sprite = MySprite.CreateText(text, label.Font ?? "White", label.Color, fittedScale, label.Alignment);
            sprite.Position = origin + position;
            frame.Add(sprite);
        }

        public static void DrawButton(MySpriteDrawFrame frame, Vector2 origin, UiButton button)
        {
            var background = button.IsPressed ? button.PressedBackground : button.IsHovered ? button.HoverBackground : button.Background;
            var foreground = button.Foreground;
            if (button.IsDisabled)
            {
                background = Dim(background, 0.6f);
                foreground = Dim(foreground, 0.5f);
            }

            DrawPanel(frame, origin, new UiPanel { Bounds = button.Bounds, Background = background });
            DrawLabel(frame, origin, new UiLabel
            {
                Bounds = button.Bounds,
                Padding = button.Padding,
                Text = button.Text,
                Font = button.Font,
                Scale = button.Scale,
                Color = foreground,
                Alignment = button.Alignment,
                VerticalAlignment = UiVerticalAlignment.Center
            });
        }

        // Scales RGB toward black while preserving alpha; used for disabled chrome.
        public static Color Dim(Color color, float factor)
        {
            return new Color((byte)(color.R * factor), (byte)(color.G * factor), (byte)(color.B * factor), color.A);
        }

        public static void DrawTextBox(MySpriteDrawFrame frame, Vector2 origin, UiTextBox box)
        {
            DrawPanel(frame, origin, new UiPanel { Bounds = box.Bounds, Background = box.Background });

            var borderThickness = box.IsFocused ? 2f : 1f;
            DrawOutline(frame, origin, box.Bounds, box.Border, borderThickness);

            var hasText = !string.IsNullOrEmpty(box.Text);
            DrawLabel(frame, origin, new UiLabel
            {
                Bounds = box.Bounds,
                Padding = box.Padding,
                Text = hasText ? box.Text : box.Placeholder,
                Font = box.Font,
                Scale = box.Scale,
                Color = hasText ? box.Foreground : box.PlaceholderColor,
                Alignment = TextAlignment.LEFT,
                VerticalAlignment = UiVerticalAlignment.Center
            });
        }

        public static void DrawChromePanel(MySpriteDrawFrame frame, Vector2 origin, UiChromePanel panel)
        {
            DrawPanel(frame, origin, new UiPanel { Bounds = panel.Bounds, Background = panel.Background });
            DrawOutline(frame, origin, panel.Bounds, panel.Border, Math.Max(1f, panel.Bounds.Height * 0.006f));

            var headerHeight = panel.Bounds.Height * 0.085f;
            var headerRect = new UiRect(panel.Bounds.X, panel.Bounds.Y, panel.Bounds.Width, headerHeight);
            DrawPanel(frame, origin, new UiPanel { Bounds = headerRect, Background = panel.Header });
            DrawLabel(frame, origin, new UiLabel
            {
                Bounds = headerRect,
                Padding = new UiThickness(headerRect.Height * 0.3f, headerRect.Height * 0.08f, headerRect.Height * 0.3f, 0f),
                Text = panel.Title,
                Font = "Monospace",
                Scale = MathHelper.Clamp(headerRect.Height / 32f, 0.36f, 0.76f),
                Color = panel.TitleColor,
                Alignment = TextAlignment.CENTER,
                VerticalAlignment = UiVerticalAlignment.Center
            });
        }

        public static void DrawSectionHeader(MySpriteDrawFrame frame, Vector2 origin, UiSectionHeader header)
        {
            DrawPanel(frame, origin, new UiPanel { Bounds = header.Bounds, Background = header.Background });

            var accentHeight = Math.Max(1f, header.Bounds.Height * 0.08f);
            DrawPanel(frame, origin, new UiPanel
            {
                Bounds = new UiRect(header.Bounds.X, header.Bounds.Y, header.Bounds.Width, accentHeight),
                Background = header.Accent
            });

            var padX = Math.Max(2f, header.Bounds.Height * 0.18f);
            var titleRect = new UiRect(header.Bounds.X + padX, header.Bounds.Y + (header.Bounds.Height * 0.16f), header.Bounds.Width - (padX * 2f), header.Bounds.Height * 0.3f);
            var detailRect = new UiRect(header.Bounds.X + padX, header.Bounds.Y + (header.Bounds.Height * 0.58f), header.Bounds.Width - (padX * 2f), header.Bounds.Height * 0.18f);

            DrawLabel(frame, origin, new UiLabel
            {
                Bounds = titleRect,
                Padding = new UiThickness(0f),
                Text = header.Title,
                Font = "Monospace",
                Scale = MathHelper.Clamp(header.Bounds.Height / 34f, 0.34f, 0.72f),
                Color = header.TitleColor,
                Alignment = TextAlignment.LEFT,
                VerticalAlignment = UiVerticalAlignment.Center
            });

            if (!string.IsNullOrEmpty(header.Detail))
            {
                DrawLabel(frame, origin, new UiLabel
                {
                    Bounds = detailRect,
                    Padding = new UiThickness(0f),
                    Text = header.Detail,
                    Font = "Monospace",
                    Scale = MathHelper.Clamp(header.Bounds.Height / 38f, 0.34f, 0.52f),
                    Color = header.DetailColor,
                    Alignment = TextAlignment.LEFT,
                    VerticalAlignment = UiVerticalAlignment.Center
                });
            }
        }

        public static void DrawStatusBadge(MySpriteDrawFrame frame, Vector2 origin, UiStatusBadge badge)
        {
            DrawPanel(frame, origin, new UiPanel { Bounds = badge.Bounds, Background = badge.Background });
            DrawOutline(frame, origin, badge.Bounds, badge.Border, Math.Max(1f, badge.Bounds.Height * 0.05f));

            var padX = Math.Max(2f, badge.Bounds.Height * 0.16f);
            var padY = Math.Max(1f, badge.Bounds.Height * 0.14f);
            var labelRect = new UiRect(badge.Bounds.X + padX, badge.Bounds.Y + padY, badge.Bounds.Width * 0.5f, badge.Bounds.Height - (padY * 2f));
            var valueRect = new UiRect(badge.Bounds.X + (badge.Bounds.Width * 0.48f), badge.Bounds.Y + padY, badge.Bounds.Width * 0.48f - padX, badge.Bounds.Height - (padY * 2f));

            DrawLabel(frame, origin, new UiLabel
            {
                Bounds = labelRect,
                Padding = new UiThickness(0f),
                Text = badge.Label,
                Font = "Monospace",
                Scale = MathHelper.Clamp(badge.Bounds.Height / 22f, 0.32f, 0.54f),
                Color = badge.LabelColor,
                Alignment = TextAlignment.LEFT,
                VerticalAlignment = UiVerticalAlignment.Center
            });
            DrawLabel(frame, origin, new UiLabel
            {
                Bounds = valueRect,
                Padding = new UiThickness(0f),
                Text = badge.Value,
                Font = "Monospace",
                Scale = MathHelper.Clamp(badge.Bounds.Height / 22f, 0.32f, 0.54f),
                Color = badge.ValueColor,
                Alignment = TextAlignment.RIGHT,
                VerticalAlignment = UiVerticalAlignment.Center
            });
        }

        public static void DrawBracketFrame(MySpriteDrawFrame frame, Vector2 origin, UiBracketFrame panel)
        {
            DrawPanel(frame, origin, new UiPanel { Bounds = panel.Bounds, Background = panel.Background });

            var inset = Math.Max(3f, panel.Bounds.Height * 0.05f);
            var titleHeight = Math.Max(10f, panel.Bounds.Height * 0.08f);
            var titleRect = new UiRect(panel.Bounds.X + inset, panel.Bounds.Y + 2f, panel.Bounds.Width - (inset * 2f), titleHeight);
            DrawLabel(frame, origin, new UiLabel
            {
                Bounds = titleRect,
                Padding = new UiThickness(0f),
                Text = panel.Title,
                Font = "Monospace",
                Scale = MathHelper.Clamp(titleHeight / 26f, 0.32f, 0.6f),
                Color = panel.TitleColor,
                Alignment = TextAlignment.CENTER,
                VerticalAlignment = UiVerticalAlignment.Center
            });

            var line = Math.Max(1f, panel.Bounds.Height * 0.008f);
            var arm = Math.Max(6f, panel.Bounds.Width * 0.1f);
            var topY = panel.Bounds.Y + titleHeight + 4f;
            var bottomY = panel.Bounds.Bottom - line;
            var leftX = panel.Bounds.X;
            var rightX = panel.Bounds.Right - line;

            DrawPanel(frame, origin, new UiPanel { Bounds = new UiRect(leftX, topY, arm, line), Background = panel.Accent });
            DrawPanel(frame, origin, new UiPanel { Bounds = new UiRect(leftX, topY, line, arm * 0.7f), Background = panel.Accent });
            DrawPanel(frame, origin, new UiPanel { Bounds = new UiRect(rightX - arm + line, topY, arm, line), Background = panel.Accent });
            DrawPanel(frame, origin, new UiPanel { Bounds = new UiRect(rightX, topY, line, arm * 0.7f), Background = panel.Accent });
            DrawPanel(frame, origin, new UiPanel { Bounds = new UiRect(leftX, bottomY, arm, line), Background = panel.Border });
            DrawPanel(frame, origin, new UiPanel { Bounds = new UiRect(leftX, bottomY - (arm * 0.7f) + line, line, arm * 0.7f), Background = panel.Border });
            DrawPanel(frame, origin, new UiPanel { Bounds = new UiRect(rightX - arm + line, bottomY, arm, line), Background = panel.Border });
            DrawPanel(frame, origin, new UiPanel { Bounds = new UiRect(rightX, bottomY - (arm * 0.7f) + line, line, arm * 0.7f), Background = panel.Border });
        }

        public static void DrawMetricBar(MySpriteDrawFrame frame, Vector2 origin, UiMetricBar bar)
        {
            var ratio = MathHelper.Clamp(bar.Ratio, 0f, 1f);
            var labelHeight = bar.Bounds.Height * 0.24f;
            var trackGap = Math.Max(1f, bar.Bounds.Height * 0.08f);
            var trackY = bar.Bounds.Y + labelHeight + trackGap;
            var trackHeight = Math.Max(4f, bar.Bounds.Bottom - trackY);
            var labelRect = new UiRect(bar.Bounds.X, bar.Bounds.Y, bar.Bounds.Width, labelHeight);
            var trackRect = new UiRect(bar.Bounds.X, trackY, bar.Bounds.Width, trackHeight);
            var inset = Math.Max(1f, trackRect.Height * 0.12f);
            var fillRect = new UiRect(trackRect.X + inset, trackRect.Y + inset, (trackRect.Width - (inset * 2f)) * ratio, Math.Max(1f, trackRect.Height - (inset * 2f)));

            DrawLabel(frame, origin, new UiLabel
            {
                Bounds = labelRect,
                Padding = new UiThickness(0f),
                Text = bar.Label,
                Font = "Monospace",
                Scale = MathHelper.Clamp(bar.Bounds.Height / 28f, 0.32f, 0.58f),
                Color = bar.LabelColor,
                Alignment = TextAlignment.LEFT,
                VerticalAlignment = UiVerticalAlignment.Center
            });
            DrawLabel(frame, origin, new UiLabel
            {
                Bounds = labelRect,
                Padding = new UiThickness(0f),
                Text = bar.ValueText,
                Font = "Monospace",
                Scale = MathHelper.Clamp(bar.Bounds.Height / 28f, 0.34f, 0.54f),
                Color = bar.ValueColor,
                Alignment = TextAlignment.RIGHT,
                VerticalAlignment = UiVerticalAlignment.Center
            });
            DrawPanel(frame, origin, new UiPanel { Bounds = trackRect, Background = bar.TrackColor });
            DrawOutline(frame, origin, trackRect, bar.LabelColor, Math.Max(1f, trackRect.Height * 0.08f));
            DrawPanel(frame, origin, new UiPanel { Bounds = fillRect, Background = bar.FillColor });
        }

        public static void DrawValueTile(MySpriteDrawFrame frame, Vector2 origin, UiValueTile tile)
        {
            DrawPanel(frame, origin, new UiPanel { Bounds = tile.Bounds, Background = tile.Background });
            DrawOutline(frame, origin, tile.Bounds, tile.Border, Math.Max(1f, tile.Bounds.Height * 0.04f));

            var captionRect = new UiRect(tile.Bounds.X, tile.Bounds.Y + (tile.Bounds.Height * 0.12f), tile.Bounds.Width, tile.Bounds.Height * 0.12f);
            var valueRect = new UiRect(tile.Bounds.X, tile.Bounds.Y + (tile.Bounds.Height * 0.42f), tile.Bounds.Width, tile.Bounds.Height * 0.22f);
            var padX = Math.Max(2f, tile.Bounds.Height * 0.1f);

            DrawLabel(frame, origin, new UiLabel
            {
                Bounds = captionRect,
                Padding = new UiThickness(padX, 0f, padX, 0f),
                Text = tile.Caption,
                Font = "Monospace",
                Scale = MathHelper.Clamp(tile.Bounds.Height / 30f, 0.34f, 0.56f),
                Color = tile.CaptionColor,
                Alignment = TextAlignment.CENTER,
                VerticalAlignment = UiVerticalAlignment.Center
            });
            DrawLabel(frame, origin, new UiLabel
            {
                Bounds = valueRect,
                Padding = new UiThickness(padX, 0f, padX, 0f),
                Text = tile.Value,
                Font = "Monospace",
                Scale = MathHelper.Clamp(tile.Bounds.Height / 22f, 0.42f, 0.84f),
                Color = tile.ValueColor,
                Alignment = TextAlignment.CENTER,
                VerticalAlignment = UiVerticalAlignment.Center
            });
        }

        public static void DrawIndicatorStrip(MySpriteDrawFrame frame, Vector2 origin, UiIndicatorStrip strip)
        {
            if (strip.Count <= 0)
            {
                return;
            }

            var gap = strip.Bounds.Width * 0.025f;
            var segmentWidth = (strip.Bounds.Width - (gap * (strip.Count - 1))) / strip.Count;
            for (var i = 0; i < strip.Count; i++)
            {
                var rect = new UiRect(strip.Bounds.X + (i * (segmentWidth + gap)), strip.Bounds.Y, segmentWidth, strip.Bounds.Height);
                DrawPanel(frame, origin, new UiPanel { Bounds = rect, Background = i < strip.ActiveCount ? strip.ActiveColor : strip.InactiveColor });
            }
        }

        public static void DrawRingButton(MySpriteDrawFrame frame, Vector2 origin, UiRect bounds, bool active, bool hovered, bool pressed, Color chrome, Color accent, Color glow)
        {
            var side = bounds.Width < bounds.Height ? bounds.Width : bounds.Height;
            var outerRadius = side * 0.5f;
            var innerCutout = outerRadius * 0.72f;
            var innerRingOuter = outerRadius * 0.54f;
            var innerRingCutout = outerRadius * 0.34f;
            var topLeft = origin + bounds.Position;
            var centerOffset = new Vector2((bounds.Width - (outerRadius * 2f)) * 0.5f, (bounds.Height - (outerRadius * 2f)) * 0.5f);
            var outerTopLeft = topLeft + centerOffset;

            if (hovered || pressed)
            {
                var glowRadius = outerRadius + 2f;
                var glowInset = outerRadius - glowRadius;
                var glowTopLeft = outerTopLeft + new Vector2(glowInset, glowInset);
                var glowSpriteTopLeft = new Vector2(glowTopLeft.X, glowTopLeft.Y + glowRadius);
                Circle(frame, glowSpriteTopLeft, glowRadius, pressed ? glow : new Color(glow.R, glow.G, glow.B, (byte)(glow.A * 0.65f)));
            }

            var ringColor = pressed ? new Color(242, 247, 255) : active ? new Color(228, 236, 252) : hovered ? new Color(216, 228, 248) : chrome;
            var accentColor = pressed ? new Color(176, 206, 255) : active ? new Color(148, 182, 236) : hovered ? new Color(128, 158, 214) : accent;
            var cutoutColor = new Color(34, 40, 58);

            var outerSpriteTopLeft = new Vector2(outerTopLeft.X, outerTopLeft.Y + outerRadius);
            var outerCutoutTopLeft = outerTopLeft + new Vector2(outerRadius - innerCutout, outerRadius - innerCutout);
            var outerCutoutSpriteTopLeft = new Vector2(outerCutoutTopLeft.X, outerCutoutTopLeft.Y + innerCutout);
            var innerRingTopLeft = outerTopLeft + new Vector2(outerRadius - innerRingOuter, outerRadius - innerRingOuter);
            var innerRingSpriteTopLeft = new Vector2(innerRingTopLeft.X, innerRingTopLeft.Y + innerRingOuter);
            var innerCutoutTopLeft = outerTopLeft + new Vector2(outerRadius - innerRingCutout, outerRadius - innerRingCutout);
            var innerCutoutSpriteTopLeft = new Vector2(innerCutoutTopLeft.X, innerCutoutTopLeft.Y + innerRingCutout);

            Circle(frame, outerSpriteTopLeft, outerRadius, ringColor);
            Circle(frame, outerCutoutSpriteTopLeft, innerCutout, cutoutColor);
            Circle(frame, innerRingSpriteTopLeft, innerRingOuter, accentColor);
            Circle(frame, innerCutoutSpriteTopLeft, innerRingCutout, cutoutColor);
        }

        public static void DrawOutline(MySpriteDrawFrame frame, Vector2 origin, UiRect rect, Color color, float thickness)
        {
            var top = new UiRect(rect.X, rect.Y, rect.Width, thickness);
            var bottom = new UiRect(rect.X, rect.Bottom - thickness, rect.Width, thickness);
            var left = new UiRect(rect.X, rect.Y, thickness, rect.Height);
            var right = new UiRect(rect.Right - thickness, rect.Y, thickness, rect.Height);
            DrawPanel(frame, origin, new UiPanel { Bounds = top, Background = color });
            DrawPanel(frame, origin, new UiPanel { Bounds = bottom, Background = color });
            DrawPanel(frame, origin, new UiPanel { Bounds = left, Background = color });
            DrawPanel(frame, origin, new UiPanel { Bounds = right, Background = color });
        }

        public static void DrawLine(MySpriteDrawFrame frame, Vector2 origin, Vector2 a, Vector2 b, Color color, float thickness)
        {
            var delta = b - a;
            var length = delta.Length();
            if (length <= 0.001f)
            {
                return;
            }

            // SquareSimple sprites in this renderer anchor at (left edge X, centre Y) and
            // rotate about the sprite centre. Drawing the bar along its HEIGHT axis with a
            // +90° offset keeps the rotation pivot on the segment midpoint, so the line runs
            // exactly a->b. (Width-as-length instead pivots half a segment off and skews
            // every diagonal.) Matches Adrian Lima's TouchScreenAPI chart line recipe.
            frame.Add(new MySprite
            {
                Type = SpriteType.TEXTURE,
                Data = "SquareSimple",
                Position = origin + ((a + b) * 0.5f),
                Size = new Vector2(Math.Max(1f, thickness), length),
                RotationOrScale = (float)Math.Atan2(delta.Y, delta.X) + MathHelper.PiOver2,
                Color = color
            });
        }

        public static void DrawCircle(MySpriteDrawFrame frame, Vector2 origin, Vector2 center, float radius, Color color)
        {
            if (radius <= 0f)
            {
                return;
            }

            // Same anchor convention as the rest of the renderer: X = left edge of
            // the bounding box, Y = vertical centre.
            var spritePosition = origin + new Vector2(center.X - radius, center.Y);
            Circle(frame, spritePosition, radius, color);
        }

        public static void DrawEllipse(MySpriteDrawFrame frame, Vector2 origin, UiRect bounds, Color color)
        {
            if (bounds.Width <= 0f || bounds.Height <= 0f)
            {
                return;
            }

            var topLeft = origin + bounds.Position;
            frame.Add(new MySprite
            {
                Type = SpriteType.TEXTURE,
                Data = "Circle",
                Position = new Vector2(topLeft.X, topLeft.Y + (bounds.Size.Y * 0.5f)),
                Size = bounds.Size,
                Color = color
            });
        }

        // Word-wrapped multi-line text within rect. Honours embedded newlines.
        public static void DrawParagraph(MySpriteDrawFrame frame, Vector2 origin, UiRect rect, string text, string font, float scale, Color color, TextAlignment alignment)
        {
            if (string.IsNullOrEmpty(text))
            {
                return;
            }

            var lineHeight = Math.Max(12f, 30f * scale);
            var lines = WrapLines(text, rect.Width, scale, font);
            var y = rect.Y;
            for (var i = 0; i < lines.Count; i++)
            {
                if (y >= rect.Bottom)
                {
                    break;
                }

                DrawLabel(frame, origin, new UiLabel
                {
                    Bounds = new UiRect(rect.X, y, rect.Width, lineHeight),
                    Padding = new UiThickness(0f),
                    Text = lines[i],
                    Font = font,
                    Scale = scale,
                    Color = color,
                    Alignment = alignment,
                    VerticalAlignment = UiVerticalAlignment.Top
                });
                y += lineHeight;
            }
        }

        public static int CountWrappedLines(string text, float maxWidth, float scale, string font)
        {
            return string.IsNullOrEmpty(text) ? 0 : WrapLines(text, maxWidth, scale, font).Count;
        }

        // Greedy word wrap that measures real glyph widths against maxWidth. Honours embedded
        // newlines, and hard-splits any single word that is itself wider than the line.
        private static List<string> WrapLines(string text, float maxWidth, float scale, string font)
        {
            var lines = new List<string>();
            var limit = Math.Max(1f, maxWidth);
            var paragraphs = (text ?? string.Empty).Split('\n');
            for (var p = 0; p < paragraphs.Length; p++)
            {
                var words = paragraphs[p].Split(' ');
                var current = string.Empty;
                for (var i = 0; i < words.Length; i++)
                {
                    var word = words[i];
                    if (word.Length == 0)
                    {
                        continue;
                    }

                    // Break a word that cannot fit on a line by itself, one chunk at a time.
                    while (MeasureWidth(word, font, scale) > limit && word.Length > 1)
                    {
                        if (current.Length > 0)
                        {
                            lines.Add(current);
                            current = string.Empty;
                        }

                        var cut = LongestPrefixThatFits(word, font, scale, limit);
                        lines.Add(word.Substring(0, cut));
                        word = word.Substring(cut);
                    }

                    if (current.Length == 0)
                    {
                        current = word;
                    }
                    else if (MeasureWidth(current + " " + word, font, scale) <= limit)
                    {
                        current = current + " " + word;
                    }
                    else
                    {
                        lines.Add(current);
                        current = word;
                    }
                }

                lines.Add(current);
            }

            return lines;
        }

        private static int LongestPrefixThatFits(string word, string font, float scale, float limit)
        {
            // At least one character, even if it overflows, to guarantee progress.
            var cut = 1;
            for (var n = 1; n <= word.Length; n++)
            {
                if (MeasureWidth(word.Substring(0, n), font, scale) <= limit)
                {
                    cut = n;
                }
                else
                {
                    break;
                }
            }

            return cut;
        }

        public static void DrawImage(MySpriteDrawFrame frame, Vector2 origin, UiRect bounds, string sprite, Color color)
        {
            if (string.IsNullOrEmpty(sprite))
            {
                return;
            }

            var topLeft = origin + bounds.Position;
            frame.Add(new MySprite
            {
                Type = SpriteType.TEXTURE,
                Data = sprite,
                Position = new Vector2(topLeft.X, topLeft.Y + (bounds.Size.Y * 0.5f)),
                Size = bounds.Size,
                Color = color
            });
        }

        private static Vector2 GetTextPosition(UiRect rect, TextAlignment alignment, UiVerticalAlignment verticalAlignment, float scale)
        {
            var x = rect.X;
            if (alignment == TextAlignment.CENTER)
            {
                x = rect.X + (rect.Width * 0.5f);
            }
            else if (alignment == TextAlignment.RIGHT)
            {
                x = rect.Right;
            }

            var textHeight = EstimateTextHeight(scale, rect.Height);
            var y = rect.Y;
            if (verticalAlignment == UiVerticalAlignment.Center)
            {
                y = rect.Y + ((rect.Height - textHeight) * 0.5f);
            }
            else if (verticalAlignment == UiVerticalAlignment.Bottom)
            {
                y = rect.Bottom - textHeight;
            }

            y += textHeight * 0.12f;
            return new Vector2(x, y);
        }

        private static float EstimateTextHeight(float scale, float rectHeight)
        {
            return Math.Min(rectHeight, Math.Max(8f, 28f * scale));
        }

        private static float FitTextScale(string text, string font, float requestedScale, float rectWidth, float rectHeight)
        {
            var fitted = requestedScale;
            var safeHeight = Math.Max(1f, rectHeight);
            var safeWidth = Math.Max(1f, rectWidth);

            var heightLimited = safeHeight / 28f;
            if (heightLimited < fitted)
            {
                fitted = heightLimited;
            }

            if (!string.IsNullOrEmpty(text))
            {
                // Measure at the requested scale and shrink proportionally if it overruns.
                var measured = MeasureWidth(text, font, requestedScale);
                if (measured > safeWidth)
                {
                    var widthLimited = requestedScale * (safeWidth / measured);
                    if (widthLimited < fitted)
                    {
                        fitted = widthLimited;
                    }
                }
            }

            if (fitted < 0.3f)
            {
                fitted = 0.3f;
            }

            return fitted;
        }

        private static MySprite SpriteRect(Vector2 spritePosition, Vector2 size, Color color)
        {
            return new MySprite
            {
                Type = SpriteType.TEXTURE,
                Data = "SquareSimple",
                Position = spritePosition,
                Size = size,
                Color = color
            };
        }

        private static void Circle(MySpriteDrawFrame frame, Vector2 spritePosition, float radius, Color color)
        {
            frame.Add(CircleSprite(spritePosition, radius, color));
        }

        private static MySprite CircleSprite(Vector2 spritePosition, float radius, Color color)
        {
            var size = new Vector2(radius * 2f, radius * 2f);
            return new MySprite
            {
                Type = SpriteType.TEXTURE,
                Data = "Circle",
                Position = spritePosition,
                Size = size,
                Color = color
            };
        }
    }
}



