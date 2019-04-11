using CsvHelper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Rosenbrock
{
    class Program
    {
        static void Main(string[] args)
        {
            TestGradient();
            Console.WriteLine("===================================================");
            //TestHogwild();
            //Console.WriteLine("===================================================");
            //TestNadam();
            //Console.WriteLine("===================================================");
            //TestParallelNadam();
            //Console.WriteLine("===================================================");
            TestNadams();
            Console.WriteLine("===================================================");

            while (true ) { }
        }

        async static void TestNadams()
        {
            var sw = new Stopwatch();

            var solver = new ParallelNadams();
            var mins = new List<double>() { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 };
            var maxs = new List<double>() { -10, -10, -10, -10, -10, -10, -10, -10, -10, -10, -10, -10, -10, -10, -10, -10 };
            sw.Reset();
            sw.Start();
            var result = await solver.Optimize(10000, 0.00001, 10, mins, maxs, 2);
            sw.Stop();

            Console.WriteLine($"elapsed time: {sw.ElapsedTicks}, " +
                $"result: {result.Select(i => i.ToString()).Aggregate((x, i) => x + " " + i)}");


            sw.Reset();
            sw.Start();
            result = await solver.Optimize(10000, 0.00001, 10, mins, maxs, 4);
            sw.Stop();
            Console.WriteLine($"elapsed time: {sw.ElapsedTicks}, " +
                $"result: {result.Select(i => i.ToString()).Aggregate((x, i) => x + " " + i)}");

            sw.Reset();
            sw.Start();
            result = await solver.Optimize(10000, 0.00001, 10, mins, maxs, 6);
            sw.Stop();
            Console.WriteLine($"elapsed time: {sw.ElapsedTicks}, " +
                $"result: {result.Select(i => i.ToString()).Aggregate((x, i) => x + " " + i)}");

            sw.Reset();
            sw.Start();
            result = await solver.Optimize(10000, 0.00001, 10, mins, maxs, 8);
            sw.Stop();
            Console.WriteLine($"elapsed time: {sw.ElapsedTicks}, " +
                $"result: {result.Select(i => i.ToString()).Aggregate((x, i) => x + " " + i)}");

            sw.Reset();
            sw.Start();
            result = await solver.Optimize(10000, 0.00001, 10, mins, maxs, 16);
            sw.Stop();
            Console.WriteLine($"elapsed time: {sw.ElapsedTicks}, " +
                $"result: {result.Select(i => i.ToString()).Aggregate((x, i) => x + " " + i)}");
        }

        static void TestHogwild()
        {
            var sw = new Stopwatch();

            var gd = new Hogwild();

            gd.SetPosition(new List<double>() { 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4 });
            sw.Reset();
            sw.Start();
            var result = gd.Optimize(10000, 0.00001, 0.00001);
            sw.Stop();
            Console.WriteLine($"elapsed time: {sw.ElapsedTicks}, " +
                $"result: {result.Select(i => i.ToString()).Aggregate((x, i) => x + " " + i)}");

            gd.SetPosition(new List<double>() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 });
            sw.Reset();
            sw.Start();
            result = gd.Optimize(10000000, 0.00001, 0.00001);
            sw.Stop();
            Console.WriteLine($"elapsed time: {sw.ElapsedTicks}, " +
                $"result: {result.Select(i => i.ToString()).Aggregate((x, i) => x + " " + i)}");

            gd.SetPosition(new List<double>() { -4, -4, 0, 8, 0, 1, 10, 5, -7, 9, 20, 16, 2, -2, 0, 10 });
            sw.Reset();
            sw.Start();
            result = gd.Optimize(10000000, 0.00001, 0.00001);
            sw.Stop();
            Console.WriteLine($"elapsed time: {sw.ElapsedTicks}, " +
                $"result: {result.Select(i => i.ToString()).Aggregate((x, i) => x + " " + i)}");
        }

        static void TestGradient()
        {

            var sw = new Stopwatch();

            var gd = new GradientDescent();

            gd.SetPosition(new List<double>() { 8, 4 });
            sw.Reset();
            sw.Start();
            var path = gd.Optimize(10000, 0.00001, 0.00001);
            sw.Stop();
            Console.WriteLine($"path size: {path.Count}, " +
                $"elapsed time: {sw.ElapsedTicks}, " +
                $"result: {gd.CurrentPosition.Select(i => i.ToString()).Aggregate((x, i) => x + " " + i)}");
            PathWriter.WritePath(path, @"C:\Workspace\Rosenbrock\notebook\gradient2-path1.csv");

            gd.SetPosition(new List<double>() { 0, 0 });
            sw.Reset();
            sw.Start();
            path = gd.Optimize(10000000, 0.00001, 0.00001);
            sw.Stop();
            Console.WriteLine($"path size: {path.Count}, " +
                $"elapsed time: {sw.ElapsedTicks}, " +
                $"result: {gd.CurrentPosition.Select(i => i.ToString()).Aggregate((x, i) => x + " " + i)}");
            PathWriter.WritePath(path, @"C:\Workspace\Rosenbrock\notebook\gradient2-path2.csv");

            gd.SetPosition(new List<double>() { -4, -4 });
            sw.Reset();
            sw.Start();
            path = gd.Optimize(10000000, 0.00001, 0.00001);
            sw.Stop();
            Console.WriteLine($"path size: {path.Count}, " +
                $"elapsed time: {sw.ElapsedTicks}, " +
                $"result: {gd.CurrentPosition.Select(i => i.ToString()).Aggregate((x, i) => x + " " + i)}");
            PathWriter.WritePath(path, @"C:\Workspace\Rosenbrock\notebook\gradient2-path3.csv");

            gd.SetPosition(new List<double>() { 4, 4, 4, 4, 4, 4, 4, 4 });
            sw.Reset();
            sw.Start();
            path = gd.Optimize(10000, 0.00001, 0.00001);
            sw.Stop();
            Console.WriteLine($"path size: {path.Count}, " +
                $"elapsed time: {sw.ElapsedTicks}, " +
                $"result: {gd.CurrentPosition.Select(i => i.ToString()).Aggregate((x, i) => x + " " + i)}");
            PathWriter.WritePath(path, @"C:\Workspace\Rosenbrock\notebook\gradient8-path1.csv");

            gd.SetPosition(new List<double>() { 0, 0, 0, 0, 0, 0, 0, 0 });
            sw.Reset();
            sw.Start();
            path = gd.Optimize(10000000, 0.00001, 0.00001);
            sw.Stop();
            Console.WriteLine($"path size: {path.Count}, " +
                $"elapsed time: {sw.ElapsedTicks}, " +
                $"result: {gd.CurrentPosition.Select(i => i.ToString()).Aggregate((x, i) => x + " " + i)}");
            PathWriter.WritePath(path, @"C:\Workspace\Rosenbrock\notebook\gradient8-path2.csv");

            gd.SetPosition(new List<double>() { -4, -4, 0, 8, 0, 1, 10, 5 });
            sw.Reset();
            sw.Start();
            path = gd.Optimize(10000000, 0.00001, 0.00001);
            sw.Stop();
            Console.WriteLine($"path size: {path.Count}, " +
                $"elapsed time: {sw.ElapsedTicks}, " +
                $"result: {gd.CurrentPosition.Select(i => i.ToString()).Aggregate((x, i) => x + " " + i)}");
            PathWriter.WritePath(path, @"C:\Workspace\Rosenbrock\notebook\gradient8-path3.csv");

            gd.SetPosition(new List<double>() { 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4 });
            sw.Reset();
            sw.Start();
            path = gd.Optimize(10000, 0.00001, 0.00001);
            sw.Stop();
            Console.WriteLine($"path size: {path.Count}, " +
                $"elapsed time: {sw.ElapsedTicks}, " +
                $"result: {gd.CurrentPosition.Select(i => i.ToString()).Aggregate((x, i) => x + " " + i)}");
            PathWriter.WritePath(path, @"C:\Workspace\Rosenbrock\notebook\gradient16-path1.csv");

            gd.SetPosition(new List<double>() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 });
            sw.Reset();
            sw.Start();
            path = gd.Optimize(10000000, 0.00001, 0.00001);
            sw.Stop();
            Console.WriteLine($"path size: {path.Count}, " +
                $"elapsed time: {sw.ElapsedTicks}, " +
                $"result: {gd.CurrentPosition.Select(i => i.ToString()).Aggregate((x, i) => x + " " + i)}");
            PathWriter.WritePath(path, @"C:\Workspace\Rosenbrock\notebook\gradient16-path2.csv");

            gd.SetPosition(new List<double>() { -4, -4, 0, 8, 0, 1, 10, 5, -7, 9, 20, 16, 2, -2, 0, 10 });
            sw.Reset();
            sw.Start();
            path = gd.Optimize(10000000, 0.00001, 0.00001);
            sw.Stop();
            Console.WriteLine($"path size: {path.Count}, " +
                $"elapsed time: {sw.ElapsedTicks}, " +
                $"result: {gd.CurrentPosition.Select(i => i.ToString()).Aggregate((x, i) => x + " " + i)}");
            PathWriter.WritePath(path, @"C:\Workspace\Rosenbrock\notebook\gradient16-path3.csv");
        }

        static void TestNadam()
        {
            var sw = new Stopwatch();

            var gd = new Nadam();

            gd.SetPosition(new List<double>() { 8, 4 });
            sw.Reset();
            sw.Start();
            var path = gd.Optimize(10000, 0.00001, 0.35);
            sw.Stop();
            Console.WriteLine($"path size: {path.Count}, " +
                $"elapsed time: {sw.ElapsedTicks}, " +
                $"result: {gd.CurrentPosition.Select(i => i.ToString()).Aggregate((x, i) => x + " " + i)}");
            PathWriter.WritePath(path, @"C:\Workspace\Rosenbrock\notebook\nadam2-path1.csv");

            gd.SetPosition(new List<double>() { 0, 0 });
            sw.Reset();
            sw.Start();
            path = gd.Optimize(10000000, 0.00001, 0.35);
            sw.Stop();
            Console.WriteLine($"path size: {path.Count}, " +
                $"elapsed time: {sw.ElapsedTicks}, " +
                $"result: {gd.CurrentPosition.Select(i => i.ToString()).Aggregate((x, i) => x + " " + i)}");
            PathWriter.WritePath(path, @"C:\Workspace\Rosenbrock\notebook\nadam2-path2.csv");

            gd.SetPosition(new List<double>() { -4, -4 });
            sw.Reset();
            sw.Start();
            path = gd.Optimize(10000000, 0.00001, 0.35);
            sw.Stop();
            Console.WriteLine($"path size: {path.Count}, " +
                $"elapsed time: {sw.ElapsedTicks}, " +
                $"result: {gd.CurrentPosition.Select(i => i.ToString()).Aggregate((x, i) => x + " " + i)}");
            PathWriter.WritePath(path, @"C:\Workspace\Rosenbrock\notebook\nadam2-path3.csv");

            gd.SetPosition(new List<double>() { 4, 4, 4, 4, 4, 4, 4, 4 });
            sw.Reset();
            sw.Start();
            path = gd.Optimize(10000, 0.00001, 10);
            sw.Stop();
            Console.WriteLine($"path size: {path.Count}, " +
                $"elapsed time: {sw.ElapsedTicks}, " +
                $"result: {gd.CurrentPosition.Select(i => i.ToString()).Aggregate((x, i) => x + " " + i)}");
            PathWriter.WritePath(path, @"C:\Workspace\Rosenbrock\notebook\nadam8-path1.csv");

            gd.SetPosition(new List<double>() { 0, 0, 0, 0, 0, 0, 0, 0 });
            sw.Reset();
            sw.Start();
            path = gd.Optimize(10000000, 0.00001, 10);
            sw.Stop();
            Console.WriteLine($"path size: {path.Count}, " +
                $"elapsed time: {sw.ElapsedTicks}, " +
                $"result: {gd.CurrentPosition.Select(i => i.ToString()).Aggregate((x, i) => x + " " + i)}");
            PathWriter.WritePath(path, @"C:\Workspace\Rosenbrock\notebook\nadam8-path2.csv");

            gd.SetPosition(new List<double>() { -4, -4, 0, 8, 0, 1, 10, 5 });
            sw.Reset();
            sw.Start();
            path = gd.Optimize(10000000, 0.00001, 10);
            sw.Stop();
            Console.WriteLine($"path size: {path.Count}, " +
                $"elapsed time: {sw.ElapsedTicks}, " +
                $"result: {gd.CurrentPosition.Select(i => i.ToString()).Aggregate((x, i) => x + " " + i)}");
            PathWriter.WritePath(path, @"C:\Workspace\Rosenbrock\notebook\nadam8-path3.csv");

            gd.SetPosition(new List<double>() { 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4 });
            sw.Reset();
            sw.Start();
            path = gd.Optimize(10000, 0.00001, 10);
            sw.Stop();
            Console.WriteLine($"path size: {path.Count}, " +
                $"elapsed time: {sw.ElapsedTicks}, " +
                $"result: {gd.CurrentPosition.Select(i => i.ToString()).Aggregate((x, i) => x + " " + i)}");
            PathWriter.WritePath(path, @"C:\Workspace\Rosenbrock\notebook\nadam16-path1.csv");

            gd.SetPosition(new List<double>() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 });
            sw.Reset();
            sw.Start();
            path = gd.Optimize(10000000, 0.00001, 10);
            sw.Stop();
            Console.WriteLine($"path size: {path.Count}, " +
                $"elapsed time: {sw.ElapsedTicks}, " +
                $"result: {gd.CurrentPosition.Select(i => i.ToString()).Aggregate((x, i) => x + " " + i)}");
            PathWriter.WritePath(path, @"C:\Workspace\Rosenbrock\notebook\nadam16-path2.csv");

            gd.SetPosition(new List<double>() { -4, -4, 0, 8, 0, 1, 10, 5, -7, 9, 20, 16, 2, -2, 0, 10 });
            sw.Reset();
            sw.Start();
            path = gd.Optimize(10000000, 0.00001, 10);
            sw.Stop();
            Console.WriteLine($"path size: {path.Count}, " +
                $"elapsed time: {sw.ElapsedTicks}, " +
                $"result: {gd.CurrentPosition.Select(i => i.ToString()).Aggregate((x, i) => x + " " + i)}");
            PathWriter.WritePath(path, @"C:\Workspace\Rosenbrock\notebook\nadam16-path3.csv");
        }


        static void TestParallelNadam()
        {
            var sw = new Stopwatch();

            var gd = new ParallelForNadam();

            gd.SetPosition(new List<double>() { 8, 4 });
            sw.Reset();
            sw.Start();
            var path = gd.Optimize(10000, 0.00001, 0.35);
            sw.Stop();
            Console.WriteLine($"path size: {path.Count}, " +
                $"elapsed time: {sw.ElapsedTicks}, " +
                $"result: {gd.CurrentPosition.Select(i => i.ToString()).Aggregate((x, i) => x + " " + i)}");
            PathWriter.WritePath(path, @"C:\Workspace\Rosenbrock\notebook\nadamp2-path1.csv");

            gd.SetPosition(new List<double>() { 0, 0 });
            sw.Reset();
            sw.Start();
            path = gd.Optimize(10000000, 0.00001, 0.35);
            sw.Stop();
            Console.WriteLine($"path size: {path.Count}, " +
                $"elapsed time: {sw.ElapsedTicks}, " +
                $"result: {gd.CurrentPosition.Select(i => i.ToString()).Aggregate((x, i) => x + " " + i)}");
            PathWriter.WritePath(path, @"C:\Workspace\Rosenbrock\notebook\nadamp2-path2.csv");

            gd.SetPosition(new List<double>() { -4, -4 });
            sw.Reset();
            sw.Start();
            path = gd.Optimize(10000000, 0.00001, 0.35);
            sw.Stop();
            Console.WriteLine($"path size: {path.Count}, " +
                $"elapsed time: {sw.ElapsedTicks}, " +
                $"result: {gd.CurrentPosition.Select(i => i.ToString()).Aggregate((x, i) => x + " " + i)}");
            PathWriter.WritePath(path, @"C:\Workspace\Rosenbrock\notebook\nadamp2-path3.csv");

            gd.SetPosition(new List<double>() { 4, 4, 4, 4, 4, 4, 4, 4 });
            sw.Reset();
            sw.Start();
            path = gd.Optimize(10000, 0.00001, 10);
            sw.Stop();
            Console.WriteLine($"path size: {path.Count}, " +
                $"elapsed time: {sw.ElapsedTicks}, " +
                $"result: {gd.CurrentPosition.Select(i => i.ToString()).Aggregate((x, i) => x + " " + i)}");
            PathWriter.WritePath(path, @"C:\Workspace\Rosenbrock\notebook\nadamp8-path1.csv");

            gd.SetPosition(new List<double>() { 0, 0, 0, 0, 0, 0, 0, 0 });
            sw.Reset();
            sw.Start();
            path = gd.Optimize(10000000, 0.00001, 10);
            sw.Stop();
            Console.WriteLine($"path size: {path.Count}, " +
                $"elapsed time: {sw.ElapsedTicks}, " +
                $"result: {gd.CurrentPosition.Select(i => i.ToString()).Aggregate((x, i) => x + " " + i)}");
            PathWriter.WritePath(path, @"C:\Workspace\Rosenbrock\notebook\nadamp8-path2.csv");

            gd.SetPosition(new List<double>() { -4, -4, 0, 8, 0, 1, 10, 5 });
            sw.Reset();
            sw.Start();
            path = gd.Optimize(10000000, 0.00001, 10);
            sw.Stop();
            Console.WriteLine($"path size: {path.Count}, " +
                $"elapsed time: {sw.ElapsedTicks}, " +
                $"result: {gd.CurrentPosition.Select(i => i.ToString()).Aggregate((x, i) => x + " " + i)}");
            PathWriter.WritePath(path, @"C:\Workspace\Rosenbrock\notebook\nadamp8-path3.csv");

            gd.SetPosition(new List<double>() { 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4 });
            sw.Reset();
            sw.Start();
            path = gd.Optimize(10000, 0.00001, 10);
            sw.Stop();
            Console.WriteLine($"path size: {path.Count}, " +
                $"elapsed time: {sw.ElapsedTicks}, " +
                $"result: {gd.CurrentPosition.Select(i => i.ToString()).Aggregate((x, i) => x + " " + i)}");
            PathWriter.WritePath(path, @"C:\Workspace\Rosenbrock\notebook\nadamp16-path1.csv");

            gd.SetPosition(new List<double>() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 });
            sw.Reset();
            sw.Start();
            path = gd.Optimize(10000000, 0.00001, 10);
            sw.Stop();
            Console.WriteLine($"path size: {path.Count}, " +
                $"elapsed time: {sw.ElapsedTicks}, " +
                $"result: {gd.CurrentPosition.Select(i => i.ToString()).Aggregate((x, i) => x + " " + i)}");
            PathWriter.WritePath(path, @"C:\Workspace\Rosenbrock\notebook\nadamp16-path2.csv");

            gd.SetPosition(new List<double>() { -4, -4, 0, 8, 0, 1, 10, 5, -7, 9, 20, 16, 2, -2, 0, 10 });
            sw.Reset();
            sw.Start();
            path = gd.Optimize(10000000, 0.00001, 10);
            sw.Stop();
            Console.WriteLine($"path size: {path.Count}, " +
                $"elapsed time: {sw.ElapsedTicks}, " +
                $"result: {gd.CurrentPosition.Select(i => i.ToString()).Aggregate((x, i) => x + " " + i)}");
            PathWriter.WritePath(path, @"C:\Workspace\Rosenbrock\notebook\nadamp16-path3.csv");
        }
    }


    public class ParallelNadams
    {
        private readonly Random random = new Random();

        private List<double> GeneratePoint(List<double> mins, List<double> maxs)
        {
            var len = mins.Count;
            return Enumerable.Range(0, len).Select(i => random.NextDouble() * (maxs[i] - mins[i]) + mins[i]).ToList();
        }

        public async Task<double[]> Optimize(int maxSteps, double eps, double stepSize, List<double> mins, List<double> maxs, int n)
        {
            var points = Enumerable.Range(0, n).Select(i => GeneratePoint(mins, maxs)).ToArray();
            var ts = new CancellationTokenSource();
            var tasks = points.Select(p => new Task<double[]>(() => OptimizationWork(p, maxSteps, eps, stepSize, ts.Token))).ToArray();
            foreach (var t in tasks) {
                t.Start();
            }
            var firstToFinish = await Task.WhenAny(tasks);
            ts.Cancel();
            return firstToFinish.Result;
        }

        public double[] OptimizationWork(List<double> startPosition, int maxSteps, double eps, double stepSize, CancellationToken ct)
        {
            var currentPosition = startPosition;
            var momentum = Enumerable.Range(0, currentPosition.Count).Select(x => 0d).ToList();
            var firstOrderMomentumEstimate = Enumerable.Range(0, currentPosition.Count).Select(x => 0d).ToList();
            var firstOrderMomentumEstimateCorrected = Enumerable.Range(0, currentPosition.Count).Select(x => 0d).ToList();
            var secondOrderMomentumEstimate = Enumerable.Range(0, currentPosition.Count).Select(x => 0d).ToList();
            var secondOrderMomentumEstimateCorrected = Enumerable.Range(0, currentPosition.Count).Select(x => 0d).ToList();
            var learningRate = stepSize;
            var momentumRemembrance = 0.9;
            var beta1 = 0.9;
            var beta2 = 0.9999;
            var e = 1e-10;

            var steps = new List<double[]>() { currentPosition.ToArray() };

            for (int step = 0; step < maxSteps; step++) {
                var grad = Rosenbrock.AntiGradientIn(currentPosition);

                for (int i = 0; i < currentPosition.Count; i++) {
                    momentum[i] = momentum[i] * momentumRemembrance + grad[i] * learningRate;
                    firstOrderMomentumEstimate[i] = beta1 * firstOrderMomentumEstimate[i] + (1 - beta1) * grad[i];
                    secondOrderMomentumEstimate[i] = beta2 * secondOrderMomentumEstimate[i] + (1 - beta2) * grad[i] * grad[i];
                    firstOrderMomentumEstimateCorrected[i] = firstOrderMomentumEstimate[i] / (1 - Math.Pow(beta1, step + 1));
                    secondOrderMomentumEstimateCorrected[i] = secondOrderMomentumEstimate[i] / (1 - Math.Pow(beta2, step + 1));

                    currentPosition[i] = currentPosition[i] +
                        learningRate / (Math.Sqrt(secondOrderMomentumEstimateCorrected[i]) + e) *
                        (beta1 * firstOrderMomentumEstimateCorrected[i] +
                            (1 - beta1) * grad[i] / (1 - Math.Pow(beta1, step + 1)));
                }

                steps.Add(currentPosition.ToArray());
                if (steps[steps.Count - 1].EuclideanDistance(steps[steps.Count - 2]) < eps || ct.IsCancellationRequested) {
                    break;
                }
            }
            return steps.Last();
        }

        public ParallelNadams()
        {
        }
    }
}
