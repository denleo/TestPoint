using Autofac;
using Core.Services.Api;
using Core.Services.Http;
using Services.Api;
using Services.HttpService;

namespace AdminSetup.Extensions;

internal static class ServicesInjectionExtension
{
    public static ContainerBuilder AddServices(this ContainerBuilder builder)
    {
        builder.RegisterType<HttpService>()
            .As<IHttpService>()
            .SingleInstance();

        builder.RegisterType<AdminApiHandler>()
            .As<IAdminApiHandler>()
            .InstancePerDependency();

        builder.RegisterType<LogsApiHandler>()
            .As<ILogsApiHandler>()
            .InstancePerDependency();

        return builder;
    }
}