using System;
using System.Collections.Generic;
using VRage.Game.GUI.TextPanel;
using VRageMath;

namespace AGS
{
    public static class SecurityView
    {
        private const string ViewPath = "Data\\UI\\Views\\Security.xml";

        public static UiElement Build(UiContext context)
        {
            var bindings = CreateBindings(context);
            var visuals = new Dictionary<string, Action<MySpriteDrawFrame, Vector2, UiRect, UiContext>>
            {
                { "OutlineMap", RenderOutlineMap },
                { "AccessGate", RenderOutlineMap }
            };

            return UiXmlLoader.Build(ViewPath, context, bindings, visuals);
        }

        private static UiXmlBindings CreateBindings(UiContext context)
        {
            var bindings = new UiXmlBindings();
            var frame = context.Frame;
            var model = frame.Security;
            var selected = FindSelected(model);
            var overview = FindEntity(model, "feature:overview");
            var access = FindEntity(model, "feature:access");
            var airlocks = FindEntity(model, "feature:airlocks");
            var turrets = FindEntity(model, "feature:turrets");
            var sensors = FindEntity(model, "feature:sensors");
            var cameras = FindEntity(model, "feature:cameras");
            var layerCount = GetLayerCount(model);
            var layerMin = GetLayerMin(model);
            var layerIndex = layerCount > 0 ? (model.Layer - layerMin) + 1 : 0;

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

            bindings.Set("title", "SECURITY STATION");
            bindings.Set("detail", frame.ControllerName ?? "COMMAND CORE ONLINE");

            bindings.Set("nav1", FormatNavLabel("OVERVIEW", overview));
            bindings.Set("nav2", FormatNavLabel("ACCESS", access));
            bindings.Set("nav3", FormatNavLabel("AIRLOCKS", airlocks));
            bindings.Set("nav4", FormatNavLabel("TURRETS", turrets));
            bindings.Set("nav5", FormatNavLabel("SENSORS", sensors));
            bindings.Set("nav6", FormatNavLabel("CAMERAS", cameras));

            bindings.Set("bodyTitle", "CONSTRUCT OUTLINE");
            bindings.Set("bodyDetail", ShipMapRenderer.GetViewLabel(model.View) + " | layer " + layerIndex.ToString("00") + "/" + layerCount.ToString("00") + " | zoom " + FormatZoom(model.ZoomStep));

            bindings.Set("detailsTitle", selected.Name);
            bindings.Set("detailsState", selected.StatusText);
            bindings.Set("detailsSummary", selected.Summary);
            bindings.Set("detailsLine1", selected.Detail);
            bindings.Set("detailsLine2", "Nodes " + selected.Count.ToString("00") + " | Active " + selected.ActiveCount.ToString("00"));
            bindings.Set("detailsLine3", "View " + ShipMapRenderer.GetViewLabel(model.View) + " | Slice " + layerIndex.ToString("00") + "/" + layerCount.ToString("00"));

            bindings.Set("metric1Label", "HEALTH");
            bindings.Set("metric1Value", ((int)Math.Round(MathHelper.Clamp(selected.Metric, 0f, 1f) * 100f)).ToString("00") + "%");
            bindings.Set("metric1Ratio", MathHelper.Clamp(selected.Metric, 0f, 1f));

            bindings.Set("metric2Label", "ALERT");
            bindings.Set("metric2Value", model.AlertLevel.ToString("00") + "%");
            bindings.Set("metric2Ratio", MathHelper.Clamp(model.AlertLevel / 100f, 0f, 1f));

            bindings.Set("metric3Label", "COVERAGE");
            bindings.Set("metric3Value", GetCoverageValue(model, selected));
            bindings.Set("metric3Ratio", GetCoverageRatio(model, selected));

            bindings.Set("viewLabel", ShipMapRenderer.GetViewLabel(model.View));
            bindings.Set("layerLabel", layerIndex.ToString("00") + "/" + layerCount.ToString("00"));
            bindings.Set("zoomLabel", FormatZoom(model.ZoomStep));
            bindings.Set("action1", model.LockdownActive ? "RELEASE LOCKDOWN" : "INITIATE LOCKDOWN");
            bindings.Set("action2", "ACKNOWLEDGE ALERTS");
            bindings.Set("action3", "OPEN DETAILS");

            return bindings;
        }

        private static string FormatNavLabel(string title, SecurityEntity entity)
        {
            if (entity == null)
            {
                return title + "  00";
            }

            return title + "  " + entity.Count.ToString("00");
        }

        private static string FormatZoom(int zoomStep)
        {
            return zoomStep > 0 ? "+" + zoomStep : zoomStep.ToString();
        }

        private static string GetCoverageValue(SecurityScreenModel model, SecurityEntity selected)
        {
            return ((int)Math.Round(GetCoverageRatio(model, selected) * 100f)).ToString("00") + "%";
        }

        private static float GetCoverageRatio(SecurityScreenModel model, SecurityEntity selected)
        {
            var overview = FindEntity(model, "feature:overview");
            if (overview == null || overview.Count <= 0 || selected == null)
            {
                return 0f;
            }

            return MathHelper.Clamp(selected.Count / (float)overview.Count, 0f, 1f);
        }

        private static SecurityEntity FindEntity(SecurityScreenModel model, string id)
        {
            if (model == null || string.IsNullOrEmpty(id))
            {
                return null;
            }

            for (var i = 0; i < model.Entities.Count; i++)
            {
                if (string.Equals(model.Entities[i].Id, id, StringComparison.OrdinalIgnoreCase))
                {
                    return model.Entities[i];
                }
            }

            return null;
        }

        private static SecurityEntity FindSelected(SecurityScreenModel model)
        {
            var selected = FindEntity(model, model != null ? model.SelectedId : string.Empty);
            if (selected != null)
            {
                return selected;
            }

            selected = FindEntity(model, "feature:overview");
            if (selected != null)
            {
                return selected;
            }

            return new SecurityEntity("feature:overview", SecurityEntityKind.Feature, "Construct Outline")
            {
                StatusText = "OFFLINE",
                Summary = "No security data available",
                Detail = "Provider data unavailable"
            };
        }

        private static void RenderOutlineMap(MySpriteDrawFrame frame, Vector2 origin, UiRect rect, UiContext context)
        {
            ShipMapRenderer.Render(frame, origin, rect, context, FindSelected(context.Frame.Security));
        }

        private static int GetLayerCount(SecurityScreenModel model)
        {
            if (model == null || model.Ship == null || !model.Ship.HasCells)
            {
                return 0;
            }

            return GetLayerMax(model) - GetLayerMin(model) + 1;
        }

        private static int GetLayerMin(SecurityScreenModel model)
        {
            switch (model.View)
            {
                case ShipView.Top:
                case ShipView.Bottom:
                    return model.Ship.MinY;
                case ShipView.Left:
                case ShipView.Right:
                    return model.Ship.MinX;
                default:
                    return model.Ship.MinZ;
            }
        }

        private static int GetLayerMax(SecurityScreenModel model)
        {
            switch (model.View)
            {
                case ShipView.Top:
                case ShipView.Bottom:
                    return model.Ship.MaxY;
                case ShipView.Left:
                case ShipView.Right:
                    return model.Ship.MaxX;
                default:
                    return model.Ship.MaxZ;
            }
        }
    }
}
