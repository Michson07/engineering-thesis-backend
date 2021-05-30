using Core.Domain.ValueObjects;
using Core.Services;

namespace Groups.Domain.ValueObjects
{
    public class GroupAccessCode : ValueObject<string>
    {
        private GroupAccessCode()
        {
        }

        public GroupAccessCode(string value = "") : base(new TokenGeneratorService(10).Generate())
        {
        }
    }
}
