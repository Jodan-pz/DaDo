using System.Reflection;
using DaDo.Command.CommandConfiguration;
using DaDo.Command.Common;
using Microsoft.Extensions.CommandLineUtils;
using Microsoft.Extensions.PlatformAbstractions;

namespace DaDo.Command.Configuration
{
    public static class RootCommandConfiguration
    {
        private static string GetVersion() => PlatformServices.Default.Application.ApplicationVersion;

        public static void Configure(CommandLineApplication app, ICommandLineOptions options)
        {
            app.HelpOption("-?|-h|--help");
            app.VersionOption("--version", GetVersion);

            app.Name = "DaDo";
            app.FullName = "DaDo -- Release asset bundle generator";
            app.Description = "Create bundle from yaml configuration";

            app.Command("clean", c => CleanCommandConfiguration.Configure(c, options));
            // app.Command("attack", c => AttackCommandConfiguration.Configure(c, options));

            app.OnExecute(() =>
            {
                options.Command = new RootCommand(app);
                return 0;
            });
        }
        //---------------------------------------------------------------------
    }
}