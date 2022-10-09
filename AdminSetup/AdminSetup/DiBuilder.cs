using AdminSetup.Extensions;
using Autofac;

namespace AdminSetup;

internal static class DiBuilder
{
    public static IContainer Build()
    {
        var builder = new ContainerBuilder();

        builder.AddConfiguration();
        builder.AddViews();
        builder.AddServices();
        builder.AddPresenters();

        return builder.Build();
    }
}