using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Rosenbrock
{
    public static class PathWriter
    {
        public static void WritePath(List<double[]> path, string file)
        {
            using (var writer = new StreamWriter(file)) {
                var items = path.Select(step =>
                        string.Join("|", step.Select(i => i.ToString())));
                foreach (var item in items) {
                    writer.WriteLine(item);
                }
            }
        }
    }
}
