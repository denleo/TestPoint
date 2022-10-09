using Autofac;
using Microsoft.Extensions.Configuration;

namespace AdminSetup.Extensions;

internal static class ConfigurationInjectionExtension
{
    public static ContainerBuilder AddConfiguration(this ContainerBuilder builder)
    {
        builder.Register(c =>
            {
                var configBuilder = new ConfigurationBuilder()
                    .AddJsonFile("config.json", false, true);
                return configBuilder.Build();
            })
            .As<IConfiguration>()
            .SingleInstance();

        return builder;
    }
}