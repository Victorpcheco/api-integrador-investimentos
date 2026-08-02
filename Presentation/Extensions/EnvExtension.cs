using DotNetEnv;

namespace Presentation.Extensions;

public static class EnvExtension
{
    public static WebApplicationBuilder AddEnvironmentVariables(this WebApplicationBuilder builder)
    {
        var envPath = Path.Combine(Directory.GetCurrentDirectory(), ".env");

        if (File.Exists(envPath))
        {
            Env.Load(envPath);
        }

        builder.Configuration.AddEnvironmentVariables();

        return builder;
    }
}
