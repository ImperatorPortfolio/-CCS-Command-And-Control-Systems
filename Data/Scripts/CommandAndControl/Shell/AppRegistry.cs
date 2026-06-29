using System.Collections.Generic;

namespace AGS
{
    public sealed class AppRegistry
    {
        private readonly Dictionary<string, IGridApp> _apps = new Dictionary<string, IGridApp>();
        private readonly List<IGridApp> _launchable = new List<IGridApp>();

        public AppRegistry()
        {
            Register(new DesktopApp());
            Register(new CommandApp());
            Register(new OperationsApp());
            Register(new EngineeringApp());
            Register(new HelmApp());
            Register(new NavigationApp());
            Register(new TacticalApp());
            Register(new SecurityApp());
            Register(new WeaponsApp());
            Register(new SensorsApp());
            Register(new DamageControlApp());
            Register(new CommsApp());
            Register(new SystemApp());
        }

        public bool Contains(string appId)
        {
            return !string.IsNullOrEmpty(appId) && _apps.ContainsKey(appId);
        }

        public IGridApp Get(string appId)
        {
            if (string.IsNullOrEmpty(appId))
            {
                return null;
            }

            IGridApp app;
            _apps.TryGetValue(appId, out app);
            return app;
        }

        public string GetTitle(string appId)
        {
            var app = Get(appId);
            return app != null ? app.Title : string.Empty;
        }

        public List<IGridApp> GetLaunchableApps(RoleId currentRole)
        {
            var list = new List<IGridApp>();
            for (var i = 0; i < _launchable.Count; i++)
            {
                var app = _launchable[i];
                if (PermissionPolicy.Allows(currentRole, app.RequiredRole))
                {
                    list.Add(app);
                }
            }

            return list;
        }

        private void Register(IGridApp app)
        {
            _apps[app.Id] = app;
            if (app.Id != DesktopApp.IdValue)
            {
                _launchable.Add(app);
            }
        }
    }
}
