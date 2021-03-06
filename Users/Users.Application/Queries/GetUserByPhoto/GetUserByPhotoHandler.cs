﻿using Core.Application;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Users.Database.UserAggregateDatabase;

namespace Users.Application.Queries.GetUserByPhoto
{
    public class GetUserByPhotoHandler : IRequestHandler<GetUserByPhotoDto, QueryResult<GetUserByPhotoView>>
    {
        private readonly IUserAggregateRepository repository;

        public GetUserByPhotoHandler(IUserAggregateRepository repository)
        {
            this.repository = repository;
        }

        public Task<QueryResult<GetUserByPhotoView>> Handle(GetUserByPhotoDto request, CancellationToken cancellationToken)
        {
            var user = repository.Get(request.Email);

            var response = new QueryResult<GetUserByPhotoView>();
            if(user == null)
            {
                response.BodyResponse = null;
            }
            else
            {
                response.BodyResponse = new GetUserByPhotoView
                {
                    Photo = user.Photo?.Image
                };
            }

            return Task.FromResult(response);
        }
    }
}
