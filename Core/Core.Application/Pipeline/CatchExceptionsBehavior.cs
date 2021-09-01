using Core.Api;
using Core.Application.Exceptions;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Application.Pipeline
{
    public class CatchExceptionsBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> 
        where TRequest : IRequest<TResponse>
        where TResponse : IResult
    {
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            try
            {
                return await next();
            }
            catch(Exception ex)
            {
                if(typeof(TResponse) == typeof(CommandResult))
                {
                    IResult response = new CommandResult
                    {
                        Result = GetResultFromExceptionType(ex)
                    };

                    return (TResponse)response;
                }
            }

            throw new ApplicationException();
        }

        private static ApiActionResult GetResultFromExceptionType(Exception ex)
        {
            return ex switch
            {
                NotFoundException => new NotFoundResult(ex.Message),
                DomainException => new ConflictResult(ex.Message),
                _ => new ApplicationErrorResult()
            };
        } 
    }
}