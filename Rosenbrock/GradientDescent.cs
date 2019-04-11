using System.Collections.Generic;

namespace Rosenbrock
{
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
}
