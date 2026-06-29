using System.Collections.Generic;
using VRage.Game.ModAPI;

namespace AGS
{
    public sealed class ShipContext
    {
        public ShipContext(long constructId, IMyCubeBlock primaryController, List<IMyCubeGrid> grids, List<IMyCubeBlock> blocks)
        {
            ConstructId = constructId;
            PrimaryController = primaryController;
            Grids = grids;
            Blocks = blocks;
        }

        public long ConstructId { get; private set; }
        public IMyCubeBlock PrimaryController { get; private set; }
        public List<IMyCubeGrid> Grids { get; private set; }
        public List<IMyCubeBlock> Blocks { get; private set; }
    }
}
