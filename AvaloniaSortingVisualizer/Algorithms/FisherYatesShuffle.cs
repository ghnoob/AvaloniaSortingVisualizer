using System.Threading.Tasks;
using AvaloniaSortingVisualizer.Services;

using System;

namespace AvaloniaSortingVisualizer.Algorithms
{
    /// <summary>
    /// Implementation of the Fisher-Yates shuffle algorithm
    /// </summary>
    public class FisherYatesShuffle : Shuffle
    {
        /// <summary>
        /// Gets the random number generator (RNG)
        /// </summary>
        private Random rng { get; }

        public FisherYatesShuffle(ISoundService soundService)
            : base(soundService)
        {
            rng = new Random();
        }

        public override async Task RunRange(int start, int end)
        {
            for (int i = start; i < end - 1; i++)
            {
                int j = rng.Next(i, end);
                await Swap(i, j);
            }
        }

        public override string ToString() => "Fisher-Yates Shuffle";
    }
}
