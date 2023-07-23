using Microsoft.Extensions.DependencyInjection;
using TESTWebApp.Domain.Models.WorkInputs;
using TESTWebApp.Infrastructure.Queries;
using TESTWebApp.Infrastructure.Repositories;
using TESTWebApp.UseCase.WorkInputs;
using TESTWebApp.UseCase.WorkInputs.Queries;

namespace TESTWebApp
{
    public static class SetUpDependencyInjections
    {
        public static WebApplicationBuilder SetUp(WebApplicationBuilder builder)
        {
            builder.Services
                // QueryService
                .AddTransient<IWorkInputDataQuery, WorkInputDataQuery>()
                .AddTransient<IWorkInputRepository, WorkInputRepository>()
                // UseCace
                .AddTransient<IWorkInputUseCase, WorkInputUseCase>()
                ;
            return builder;
        }
    }
}
