using DaDo.Command.Common;
using Microsoft.Extensions.CommandLineUtils;

namespace DaDo.Command.CommandConfiguration
{
    public class CleanCommandConfiguration
    {
        public static void Configure(CommandLineApplication cmd, ICommandLineOptions options)
        {
            cmd.Description = "clean output folder";
            cmd.HelpOption("-?|-h|--help");

            var excludeOption = cmd.Option("-e|--exclude <exclusions>", "files to exclude from cleaning", CommandOptionType.MultipleValue);
            var forceOption = cmd.Option("-f|--force", "force clean", CommandOptionType.NoValue);

            cmd.OnExecute(() =>
            {
                options.Command = new CleanCommand(options.Globals, excludeOption.Values, forceOption.HasValue());
                return 0;
            });
        }

    }
}