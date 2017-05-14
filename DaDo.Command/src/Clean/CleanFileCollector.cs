using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DaDo.Command
{
    class CleanFileCollector
    {
        public List<string> ToDelete;
        public List<string> ToExclude;
        public void Collect(IEnumerable<string> exclusions, string outputFolder)
        {
            ToDelete = new List<string>();
            ToExclude = new List<string>();
            var checker = exclusions.Select(e => new WildcardPattern(e));
            foreach (string file in Directory.EnumerateFileSystemEntries(outputFolder, "*", SearchOption.AllDirectories))
            {
                if (checker.Any(m => m.IsMatch(file) || m.IsMatch(Path.GetFileName(file))))
                    ToExclude.Add(file);
                else
                    ToDelete.Add(file);
            }

            // remove directory with files
            var notEmpty = ToDelete.Where(s => ToExclude.Any(ex => ex.Contains(s)));
            ToDelete = ToDelete.Except(notEmpty).ToList();
            ToDelete.Reverse();
        }
    }

}