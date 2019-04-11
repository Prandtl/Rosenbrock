using System;
using System.Collections.Generic;
using System.Linq;

namespace Rosenbrock
{
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

        public static double PartialDiffIn(int i, List<double> vec)
        {
            if (i == 0) {
                return 400 * Math.Pow(vec[0], 3) - 400 * vec[1] + 2 * vec[0] - 2;
            }
            var dim = vec.Count;
            if (i == dim - 1) {
                return 200 * vec[dim - 1] - 200 * vec[dim - 2];
            }
            return 400 * Math.Pow(vec[i], 3) - 200 * Math.Pow(vec[i - 1], 2) - 400 * vec[i + 1] + 202 * vec[i] - 2;
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
