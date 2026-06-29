namespace AGS
{
    public static class PermissionPolicy
    {
        public static bool Allows(RoleId current, RoleId required)
        {
            if (required == RoleId.Unknown)
            {
                return true;
            }

            if (current == RoleId.Admin || current == RoleId.Captain)
            {
                return true;
            }

            if (current == required)
            {
                return true;
            }

            if (current == RoleId.Command)
            {
                return required != RoleId.Admin;
            }

            return false;
        }
    }
}
