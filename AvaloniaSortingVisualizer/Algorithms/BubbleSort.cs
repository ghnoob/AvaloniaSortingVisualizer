namespace AvaloniaSortingVisualizer.Algorithms
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using AvaloniaSortingVisualizer.Models;
    using AvaloniaSortingVisualizer.Services;
    using AvaloniaSortingVisualizer.ViewModels;

    /// <summary>
    /// Implementation of the bubble sort algorithm.
    /// </summary>
    public class BubbleSort : SortingAlgorithm
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BubbleSort"/> class.
        /// </summary>
        /// <param name="soundService">Service that will be used for playing sounds.</param>
        public BubbleSort(ISoundService soundService)
            : base(soundService)
        {
        }

        /// <inheritdoc/>
        public override async Task RunRange(IList<SortableElementViewModel> items, int start, int end, CancellationToken token)
        {
            bool sorted;
            int i = end;

            do
            {
                sorted = !(await this.BubbleFromLeftToRight(items, start, i, token));
                i--;
            }
            while (!sorted);
        }

        /// <inheritdoc/>
        public override string ToString() => "Bubble Sort";

        /// <summary>
        /// Performs the bubble sort algorithm by moving elements from left to right
        /// in the specified range of a collection.
        /// </summary>
        /// <param name="items">The collection of items.</param>
        /// <param name="left">The starting index of the range (inclusive).</param>
        /// <param name="right">The ending index of the range (exclusive).</param>
        /// <param name="token">Token to cancel the operation.</param>
        /// <returns>
        /// A task representing the asynchronous operation.
        /// The task result is a boolean value indicating whether any elements were changed
        /// during the sorting process.
        /// </returns>
        protected async Task<bool> BubbleFromLeftToRight(IList<SortableElementViewModel> items, int left, int right, CancellationToken token)
        {
            bool changed = await this.BubbleFromLeftToRight(items, left, right, 1, token);
            items[right - 1].Status = SortableElementStatus.Sorted;
            return changed;
        }

        /// <summary>
        /// Performs the gapped bubble sort algorithm by moving elements from left to right
        /// in the specified range of a collection.
        /// </summary>
        /// <param name="items">The collection of items.</param>
        /// <param name="left">The starting index of the range (inclusive).</param>
        /// <param name="right">The ending index of the range (exclusive).</param>
        /// <param name="gap">Width of the gap.</param>
        /// <param name="token">Token to cancel the operation.</param>
        /// <returns>
        /// A task representing the asynchronous operation.
        /// The task result is a boolean value indicating whether any elements were changed
        /// during the sorting process.
        /// </returns>
        protected async Task<bool> BubbleFromLeftToRight(IList<SortableElementViewModel> items, int left, int right, int gap, CancellationToken token)
        {
            bool changed = false;

            for (int i = left; i < right - gap; i++)
            {
                if (this.Compare(items[i], items[i + gap]) > 0)
                {
                    await this.Swap(items, i, i + gap, token);
                    changed = true;
                }
                else
                {
                    await this.UpdateBox(items, i, token);
                }
            }

            return changed;
        }
    }
}
