using System.Collections.Generic;
using VRage.Game.ModAPI;

namespace AGS
{
    public enum SecuritySubpage
    {
        Overview,
        Access,
        Airlocks,
        Turrets,
        Sensors,
        Cameras,
        Zones,
        Alerts
    }

    public enum SecurityEntityKind
    {
        Feature,
        Zone,
        Alert
    }

    public enum SecurityFeatureKind
    {
        Overview,
        Access,
        Airlocks,
        Turrets,
        Sensors,
        Cameras,
        Alarms
    }

    public enum SecurityCommandType
    {
        None,
        Open,
        Close,
        Enable,
        Disable,
        ToggleEnabled,
        ToggleLockdown,
        SetAlertLevel,
        AcknowledgeAlert,
        ZoneLockdown,
        ZoneRelease
    }

    public enum ShipView
    {
        // Orthographic 2D views (look straight down an axis).
        Top,
        Bottom,
        Left,
        Right,
        Front,
        Back,
        // Angled axonometric ("perspective") views.
        Iso,
        IsoRear
    }

    public enum ShipShapeId
    {
        Cube,
        Slope,
        Corner,
        InvertedCorner,
        Slope2x1,
        Slope2x1Tip
    }

    // Recognised specialty (non-armour) systems, drawn as icons over the hull wireframe.
    public enum ShipDeviceCategory
    {
        Thruster,
        Reactor,
        Battery,
        Tank,
        Controller,
        Weapon,
        Cargo,
        Gyro,
        Conveyor,
        Connector,
        Antenna,
        Medical,
        Production,
        Other
    }

    // A specialty block to mark on the map, positioned at its centre cell in model space.
    public sealed class ShipDeviceMarker
    {
        public ShipDeviceMarker(ShipDeviceCategory category, float x, float y, float z)
        {
            Category = category;
            X = x;
            Y = y;
            Z = z;
        }

        public ShipDeviceCategory Category { get; private set; }
        public float X { get; private set; }
        public float Y { get; private set; }
        public float Z { get; private set; }
    }

    public sealed class ShipCell
    {
        public ShipCell(int x, int y, int z, int ownerIndex)
        {
            X = x;
            Y = y;
            Z = z;
            OwnerIndex = ownerIndex;
        }

        public int X { get; private set; }
        public int Y { get; private set; }
        public int Z { get; private set; }
        public int OwnerIndex { get; private set; }
    }

    public sealed class ShipBlockGeometry
    {
        public ShipBlockGeometry(int index, string subtypeName, ShipShapeId shapeId, int minX, int minY, int minZ, int maxX, int maxY, int maxZ, int forward, int up, int right)
        {
            Index = index;
            SubtypeName = subtypeName ?? string.Empty;
            ShapeId = shapeId;
            MinX = minX;
            MinY = minY;
            MinZ = minZ;
            MaxX = maxX;
            MaxY = maxY;
            MaxZ = maxZ;
            Forward = forward;
            Up = up;
            Right = right;
            Cells = new List<ShipCell>();
        }

        public int Index { get; private set; }
        public string SubtypeName { get; private set; }
        public ShipShapeId ShapeId { get; private set; }
        public int MinX { get; private set; }
        public int MinY { get; private set; }
        public int MinZ { get; private set; }
        public int MaxX { get; private set; }
        public int MaxY { get; private set; }
        public int MaxZ { get; private set; }
        public int Forward { get; private set; }
        public int Up { get; private set; }
        public int Right { get; private set; }
        public List<ShipCell> Cells { get; private set; }

        public float CenterX { get { return (MinX + MaxX) * 0.5f; } }
        public float CenterY { get { return (MinY + MaxY) * 0.5f; } }
        public float CenterZ { get { return (MinZ + MaxZ) * 0.5f; } }

        public void AddCell(ShipCell cell)
        {
            if (cell != null)
            {
                Cells.Add(cell);
            }
        }
    }

    public sealed class ShipVoxelModel
    {
        public ShipVoxelModel()
        {
            Blocks = new List<ShipBlockGeometry>();
            Cells = new List<ShipCell>();
            Devices = new List<ShipDeviceMarker>();
            ResetBounds();
        }

        public List<ShipBlockGeometry> Blocks { get; private set; }
        public List<ShipCell> Cells { get; private set; }
        public List<ShipDeviceMarker> Devices { get; private set; }
        // Bumped whenever the geometry is replaced, so renderers can cache derived data
        // (e.g. the extracted wireframe) and rebuild it only when the hull changes.
        public int Version { get; private set; }
        public int MinX { get; private set; }
        public int MinY { get; private set; }
        public int MinZ { get; private set; }
        public int MaxX { get; private set; }
        public int MaxY { get; private set; }
        public int MaxZ { get; private set; }

        public bool HasCells
        {
            get { return Cells.Count > 0; }
        }

        public bool HasBlocks
        {
            get { return Blocks.Count > 0; }
        }

        public void Clear()
        {
            Blocks.Clear();
            Cells.Clear();
            Devices.Clear();
            ResetBounds();
            Version++;
        }

        public void AddBlock(ShipBlockGeometry block)
        {
            if (block != null)
            {
                Blocks.Add(block);
            }
        }

        public void AddDevice(ShipDeviceMarker device)
        {
            if (device != null)
            {
                Devices.Add(device);
            }
        }

        public ShipCell AddCell(int x, int y, int z, int ownerIndex)
        {
            var cell = new ShipCell(x, y, z, ownerIndex);
            Cells.Add(cell);
            if (x < MinX) MinX = x;
            if (y < MinY) MinY = y;
            if (z < MinZ) MinZ = z;
            if (x > MaxX) MaxX = x;
            if (y > MaxY) MaxY = y;
            if (z > MaxZ) MaxZ = z;
            return cell;
        }

        public void CopyFrom(ShipVoxelModel source)
        {
            Clear();
            if (source == null)
            {
                return;
            }

            for (var i = 0; i < source.Blocks.Count; i++)
            {
                var sourceBlock = source.Blocks[i];
                var block = new ShipBlockGeometry(sourceBlock.Index, sourceBlock.SubtypeName, sourceBlock.ShapeId, sourceBlock.MinX, sourceBlock.MinY, sourceBlock.MinZ, sourceBlock.MaxX, sourceBlock.MaxY, sourceBlock.MaxZ, sourceBlock.Forward, sourceBlock.Up, sourceBlock.Right);
                AddBlock(block);
            }

            for (var i = 0; i < source.Cells.Count; i++)
            {
                var sourceCell = source.Cells[i];
                var cell = AddCell(sourceCell.X, sourceCell.Y, sourceCell.Z, sourceCell.OwnerIndex);
                if (cell.OwnerIndex >= 0 && cell.OwnerIndex < Blocks.Count)
                {
                    Blocks[cell.OwnerIndex].AddCell(cell);
                }
            }

            for (var i = 0; i < source.Devices.Count; i++)
            {
                var device = source.Devices[i];
                AddDevice(new ShipDeviceMarker(device.Category, device.X, device.Y, device.Z));
            }
        }

        private void ResetBounds()
        {
            MinX = int.MaxValue;
            MinY = int.MaxValue;
            MinZ = int.MaxValue;
            MaxX = int.MinValue;
            MaxY = int.MinValue;
            MaxZ = int.MinValue;
        }
    }

    public sealed class SecurityFeatureMarker
    {
        public string Id { get; set; }
        public string Label { get; set; }
        public SecurityFeatureKind Kind { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
    }

    public sealed class SecurityEntity
    {
        public SecurityEntity(string id, SecurityEntityKind kind, string name)
        {
            Id = id ?? string.Empty;
            Kind = kind;
            Name = name ?? string.Empty;
            StatusText = string.Empty;
            Summary = string.Empty;
            Detail = string.Empty;
        }

        public string Id { get; set; }
        public SecurityEntityKind Kind { get; set; }
        public SecurityFeatureKind FeatureKind { get; set; }
        public string Name { get; set; }
        public string StatusText { get; set; }
        public string Summary { get; set; }
        public string Detail { get; set; }
        public int Count { get; set; }
        public int ActiveCount { get; set; }
        public bool Enabled { get; set; }
        public bool Functional { get; set; }
        public float Metric { get; set; }
    }

    public sealed class SecurityZone
    {
        public SecurityZone(string id, string name)
        {
            Id = id ?? string.Empty;
            Name = name ?? string.Empty;
            Summary = string.Empty;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public int EntityCount { get; set; }
        public int ActiveCount { get; set; }
        public bool LockedDown { get; set; }
        public string Summary { get; set; }
    }

    public sealed class ScreenLayoutState
    {
        public ScreenLayoutState()
        {
            SidebarRatio = 0.24f;
            DetailsRatio = 0.24f;
        }

        public float SidebarRatio { get; set; }
        public float DetailsRatio { get; set; }
    }

    public sealed class SecurityScreenModel
    {
        public SecurityScreenModel()
        {
            Entities = new List<SecurityEntity>();
            Zones = new List<SecurityZone>();
            Markers = new List<SecurityFeatureMarker>();
            Layout = new ScreenLayoutState();
            Ship = new ShipVoxelModel();
            SelectedId = string.Empty;
            View = ShipView.Top;
            ZoomStep = 0;
            Layer = int.MaxValue;
        }

        public SecuritySubpage ActiveSubpage { get; set; }
        public string SelectedId { get; set; }
        public int ScrollOffset { get; set; }
        public bool LockdownActive { get; set; }
        public int AlertLevel { get; set; }
        public ShipView View { get; set; }
        public int ZoomStep { get; set; }
        public int Layer { get; set; }
        public ScreenLayoutState Layout { get; set; }
        public List<SecurityEntity> Entities { get; private set; }
        public List<SecurityZone> Zones { get; private set; }
        public ShipVoxelModel Ship { get; private set; }
        public List<SecurityFeatureMarker> Markers { get; private set; }
    }

    public sealed class SecurityState
    {
        public SecurityState()
        {
            Entities = new List<SecurityEntity>();
            Zones = new List<SecurityZone>();
            Markers = new List<SecurityFeatureMarker>();
            GroupLookup = new Dictionary<string, List<IMyCubeBlock>>();
            AlertLookup = new Dictionary<string, AlertRecord>();
            Ship = new ShipVoxelModel();
            AlertLevel = 35;
        }

        public List<SecurityEntity> Entities { get; private set; }
        public List<SecurityZone> Zones { get; private set; }
        public ShipVoxelModel Ship { get; private set; }
        public List<SecurityFeatureMarker> Markers { get; private set; }
        public Dictionary<string, List<IMyCubeBlock>> GroupLookup { get; private set; }
        public Dictionary<string, AlertRecord> AlertLookup { get; private set; }
        public bool LockdownActive { get; set; }
        public int AlertLevel { get; set; }
    }
}
