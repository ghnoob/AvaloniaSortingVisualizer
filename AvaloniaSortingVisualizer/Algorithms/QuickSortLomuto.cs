namespace AvaloniaSortingVisualizer.Algorithms
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using AvaloniaSortingVisualizer.Models;
    using AvaloniaSortingVisualizer.Services;
    using AvaloniaSortingVisualizer.ViewModels;

    /// <summary>
    /// Quicksort implementation (Lomuto's partition method).
    /// </summary>
    public class QuickSortLomuto : SortingAlgorithm
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QuickSortLomuto"/> class.
        /// </summary>
        /// <param name="soundService">Service to play sounds to help wih visualization.</param>
        public QuickSortLomuto(ISoundService soundService)
            : base(soundService)
        {
        }

        /// <inheritdoc/>
        public override Task RunRange(IList<SortableElementViewModel> items, int start, int end, CancellationToken token) => this.QuickSort(items, start, end - 1, token);

        /// <inheritdoc/>
        public override string ToString() => "Quick Sort (Lomuto's partition)";

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

            await this.Swap(items, mid, hi, token);

            return items[hi];
        }

        private async Task<int> Partition(IList<SortableElementViewModel> items, int lo, int hi, CancellationToken token)
        {
            SortableElementViewModel pivot = await this.MedianOfThree(items, lo, hi, token);
            pivot.Status = SortableElementStatus.Tracked;

            int j = lo;

            for (int i = lo; i < hi; i++)
            {
                if (this.Compare(items[i], pivot) <= 0)
                {
                    items[j].Status = SortableElementStatus.Normal;
                    await this.Swap(items, i, j, token);
                    j++;
                }
                else
                {
                    // just for visualization, not really necessary for the algo to work
                    items[j].Status = SortableElementStatus.Tracked;
                    await this.UpdateBox(items, i, token);
                }
            }

            pivot.Status = SortableElementStatus.Normal;
            items[j].Status = SortableElementStatus.Normal;
            await this.Swap(items, j, hi, token);
            return j;
        }

        private async Task QuickSort(IList<SortableElementViewModel> items, int lo, int hi, CancellationToken token)
        {
            if (lo < hi)
            {
                int p = await this.Partition(items, lo, hi, token);

                await this.QuickSort(items, lo, p - 1, token);
                await this.QuickSort(items, p + 1, hi, token);
            }
        }
    }
}
