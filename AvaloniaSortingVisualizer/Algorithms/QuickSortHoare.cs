namespace AvaloniaSortingVisualizer.Algorithms
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using AvaloniaSortingVisualizer.Models;
    using AvaloniaSortingVisualizer.Services;
    using AvaloniaSortingVisualizer.ViewModels;

    /// <summary>
    /// Quicksort implementation (Hoare's partition method).
    /// </summary>
    public class QuickSortHoare : SortingAlgorithm
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QuickSortHoare"/> class.
        /// </summary>
        /// <param name="soundService">Service to play sounds to help wih visualization.</param>
        public QuickSortHoare(ISoundService soundService)
            : base(soundService)
        {
        }

        /// <inheritdoc/>
        public override Task RunRange(IList<SortableElementViewModel> items, int start, int end, CancellationToken token) => this.QuickSort(items, start, end - 1, token);

        /// <inheritdoc/>
        public override string ToString() => "Quick Sort (Hoare's partition)";

        /// <summary>
        /// Partitions the given list of sortable elements within the specified range using the quicksort algorithm.
        /// </summary>
        /// <param name="items">The list of sortable elements to partition.</param>
        /// <param name="lo">The starting index of the range to partition.</param>
        /// <param name="hi">The ending index of the range to partition.</param>
        /// <param name="token">A cancellation token to allow the operation to be canceled.</param>
        /// <returns>The index of the pivot element after partitioning.</returns>
        public async Task<int> Partition(IList<SortableElementViewModel> items, int lo, int hi, CancellationToken token)
        {
            SortableElementViewModel pivot = await this.MedianOfThree(items, lo, hi, token);
            pivot.Status = SortableElementStatus.Tracked;

            int i = lo - 1,
                j = hi + 1;

            while (true)
            {
                do
                {
                    i++;
                    await this.UpdateBox(items, i, token);
                }
                while (this.Compare(items[i], pivot) < 0);

                items[i].Status = SortableElementStatus.Tracked;

                do
                {
                    j--;
                    await this.UpdateBox(items, j, token);
                }
                while (this.Compare(items[j], pivot) > 0);

                items[i].Status = SortableElementStatus.Normal;

                if (i >= j)
                {
                    pivot.Status = SortableElementStatus.Normal;
                    return j;
                }

                await this.Swap(items, i, j, token);
            }
        }

        private async Task<SortableElementViewModel> MedianOfThree(IList<SortableElementViewModel> items, int lo, int hi, CancellationToken token)
        {
            int mid = (lo + hi) / 2;

            if (this.Compare(items[lo], items[mid]) > 0)
            {
                await this.Swap(items, lo, mid, token);
            }

            if (this.Compare(items[lo], items[hi]) > 0)
            {
                await this.Swap(items, lo, hi, token);
            }

            if (this.Compare(items[mid], items[hi]) > 0)
            {
                await this.Swap(items, mid, hi, token);
            }

            return items[mid];
        }

        private async Task QuickSort(IList<SortableElementViewModel> items, int lo, int hi, CancellationToken token)
        {
            if (lo >= hi)
            {
                return;
            }

            if (hi - lo < 3)
            {
                await this.MedianOfThree(items, lo, hi, token);
                return;
            }

            int p = await this.Partition(items, lo, hi, token);

            await this.QuickSort(items, lo, p, token);
            await this.QuickSort(items, p + 1, hi, token);
        }
    }
}
