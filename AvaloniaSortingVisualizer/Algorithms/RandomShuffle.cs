using System;
using AvaloniaSortingVisualizer.Services;

namespace AvaloniaSortingVisualizer.Algorithms
{
    /// <summary>
    /// Base class for all shuffle algorithms that involve randomness
    /// </summary>
    public abstract class RandomShuffle : Shuffle
    {
        /// <summary>
        /// Gets the random number generator (RNG)
        /// </summary>
        protected Random Rng { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Shuffle"/> class.
        /// </summary>
        protected RandomShuffle(ISoundService soundService)
            : base(soundService)
        {
            Rng = new Random();
        }
    }
}
