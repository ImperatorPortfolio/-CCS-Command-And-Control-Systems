using System;
using System.Collections.Generic;
using VRage.Game.GUI.TextPanel;
using VRageMath;

namespace AGS
{
    public static class EngineeringView
    {
        private const string ViewPath = "Data\\UI\\Views\\Engineering.xml";
        private const int VisibleItems = 6;

        public static UiElement Build(UiContext context)
        {
            var bindings = CreateBindings(context);
            var visuals = new Dictionary<string, Action<MySpriteDrawFrame, Vector2, UiRect, UiContext>>
            {
                { "PowerBoard", RenderPowerBoard }
            };

            return UiXmlLoader.Build(ViewPath, context, bindings, visuals);
        }

        private static UiXmlBindings CreateBindings(UiContext context)
        {
            var bindings = new UiXmlBindings();
            var frame = context.Frame;
            var model = frame.Engineering;
            var items = GetVisibleEntities(model);
            var selected = FindSelected(model);
            var power = FindProvider(frame, "power");
            var integrity = FindProvider(frame, "integrity");
            var damage = FindProvider(frame, "damage");
            var logistics = FindProvider(frame, "logistics");

            var batteries = CountSubpage(model, EngineeringSubpage.Batteries);
            var reactors = CountSubpage(model, EngineeringSubpage.Reactors);
            var h2 = CountSubpage(model, EngineeringSubpage.Hydrogen);
            var damageCount = CountSubpage(model, EngineeringSubpage.Damage);
            var healthRatio = GetIntegrityRatio(integrity);
            var sourceRatio = MathHelper.Clamp((batteries + reactors + h2) / 12f, 0f, 1f);
            var damageRatio = GetDamageRatio(damage, integrity);

            BindTheme(bindings, context);
            bindings.Set("title", "ENGINEERING STATION");
            bindings.Set("detail", frame.ControllerName ?? "POWER BUS ONLINE");

            bindings.Set("nav1", FormatNav("POWER", model.ActiveSubpage == EngineeringSubpage.Power, batteries + reactors + h2));
            bindings.Set("nav2", FormatNav("HYDROGEN", model.ActiveSubpage == EngineeringSubpage.Hydrogen, h2));
            bindings.Set("nav3", FormatNav("BATTERIES", model.ActiveSubpage == EngineeringSubpage.Batteries, batteries));
            bindings.Set("nav4", FormatNav("REACTORS", model.ActiveSubpage == EngineeringSubpage.Reactors, reactors));
            bindings.Set("nav5", FormatNav("CONVEYORS", model.ActiveSubpage == EngineeringSubpage.Conveyors, CountSubpage(model, EngineeringSubpage.Conveyors)));
            bindings.Set("nav6", FormatNav("DAMAGE", model.ActiveSubpage == EngineeringSubpage.Damage, damageCount));

            bindings.Set("bodyTitle", "CONTROL SURFACE");
            bindings.Set("bodyDetail", GetProviderStateText(power) + " | " + selected.Kind + " | SELECT " + (model.SelectedId ?? string.Empty));

            for (var i = 0; i < VisibleItems; i++)
            {
                var slot = i + 1;
                var item = i < items.Count ? items[i] : null;
                bindings.Set("item" + slot + "Text", FormatItem(item, selected));
                bindings.Set("item" + slot + "Data", item != null ? item.Id : string.Empty);
            }

            bindings.Set("detailsTitle", selected.Name);
            bindings.Set("detailsState", selected.StatusText);
            bindings.Set("detailsSummary", selected.Summary);
            bindings.Set("detailsLine1", selected.Detail);
            bindings.Set("detailsLine2", selected.Secondary);
            bindings.Set("detailsLine3", "SUBPAGE " + model.ActiveSubpage.ToString().ToUpperInvariant() + " | NODE " + items.Count.ToString("00"));

            bindings.Set("metric1Label", "GRID HEALTH");
            bindings.Set("metric1Value", ((int)Math.Round(healthRatio * 100f)).ToString("00") + "%");
            bindings.Set("metric1Ratio", healthRatio);
            bindings.Set("metric2Label", "SOURCE LOAD");
            bindings.Set("metric2Value", (batteries + reactors + h2).ToString("00"));
            bindings.Set("metric2Ratio", sourceRatio);
            bindings.Set("metric3Label", "DAMAGE LOAD");
            bindings.Set("metric3Value", ((int)Math.Round(damageRatio * 100f)).ToString("00") + "%");
            bindings.Set("metric3Ratio", damageRatio);

            bindings.Set("tile1Caption", "BAT");
            bindings.Set("tile1Value", batteries.ToString("00"));
            bindings.Set("tile2Caption", "RCT");
            bindings.Set("tile2Value", reactors.ToString("00"));
            bindings.Set("tile3Caption", "H2");
            bindings.Set("tile3Value", h2.ToString("00"));
            bindings.Set("tile4Caption", "DMG");
            bindings.Set("tile4Value", damageCount.ToString("00"));

            bindings.Set("powerStatusLabel", "POWER");
            bindings.Set("powerStatusValue", GetProviderStateText(power));
            bindings.Set("powerStatusColor", GetStatusColor(power, context));
            bindings.Set("integrityStatusLabel", "INTEGRITY");
            bindings.Set("integrityStatusValue", GetProviderStateText(integrity));
            bindings.Set("integrityStatusColor", GetStatusColor(integrity, context));
            bindings.Set("damageStatusLabel", "DAMAGE");
            bindings.Set("damageStatusValue", GetProviderStateText(damage));
            bindings.Set("damageStatusColor", GetStatusColor(damage, context));
            bindings.Set("logisticsStatusLabel", "LOGISTICS");
            bindings.Set("logisticsStatusValue", GetProviderStateText(logistics));
            bindings.Set("logisticsStatusColor", GetStatusColor(logistics, context));
            bindings.Set("selectedEnabledLabel", "STATE");
            bindings.Set("selectedEnabledValue", selected.Enabled ? "ENABLED" : "DISABLED");
            bindings.Set("selectedEnabledColor", selected.Enabled ? context.Theme.Success : context.Theme.Warning);
            bindings.Set("selectedFunctionalLabel", "CORE");
            bindings.Set("selectedFunctionalValue", selected.Functional ? "FUNCTIONAL" : "OFFLINE");
            bindings.Set("selectedFunctionalColor", selected.Functional ? context.Theme.Accent : context.Theme.Danger);
            return bindings;
        }

        private static void BindTheme(UiXmlBindings bindings, UiContext context)
        {
            bindings.Set("bg0", context.Theme.Background0);
            bindings.Set("bg1", context.Theme.Background1);
            bindings.Set("bg2", context.Theme.Background2);
            bindings.Set("border", context.Theme.Border);
            bindings.Set("accent", context.Theme.Accent);
            bindings.Set("accentSoft", context.Theme.AccentSoft);
            bindings.Set("success", context.Theme.Success);
            bindings.Set("warning", context.Theme.Warning);
            bindings.Set("danger", context.Theme.Danger);
            bindings.Set("fg", context.Theme.ForegroundPrimary);
            bindings.Set("muted", context.Theme.ForegroundMuted);
            bindings.Set("dim", context.Theme.ForegroundDim);
        }

        private static void RenderPowerBoard(MySpriteDrawFrame frame, Vector2 origin, UiRect rect, UiContext context)
        {
            var model = context.Frame.Engineering;
            var selected = FindSelected(model);
            var items = GetVisibleEntities(model);
            UiRenderer.DrawPanel(frame, origin, new UiPanel { Bounds = rect, Background = context.Theme.Background0 });
            UiRenderer.DrawOutline(frame, origin, rect, context.Theme.Border, Math.Max(1f, context.Theme.StrokeThin));

            var inset = Math.Max(6f, rect.Height * 0.03f);
            var inner = rect.Deflate(new UiThickness(inset));
            var topRect = new UiRect(inner.X, inner.Y, inner.Width, inner.Height * 0.34f);
            var middleRect = new UiRect(inner.X, topRect.Bottom + Math.Max(6f, inner.Height * 0.03f), inner.Width, inner.Height * 0.2f);
            var lowerRect = new UiRect(inner.X, middleRect.Bottom + Math.Max(6f, inner.Height * 0.03f), inner.Width, inner.Bottom - (middleRect.Bottom + Math.Max(6f, inner.Height * 0.03f)));

            DrawBus(frame, origin, topRect, context, model, selected);
            DrawItemStrip(frame, origin, middleRect, context, items, selected);
            DrawTelemetry(frame, origin, lowerRect, context, model);
        }

        private static void DrawBus(MySpriteDrawFrame frame, Vector2 origin, UiRect rect, UiContext context, EngineeringScreenModel model, EngineeringEntity selected)
        {
            UiRenderer.DrawPanel(frame, origin, new UiPanel { Bounds = rect, Background = context.Theme.Background1 });
            UiRenderer.DrawOutline(frame, origin, rect, context.Theme.Border, 1f);
            UiRenderer.DrawSectionHeader(frame, origin, new UiSectionHeader
            {
                Bounds = new UiRect(rect.X, rect.Y, rect.Width, Math.Max(16f, rect.Height * 0.16f)),
                Title = selected.Name,
                Detail = selected.StatusText,
                Background = context.Theme.Background2,
                Accent = context.Theme.Accent,
                TitleColor = context.Theme.ForegroundPrimary,
                DetailColor = context.Theme.ForegroundMuted
            });

            var centerX = rect.X + rect.Width * 0.5f;
            var topY = rect.Y + rect.Height * 0.26f;
            var bottomY = rect.Bottom - rect.Height * 0.12f;
            var thickness = Math.Max(2f, rect.Width * 0.012f);
            UiRenderer.DrawLine(frame, origin, new Vector2(centerX, topY), new Vector2(centerX, bottomY), context.Theme.Border, thickness);
            UiRenderer.DrawLine(frame, origin, new Vector2(rect.X + rect.Width * 0.18f, rect.Y + rect.Height * 0.46f), new Vector2(rect.Right - rect.Width * 0.18f, rect.Y + rect.Height * 0.46f), context.Theme.AccentSoft, Math.Max(1f, thickness * 0.8f));
            UiRenderer.DrawLine(frame, origin, new Vector2(rect.X + rect.Width * 0.28f, rect.Y + rect.Height * 0.66f), new Vector2(rect.Right - rect.Width * 0.28f, rect.Y + rect.Height * 0.66f), context.Theme.AccentSoft, Math.Max(1f, thickness * 0.8f));

            DrawNode(frame, origin, new Vector2(centerX, rect.Y + rect.Height * 0.22f), context.Theme.Warning);
            DrawNode(frame, origin, new Vector2(rect.X + rect.Width * 0.18f, rect.Y + rect.Height * 0.46f), context.Theme.Accent);
            DrawNode(frame, origin, new Vector2(rect.Right - rect.Width * 0.18f, rect.Y + rect.Height * 0.46f), context.Theme.Warning);
            DrawNode(frame, origin, new Vector2(centerX, rect.Y + rect.Height * 0.66f), selected.Enabled ? context.Theme.Success : context.Theme.Danger);

            DrawReadout(frame, origin, new UiRect(rect.X + rect.Width * 0.1f, rect.Bottom - rect.Height * 0.18f, rect.Width * 0.22f, rect.Height * 0.1f), context, "MODE", model.ActiveSubpage.ToString().ToUpperInvariant());
            DrawReadout(frame, origin, new UiRect(rect.X + rect.Width * 0.39f, rect.Bottom - rect.Height * 0.18f, rect.Width * 0.22f, rect.Height * 0.1f), context, "STATE", selected.Enabled ? "ON" : "OFF");
            DrawReadout(frame, origin, new UiRect(rect.X + rect.Width * 0.68f, rect.Bottom - rect.Height * 0.18f, rect.Width * 0.22f, rect.Height * 0.1f), context, "HEALTH", ((int)Math.Round(selected.Metric * 100f)).ToString("00"));
        }

        private static void DrawItemStrip(MySpriteDrawFrame frame, Vector2 origin, UiRect rect, UiContext context, List<EngineeringEntity> items, EngineeringEntity selected)
        {
            UiRenderer.DrawPanel(frame, origin, new UiPanel { Bounds = rect, Background = context.Theme.Background1 });
            UiRenderer.DrawOutline(frame, origin, rect, context.Theme.Border, 1f);
            var slotCount = Math.Max(1, VisibleItems);
            var gap = Math.Max(4f, rect.Width * 0.01f);
            var slotWidth = (rect.Width - (gap * (slotCount + 1))) / slotCount;
            var slotHeight = rect.Height - gap * 2f;
            for (var i = 0; i < slotCount; i++)
            {
                var x = rect.X + gap + i * (slotWidth + gap);
                var bounds = new UiRect(x, rect.Y + gap, slotWidth, slotHeight);
                var item = i < items.Count ? items[i] : null;
                var isSelected = item != null && selected != null && string.Equals(item.Id, selected.Id, StringComparison.OrdinalIgnoreCase);
                UiRenderer.DrawPanel(frame, origin, new UiPanel { Bounds = bounds, Background = isSelected ? context.Theme.Background2 : context.Theme.Background0 });
                UiRenderer.DrawOutline(frame, origin, bounds, isSelected ? context.Theme.Accent : context.Theme.Border, 1f);
                if (item == null)
                {
                    continue;
                }
                UiRenderer.DrawLabel(frame, origin, new UiLabel
                {
                    Bounds = new UiRect(bounds.X + 2f, bounds.Y + bounds.Height * 0.12f, bounds.Width - 4f, bounds.Height * 0.18f),
                    Padding = new UiThickness(0f),
                    Text = item.Kind,
                    Font = "Monospace",
                    Scale = 0.34f,
                    Color = context.Theme.ForegroundMuted,
                    Alignment = TextAlignment.CENTER,
                    VerticalAlignment = UiVerticalAlignment.Center
                });
                UiRenderer.DrawLabel(frame, origin, new UiLabel
                {
                    Bounds = new UiRect(bounds.X + 4f, bounds.Y + bounds.Height * 0.36f, bounds.Width - 8f, bounds.Height * 0.28f),
                    Padding = new UiThickness(0f),
                    Text = TrimName(item.Name, 10),
                    Font = "Monospace",
                    Scale = 0.42f,
                    Color = context.Theme.ForegroundPrimary,
                    Alignment = TextAlignment.CENTER,
                    VerticalAlignment = UiVerticalAlignment.Center
                });
                UiRenderer.DrawLabel(frame, origin, new UiLabel
                {
                    Bounds = new UiRect(bounds.X + 2f, bounds.Y + bounds.Height * 0.7f, bounds.Width - 4f, bounds.Height * 0.16f),
                    Padding = new UiThickness(0f),
                    Text = item.Enabled ? "ENABLED" : "DISABLED",
                    Font = "Monospace",
                    Scale = 0.34f,
                    Color = item.Enabled ? context.Theme.Success : context.Theme.Warning,
                    Alignment = TextAlignment.CENTER,
                    VerticalAlignment = UiVerticalAlignment.Center
                });
            }
        }

        private static void DrawTelemetry(MySpriteDrawFrame frame, Vector2 origin, UiRect rect, UiContext context, EngineeringScreenModel model)
        {
            UiRenderer.DrawPanel(frame, origin, new UiPanel { Bounds = rect, Background = context.Theme.Background1 });
            UiRenderer.DrawOutline(frame, origin, rect, context.Theme.Border, 1f);
            UiRenderer.DrawSectionHeader(frame, origin, new UiSectionHeader
            {
                Bounds = new UiRect(rect.X, rect.Y, rect.Width, Math.Max(16f, rect.Height * 0.18f)),
                Title = "LIVE TELEMETRY",
                Detail = model.ActiveSubpage.ToString().ToUpperInvariant(),
                Background = context.Theme.Background2,
                Accent = context.Theme.Accent,
                TitleColor = context.Theme.ForegroundPrimary,
                DetailColor = context.Theme.ForegroundMuted
            });

            var counts = new[]
            {
                CountSubpage(model, EngineeringSubpage.Batteries),
                CountSubpage(model, EngineeringSubpage.Reactors),
                CountSubpage(model, EngineeringSubpage.Hydrogen),
                CountFunctional(model)
            };
            var labels = new[] { "BAT", "RCT", "H2", "LIVE" };
            var fills = new[] { context.Theme.Accent, context.Theme.Warning, context.Theme.Success, context.Theme.AccentSoft };
            var barTop = rect.Y + rect.Height * 0.28f;
            var barHeight = Math.Max(8f, rect.Height * 0.13f);
            var gap = Math.Max(5f, rect.Height * 0.06f);
            for (var i = 0; i < labels.Length; i++)
            {
                DrawTelemetryBar(frame, origin, new UiRect(rect.X + rect.Width * 0.08f, barTop + i * (barHeight + gap), rect.Width * 0.84f, barHeight), context, labels[i], MathHelper.Clamp(counts[i] / 8f, 0f, 1f), fills[i], counts[i].ToString("00"));
            }
        }

        private static void DrawTelemetryBar(MySpriteDrawFrame frame, Vector2 origin, UiRect rect, UiContext context, string label, float ratio, Color fill, string value)
        {
            UiRenderer.DrawLabel(frame, origin, new UiLabel
            {
                Bounds = new UiRect(rect.X, rect.Y, rect.Width * 0.18f, rect.Height),
                Padding = new UiThickness(0f),
                Text = label,
                Font = "Monospace",
                Scale = 0.42f,
                Color = context.Theme.ForegroundPrimary,
                Alignment = TextAlignment.LEFT,
                VerticalAlignment = UiVerticalAlignment.Center
            });
            UiRenderer.DrawLabel(frame, origin, new UiLabel
            {
                Bounds = new UiRect(rect.Right - rect.Width * 0.12f, rect.Y, rect.Width * 0.12f, rect.Height),
                Padding = new UiThickness(0f),
                Text = value,
                Font = "Monospace",
                Scale = 0.38f,
                Color = context.Theme.ForegroundPrimary,
                Alignment = TextAlignment.RIGHT,
                VerticalAlignment = UiVerticalAlignment.Center
            });
            var track = new UiRect(rect.X + rect.Width * 0.22f, rect.Y, rect.Width * 0.6f, rect.Height);
            UiRenderer.DrawPanel(frame, origin, new UiPanel { Bounds = track, Background = context.Theme.Background0 });
            UiRenderer.DrawOutline(frame, origin, track, context.Theme.Border, 1f);
            var fillRect = track.Deflate(new UiThickness(1f));
            fillRect = new UiRect(fillRect.X, fillRect.Y, fillRect.Width * MathHelper.Clamp(ratio, 0f, 1f), fillRect.Height);
            UiRenderer.DrawPanel(frame, origin, new UiPanel { Bounds = fillRect, Background = fill });
        }

        private static void DrawNode(MySpriteDrawFrame frame, Vector2 origin, Vector2 center, Color color)
        {
            var rect = new UiRect(center.X - 4f, center.Y - 4f, 8f, 8f);
            UiRenderer.DrawPanel(frame, origin, new UiPanel { Bounds = rect, Background = color });
            UiRenderer.DrawOutline(frame, origin, rect, color, 1f);
        }

        private static void DrawReadout(MySpriteDrawFrame frame, Vector2 origin, UiRect rect, UiContext context, string caption, string value)
        {
            UiRenderer.DrawPanel(frame, origin, new UiPanel { Bounds = rect, Background = context.Theme.Background0 });
            UiRenderer.DrawOutline(frame, origin, rect, context.Theme.Border, 1f);
            UiRenderer.DrawLabel(frame, origin, new UiLabel { Bounds = new UiRect(rect.X, rect.Y + rect.Height * 0.08f, rect.Width, rect.Height * 0.22f), Padding = new UiThickness(0f), Text = caption, Font = "Monospace", Scale = 0.34f, Color = context.Theme.ForegroundMuted, Alignment = TextAlignment.CENTER, VerticalAlignment = UiVerticalAlignment.Center });
            UiRenderer.DrawLabel(frame, origin, new UiLabel { Bounds = new UiRect(rect.X, rect.Y + rect.Height * 0.34f, rect.Width, rect.Height * 0.34f), Padding = new UiThickness(0f), Text = value, Font = "Monospace", Scale = 0.42f, Color = context.Theme.ForegroundPrimary, Alignment = TextAlignment.CENTER, VerticalAlignment = UiVerticalAlignment.Center });
        }

        private static string FormatNav(string title, bool active, int count)
        {
            return (active ? "> " : "  ") + title + "  " + count.ToString("00");
        }

        private static string FormatItem(EngineeringEntity item, EngineeringEntity selected)
        {
            if (item == null)
            {
                return string.Empty;
            }
            var prefix = selected != null && string.Equals(item.Id, selected.Id, StringComparison.OrdinalIgnoreCase) ? "> " : "  ";
            return prefix + item.Name + " | " + item.StatusText;
        }

        private static List<EngineeringEntity> GetVisibleEntities(EngineeringScreenModel model)
        {
            var list = new List<EngineeringEntity>();
            if (model == null)
            {
                return list;
            }
            for (var i = 0; i < model.Entities.Count; i++)
            {
                var entity = model.Entities[i];
                if (model.ActiveSubpage == EngineeringSubpage.Power || entity.Subpage == model.ActiveSubpage)
                {
                    list.Add(entity);
                }
            }
            return list;
        }

        private static EngineeringEntity FindSelected(EngineeringScreenModel model)
        {
            if (model != null)
            {
                for (var i = 0; i < model.Entities.Count; i++)
                {
                    if (string.Equals(model.Entities[i].Id, model.SelectedId, StringComparison.OrdinalIgnoreCase))
                    {
                        return model.Entities[i];
                    }
                }
            }
            return new EngineeringEntity("eng:none", EngineeringSubpage.Power, "NO SOURCE") { Kind = "NODE", StatusText = "OFFLINE", Summary = "No engineering node selected", Detail = "Awaiting runtime state", Secondary = "No command target", Enabled = false, Functional = false, Metric = 0f };
        }

        private static int CountSubpage(EngineeringScreenModel model, EngineeringSubpage subpage)
        {
            if (model == null)
            {
                return 0;
            }
            var count = 0;
            for (var i = 0; i < model.Entities.Count; i++)
            {
                if (model.Entities[i].Subpage == subpage)
                {
                    count++;
                }
            }
            return count;
        }

        private static int CountFunctional(EngineeringScreenModel model)
        {
            if (model == null)
            {
                return 0;
            }
            var count = 0;
            for (var i = 0; i < model.Entities.Count; i++)
            {
                if (model.Entities[i].Functional)
                {
                    count++;
                }
            }
            return count;
        }

        private static float GetIntegrityRatio(ProviderSnapshot integrity)
        {
            var total = ReadIntAfter(integrity != null ? integrity.Summary : string.Empty, "Total ");
            var functional = ReadIntAfter(integrity != null ? integrity.Summary : string.Empty, "Func ");
            return total > 0 ? MathHelper.Clamp(functional / (float)total, 0f, 1f) : 0f;
        }

        private static float GetDamageRatio(ProviderSnapshot damage, ProviderSnapshot integrity)
        {
            var total = ReadIntAfter(integrity != null ? integrity.Summary : string.Empty, "Total ");
            var damaged = ReadIntAfter(integrity != null ? integrity.Summary : string.Empty, "Damaged ");
            if (damaged <= 0)
            {
                damaged = ReadIntAfter(damage != null ? damage.Summary : string.Empty, string.Empty);
            }
            return total > 0 ? MathHelper.Clamp(damaged / (float)total, 0f, 1f) : 0f;
        }

        private static int ReadIntAfter(string text, string token)
        {
            if (string.IsNullOrEmpty(text))
            {
                return 0;
            }
            var index = string.IsNullOrEmpty(token) ? 0 : text.IndexOf(token, StringComparison.OrdinalIgnoreCase);
            if (index < 0)
            {
                return 0;
            }
            index += token.Length;
            while (index < text.Length && !char.IsDigit(text[index]))
            {
                index++;
            }
            var end = index;
            while (end < text.Length && char.IsDigit(text[end]))
            {
                end++;
            }
            int value;
            return end > index && int.TryParse(text.Substring(index, end - index), out value) ? value : 0;
        }

        private static string GetProviderStateText(ProviderSnapshot snapshot)
        {
            if (snapshot == null)
            {
                return "OFFLINE";
            }
            switch (snapshot.Status)
            {
                case SubsystemStatus.Online: return "ONLINE";
                case SubsystemStatus.Degraded: return "DEGRADED";
                default: return "OFFLINE";
            }
        }

        private static Color GetStatusColor(ProviderSnapshot snapshot, UiContext context)
        {
            if (snapshot == null)
            {
                return context.Theme.Danger;
            }
            switch (snapshot.Status)
            {
                case SubsystemStatus.Online: return context.Theme.Success;
                case SubsystemStatus.Degraded: return context.Theme.Warning;
                default: return context.Theme.Danger;
            }
        }

        private static ProviderSnapshot FindProvider(UiFrame frame, string id)
        {
            for (var i = 0; i < frame.Providers.Count; i++)
            {
                if (string.Equals(frame.Providers[i].Id, id, StringComparison.OrdinalIgnoreCase))
                {
                    return frame.Providers[i];
                }
            }
            return null;
        }

        private static string TrimName(string value, int max)
        {
            if (string.IsNullOrEmpty(value) || value.Length <= max)
            {
                return value ?? string.Empty;
            }
            return value.Substring(0, max - 1) + ".";
        }
    }
}

