using System;
using System.IO;
using DaDo.Command;
using DaDo.Command.Common;
using DaDo.Command.Configuration;
using Microsoft.Extensions.CommandLineUtils;

namespace DaDo
{
    public class CommandLineOptions : ICommandLineOptions
    {
        public ICommand Command { get; set; }
        public GlobalOptions Globals { get; } = new GlobalOptions();
        //---------------------------------------------------------------------
        public static CommandLineOptions Parse(string[] args)
        {
            var cmdOptions = new CommandLineOptions();
            var app = new CommandLineApplication();

            var simulate = app.Option("-s|--simulate", "perform a simulation dumping to standard output (does not affect any file!)", CommandOptionType.NoValue);
            var output = app.Option("-o|--output", "output folder. default is current directory.", CommandOptionType.SingleValue);

            RootCommandConfiguration.Configure(app, cmdOptions);

            int result = 1;
            string error = "error!";
            try
            {
                result = app.Execute(args);
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }

            if (result != 0)
            {
                Console.Error.WriteLine(error);
                return null;
            }

            cmdOptions.Globals.Simulate = simulate.HasValue();
            cmdOptions.Globals.OutputFolder = output.Value() ?? Directory.GetCurrentDirectory();

            return cmdOptions;
        }
    }
}