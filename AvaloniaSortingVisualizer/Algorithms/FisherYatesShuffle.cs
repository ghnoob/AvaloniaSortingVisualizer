namespace AvaloniaSortingVisualizer.Algorithms
{
    using System;
    using System.Threading.Tasks;
    using AvaloniaSortingVisualizer.Services;

    /// <summary>
    /// Implementation of the Fisher-Yates shuffle algorithm.
    /// </summary>
    public class FisherYatesShuffle : Shuffle
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FisherYatesShuffle"/> class.
        /// </summary>
        /// <param name="soundService">Service to play sounds to help wih visualization.</param>
        public FisherYatesShuffle(ISoundService soundService)
            : base(soundService)
        {
            this.Rng = new Random();
        }

        /// <summary>
        /// Gets the random number generator (RNG).
        /// </summary>
        private Random Rng { get; }

        /// <inheritdoc/>
        public override async Task RunRange(int start, int end)
        {
            for (int i = start; i < end - 1; i++)
            {
                int j = this.Rng.Next(i, end);
                await this.Swap(i, j);
            }
        }

        /// <inheritdoc/>
        public override string ToString() => "Fisher-Yates Shuffle";
    }
}
