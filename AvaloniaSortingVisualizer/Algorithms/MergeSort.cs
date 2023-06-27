namespace AvaloniaSortingVisualizer.Algorithms
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using AvaloniaSortingVisualizer.Services;
    using AvaloniaSortingVisualizer.ViewModels;

    /// <summary>
    /// Top down merge sort implementation.
    /// </summary>
    public class MergeSort : MergeSortBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MergeSort"/> class.
        /// </summary>
        /// <param name="soundService">Service to play sounds to help wih visualization.</param>
        public MergeSort(ISoundService soundService)
            : base(soundService)
        {
        }

        /// <inheritdoc/>
        public override Task RunRange(IList<SortableElementViewModel> items, int start, int end, CancellationToken token)
        {
            double[] tmpArray = new double[end - start];

            return this.RunMergeSort(items, start, end, tmpArray, token);
        }

        /// <inheritdoc/>
        public override string ToString() => "Merge Sort";

        private async Task RunMergeSort(IList<SortableElementViewModel> items, int start, int end, double[] tmpArray, CancellationToken token)
        {
            if (start < end - 1)
            {
                int mid = (start + end) / 2;

                await this.RunMergeSort(items, start, mid, tmpArray, token);
                await this.RunMergeSort(items, mid, end, tmpArray, token);
                await this.Merge(items, start, mid, end, tmpArray, token);
            }
        }
    }
}
