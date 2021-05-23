namespace Groups.Domain
{
    public static class CheckedState
    {
        private readonly static string[] states = { "Checked", "NotChecked" };

        public static string Checked { get; } = states[0];
        public static string NotChecked { get; } = states[1];
    }
}
