using Autofac;
using Core.Views;

namespace AdminSetup.Extensions;

internal static class ViewsInjectionExtension
{
    public static ContainerBuilder AddViews(this ContainerBuilder builder)
    {
        builder.RegisterType<Tabs.AdminSetup>()
            .As<IAdminSetupView>()
            .InstancePerDependency();

        builder.RegisterType<Tabs.Logs>()
            .As<ILogsView>()
            .InstancePerDependency();

        return builder;
    }
}