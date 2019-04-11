using System;

namespace Rosenbrock
{
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
}
