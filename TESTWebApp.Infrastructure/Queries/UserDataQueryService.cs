﻿using TESTWebApp.Infrastructure.Database;
using TESTWebApp.UseCase.Users.Queries;
using TESTWebApp.Infrastructure.Database.Tables;

namespace TESTWebApp.Infrastructure.Queries
{
    public sealed class UserDataQueryService : IUserDataQueryService
    {
        private readonly AppDbContext _database;

        public UserDataQueryService(AppDbContext database)
        {
            _database = database;
        }

        public IEnumerable<UserDataResponse> GetAllUser()
        {
            IEnumerable<USER> userModels = _database.UserDataModels;

            if (!userModels.Any())
                return new List<UserDataResponse>();

            return ToQueryModels(userModels);
        }

        public UserDataResponse FindUserById(string id)
        {
            USER user = _database.UserDataModels
                .Where(x => x.Id == id)
                .FirstOrDefault();

            if (user is null)
                return null;

            return ToQueryModel(user);
        }

        private static IEnumerable<UserDataResponse> ToQueryModels(IEnumerable<USER> from)
        {
            foreach (var i in from)
                yield return ToQueryModel(i);
        }

        private static UserDataResponse ToQueryModel(USER from)
        {
            return new UserDataResponse()
            {
                Id = from.Id,
                UserName = from.UserName,
                CreatedBy = from.CreatedBy,
                ModifiedBy = from.ModifiedBy,
                Created = from.Created,
                Modified = from.Modified,
                IsDeleted = from.IsDeleted,
            };
        }
    }
}
