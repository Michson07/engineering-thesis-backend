using MediatR;
using NSubstitute;

namespace Core.Application.Test
{
    public abstract class ServicesMock
    {
        protected IMediator mediator = Substitute.For<IMediator>();
    }
}
