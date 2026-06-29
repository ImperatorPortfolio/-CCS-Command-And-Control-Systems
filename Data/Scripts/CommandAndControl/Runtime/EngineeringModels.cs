using System.Collections.Generic;
using VRage.Game.ModAPI;

namespace AGS
{
    public enum EngineeringSubpage
    {
        Power,
        Hydrogen,
        Batteries,
        Reactors,
        Conveyors,
        Damage
    }

    public enum EngineeringCommandType
    {
        None,
        Enable,
        Disable,
        Toggle
    }

    public sealed class EngineeringEntity
    {
        public EngineeringEntity(string id, EngineeringSubpage subpage, string name)
        {
            Id = id ?? string.Empty;
            Subpage = subpage;
            Name = name ?? string.Empty;
            StatusText = string.Empty;
            Summary = string.Empty;
            Detail = string.Empty;
            Secondary = string.Empty;
        }

        public string Id { get; set; }
        public EngineeringSubpage Subpage { get; set; }
        public string Name { get; set; }
        public string StatusText { get; set; }
        public string Summary { get; set; }
        public string Detail { get; set; }
        public string Secondary { get; set; }
        public bool Enabled { get; set; }
        public bool Functional { get; set; }
        public float Metric { get; set; }
        public string Kind { get; set; }
    }

    public sealed class EngineeringScreenModel
    {
        public EngineeringScreenModel()
        {
            Entities = new List<EngineeringEntity>();
            ActiveSubpage = EngineeringSubpage.Power;
            SelectedId = string.Empty;
        }

        public EngineeringSubpage ActiveSubpage { get; set; }
        public string SelectedId { get; set; }
        public int ScrollOffset { get; set; }
        public List<EngineeringEntity> Entities { get; private set; }
    }

    public sealed class EngineeringState
    {
        public EngineeringState()
        {
            Entities = new List<EngineeringEntity>();
            BlockLookup = new Dictionary<string, IMyCubeBlock>();
        }

        public List<EngineeringEntity> Entities { get; private set; }
        public Dictionary<string, IMyCubeBlock> BlockLookup { get; private set; }
    }
}
