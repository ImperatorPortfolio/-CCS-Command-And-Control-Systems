using System.Collections.Generic;
using VRage.Game.ModAPI;

namespace AGS
{
    public sealed class ControllerRegistry
    {
        private readonly Dictionary<long, IMyCubeBlock> _controllers = new Dictionary<long, IMyCubeBlock>();
        private readonly HashSet<long> _seen = new HashSet<long>();

        public int Count { get { return _controllers.Count; } }
        public IMyCubeBlock Primary { get; private set; }
        public string PrimaryName
        {
            get
            {
                return Primary != null ? Primary.BlockDefinition.SubtypeId : "Offline";
            }
        }

        public void BeginScan()
        {
            _seen.Clear();
        }

        public void Register(IMyCubeBlock block)
        {
            if (block == null || block.MarkedForClose || block.Closed)
            {
                return;
            }

            _controllers[block.EntityId] = block;
            _seen.Add(block.EntityId);
        }

        public void EndScan()
        {
            var remove = new List<long>();
            foreach (var pair in _controllers)
            {
                if (!_seen.Contains(pair.Key) || pair.Value == null || pair.Value.MarkedForClose || pair.Value.Closed)
                {
                    remove.Add(pair.Key);
                }
            }

            for (var i = 0; i < remove.Count; i++)
            {
                _controllers.Remove(remove[i]);
            }

            ElectPrimary();
        }

        public void Clear()
        {
            _controllers.Clear();
            _seen.Clear();
            Primary = null;
        }

        private void ElectPrimary()
        {
            Primary = null;
            foreach (var pair in _controllers)
            {
                if (Primary == null || pair.Key < Primary.EntityId)
                {
                    Primary = pair.Value;
                }
            }
        }
    }
}
