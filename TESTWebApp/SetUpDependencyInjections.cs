using Microsoft.Extensions.DependencyInjection;
using TESTWebApp.Domain.Services.Users;
using TESTWebApp.Domain.Services.WorkInputs;
using TESTWebApp.Domain.Services.WorkItems;
using TESTWebApp.Infrastructure.Queries;
using TESTWebApp.Infrastructure.Repositories;
using TESTWebApp.UseCase.Users.Commands.Create;
using TESTWebApp.UseCase.Users.Queries;
using TESTWebApp.UseCase.WorkInputs;
using TESTWebApp.UseCase.WorkInputs.Commands.Create;
using TESTWebApp.UseCase.WorkInputs.Queries;
using TESTWebApp.UseCase.WorkItems.Commands.Create;

namespace TESTWebApp
{
    public static class SetUpDependencyInjections
    {
        public static WebApplicationBuilder SetUp(WebApplicationBuilder builder)
        {
            builder.Services
                // RepositoryService
                .AddTransient<IWorkInputRepository, WorkInputRepository>()
                .AddTransient<IWorkItemRepository, WorkItemRepository>()
                .AddTransient<IUserRepository, UserRepository>()
                // QueryService
                .AddTransient<IWorkInputDataQuery, WorkInputDataQuery>()
                .AddTransient<IUserDataQuery, UserDataQuery>()
                // CommandService
                .AddTransient<IWorkInputCreateCommand, WorkInputCreateCommand>()
                .AddTransient<IWorkItemCreateCommand, WorkItemCreateCommand>()
                .AddTransient<IUserCreateCommand, UserCreateCommand>()
                ;
            return builder;
        }
    }
}
