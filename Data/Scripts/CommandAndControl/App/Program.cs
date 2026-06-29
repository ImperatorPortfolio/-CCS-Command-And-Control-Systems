using Sandbox.ModAPI;
using VRage.Game.ModAPI;

namespace AGS
{
    public sealed class Program
    {
        private readonly GridRegistry _grids;
        private readonly ResolutionPatchService _resolution;

        public Program()
        {
            _grids = new GridRegistry(new SurfaceMap(), new AppRegistry());
            _resolution = new ResolutionPatchService();
        }

        public void Load()
        {
            _resolution.Load();
        }

        public void Update()
        {
            _grids.Update();
        }

        public void Unload()
        {
            _grids.Clear();
            _resolution.Unload();
        }

        public void RegisterScreen(IMyCubeBlock block, Sandbox.ModAPI.IMyTextSurface surface)
        {
            _grids.RegisterScreen(block, surface);
        }

        public void UnregisterScreen(IMyCubeBlock block, Sandbox.ModAPI.IMyTextSurface surface)
        {
            _grids.UnregisterScreen(block, surface);
        }

        public UiFrame GetFrame(IMyCubeBlock block, Sandbox.ModAPI.IMyTextSurface surface)
        {
            return _grids.GetFrame(block, surface);
        }

        public void SubmitIntent(IMyCubeBlock block, Sandbox.ModAPI.IMyTextSurface surface, UiIntent intent)
        {
            _grids.SubmitIntent(block, surface, intent);
        }
    }
}
