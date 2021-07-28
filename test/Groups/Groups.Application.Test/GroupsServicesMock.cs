using Core.Application.Test;
using Groups.Application.Test.fakes;
using Groups.Database.AnnouncementAggregateDatabase;
using Groups.Database.GroupsAggregateDatabase;
using Groups.Database.QuestionAggregateDatabase;
using Groups.Database.TestAggregateDatabase;
using Groups.Database.TestResultAggregateDatabase;

namespace Groups.Application.Test
{
    public class GroupsServicesMock : ServicesMock
    {
        protected IGroupAggregateRepository groupAggregateRepository = new GroupAggregateRepositoryFake();
        protected IQuestionAggregateRepository questionAggregateRepository = new QuestionAggregateRepositoryFake();
        protected ITestAggregateRepository testAggregateRepository = new TestAggregateRepositoryFake();
        protected ITestResultAggregateRepository testResultAggregateRepository = new TestResultAggregateRepositoryFake();
        protected IAnnouncementAggregateRepository announcementAggregateRepository = new AnnouncementAggregateRepositoryFake();
    }
}
