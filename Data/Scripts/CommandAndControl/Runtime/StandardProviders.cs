using System;
using System.Globalization;
using System.Collections.Generic;
using Sandbox.ModAPI;
using VRage.Game.ModAPI;
using VRageMath;

namespace AGS
{
    public sealed class ControllerCoreProvider : ISystemProvider
    {
        public string Id { get { return "core"; } }
        public string Title { get { return "Controller Core"; } }
        public void Update(ShipContext context, ShipState state)
        {
            var summary = context.PrimaryController != null ? context.PrimaryController.BlockDefinition.SubtypeId : "No core";
            ProviderUtil.AddSnapshot(state, Id, Title, context.PrimaryController != null ? SubsystemStatus.Online : SubsystemStatus.Offline, summary);
        }
    }

    public sealed class PowerProvider : ISystemProvider
    {
        public string Id { get { return "power"; } }
        public string Title { get { return "Power"; } }
        public void Update(ShipContext context, ShipState state)
        {
            var engineering = state.Engineering;
            engineering.Entities.Clear();
            engineering.BlockLookup.Clear();

            var batteries = 0;
            var reactors = 0;
            var hydrogenEngines = 0;

            for (var i = 0; i < context.Blocks.Count; i++)
            {
                var block = context.Blocks[i];
                if (block == null)
                {
                    continue;
                }

                var battery = block as IMyBatteryBlock;
                if (battery != null)
                {
                    batteries++;
                    AddEngineeringBlock(engineering, battery, EngineeringSubpage.Batteries, "BATTERY", "Stored charge and output buffer");
                    continue;
                }

                var reactor = block as IMyReactor;
                if (reactor != null)
                {
                    reactors++;
                    AddEngineeringBlock(engineering, reactor, EngineeringSubpage.Reactors, "REACTOR", "Primary reactor bus source");
                    continue;
                }

                if (block.BlockDefinition.SubtypeId.ToString().IndexOf("HydrogenEngine", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    hydrogenEngines++;
                    AddEngineeringBlock(engineering, block, EngineeringSubpage.Hydrogen, "H2 ENGINE", "Hydrogen power feed");
                }
            }

            var total = batteries + reactors + hydrogenEngines;
            engineering.Entities.Add(new EngineeringEntity("eng:conveyors", EngineeringSubpage.Conveyors, "CONVEYOR FABRIC")
            {
                Kind = "LOGISTICS",
                StatusText = ProviderUtil.CountBlocks<IMyShipConnector>(context.Blocks) > 0 ? "ONLINE" : "DEGRADED",
                Summary = "Cargo and connector routing spine",
                Detail = "Logistics grid monitoring",
                Secondary = "Passive readout",
                Enabled = true,
                Functional = true,
                Metric = MathHelper.Clamp(ProviderUtil.CountBlocks<IMyCargoContainer>(context.Blocks) / 8f, 0f, 1f)
            });
            engineering.Entities.Add(new EngineeringEntity("eng:damage", EngineeringSubpage.Damage, "DAMAGE REGISTER")
            {
                Kind = "DAMAGE",
                StatusText = total > 0 ? "WATCH" : "OFFLINE",
                Summary = "Engineering maintenance watchlist",
                Detail = "Provider-backed damage summary",
                Secondary = "Passive readout",
                Enabled = true,
                Functional = total > 0,
                Metric = MathHelper.Clamp((context.Blocks.Count - ProviderUtil.CountFunctional(context.Blocks)) / (float)Math.Max(1, context.Blocks.Count), 0f, 1f)
            });

            var status = total > 0 ? SubsystemStatus.Online : SubsystemStatus.Offline;
            ProviderUtil.AddSnapshot(state, Id, Title, status, "Bat " + batteries + " | React " + reactors + " | H2 " + hydrogenEngines);
        }

        private static void AddEngineeringBlock(EngineeringState engineering, IMyCubeBlock block, EngineeringSubpage subpage, string kind, string detail)
        {
            var id = "eng:" + subpage.ToString().ToLowerInvariant() + ":" + block.EntityId.ToString(CultureInfo.InvariantCulture);
            engineering.BlockLookup[id] = block;

            var functional = block as Sandbox.ModAPI.IMyFunctionalBlock;
            var enabled = functional == null || functional.Enabled;
            var isFunctional = block.IsFunctional;
            engineering.Entities.Add(new EngineeringEntity(id, subpage, GetEngineeringName(block, kind))
            {
                Kind = kind,
                StatusText = !isFunctional ? "OFFLINE" : enabled ? "ONLINE" : "STANDBY",
                Summary = detail,
                Detail = block.BlockDefinition.SubtypeId.ToString(),
                Secondary = enabled ? "Enabled" : "Disabled",
                Enabled = enabled,
                Functional = isFunctional,
                Metric = isFunctional ? (enabled ? 1f : 0.45f) : 0f
            });
        }

        private static string GetEngineeringName(IMyCubeBlock block, string fallback)
        {
            if (block == null)
            {
                return fallback;
            }

            var terminal = block as Sandbox.ModAPI.IMyTerminalBlock;
            if (terminal != null && !string.IsNullOrEmpty(terminal.CustomName))
            {
                return terminal.CustomName.ToUpperInvariant();
            }

            return string.IsNullOrEmpty(fallback) ? block.BlockDefinition.SubtypeId.ToString().ToUpperInvariant() : fallback;
        }
    }

    public sealed class PropulsionProvider : ISystemProvider
    {
        public string Id { get { return "propulsion"; } }
        public string Title { get { return "Propulsion"; } }
        public void Update(ShipContext context, ShipState state)
        {
            var thrusters = ProviderUtil.CountBlocks<IMyThrust>(context.Blocks);
            var gyros = ProviderUtil.CountBlocks<IMyGyro>(context.Blocks);
            var controllers = ProviderUtil.CountBlocks<IMyShipController>(context.Blocks);
            var status = thrusters + gyros > 0 ? SubsystemStatus.Online : SubsystemStatus.Offline;
            ProviderUtil.AddSnapshot(state, Id, Title, status, "Thr " + thrusters + " | Gyr " + gyros + " | Ctrl " + controllers);
        }
    }

    public sealed class NavigationProvider : ISystemProvider
    {
        public string Id { get { return "navigation"; } }
        public string Title { get { return "Navigation"; } }
        public void Update(ShipContext context, ShipState state)
        {
            var remotes = ProviderUtil.CountBlocks<IMyRemoteControl>(context.Blocks);
            var jumps = ProviderUtil.CountBlocks<IMyJumpDrive>(context.Blocks);
            var cockpits = ProviderUtil.CountBlocks<IMyShipController>(context.Blocks);
            var status = remotes + jumps + cockpits > 0 ? SubsystemStatus.Online : SubsystemStatus.Offline;
            ProviderUtil.AddSnapshot(state, Id, Title, status, "Remote " + remotes + " | Jump " + jumps + " | Pilot " + cockpits);
        }
    }

    public sealed class SensorProvider : ISystemProvider
    {
        public string Id { get { return "sensors"; } }
        public string Title { get { return "Sensors"; } }
        public void Update(ShipContext context, ShipState state)
        {
            var sensors = ProviderUtil.CountBlocks<IMySensorBlock>(context.Blocks);
            var cameras = ProviderUtil.CountBlocks<IMyCameraBlock>(context.Blocks);
            var antennae = ProviderUtil.CountBlocks<IMyRadioAntenna>(context.Blocks);
            var beacons = ProviderUtil.CountBlocks<IMyBeacon>(context.Blocks);
            var status = sensors + cameras + antennae + beacons > 0 ? SubsystemStatus.Online : SubsystemStatus.Offline;
            ProviderUtil.AddSnapshot(state, Id, Title, status, "Sens " + sensors + " | Cam " + cameras + " | Link " + (antennae + beacons));
            state.Contacts.Add(new ContactRecord("Contact Registry", "No active track solver yet"));
        }
    }

    public sealed class WeaponProvider : ISystemProvider
    {
        public string Id { get { return "weapons"; } }
        public string Title { get { return "Weapons"; } }
        public void Update(ShipContext context, ShipState state)
        {
            var guns = ProviderUtil.CountBlocks<IMyUserControllableGun>(context.Blocks);
            var turrets = ProviderUtil.CountBlocks<IMyLargeTurretBase>(context.Blocks);
            var warheads = ProviderUtil.CountBlocks<IMyWarhead>(context.Blocks);
            var total = guns + turrets + warheads;
            var status = total > 0 ? SubsystemStatus.Online : SubsystemStatus.Offline;
            ProviderUtil.AddSnapshot(state, Id, Title, status, "Gun " + guns + " | Tur " + turrets + " | WH " + warheads);
        }
    }

    public sealed class DefenseProvider : ISystemProvider
    {
        public string Id { get { return "defense"; } }
        public string Title { get { return "Defense"; } }
        public void Update(ShipContext context, ShipState state)
        {
            var safeZones = ProviderUtil.CountSubtype(context.Blocks, "SafeZone");
            var shieldHooks = ProviderUtil.CountSubtype(context.Blocks, "Shield");
            var status = safeZones + shieldHooks > 0 ? SubsystemStatus.Online : SubsystemStatus.Degraded;
            ProviderUtil.AddSnapshot(state, Id, Title, status, "SafeZone " + safeZones + " | Shield hooks " + shieldHooks);
        }
    }

    public sealed class IntegrityProvider : ISystemProvider
    {
        public string Id { get { return "integrity"; } }
        public string Title { get { return "Hull Integrity"; } }
        public void Update(ShipContext context, ShipState state)
        {
            var total = context.Blocks.Count;
            var functional = ProviderUtil.CountFunctional(context.Blocks);
            var damaged = total - functional;
            var status = damaged == 0 ? SubsystemStatus.Online : functional > 0 ? SubsystemStatus.Degraded : SubsystemStatus.Offline;
            ProviderUtil.AddSnapshot(state, Id, Title, status, "Func " + functional + " / Total " + total + " | Damaged " + damaged);
        }
    }

    public sealed class DamageProvider : ISystemProvider
    {
        public string Id { get { return "damage"; } }
        public string Title { get { return "Damage Control"; } }
        public void Update(ShipContext context, ShipState state)
        {
            var total = context.Blocks.Count;
            var functional = ProviderUtil.CountFunctional(context.Blocks);
            var damaged = total - functional;
            var status = damaged > 0 ? SubsystemStatus.Degraded : SubsystemStatus.Online;
            ProviderUtil.AddSnapshot(state, Id, Title, status, damaged > 0 ? damaged + " blocks need attention" : "No critical damage detected");
        }
    }

    public sealed class SecurityProvider : ISystemProvider
    {
        public string Id { get { return "security"; } }
        public string Title { get { return "Security"; } }

        public void Update(ShipContext context, ShipState state)
        {
            var security = state.Security;
            security.Entities.Clear();
            security.Zones.Clear();
            security.Ship.Clear();
            security.Markers.Clear();
            security.GroupLookup.Clear();
            security.AlertLookup.Clear();
            security.LockdownActive = false;

            var transform = CreateTransform(context);
            var access = BuildFeature(context.Blocks, transform, SecurityFeatureKind.Access, "Access", "Hatches, doors, and security barriers", IsAccessBlock);
            var airlocks = BuildFeature(context.Blocks, transform, SecurityFeatureKind.Airlocks, "Airlocks", "Vent and pressurization control nodes", delegate(IMyCubeBlock block) { return block is SpaceEngineers.Game.ModAPI.IMyAirVent; });
            var turrets = BuildFeature(context.Blocks, transform, SecurityFeatureKind.Turrets, "Turrets", "Defensive turret coverage", delegate(IMyCubeBlock block) { return block is IMyLargeTurretBase; });
            var sensors = BuildFeature(context.Blocks, transform, SecurityFeatureKind.Sensors, "Sensors", "Motion and trigger detection", delegate(IMyCubeBlock block) { return block is IMySensorBlock; });
            var cameras = BuildFeature(context.Blocks, transform, SecurityFeatureKind.Cameras, "Cameras", "Surveillance and targeting optics", delegate(IMyCubeBlock block) { return block is IMyCameraBlock; });
            var alarms = BuildFeature(context.Blocks, transform, SecurityFeatureKind.Alarms, "Alarms", "Alarm lights and security timers", IsAlarmBlock);

            var groups = new List<SecurityFeatureGroup> { access, airlocks, turrets, sensors, cameras, alarms };
            var totalTracked = 0;
            var totalFunctional = 0;
            for (var i = 0; i < groups.Count; i++)
            {
                var group = groups[i];
                totalTracked += group.Blocks.Count;
                totalFunctional += group.FunctionalCount;
                AddFeatureState(security, group);
            }

            security.Zones.Add(new SecurityZone("zone:construct", "Construct Envelope")
            {
                EntityCount = totalTracked,
                ActiveCount = totalFunctional,
                LockedDown = false,
                Summary = totalTracked > 0 ? totalTracked + " tracked security nodes" : "No security systems detected"
            });

            security.Entities.Insert(0, new SecurityEntity("feature:overview", SecurityEntityKind.Feature, "Construct Outline")
            {
                FeatureKind = SecurityFeatureKind.Overview,
                StatusText = totalTracked > 0 ? "TRACKED" : "OFFLINE",
                Summary = security.Zones[0].Summary,
                Detail = "Multi-view hull outline with layer slice control and major security markers",
                Count = totalTracked,
                ActiveCount = totalFunctional,
                Enabled = totalTracked > 0,
                Functional = totalFunctional > 0,
                Metric = totalTracked > 0 ? (float)totalFunctional / totalTracked : 0f
            });

            BuildShipVoxels(context, security, transform);

            var status = totalTracked > 0 ? SubsystemStatus.Online : SubsystemStatus.Degraded;
            ProviderUtil.AddSnapshot(state, Id, Title, status, "Access " + access.Blocks.Count + " | Turrets " + turrets.Blocks.Count + " | Sensors " + sensors.Blocks.Count);
        }

        private sealed class SecurityFeatureGroup
        {
            public SecurityFeatureGroup(SecurityFeatureKind kind, string name, string detail)
            {
                Kind = kind;
                Name = name;
                Detail = detail;
                Blocks = new List<IMyCubeBlock>();
                Points = new List<Vector3I>();
            }

            public SecurityFeatureKind Kind;
            public string Name;
            public string Detail;
            public readonly List<IMyCubeBlock> Blocks;
            public readonly List<Vector3I> Points;
            public int FunctionalCount;
        }

        private sealed class OutlineTransform
        {
            public MatrixD WorldToLocal;
            public double GridSize;
        }

        private static OutlineTransform CreateTransform(ShipContext context)
        {
            var primary = context.PrimaryController != null && context.PrimaryController.CubeGrid != null ? context.PrimaryController.CubeGrid : null;
            var matrix = primary != null ? MatrixD.Invert(primary.WorldMatrix) : MatrixD.Identity;
            var gridSize = primary != null ? primary.GridSize : 2.5;
            return new OutlineTransform { WorldToLocal = matrix, GridSize = gridSize };
        }

        private static SecurityFeatureGroup BuildFeature(List<IMyCubeBlock> blocks, OutlineTransform transform, SecurityFeatureKind kind, string name, string detail, Func<IMyCubeBlock, bool> predicate)
        {
            var group = new SecurityFeatureGroup(kind, name, detail);
            for (var i = 0; i < blocks.Count; i++)
            {
                var block = blocks[i];
                if (block == null || !predicate(block))
                {
                    continue;
                }

                group.Blocks.Add(block);
                if (block.IsFunctional)
                {
                    group.FunctionalCount++;
                }

                group.Points.Add(ProjectBlockCenter(block, transform));
            }

            return group;
        }

        private static bool IsAccessBlock(IMyCubeBlock block)
        {
            return block is IMyDoor;
        }

        private static bool IsAlarmBlock(IMyCubeBlock block)
        {
            if (block is IMyLightingBlock)
            {
                return true;
            }

            return block is SpaceEngineers.Game.ModAPI.IMyTimerBlock;
        }

        private static void AddFeatureState(SecurityState security, SecurityFeatureGroup group)
        {
            if (group.Blocks.Count == 0)
            {
                return;
            }

            var featureId = "feature:" + group.Kind.ToString().ToLowerInvariant();
            security.GroupLookup[featureId] = group.Blocks;
            security.Entities.Add(new SecurityEntity(featureId, SecurityEntityKind.Feature, group.Name)
            {
                FeatureKind = group.Kind,
                StatusText = group.FunctionalCount == group.Blocks.Count ? "ONLINE" : group.FunctionalCount > 0 ? "DEGRADED" : "OFFLINE",
                Summary = group.Blocks.Count + " nodes tracked",
                Detail = group.Detail,
                Count = group.Blocks.Count,
                ActiveCount = group.FunctionalCount,
                Enabled = group.FunctionalCount > 0,
                Functional = group.FunctionalCount > 0,
                Metric = group.Blocks.Count > 0 ? (float)group.FunctionalCount / group.Blocks.Count : 0f
            });

            if (group.Points.Count == 0)
            {
                return;
            }

            var sumX = 0f;
            var sumY = 0f;
            var sumZ = 0f;
            for (var i = 0; i < group.Points.Count; i++)
            {
                sumX += group.Points[i].X;
                sumY += group.Points[i].Y;
                sumZ += group.Points[i].Z;
            }

            security.Markers.Add(new SecurityFeatureMarker
            {
                Id = featureId,
                Label = group.Name,
                Kind = group.Kind,
                X = sumX / group.Points.Count,
                Y = sumY / group.Points.Count,
                Z = sumZ / group.Points.Count
            });
        }

        private static void BuildShipVoxels(ShipContext context, SecurityState security, OutlineTransform transform)
        {
            security.Ship.CopyFrom(ShipGeometryBuilder.Build(context.Grids, transform.WorldToLocal, transform.GridSize));
        }

        private static Vector3I ProjectBlockCenter(IMyCubeBlock block, OutlineTransform transform)
        {
            var world = block.GetPosition();
            var local = Vector3D.Transform(world, transform.WorldToLocal);
            var scale = transform.GridSize <= 0.001 ? 2.5 : transform.GridSize;
            return new Vector3I((int)Math.Round(local.X / scale), (int)Math.Round(local.Y / scale), (int)Math.Round(local.Z / scale));
        }


    }

    public sealed class InventoryLogisticsProvider : ISystemProvider
    {
        public string Id { get { return "logistics"; } }
        public string Title { get { return "Logistics"; } }
        public void Update(ShipContext context, ShipState state)
        {
            var cargo = ProviderUtil.CountBlocks<IMyCargoContainer>(context.Blocks);
            var connectors = ProviderUtil.CountBlocks<IMyShipConnector>(context.Blocks);
            var assemblers = ProviderUtil.CountBlocks<IMyAssembler>(context.Blocks);
            var refineries = ProviderUtil.CountBlocks<IMyRefinery>(context.Blocks);
            var status = cargo + connectors + assemblers + refineries > 0 ? SubsystemStatus.Online : SubsystemStatus.Degraded;
            ProviderUtil.AddSnapshot(state, Id, Title, status, "Cargo " + cargo + " | Conn " + connectors + " | Prod " + (assemblers + refineries));
        }
    }

    public sealed class AlertProvider : ISystemProvider
    {
        public string Id { get { return "alerts"; } }
        public string Title { get { return "Alerts"; } }
        public void Update(ShipContext context, ShipState state)
        {
            var critical = 0;
            var degraded = 0;
            for (var i = 0; i < state.Providers.Count; i++)
            {
                var provider = state.Providers[i];
                if (provider.Status == SubsystemStatus.Offline)
                {
                    critical++;
                }
                else if (provider.Status == SubsystemStatus.Degraded)
                {
                    degraded++;
                }
            }

            if (critical > 0)
            {
                state.Alerts.Add(new AlertRecord("critical", critical + " critical subsystem failures", AlertSeverity.Critical));
            }
            else if (degraded > 0)
            {
                state.Alerts.Add(new AlertRecord("degraded", degraded + " degraded subsystems", AlertSeverity.Warning));
            }
            else
            {
                state.Alerts.Add(new AlertRecord("nominal", "Ship systems nominal", AlertSeverity.Info));
            }

            state.Security.AlertLookup.Clear();
            for (var i = 0; i < state.Alerts.Count; i++)
            {
                var alert = state.Alerts[i];
                state.Security.AlertLookup[alert.Id] = alert;
                state.Security.Entities.Add(new SecurityEntity("alert:" + alert.Id, SecurityEntityKind.Alert, alert.Severity.ToString() + " Alert")
                {
                    FeatureKind = SecurityFeatureKind.Overview,
                    StatusText = alert.Severity.ToString().ToUpperInvariant(),
                    Summary = alert.Message,
                    Detail = "Runtime synthesized security posture event",
                    Count = 1,
                    ActiveCount = alert.Severity == AlertSeverity.Info ? 0 : 1,
                    Enabled = true,
                    Functional = alert.Severity != AlertSeverity.Critical,
                    Metric = alert.Severity == AlertSeverity.Critical ? 1f : alert.Severity == AlertSeverity.Warning ? 0.55f : 0.2f
                });
            }

            state.Security.AlertLevel = MathHelper.Clamp((critical * 35) + (degraded * 15), 0, 100);
            state.Security.LockdownActive = critical > 0;
            ProviderUtil.AddSnapshot(state, Id, Title, critical > 0 ? SubsystemStatus.Degraded : SubsystemStatus.Online, state.Alerts[0].Message);
        }
    }
}
