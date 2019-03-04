using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rosenbrock
{
    class Program
    {
        static void Main(string[] args)
        {
            var gd = new GradientDescent();
            gd.SetPosition(new List<double>() { 4, 8 });
            var path = gd.Optimize(10000000, 0.00001, 0.00001);
            Console.WriteLine($"path size: {path.Count}, " +
                $"result: {gd.CurrentPosition.Select(i => i.ToString()).Aggregate((x, i) => x + " " + i)}");
            PathWriter.WritePath(path, @"C:\Workspace\Rosenbrock\notebook\path.csv");
        }
    }

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

    public class GradientDescent
    {
        public List<double> CurrentPosition { get; private set; }

        public void SetPosition(List<double> position)
        {
            CurrentPosition = position;
        }

        public List<double[]> Optimize(int maxSteps, double eps, double stepSize)
        {
            var steps = new List<double[]>() { CurrentPosition.ToArray() };
            for (int step = 0; step < maxSteps; step++) {
                var grad = Rosenbrock.AntiGradientIn(CurrentPosition);
                for (int i = 0; i < CurrentPosition.Count; i++) {
                    CurrentPosition[i] += grad[i] * stepSize;
                }
                steps.Add(CurrentPosition.ToArray());
                if (steps[steps.Count - 1].EuclideanDistance(steps[steps.Count - 2]) < eps) {
                    break;
                }
            }
            return steps;
        }

        public GradientDescent()
        {
            CurrentPosition = new List<double>();
        }
    }

    public static class Helpers
    {
        public static double EuclideanDistance(this double[] from, double[] to)
        {
            if (from.Length != to.Length) {
                throw new ArgumentException("Can't calculate distance for vectors with different size.");
            }

            var distanceSquared = 0d;
            for (int i = 0; i < from.Length; i++) {
                distanceSquared += Math.Pow(from[i] - to[i], 2);
            }

            var result = Math.Sqrt(distanceSquared);
            return result;
        }
    }

    public static class Rosenbrock
    {
        public static double ValueIn(List<double> vec)
        {
            var dim = vec.Count;
            var value = 0d;
            for (int i = 0; i < dim - 1; i++) {
                var component = Math.Pow(1 - vec[i], 2) + 100 * Math.Pow(vec[i + 1] - vec[i] * vec[i], 2);
                value += component;
            }
            return value;
        }

        public static List<double> GradientIn(List<double> vec)
        {
            var dim = vec.Count;
            var gradient = new List<double>(new double[dim]);
            gradient[0] = 400 * Math.Pow(vec[0], 3) - 400 * vec[1] + 2 * vec[0] - 2;
            for (int i = 1; i < dim - 1; i++) {
                gradient[i] = 400 * Math.Pow(vec[i], 3) - 200 * Math.Pow(vec[i - 1], 2) - 400 * vec[i + 1] + 202 * vec[i] - 2;
            }
            gradient[dim - 1] = 200 * vec[dim - 1] - 200 * vec[dim - 2];
            return gradient;
        }

        public static List<double> AntiGradientIn(List<double> vec)
        {
            return GradientIn(vec).Select(x => -x).ToList();
        }
    }
}
