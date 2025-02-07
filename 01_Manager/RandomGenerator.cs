using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamRPG_17
{
    public class RandomGenerator
    {
        private static readonly RandomGenerator instance = new RandomGenerator();
        private readonly Random random;

        private RandomGenerator()
        {
            random = new Random();
        }

        public static RandomGenerator Instance
        {
            get { return instance; }
        }

        public int Next(int MaxValue)
        {
            return random.Next(MaxValue);
        }

        public int Next(int minValue, int maxValue)
        {
            return random.Next(minValue, maxValue);
        }

        public double NextDouble()
        {
            return random.NextDouble();
        }
    }
}
