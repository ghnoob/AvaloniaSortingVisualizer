namespace AvaloniaSortingVisualizer.Algorithms
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using AvaloniaSortingVisualizer.Services;
    using AvaloniaSortingVisualizer.ViewModels;

    /// <summary>
    /// Base class for out of place merge sort algorithms.
    /// </summary>
    public abstract class MergeSortBase : SortingAlgorithm
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MergeSortBase"/> class.
        /// </summary>
        /// <param name="soundService">Service to play sounds to help wih visualization.</param>
        protected MergeSortBase(ISoundService soundService)
            : base(soundService)
        {
        }

        /// <summary>
        /// Merges two sorted subarrays within the given list of sortable elements.
        /// </summary>
        /// <param name="items">The list of sortable elements.</param>
        /// <param name="start">The starting index of the first subarray.</param>
        /// <param name="mid">The ending index of the first subarray and the starting index of the second subarray.</param>
        /// <param name="end">The ending index of the second subarray.</param>
        /// <param name="tmpArray">A temporary array used for merging.</param>
        /// <param name="token">A cancellation token to allow the operation to be canceled.</param>
        /// <returns>A <see cref="Task"/> representing the operations.</returns>
        protected async Task Merge(IList<SortableElementViewModel> items, int start, int mid, int end, double[] tmpArray, CancellationToken token)
        {
            if (this.Compare(items[mid - 1], items[mid]) <= 0)
            {
                return;
            }

            int i = start,
                j = mid;
            for (int k = start; k < end; k++)
            {
                tmpArray[k] = i < mid && (j >= end || this.Compare(items[i], items[j]) <= 0) ? items[i++].Value : items[j++].Value;
            }

            for (int k = start; k < end; k++)
            {
                items[k].Value = tmpArray[k];
                await this.UpdateBox(items, k, token);
            }
        }
    }
}
