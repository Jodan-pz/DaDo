using System;
using DaDo.Command.Common;

namespace DaDo.Command.Common
{
    public abstract class BaseCommand : ICommand
    {
        private readonly GlobalOptions _globals;
        protected GlobalOptions Globals => _globals;

        public BaseCommand(GlobalOptions globals)
        {
            _globals = globals;
        }

        public int Do()
        {
            if (!_globals.Simulate) return OnDo();
            Title("DaDo Simulation Mode");
            return OnSimulate();
        }

        abstract protected int OnDo();
        protected virtual int OnSimulate()
        {
            Info("Command does nothing to simulate. Nothing done!");
            return 0;
        }
        protected void Title(string title)
        {
            Console.ResetColor();
            Console.WriteLine($" --- {title} ---");
        }

        protected void Info(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($" -) {message}");
            Console.ResetColor();
        }

        protected void Warning(string warn)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($" -O {warn}");
            Console.ResetColor();
        }
        protected void Error(string error)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($" -( {error}");
            Console.ResetColor();
        }
    }
}