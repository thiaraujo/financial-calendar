using Application.Core.Commands.Account;
using Application.Core.Commands.Authentication;
using Application.Core.Commands.Organization;
using Application.Core.Helpers;
using Application.Core.Responses.Account;
using Application.Core.Responses.Authentication;
using Application.Core.Responses.Organization;
using AutoMapper;
using Data.Infra.Context;
using Data.Infra.Persistence;
using Data.Interfaces;
using Data.Repository;
using Data.UnitOfWork;
using Domain.Entities.Entities;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Middleware.Notifications;
using Middleware.Security.Interfaces;
using Middleware.Security.Services;

namespace Infra.IoC
{
    public static class RegisterContainers
    {
        public static void RegisterContainer(this IServiceCollection services)
        {
            // Context
            services.AddScoped<MongoContext>();
            services.AddScoped<IMongoContext, MongoContext>();
            MongoDbPersistence.Configure(); //Configure documents

            // Unit of Work
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Base
            services.AddScoped(typeof(IBase<>), typeof(BaseRepository<>));

            // Entities
            services.AddScoped<IAccount, AccountRepository>();
            services.AddScoped<IOrganization, OrganizationRepository>();
            services.AddScoped<IAccountLoginLog, AccountLoginLogRepository>();

            // Middleware
            services.AddScoped<NotificationContext>();

            // Mapper
            var autoMapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CreateAccountCommand, Account>().ReverseMap();
                cfg.CreateMap<CreateOrganizationCommand, Organization>().ReverseMap();
                cfg.CreateMap<CreateAuthenticationLogCommand, AccountLoginLog>().ReverseMap();

                cfg.CreateMap<AccountResponse, Account>().ReverseMap();
                cfg.CreateMap<AuthenticationResponse, Account>().ReverseMap();
                cfg.CreateMap<OrganizationResponse, Organization>().ReverseMap();
            });
            services.AddSingleton(autoMapperConfig.CreateMapper());

            // Mediator
            services.AddMediatR(typeof(ApplicationMediatREntrypoint).Assembly);

            // Security
            services.AddSingleton<ITokenService, TokenService>();
        }
    }
}