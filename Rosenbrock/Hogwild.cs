using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rosenbrock
{
    public class Hogwild
    {
        public List<double> CurrentPosition { get; private set; }

        public void SetPosition(List<double> position)
        {
            CurrentPosition = position;
        }

        private int Step { get; set; }
        private int MaxStep { get; set; }
        private double LearningRate { get; set; }
        private double Eps { get; set; }
        private bool Stop { get; set; }

        private readonly Random Random = new Random();

        public double[] Optimize(int maxSteps, double eps, double stepSize, int taskNumber = 8)
        {
            LearningRate = stepSize;
            Stop = false;
            Step = 0;
            MaxStep = maxSteps;

            var tasks = Enumerable.Range(0, taskNumber).Select(i => new Task(() => Work(2))).ToArray();
            foreach(var t in tasks) {
                t.Start();
            }
            Task.WaitAll(tasks);

            return CurrentPosition.ToArray();
        }

        private void Work(int updatesAmount)
        {
            var dim = CurrentPosition.Count;
            var position = CurrentPosition.ToArray();

            while (Step < MaxStep) {
                if (Stop) {
                    return;
                }

                var updates = Enumerable.Range(0, updatesAmount).Select(i => Random.Next(dim)).Distinct();
                var previous = position;
                foreach (var i in updates) {
                    CurrentPosition[i] -= Rosenbrock.PartialDiffIn(i, CurrentPosition) * LearningRate;
                }
                position = CurrentPosition.ToArray();
                if (position.EuclideanDistance(previous) < Eps) {
                    Stop = true;
                }
                Step++;
            }
        }

        public Hogwild()
        {
            CurrentPosition = new List<double>();
        }
    }
}
