namespace Groups.Application.TestResultQueries
{
    public class TestStudentsResultsView
    {
        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public byte[]? Photo { get; set; } = null!;
        public TestResultView Result { get; set; } = null!;
    }
}