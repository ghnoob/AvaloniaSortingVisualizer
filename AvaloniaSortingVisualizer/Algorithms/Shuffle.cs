using AvaloniaSortingVisualizer.Services;

namespace AvaloniaSortingVisualizer.Algorithms
{
    /// <summary>
    /// Base class for all algorithms that involve shuffling
    /// </summary>
    public abstract class Shuffle : Algorithm
    {
        protected Shuffle(ISoundService soundService)
            : base(soundService) { }
    }
}
