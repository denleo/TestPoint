using Autofac;
using Core.Services.Api;
using Services.Api;

namespace AdminSetup.Extensions;

internal static class ServicesInjectionExtension
{
    public static ContainerBuilder AddServices(this ContainerBuilder builder)
    {
        builder.RegisterType<AdminApiHandler>()
            .As<IAdminApiHandler>()
            .InstancePerDependency();

        return builder;
    }
}