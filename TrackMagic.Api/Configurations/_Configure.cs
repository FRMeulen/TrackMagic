namespace TrackMagic.Api.Configurations
{
    public static class _Configure
    {
        private static string configDir = "Configurations";
        private static string[] configFiles = ["database", "openapi"];

        public static WebApplicationBuilder AddConfigurations(this WebApplicationBuilder builder)
        {
            builder.Configuration
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);

            foreach (var file in configFiles)
            {
                AddConfig(builder, file);
            }

            return builder;
        }

        private static void AddConfig(WebApplicationBuilder builder, string fileName)
        {
            builder.Configuration
                .AddJsonFile($"{configDir}/{fileName}.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"{configDir}/{fileName}.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);
        }
    }
}
