using System;
using System.Collections.Generic;
using Sandbox.ModAPI;
using VRage.Game.ModAPI;
using VRageMath;

namespace AGS
{
    public sealed class VirtualPointer
    {
        private readonly SurfaceMap _surfaceMap;
        private readonly List<ScreenBinding> _screens;
        private readonly Dictionary<string, PointerState> _states;

        public VirtualPointer(SurfaceMap surfaceMap)
        {
            _surfaceMap = surfaceMap;
            _screens = new List<ScreenBinding>();
            _states = new Dictionary<string, PointerState>();
        }

        public void RegisterScreen(IMyCubeBlock block, Sandbox.ModAPI.IMyTextSurface surface)
        {
            if (block == null || surface == null)
            {
                return;
            }

            var provider = block as Sandbox.ModAPI.IMyTextSurfaceProvider;
            if (provider == null)
            {
                return;
            }

            var index = SurfaceUtil.GetSurfaceIndex(provider, surface);
            if (index < 0)
            {
                return;
            }

            var key = GetKey(block.EntityId, index);
            _screens.RemoveAll(x => x.Key == key);
            _screens.Add(new ScreenBinding { Block = block, Surface = surface, SurfaceIndex = index, Key = key });
            _states[key] = new PointerState();
        }

        public void UnregisterScreen(IMyCubeBlock block, Sandbox.ModAPI.IMyTextSurface surface)
        {
            if (block == null || surface == null)
            {
                return;
            }

            var provider = block as Sandbox.ModAPI.IMyTextSurfaceProvider;
            if (provider == null)
            {
                return;
            }

            var index = SurfaceUtil.GetSurfaceIndex(provider, surface);
            if (index < 0)
            {
                return;
            }

            var key = GetKey(block.EntityId, index);
            _screens.RemoveAll(x => x.Key == key);
            _states.Remove(key);
        }

        public PointerState GetPointer(IMyCubeBlock block, Sandbox.ModAPI.IMyTextSurface surface)
        {
            if (block == null || surface == null)
            {
                return null;
            }

            var provider = block as Sandbox.ModAPI.IMyTextSurfaceProvider;
            if (provider == null)
            {
                return null;
            }

            var index = SurfaceUtil.GetSurfaceIndex(provider, surface);
            if (index < 0)
            {
                return null;
            }

            PointerState state;
            _states.TryGetValue(GetKey(block.EntityId, index), out state);
            return state;
        }

        public void Update()
        {
            var session = MyAPIGateway.Session;
            if (session == null || MyAPIGateway.Gui.IsCursorVisible)
            {
                ResetAll(true);
                return;
            }

            var cameraMatrix = session.Camera.WorldMatrix;
            var cameraPosition = cameraMatrix.Translation;
            var cameraDirection = cameraMatrix.Forward;

            string activeKey = null;
            var closestDistance = double.MaxValue;

            for (var i = _screens.Count - 1; i >= 0; i--)
            {
                var screen = _screens[i];
                if (!IsValid(screen))
                {
                    _screens.RemoveAt(i);
                    _states.Remove(screen.Key);
                    continue;
                }

                var state = EnsureState(screen.Key);
                ResetState(state, false);

                SurfaceCoords coords;
                if (!_surfaceMap.TryGet(screen.Block.BlockDefinition.SubtypeId, screen.SurfaceIndex, out coords))
                {
                    continue;
                }

                var topLeft = SurfaceMath.LocalToGlobal(coords.TopLeft, screen.Block.WorldMatrix);
                var bottomLeft = SurfaceMath.LocalToGlobal(coords.BottomLeft, screen.Block.WorldMatrix);
                var bottomRight = SurfaceMath.LocalToGlobal(coords.BottomRight, screen.Block.WorldMatrix);
                var normal = coords.GetNormal(screen.Block.WorldMatrix);
                var intersection = SurfaceMath.GetLinePlaneIntersection(topLeft, normal, cameraPosition, cameraDirection);
                if (intersection == Vector3D.Zero)
                {
                    continue;
                }

                var distance = Vector3D.Distance(cameraPosition, intersection);
                if (distance > SurfaceUtil.GetInteractiveDistance(coords))
                {
                    continue;
                }

                var screenUp = Vector3D.Normalize(topLeft - bottomLeft);
                var screenRight = Vector3D.Normalize(bottomLeft - bottomRight);
                var point = SurfaceMath.GetPointOnPlane(intersection, topLeft, screenUp, screenRight);
                var width = (float)Vector3D.Distance(bottomLeft, bottomRight);
                var height = (float)Vector3D.Distance(bottomLeft, topLeft);
                if (point.X < 0f || point.X > width || point.Y < 0f || point.Y > height)
                {
                    continue;
                }

                var normalized = new Vector2(point.X / width, point.Y / height);
                var rotated = SurfaceUtil.RotateScreenCoord(normalized, SurfaceUtil.GetRotation(screen.Block));
                var offset = (screen.Surface.TextureSize - screen.Surface.SurfaceSize) * 0.5f;

                state.IsOnScreen = true;
                state.Position = new Vector2(rotated.X * screen.Surface.SurfaceSize.X + offset.X, rotated.Y * screen.Surface.SurfaceSize.Y + offset.Y);
                state.Distance = distance;
                state.Scale = MathHelper.Clamp((float)Math.Min(screen.Surface.SurfaceSize.X, screen.Surface.SurfaceSize.Y) / 512f, 0.75f, 2f);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    activeKey = screen.Key;
                }
            }

            if (activeKey != null)
            {
                EnsureState(activeKey).IsActive = true;
            }
        }

        // True when the local view is aimed at one of this construct's screens, so the
        // session can suppress the game's click-to-open-terminal behaviour.
        public bool IsEngaged
        {
            get
            {
                foreach (var state in _states.Values)
                {
                    if (state.IsActive)
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        public void Clear()
        {
            _screens.Clear();
            _states.Clear();
        }

        private void ResetAll(bool clearPressed)
        {
            foreach (var state in _states.Values)
            {
                ResetState(state, clearPressed);
            }
        }

        private PointerState EnsureState(string key)
        {
            PointerState state;
            if (!_states.TryGetValue(key, out state))
            {
                state = new PointerState();
                _states[key] = state;
            }

            return state;
        }

        private static void ResetState(PointerState state, bool clearPressed)
        {
            state.IsOnScreen = false;
            state.IsActive = false;
            state.IsPressed = false;
            if (clearPressed)
            {
                state.WasPressed = false;
            }
            state.Position = Vector2.Zero;
            state.Distance = double.MaxValue;
            state.Scale = 1f;
        }

        private static bool IsValid(ScreenBinding screen)
        {
            return screen.Block != null && screen.Surface != null && !screen.Block.MarkedForClose && !screen.Block.Closed;
        }

        private static string GetKey(long entityId, int surfaceIndex)
        {
            return entityId + ":" + surfaceIndex;
        }

        private sealed class ScreenBinding
        {
            public IMyCubeBlock Block;
            public Sandbox.ModAPI.IMyTextSurface Surface;
            public int SurfaceIndex;
            public string Key;
        }
    }

    public sealed class PointerState
    {
        public bool IsOnScreen { get; set; }
        public bool IsActive { get; set; }
        public bool IsPressed { get; set; }
        public bool WasPressed { get; set; }
        public Vector2 Position { get; set; }
        public double Distance { get; set; }
        public float Scale { get; set; }
    }
}
