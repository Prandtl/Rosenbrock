using System.Collections.Generic;
using System.Linq;

namespace Rosenbrock
{
    public class NAG
    {
        public List<double> CurrentPosition { get; private set; }

        public void SetPosition(List<double> position)
        {
            CurrentPosition = position;
        }

        public List<double[]> Optimize(int maxSteps, double eps, double stepSize)
        {
            var momentum = Enumerable.Range(0, CurrentPosition.Count).Select(x => 0d).ToList();
            var prediction = Enumerable.Range(0, CurrentPosition.Count).Select(x => 0d).ToList();
            var learningRate = stepSize;
            var momentumRemembrance = 0.9;

            var steps = new List<double[]>() { CurrentPosition.ToArray() };

            for (int step = 0; step < maxSteps; step++) {
                for (int i = 0; i < CurrentPosition.Count; i++) {
                    prediction[i] = CurrentPosition[i] - momentum[i] * momentumRemembrance;
                }

                var grad = Rosenbrock.AntiGradientIn(prediction);

                for (int i = 0; i < CurrentPosition.Count; i++) {
                    momentum[i] = momentum[i] * momentumRemembrance + grad[i] * learningRate;
                    CurrentPosition[i] = CurrentPosition[i] + momentum[i];
                }

                steps.Add(CurrentPosition.ToArray());
                if (steps[steps.Count - 1].EuclideanDistance(steps[steps.Count - 2]) < eps) {
                    break;
                }
            }
            return steps;
        }

        public NAG()
        {
            CurrentPosition = new List<double>();
        }
    }
}
