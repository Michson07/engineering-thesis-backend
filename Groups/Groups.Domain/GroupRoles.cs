namespace Groups.Domain
{
    public static class GroupRoles
    {
        private static readonly string[] roles = { "Owner", "Student" };

        public static string Owner = roles[0];
        public static string Student = roles[1];
    }
}
