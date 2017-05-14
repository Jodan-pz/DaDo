using System;
using DaDo.Command.Common;

namespace DaDo
{
    class Program
    {
        public static int Main(string[] args)
        {
            var options = CommandLineOptions.Parse(args);
            if (options?.Command == null) return 1;
            return options.Command.Do();
        }
    }
}