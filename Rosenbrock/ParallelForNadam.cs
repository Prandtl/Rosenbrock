using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rosenbrock
{
    public class ParallelForNadam
    {
        public List<double> CurrentPosition { get; private set; }

        public void SetPosition(List<double> position)
        {
            CurrentPosition = position;
        }

        public List<double[]> Optimize(int maxSteps, double eps, double stepSize)
        {
            var momentum = Enumerable.Range(0, CurrentPosition.Count).Select(x => 0d).ToList();
            var firstOrderMomentumEstimate = Enumerable.Range(0, CurrentPosition.Count).Select(x => 0d).ToList();
            var firstOrderMomentumEstimateCorrected = Enumerable.Range(0, CurrentPosition.Count).Select(x => 0d).ToList();
            var secondOrderMomentumEstimate = Enumerable.Range(0, CurrentPosition.Count).Select(x => 0d).ToList();
            var secondOrderMomentumEstimateCorrected = Enumerable.Range(0, CurrentPosition.Count).Select(x => 0d).ToList();
            var learningRate = stepSize;
            var momentumRemembrance = 0.9;
            var beta1 = 0.9;
            var beta2 = 0.9999;
            var e = 1e-10;

            var steps = new List<double[]>() { CurrentPosition.ToArray() };
            var options = new ParallelOptions();
            options.MaxDegreeOfParallelism = 4;

            for (int step = 0; step < maxSteps; step++) {
                var grad = Rosenbrock.AntiGradientIn(CurrentPosition);
                Parallel.For(0, CurrentPosition.Count, i => {
                    momentum[i] = momentum[i] * momentumRemembrance + grad[i] * learningRate;
                    firstOrderMomentumEstimate[i] = beta1 * firstOrderMomentumEstimate[i] + (1 - beta1) * grad[i];
                    secondOrderMomentumEstimate[i] = beta2 * secondOrderMomentumEstimate[i] + (1 - beta2) * grad[i] * grad[i];
                    firstOrderMomentumEstimateCorrected[i] = firstOrderMomentumEstimate[i] / (1 - Math.Pow(beta1, step + 1));
                    secondOrderMomentumEstimateCorrected[i] = secondOrderMomentumEstimate[i] / (1 - Math.Pow(beta2, step + 1));

                    CurrentPosition[i] = CurrentPosition[i] +
                        learningRate / (Math.Sqrt(secondOrderMomentumEstimateCorrected[i]) + e) *
                        (beta1 * firstOrderMomentumEstimateCorrected[i] +
                            (1 - beta1) * grad[i] / (1 - Math.Pow(beta1, step + 1)));
                });


                steps.Add(CurrentPosition.ToArray());
                if (steps[steps.Count - 1].EuclideanDistance(steps[steps.Count - 2]) < eps) {
                    break;
                }
            }
            return steps;
        }

        public ParallelForNadam()
        {
            CurrentPosition = new List<double>();
        }
    }
}
