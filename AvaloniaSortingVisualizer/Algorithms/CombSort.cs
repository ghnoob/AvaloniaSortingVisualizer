namespace AvaloniaSortingVisualizer.Algorithms
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using AvaloniaSortingVisualizer.Services;
    using AvaloniaSortingVisualizer.ViewModels;

    /// <summary>
    /// Comb sort implementation.
    /// </summary>
    public class CombSort : BubbleSort
    {
        private const double ShrinkFactor = 1.3;

        /// <summary>
        /// Initializes a new instance of the <see cref="CombSort"/> class.
        /// </summary>
        /// <param name="soundService">
        /// Service to play sounds to help wih visualization.
        /// </param>
        public CombSort(ISoundService soundService)
            : base(soundService)
        {
        }

        /// <inheritdoc/>
        public override async Task RunRange(IList<SortableElementViewModel> items, int start, int end, CancellationToken token)
        {
            for (
                int gap = (int)((end - start) / ShrinkFactor);
                gap > 1;
                gap = (int)(gap / ShrinkFactor))
            {
                await this.BubbleFromLeftToRight(items, start, end, gap, token);
            }

            await base.RunRange(items, start, end, token);
        }

        /// <inheritdoc/>
        public override string ToString() => "Comb Sort";
    }
}
