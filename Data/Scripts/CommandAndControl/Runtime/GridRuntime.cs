using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Sandbox.ModAPI;
using VRage.Game.Components;
using VRage.Game.ModAPI;
using VRageMath;

namespace AGS
{
    public sealed class GridRuntime
    {
        private static readonly Guid StorageKey = new Guid("2E0F08D0-17CA-44A8-B505-9F174B16E24A");

        private readonly VirtualPointer _pointer;
        private readonly AppRegistry _apps;
        private readonly ProviderRegistry _providers;
        private readonly Dictionary<string, ShellSession> _sessions = new Dictionary<string, ShellSession>();
        private readonly Dictionary<string, StationState> _stations = new Dictionary<string, StationState>();
        private readonly ShipState _shipState;
        private bool _leftWasPressed;
        private bool _storageLoaded;
        private bool _storageDirty;
        private long _loadedFromController;
        private int _tick;

        public GridRuntime(long constructId, SurfaceMap surfaceMap, AppRegistry apps)
        {
            ConstructId = constructId;
            _apps = apps;
            _providers = new ProviderRegistry();
            _shipState = new ShipState(constructId);
            Controllers = new ControllerRegistry();
            _pointer = new VirtualPointer(surfaceMap);
        }

        public long ConstructId { get; private set; }
        public ControllerRegistry Controllers { get; private set; }
        public bool HasControllers { get { return Controllers.Count > 0; } }
        public int ScreenCount { get { return _sessions.Count; } }
        public ShipState ShipState { get { return _shipState; } }

        public void BeginControllerScan() { Controllers.BeginScan(); }
        public void RegisterController(IMyCubeBlock block) { Controllers.Register(block); }
        public void EndControllerScan()
        {
            Controllers.EndScan();
            EnsureStorageLoaded();
        }

        public void RegisterScreen(IMyCubeBlock block, Sandbox.ModAPI.IMyTextSurface surface)
        {
            var key = GetScreenKey(block, surface);
            _pointer.RegisterScreen(block, surface);

            ShellSession session;
            if (!_sessions.TryGetValue(key, out session))
            {
                session = new ShellSession(key, block, surface);
                _sessions[key] = session;
            }
            else
            {
                session.Bind(block, surface);
            }

            var station = GetOrCreateStation(key);
            session.ResetShell(station);
        }

        public void UnregisterScreen(IMyCubeBlock block, Sandbox.ModAPI.IMyTextSurface surface)
        {
            _pointer.UnregisterScreen(block, surface);
            _sessions.Remove(GetScreenKey(block, surface));
        }

        public void Update()
        {
            _tick++;
            EnsureStorageLoaded();
            _providers.Update(ConstructId, Controllers.Primary, _shipState, _tick);
            _pointer.Update();

            var remove = new List<string>();
            foreach (var pair in _sessions)
            {
                if (!pair.Value.IsValid)
                {
                    remove.Add(pair.Key);
                    continue;
                }

                var station = GetOrCreateStation(pair.Key);
                pair.Value.SyncStation(station);
                pair.Value.UpdateBoot(HasControllers);
            }

            for (var i = 0; i < remove.Count; i++)
            {
                var session = _sessions[remove[i]];
                _pointer.UnregisterScreen(session.Block, session.Surface);
                _sessions.Remove(remove[i]);
            }

            UpdateInput();
            UpdatePower();
            SaveStorage();
        }

        public UiFrame GetFrame(IMyCubeBlock block, Sandbox.ModAPI.IMyTextSurface surface)
        {
            ShellSession session;
            if (!_sessions.TryGetValue(GetScreenKey(block, surface), out session))
            {
                return UiFrame.CreateOffline();
            }

            var station = GetOrCreateStation(session.Key);
            var frame = new UiFrame();
            frame.ConstructId = ConstructId;
            frame.HasController = HasControllers;
            frame.ControllerCount = Controllers.Count;
            frame.ControllerName = Controllers.PrimaryName;
            frame.ActiveAppId = session.State.ActiveAppId;
            frame.ActiveAppTitle = _apps.GetTitle(session.State.ActiveAppId);
            frame.AssignedRole = station.AssignedRole;
            frame.StationTitle = StationCatalog.GetTitle(station.AssignedRole);
            frame.StationPurpose = StationCatalog.GetPurpose(station.AssignedRole);
            frame.AccessRole = StationCatalog.GetRoleId(station.AssignedRole);
            frame.BootPhase = session.State.Boot.Phase;
            frame.Pointer = _pointer.GetPointer(block, surface);
            frame.StartMenuOpen = session.State.StartMenuOpen;
            frame.StartMenuPage = session.State.StartMenuPage;
            frame.SettingsOpen = session.State.SettingsOpen;
            frame.IsStandby = session.State.IsStandby;
            frame.AlwaysOn = session.State.AlwaysOn;
            frame.PendingAlwaysOn = session.State.PendingAlwaysOn;
            frame.TimeoutMinutes = session.State.TimeoutMinutes;
            frame.PendingTimeoutMinutes = session.State.PendingTimeoutMinutes;
            frame.Message = HasControllers ? string.Empty : "Command Core required";
            frame.AccentColor = ResolveAccentColor();
            frame.AccentGlow = ResolveAccentGlow(frame.AccentColor);
            CopyShipData(frame, station);
            CopyEngineeringData(frame, station);

            if (frame.BootPhase == BootPhase.Desktop && !frame.IsStandby)
            {
                var context = new AppContext(this, session, station, _shipState);
                var launchable = _apps.GetLaunchableApps(RoleId.Command);
                for (var i = 0; i < launchable.Count; i++)
                {
                    frame.StartItems.Add(new AppEntry(launchable[i].Id, launchable[i].Title));
                }

                var active = _apps.Get(session.State.ActiveAppId);
                if (active != null)
                {
                    if (!PermissionPolicy.Allows(frame.AccessRole, active.RequiredRole))
                    {
                        frame.Lines.Add("Permission denied for " + active.Title);
                    }
                    else
                    {
                        active.Build(context, frame);
                    }
                }
            }

            return frame;
        }

        public void SubmitIntent(IMyCubeBlock block, Sandbox.ModAPI.IMyTextSurface surface, UiIntent intent)
        {
            ShellSession session;
            if (!_sessions.TryGetValue(GetScreenKey(block, surface), out session) || intent == null)
            {
                return;
            }

            var station = GetOrCreateStation(session.Key);

            if (intent.Type == UiIntentType.WakeScreen)
            {
                session.Wake();
                return;
            }

            if (intent.Type == UiIntentType.EnterStandby)
            {
                session.EnterStandby();
                return;
            }

            if (session.State.IsStandby)
            {
                return;
            }

            if (intent.Type == UiIntentType.ToggleStart)
            {
                session.State.StartMenuOpen = !session.State.StartMenuOpen;
                if (!session.State.StartMenuOpen)
                {
                    session.State.SettingsOpen = false;
                }
                return;
            }

            if (intent.Type == UiIntentType.OpenDesktop)
            {
                session.State.ActiveAppId = DesktopApp.IdValue;
                session.State.StartMenuOpen = false;
                session.State.SettingsOpen = false;
                return;
            }

            if (intent.Type == UiIntentType.LaunchApp && _apps.Contains(intent.AppId))
            {
                var app = _apps.Get(intent.AppId);
                if (app == null || !PermissionPolicy.Allows(RoleId.Command, app.RequiredRole))
                {
                    return;
                }

                session.State.ActiveAppId = intent.AppId;
                session.State.StartMenuOpen = false;
                session.State.SettingsOpen = false;

                if (app.StationRole.HasValue)
                {
                    station.AssignedRole = app.StationRole.Value;
                    station.PreferredAppId = app.Id;
                    session.State.ActiveAppId = app.Id;
                    MarkStorageDirty();
                }

                app.Activate(new AppContext(this, session, station, _shipState));
                return;
            }

            if (intent.Type == UiIntentType.SelectSubpage)
            {
                SecuritySubpage subpage;
                if (!Enum.TryParse(intent.Data, true, out subpage))
                {
                    return;
                }

                station.SecuritySubpage = subpage;
                station.SecuritySelectedId = GetDefaultSecuritySelection(_shipState.Security, subpage);
                MarkStorageDirty();
                return;
            }

            if (intent.Type == UiIntentType.SelectEngineeringSubpage)
            {
                EngineeringSubpage engineeringSubpage;
                if (!Enum.TryParse(intent.Data, true, out engineeringSubpage))
                {
                    return;
                }

                station.EngineeringSubpage = engineeringSubpage;
                station.EngineeringSelectedId = GetDefaultEngineeringSelection(_shipState.Engineering, engineeringSubpage);
                MarkStorageDirty();
                return;
            }

            if (intent.Type == UiIntentType.SelectEngineeringNode)
            {
                if (string.IsNullOrEmpty(intent.Data))
                {
                    return;
                }

                station.EngineeringSelectedId = intent.Data;
                MarkStorageDirty();
                return;
            }

            if (intent.Type == UiIntentType.RunEngineeringCommand)
            {
                RunEngineeringCommand(station, intent.Data);
                return;
            }

            if (intent.Type == UiIntentType.SelectNode)
            {
                if (string.IsNullOrEmpty(intent.Data))
                {
                    return;
                }

                station.SecuritySelectedId = intent.Data;
                MarkStorageDirty();
                return;
            }

            if (intent.Type == UiIntentType.SetSecurityScroll)
            {
                station.SecurityScrollOffset = Math.Max(0, intent.Value);
                MarkStorageDirty();
                return;
            }

            if (intent.Type == UiIntentType.CycleShipView)
            {
                station.SecurityShipView = CycleShipView(station.SecurityShipView, intent.Value == 0 ? 1 : intent.Value);
                station.SecurityShipLayer = int.MaxValue;
                MarkStorageDirty();
                return;
            }

            if (intent.Type == UiIntentType.AdjustShipZoom)
            {
                station.SecurityShipZoomStep = ClampZoom(station.SecurityShipZoomStep + intent.Value);
                MarkStorageDirty();
                return;
            }

            if (intent.Type == UiIntentType.AdjustShipLayer)
            {
                station.SecurityShipLayer = ClampShipLayer(station.SecurityShipView, station.SecurityShipLayer, intent.Value);
                MarkStorageDirty();
                return;
            }

            if (intent.Type == UiIntentType.AdjustSidebarWidth)
            {
                station.Layout.SidebarRatio = ClampRatio(station.Layout.SidebarRatio + (intent.Value * 0.01f), 0.16f, 0.34f);
                MarkStorageDirty();
                return;
            }

            if (intent.Type == UiIntentType.AdjustDetailsWidth)
            {
                station.Layout.DetailsRatio = ClampRatio(station.Layout.DetailsRatio + (intent.Value * 0.01f), 0.16f, 0.34f);
                MarkStorageDirty();
                return;
            }

            if (intent.Type == UiIntentType.ToggleSettings)
            {
                session.State.SettingsOpen = !session.State.SettingsOpen;
                if (session.State.SettingsOpen)
                {
                    session.ResetDraftSettings();
                }
                return;
            }

            if (intent.Type == UiIntentType.CloseSettings)
            {
                session.State.SettingsOpen = false;
                session.ResetDraftSettings();
                return;
            }

            if (intent.Type == UiIntentType.PrevStartPage)
            {
                if (session.State.StartMenuPage > 0)
                {
                    session.State.StartMenuPage--;
                }
                return;
            }

            if (intent.Type == UiIntentType.NextStartPage)
            {
                session.State.StartMenuPage++;
                return;
            }

            if (!session.State.SettingsOpen)
            {
                return;
            }

            if (intent.Type == UiIntentType.ToggleAlwaysOn)
            {
                session.State.PendingAlwaysOn = !session.State.PendingAlwaysOn;
                return;
            }

            if (intent.Type == UiIntentType.AdjustTimeout)
            {
                session.State.PendingTimeoutMinutes = ClampTimeout(session.State.PendingTimeoutMinutes + intent.Value);
                return;
            }

            if (intent.Type == UiIntentType.ApplySettings)
            {
                session.ApplyDraftSettings();
                station.AlwaysOn = session.State.AlwaysOn;
                station.TimeoutMinutes = session.State.TimeoutMinutes;
                session.State.SettingsOpen = false;
                MarkStorageDirty();
            }
        }

        public void Clear()
        {
            _pointer.Clear();
            _sessions.Clear();
            _stations.Clear();
            Controllers.Clear();
        }

        private void CopyShipData(UiFrame frame, StationState station)
        {
            for (var i = 0; i < _shipState.Providers.Count; i++)
            {
                frame.Providers.Add(_shipState.Providers[i]);
            }

            for (var i = 0; i < _shipState.Alerts.Count; i++)
            {
                frame.Alerts.Add(_shipState.Alerts[i]);
            }

            var source = _shipState.Security;
            var target = frame.Security;
            target.ActiveSubpage = station.SecuritySubpage;
            target.SelectedId = ResolveSecuritySelection(source, station);
            target.ScrollOffset = station.SecurityScrollOffset;
            target.LockdownActive = source.LockdownActive;
            target.AlertLevel = source.AlertLevel;
            target.View = station.SecurityShipView;
            target.ZoomStep = ClampZoom(station.SecurityShipZoomStep);
            target.Layer = ResolveShipLayer(source.Ship, station.SecurityShipView, station.SecurityShipLayer);
            target.Layout.SidebarRatio = station.Layout.SidebarRatio;
            target.Layout.DetailsRatio = station.Layout.DetailsRatio;

            for (var i = 0; i < source.Entities.Count; i++)
            {
                target.Entities.Add(source.Entities[i]);
            }

            for (var i = 0; i < source.Zones.Count; i++)
            {
                target.Zones.Add(source.Zones[i]);
            }

            target.Ship.CopyFrom(source.Ship);

            for (var i = 0; i < source.Markers.Count; i++)
            {
                target.Markers.Add(source.Markers[i]);
            }
        }

        private void CopyEngineeringData(UiFrame frame, StationState station)
        {
            var source = _shipState.Engineering;
            var target = frame.Engineering;
            target.ActiveSubpage = station.EngineeringSubpage;
            target.SelectedId = ResolveEngineeringSelection(source, station);
            target.ScrollOffset = station.EngineeringScrollOffset;

            for (var i = 0; i < source.Entities.Count; i++)
            {
                target.Entities.Add(source.Entities[i]);
            }
        }

        private StationState GetOrCreateStation(string key)
        {
            StationState station;
            if (!_stations.TryGetValue(key, out station))
            {
                station = new StationState(key);
                _stations[key] = station;
                MarkStorageDirty();
            }

            if (string.IsNullOrEmpty(station.PreferredAppId))
            {
                station.PreferredAppId = StationCatalog.GetDefaultAppId(station.AssignedRole);
            }

            station.TimeoutMinutes = ClampTimeout(station.TimeoutMinutes);
            station.SecurityShipZoomStep = ClampZoom(station.SecurityShipZoomStep);
            station.Layout.SidebarRatio = ClampRatio(station.Layout.SidebarRatio, 0.16f, 0.34f);
            station.Layout.DetailsRatio = ClampRatio(station.Layout.DetailsRatio, 0.16f, 0.34f);
            station.EngineeringScrollOffset = Math.Max(0, station.EngineeringScrollOffset);
            return station;
        }

        private void EnsureStorageLoaded()
        {
            var primary = Controllers.Primary;
            if (primary == null)
            {
                return;
            }

            if (_storageLoaded && _loadedFromController == primary.EntityId)
            {
                return;
            }

            _loadedFromController = primary.EntityId;
            _storageLoaded = true;
            _stations.Clear();

            var storage = primary.Storage;
            string raw;
            if (storage == null || !storage.TryGetValue(StorageKey, out raw) || string.IsNullOrEmpty(raw))
            {
                return;
            }

            DeserializeStations(raw);
        }

        private void SaveStorage()
        {
            if (!_storageDirty)
            {
                return;
            }

            var primary = Controllers.Primary;
            if (primary == null)
            {
                return;
            }

            if (primary.Storage == null)
            {
                return;
            }

            primary.Storage[StorageKey] = SerializeStations();
            _storageDirty = false;
        }

        private string SerializeStations()
        {
            var builder = new StringBuilder();
            foreach (var pair in _stations)
            {
                var station = pair.Value;
                builder.Append(Escape(station.Key)).Append('|')
                    .Append((int)station.AssignedRole).Append('|')
                    .Append(Escape(station.PreferredAppId)).Append('|')
                    .Append(station.AlwaysOn ? '1' : '0').Append('|')
                    .Append(ClampTimeout(station.TimeoutMinutes)).Append('|')
                    .Append((int)station.SecuritySubpage).Append('|')
                    .Append(Escape(station.SecuritySelectedId)).Append('|')
                    .Append(Math.Max(0, station.SecurityScrollOffset)).Append('|')
                    .Append((int)station.SecurityShipView).Append('|')
                    .Append(ClampZoom(station.SecurityShipZoomStep)).Append('|')
                    .Append(station.SecurityShipLayer).Append('|')
                    .Append(station.Layout.SidebarRatio.ToString("0.000", CultureInfo.InvariantCulture)).Append('|')
                    .Append(station.Layout.DetailsRatio.ToString("0.000", CultureInfo.InvariantCulture)).Append('|')
                    .Append((int)station.EngineeringSubpage).Append('|')
                    .Append(Escape(station.EngineeringSelectedId)).Append('|')
                    .Append(Math.Max(0, station.EngineeringScrollOffset))
                    .Append('\n');
            }

            return builder.ToString();
        }

        private void DeserializeStations(string raw)
        {
            var lines = raw.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            for (var i = 0; i < lines.Length; i++)
            {
                var parts = lines[i].Split('|');
                if (parts.Length < 5)
                {
                    continue;
                }

                var key = Unescape(parts[0]);
                if (string.IsNullOrEmpty(key))
                {
                    continue;
                }

                var station = new StationState(key);
                int role;
                if (int.TryParse(parts[1], out role) && Enum.IsDefined(typeof(StationRole), role))
                {
                    station.AssignedRole = (StationRole)role;
                }

                station.PreferredAppId = Unescape(parts[2]);
                station.AlwaysOn = parts[3] == "1";

                int timeout;
                if (int.TryParse(parts[4], out timeout))
                {
                    station.TimeoutMinutes = ClampTimeout(timeout);
                }

                int subpage;
                if (parts.Length > 5 && int.TryParse(parts[5], out subpage) && Enum.IsDefined(typeof(SecuritySubpage), subpage))
                {
                    station.SecuritySubpage = (SecuritySubpage)subpage;
                }

                if (parts.Length > 6)
                {
                    station.SecuritySelectedId = Unescape(parts[6]);
                }

                int scrollOffset;
                if (parts.Length > 7 && int.TryParse(parts[7], out scrollOffset))
                {
                    station.SecurityScrollOffset = Math.Max(0, scrollOffset);
                }

                int shipView;
                if (parts.Length > 8 && int.TryParse(parts[8], out shipView) && Enum.IsDefined(typeof(ShipView), shipView))
                {
                    station.SecurityShipView = (ShipView)shipView;
                }

                int zoomStep;
                if (parts.Length > 9 && int.TryParse(parts[9], out zoomStep))
                {
                    station.SecurityShipZoomStep = ClampZoom(zoomStep);
                }

                int layer;
                if (parts.Length > 10 && int.TryParse(parts[10], out layer))
                {
                    station.SecurityShipLayer = layer;
                }

                float sidebarRatio;
                if (parts.Length > 11 && float.TryParse(parts[11], NumberStyles.Float, CultureInfo.InvariantCulture, out sidebarRatio))
                {
                    station.Layout.SidebarRatio = ClampRatio(sidebarRatio, 0.16f, 0.34f);
                }

                float detailsRatio;
                if (parts.Length > 12 && float.TryParse(parts[12], NumberStyles.Float, CultureInfo.InvariantCulture, out detailsRatio))
                {
                    station.Layout.DetailsRatio = ClampRatio(detailsRatio, 0.16f, 0.34f);
                }

                int engineeringSubpage;
                if (parts.Length > 13 && int.TryParse(parts[13], out engineeringSubpage) && Enum.IsDefined(typeof(EngineeringSubpage), engineeringSubpage))
                {
                    station.EngineeringSubpage = (EngineeringSubpage)engineeringSubpage;
                }

                if (parts.Length > 14)
                {
                    station.EngineeringSelectedId = Unescape(parts[14]);
                }

                int engineeringScrollOffset;
                if (parts.Length > 15 && int.TryParse(parts[15], out engineeringScrollOffset))
                {
                    station.EngineeringScrollOffset = Math.Max(0, engineeringScrollOffset);
                }

                if (string.IsNullOrEmpty(station.PreferredAppId))
                {
                    station.PreferredAppId = StationCatalog.GetDefaultAppId(station.AssignedRole);
                }

                _stations[key] = station;
            }
        }

        private static string Escape(string value)
        {
            return string.IsNullOrEmpty(value) ? string.Empty : value.Replace("%", "%25").Replace("|", "%7C").Replace("\n", "%0A");
        }

        private static string Unescape(string value)
        {
            return string.IsNullOrEmpty(value) ? string.Empty : value.Replace("%0A", "\n").Replace("%7C", "|").Replace("%25", "%");
        }

        private void MarkStorageDirty()
        {
            _storageDirty = true;
        }

        private Color ResolveAccentColor()
        {
            var fallback = new Color(120, 136, 164);
            var ownerId = ResolvePrimaryOwnerId(Controllers.Primary);
            if (ownerId == 0)
            {
                return fallback;
            }

            var session = MyAPIGateway.Session;
            if (session == null || session.Factions == null)
            {
                return fallback;
            }

            var faction = session.Factions.TryGetPlayerFaction(ownerId);
            if (faction == null)
            {
                return fallback;
            }

            return NormalizeFactionColor(faction.IconColor, fallback);
        }

        private Color ResolveAccentGlow(Color accent)
        {
            var ownerId = ResolvePrimaryOwnerId(Controllers.Primary);
            var session = MyAPIGateway.Session;
            if (ownerId == 0 || session == null || session.Factions == null)
            {
                return new Color(accent.R, accent.G, accent.B, 44);
            }

            var faction = session.Factions.TryGetPlayerFaction(ownerId);
            if (faction == null)
            {
                return new Color(accent.R, accent.G, accent.B, 44);
            }

            var glow = NormalizeFactionColor(faction.CustomColor, accent);
            return new Color(glow.R, glow.G, glow.B, 44);
        }

        private static long ResolvePrimaryOwnerId(IMyCubeBlock block)
        {
            if (block == null)
            {
                return 0;
            }

            var slim = block.SlimBlock;
            if (slim != null && slim.BuiltBy != 0)
            {
                return slim.BuiltBy;
            }

            var owners = block.CubeGrid != null ? block.CubeGrid.BigOwners : null;
            if (owners != null && owners.Count > 0)
            {
                return owners[0];
            }

            return 0;
        }

        private static Color NormalizeFactionColor(Vector3 rawColor, Color fallback)
        {
            if (rawColor.LengthSquared() <= 0.0001f)
            {
                return fallback;
            }

            var color = ColorExtensions.HSVtoColor(rawColor);
            if (color.R <= 4 && color.G <= 4 && color.B <= 4)
            {
                return fallback;
            }

            return color;
        }

        private void UpdateInput()
        {
            var input = MyAPIGateway.Input;
            if (input == null)
            {
                _leftWasPressed = false;
                return;
            }

            var leftPressed = !MyAPIGateway.Gui.IsCursorVisible && input.IsLeftMousePressed();
            var clicked = leftPressed && !_leftWasPressed;
            _leftWasPressed = leftPressed;

            foreach (var pair in _sessions)
            {
                var state = _pointer.GetPointer(pair.Value.Block, pair.Value.Surface);
                if (state == null)
                {
                    continue;
                }

                state.IsPressed = state.IsActive && leftPressed;
                if (state.IsActive && clicked)
                {
                    state.WasPressed = true;
                }
            }
        }

        private void UpdatePower()
        {
            foreach (var pair in _sessions)
            {
                var state = _pointer.GetPointer(pair.Value.Block, pair.Value.Surface);
                var activeOnScreen = state != null && (state.IsActive || state.IsPressed || state.WasPressed);
                var wakeInteraction = state != null && state.WasPressed;
                pair.Value.UpdatePower(activeOnScreen, wakeInteraction);
            }
        }

        private void RunEngineeringCommand(StationState station, string commandName)
        {
            if (station == null || string.IsNullOrEmpty(commandName))
            {
                return;
            }

            var selectedId = ResolveEngineeringSelection(_shipState.Engineering, station);
            if (string.IsNullOrEmpty(selectedId))
            {
                return;
            }

            IMyCubeBlock block;
            if (!_shipState.Engineering.BlockLookup.TryGetValue(selectedId, out block) || block == null)
            {
                return;
            }

            var functional = block as Sandbox.ModAPI.IMyFunctionalBlock;
            if (functional == null)
            {
                return;
            }

            var command = commandName.ToLowerInvariant();
            if (command == "enable")
            {
                functional.Enabled = true;
            }
            else if (command == "disable")
            {
                functional.Enabled = false;
            }
            else if (command == "toggle")
            {
                functional.Enabled = !functional.Enabled;
            }
        }

        private static string ResolveEngineeringSelection(EngineeringState source, StationState station)
        {
            if (source == null)
            {
                return string.Empty;
            }

            if (!string.IsNullOrEmpty(station.EngineeringSelectedId) && HasEngineeringEntity(source, station.EngineeringSelectedId))
            {
                return station.EngineeringSelectedId;
            }

            return GetDefaultEngineeringSelection(source, station.EngineeringSubpage);
        }

        private static bool HasEngineeringEntity(EngineeringState source, string id)
        {
            if (source == null || string.IsNullOrEmpty(id))
            {
                return false;
            }

            for (var i = 0; i < source.Entities.Count; i++)
            {
                if (string.Equals(source.Entities[i].Id, id, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        private static string GetDefaultEngineeringSelection(EngineeringState source, EngineeringSubpage subpage)
        {
            if (source == null)
            {
                return string.Empty;
            }

            for (var i = 0; i < source.Entities.Count; i++)
            {
                var entity = source.Entities[i];
                if (subpage == EngineeringSubpage.Power || entity.Subpage == subpage)
                {
                    return entity.Id;
                }
            }

            return source.Entities.Count > 0 ? source.Entities[0].Id : string.Empty;
        }

        private static int ClampTimeout(int minutes)
        {
            if (minutes < 1)
            {
                return 1;
            }

            if (minutes > 60)
            {
                return 60;
            }

            return minutes;
        }

        private static int ClampZoom(int step)
        {
            if (step < -4)
            {
                return -4;
            }

            if (step > 4)
            {
                return 4;
            }

            return step;
        }

        private int ClampShipLayer(ShipView view, int current, int delta)
        {
            var ship = _shipState.Security.Ship;
            if (ship == null || !ship.HasCells)
            {
                return int.MaxValue;
            }

            var min = GetLayerMin(ship, view);
            var max = GetLayerMax(ship, view);
            var resolved = current == int.MaxValue ? max : current;
            var next = resolved + delta;
            if (next < min)
            {
                next = min;
            }
            if (next > max)
            {
                next = max;
            }
            return next;
        }

        private static int ResolveShipLayer(ShipVoxelModel ship, ShipView view, int requested)
        {
            if (ship == null || !ship.HasCells)
            {
                return 0;
            }

            var min = GetLayerMin(ship, view);
            var max = GetLayerMax(ship, view);
            if (requested == int.MaxValue)
            {
                return max;
            }
            if (requested < min)
            {
                return min;
            }
            if (requested > max)
            {
                return max;
            }
            return requested;
        }

        private static int GetLayerMin(ShipVoxelModel ship, ShipView view)
        {
            switch (view)
            {
                case ShipView.Top:
                case ShipView.Bottom:
                    return ship.MinY;
                case ShipView.Left:
                case ShipView.Right:
                    return ship.MinX;
                default:
                    return ship.MinZ;
            }
        }

        private static int GetLayerMax(ShipVoxelModel ship, ShipView view)
        {
            switch (view)
            {
                case ShipView.Top:
                case ShipView.Bottom:
                    return ship.MaxY;
                case ShipView.Left:
                case ShipView.Right:
                    return ship.MaxX;
                default:
                    return ship.MaxZ;
            }
        }

        private static ShipView CycleShipView(ShipView view, int delta)
        {
            var count = 6;
            var next = ((int)view + delta) % count;
            if (next < 0)
            {
                next += count;
            }
            return (ShipView)next;
        }

        private static float ClampRatio(float value, float min, float max)
        {
            if (value < min)
            {
                return min;
            }

            if (value > max)
            {
                return max;
            }

            return value;
        }

        private static string ResolveSecuritySelection(SecurityState source, StationState station)
        {
            if (source == null)
            {
                return station.SecuritySelectedId;
            }

            if (!string.IsNullOrEmpty(station.SecuritySelectedId) && HasSecurityEntity(source, station.SecuritySelectedId))
            {
                return station.SecuritySelectedId;
            }

            return GetDefaultSecuritySelection(source, station.SecuritySubpage);
        }

        private static bool HasSecurityEntity(SecurityState source, string id)
        {
            for (var i = 0; i < source.Entities.Count; i++)
            {
                if (string.Equals(source.Entities[i].Id, id, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        private static string GetDefaultSecuritySelection(SecurityState source, SecuritySubpage subpage)
        {
            if (source == null)
            {
                return "feature:overview";
            }

            string preferred;
            switch (subpage)
            {
                case SecuritySubpage.Access:
                    preferred = "feature:access";
                    break;
                case SecuritySubpage.Airlocks:
                    preferred = "feature:airlocks";
                    break;
                case SecuritySubpage.Turrets:
                    preferred = "feature:turrets";
                    break;
                case SecuritySubpage.Sensors:
                    preferred = "feature:sensors";
                    break;
                case SecuritySubpage.Cameras:
                    preferred = "feature:cameras";
                    break;
                case SecuritySubpage.Zones:
                    preferred = source.Zones.Count > 0 ? source.Zones[0].Id : "feature:overview";
                    break;
                case SecuritySubpage.Alerts:
                    preferred = source.AlertLookup.Count > 0 ? "alert:critical" : "feature:overview";
                    break;
                default:
                    preferred = "feature:overview";
                    break;
            }

            return HasSecurityEntity(source, preferred) ? preferred : source.Entities.Count > 0 ? source.Entities[0].Id : "feature:overview";
        }

        private static string GetScreenKey(IMyCubeBlock block, Sandbox.ModAPI.IMyTextSurface surface)
        {
            if (block == null || surface == null)
            {
                return string.Empty;
            }

            var provider = block as Sandbox.ModAPI.IMyTextSurfaceProvider;
            var index = provider != null ? SurfaceUtil.GetSurfaceIndex(provider, surface) : -1;
            return block.EntityId + ":" + index;
        }
    }
}
