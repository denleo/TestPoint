using Autofac;
using Core;
using Core.Views;
using Presenters;

namespace AdminSetup.Extensions;

internal static class PresentersInjectionExtension
{
    public static ContainerBuilder AddPresenters(this ContainerBuilder builder)
    {
        builder.RegisterType<AdminSetupPresenter>()
            .As<PresenterBase<IAdminSetupView>>()
            .InstancePerDependency();

        return builder;
    }
}