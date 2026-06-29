using Sandbox.ModAPI;
using VRage.Game;
using VRage.Game.Components;

namespace AGS
{
    [MySessionComponentDescriptor(MyUpdateOrder.BeforeSimulation)]
    public sealed class Session : MySessionComponentBase
    {
        public static Program Program { get; private set; }
        public static MyObjectBuilder_Checkpoint.ModItem? ModItem { get; private set; }

        public override void LoadData()
        {
            ModItem = ModContext.ModItem;
            Program = new Program();
            Program.Load();
        }

        public override void UpdateBeforeSimulation()
        {
            if (MyAPIGateway.Session == null)
            {
                return;
            }

            Program?.Update();
        }

        protected override void UnloadData()
        {
            Program?.Unload();
            Program = null;
            ModItem = null;
        }
    }
}
