using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using DaDo.Command.Common;

namespace DaDo.Command
{
    public class CleanCommand : BaseCommand
    {
        private readonly List<string> _exclusions;
        private readonly bool _force;
        public CleanCommand(GlobalOptions globals, List<string> values, bool force) : base(globals)
        {
            _exclusions = values;
            _force = force;
        }

        protected override int OnSimulate()
        {
            Info($"Cleaning: {Globals.OutputFolder}");
            if (_force) Info("Force: enabled");
            if (_exclusions?.Count > 0) Info($"Excluding: {string.Join(", ", _exclusions)}");

            if (!Directory.Exists(Globals.OutputFolder))
            {
                Error($"Output folder '{Globals.OutputFolder}' does not exists!");
                return 1;
            }
            CleanFileCollector cf = new CleanFileCollector();
            cf.Collect(_exclusions, Globals.OutputFolder);
            Title("Clean will consider for deletion");
            foreach (string file in cf.ToDelete) Info(file);
            foreach (string file in cf.ToExclude) Warning($"Excluded: {file}");
            return 0;
        }

        protected override int OnDo()
        {
            Info($"Cleaning folder '{Globals.OutputFolder}'");
            if (!Directory.Exists(Globals.OutputFolder))
            {
                Error($"Output folder '{Globals.OutputFolder}' does not exists!");
                return 1;
            }
            CleanFileCollector cf = new CleanFileCollector();
            cf.Collect(_exclusions, Globals.OutputFolder);
            foreach (string toDelete in cf.ToDelete)
            {
                if (_force) File.SetAttributes(toDelete, FileAttributes.Normal);
                if (Directory.Exists(toDelete)) Directory.Delete(toDelete);
                else if (File.Exists(toDelete)) File.Delete(toDelete);
            }
            return 0;
        }
    }
}