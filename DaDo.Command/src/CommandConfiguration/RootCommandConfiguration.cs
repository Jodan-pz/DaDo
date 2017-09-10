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

        public static void Configure(CommandLineApplication cmd, ICommandLineOptions options)
        {
            cmd.HelpOption("-?|-h|--help");
            cmd.VersionOption("--version", GetVersion);

            cmd.Name = "DaDo";
            cmd.FullName = "DaDo -- Release asset bundle generator";
            cmd.Description = "Create bundle from yaml configuration";

            cmd.Command("clean", c => CleanCommandConfiguration.Configure(c, options));
            // app.Command("attack", c => AttackCommandConfiguration.Configure(c, options));

            cmd.OnExecute(() =>
            {
                options.Command = new RootCommand(cmd);
                return 0;
            });
        }
        //---------------------------------------------------------------------
    }
}