using TESTWebApp.Domain.Services.Users;
using TESTWebApp.Domain.Services.WorkInputs;
using TESTWebApp.Domain.Services.MajorWorkItems;
using TESTWebApp.Infrastructure.Queries;
using TESTWebApp.Infrastructure.Repositories;
using TESTWebApp.UseCase.Users.Commands.Create;
using TESTWebApp.UseCase.Users.Queries;
using TESTWebApp.UseCase.WorkInputs.Commands.Create;
using TESTWebApp.UseCase.WorkInputs.Queries;
using TESTWebApp.UseCase.MajorWorkItems.Commands.Create;
using TESTWebApp.UseCase.MajorWorkItems.Queries;
using TESTWebApp.UseCase.MiddleWorkItems.Queries;
using TESTWebApp.Domain.Services.MiddleWorkItems;
using TESTWebApp.UseCase.MiddleWorkItems.Commands.Create;
using TESTWebApp.Domain.Services.MinorWorkItems;
using TESTWebApp.UseCase.MinorWorkItems.Queries;
using TESTWebApp.UseCase.MinorWorkItems.Commands.Create;
using TESTWebApp.UseCase.Users.Commands.CreateSession;
using TESTWebApp.UseCase.MajorWorkItems.Commands.Delete;
using TESTWebApp.UseCase.MajorWorkItems.Commands.Edit;
using TESTWebApp.UseCase.MiddleWorkItems.Commands.Delete;
using TESTWebApp.UseCase.MiddleWorkItems.Commands.Edit;
using TESTWebApp.UseCase.MinorWorkItems.Commands.Edit;
using TESTWebApp.UseCase.MinorWorkItems.Commands.Delete;

namespace TESTWebApp
{
    public static class SetUpDependencyInjections
    {
        public static WebApplicationBuilder SetUp(WebApplicationBuilder builder)
        {
            builder.Services
                // RepositoryService
                //  ユーザ
                .AddTransient<IUserRepository, UserRepository>()
                //  作業登録
                .AddTransient<IWorkInputRepository, WorkInputRepository>()
                //  マスタ大項目
                .AddTransient<IMajorWorkItemRepository, MajorWorkItemRepository>()
                //  マスタ中項目
                .AddTransient<IMiddleWorkItemRepository, MiddleWorkItemRepository>()
                //  マスタ小項目
                .AddTransient<IMinorWorkItemRepository, MinorWorkItemRepository>()

                // QueryService
                //  ユーザ
                .AddTransient<IUserDataQueryService, UserDataQueryService>()
                //  作業登録
                .AddTransient<IWorkInputDataQueryService, WorkInputDataQueryService>()
                //  マスタ大項目
                .AddTransient<IMajorWorkItemQueryService, MajorWorkItemQueryService>()
                //  マスタ中項目
                .AddTransient<IMiddleWorkItemQueryService, MiddleWorkItemQueryService>()
                //  マスタ小項目
                .AddTransient<IMinorWorkItemQueryService, MinorWorkItemQueryService>()

                // CommandService
                //  ユーザ
                .AddTransient<IUserCreateCommand, UserCreateCommand>()
                .AddTransient<IWebSessionCreateCommand, WebSessionCreateCommand>()
                //  作業登録
                .AddTransient<IWorkInputCreateCommand, WorkInputCreateCommand>()
                //  マスタ大項目
                .AddTransient<IMajorWorkItemCreateCommand, MajorWorkItemCreateCommand>()
                .AddTransient<IMajorWorkItemEditCommand, MajorWorkItemEditCommand>()
                .AddTransient<IMajorWorkItemDeleteCommand, MajorWorkItemDeleteCommand>()
                //  マスタ中項目
                .AddTransient<IMiddleWorkItemCreateCommand, MiddleWorkItemCreateCommand>()
                .AddTransient<IMiddleWorkItemEditCommand, MiddleWorkItemEditCommand>()
                .AddTransient<IMiddleWorkItemDeleteCommand, MiddleWorkItemDeleteCommand>()
                //  マスタ小項目
                .AddTransient<IMinorWorkItemCreateCommand, MinorWorkItemCreateCommand>()
                .AddTransient<IMinorWorkItemEditCommand, MinorWorkItemEditCommand>()
                .AddTransient<IMinorWorkItemDeleteCommand, MinorWorkItemDeleteCommand>()
                ;
            return builder;
        }
    }
}
