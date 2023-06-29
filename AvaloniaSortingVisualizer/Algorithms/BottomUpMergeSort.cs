namespace AvaloniaSortingVisualizer.Algorithms
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using AvaloniaSortingVisualizer.Services;
    using AvaloniaSortingVisualizer.ViewModels;

    /// <summary>
    /// Bottom up merge sort implementation.
    /// </summary>
    public class BottomUpMergeSort : MergeSortBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BottomUpMergeSort"/> class.
        /// </summary>
        /// <param name="soundService">Service to play sounds to help wih visualization.</param>
        public BottomUpMergeSort(ISoundService soundService)
            : base(soundService)
        {
        }

        /// <inheritdoc/>
        public async override Task RunRange(IList<SortableElementViewModel> items, int start, int end, CancellationToken token)
        {
            double[] tmpArray = new double[end - start];

            for (int width = 1; width < end - start; width *= 2)
            {
                for (int i = start; i < end - width; i += width * 2)
                {
                    await this.Merge(items, i, i + width, Math.Min(i + (width * 2), end), tmpArray, token);
                }
            }
        }

        /// <inheritdoc/>
        public override string ToString() => "Bottom-up Merge Sort";
    }
}
