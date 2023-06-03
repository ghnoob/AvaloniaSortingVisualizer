namespace AvaloniaSortingVisualizer.Algorithms
{
    using AvaloniaSortingVisualizer.Services;

    /// <summary>
    /// Base class for all algorithms that involve shuffling.
    /// </summary>
    public abstract class Shuffle : Algorithm
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Shuffle"/> class.
        /// </summary>
        /// <param name="soundService">Service to play sounds to help wih visualization.</param>
        protected Shuffle(ISoundService soundService)
            : base(soundService)
        {
        }
    }
}
