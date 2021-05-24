namespace Groups.Application.TestResultQueries
{
    public class TestStudentsResultsView
    {
        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public TestResultView Result { get; set; } = null!;
    }
}