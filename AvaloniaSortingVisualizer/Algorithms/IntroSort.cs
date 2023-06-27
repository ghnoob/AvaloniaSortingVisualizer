namespace AvaloniaSortingVisualizer.Algorithms
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using AvaloniaSortingVisualizer.Models;
    using AvaloniaSortingVisualizer.Services;
    using AvaloniaSortingVisualizer.ViewModels;

    /// <summary>
    /// Intro sort implementation.
    /// </summary>
    public class IntroSort : SortingAlgorithm
    {
        private readonly InsertionSort insertionSort;
        private readonly HeapSort heapSort;
        private readonly QuickSortHoare quickSort;

        /// <summary>
        /// Initializes a new instance of the <see cref="IntroSort"/> class.
        /// </summary>
        /// <param name="soundService">Service to play sounds to help wih visualization.</param>
        /// <param name="insertionSort">Insertion sort algorithm to be used by this algorithm.</param>
        /// <param name="heapSort">Heap sort algorithm to be used by this algorithm.</param>
        /// <param name="quickSort">Quick sort algorithm to be used by this algorithm.</param>
        public IntroSort(ISoundService soundService, InsertionSort insertionSort, HeapSort heapSort, QuickSortHoare quickSort)
            : base(soundService)
        {
            this.insertionSort = insertionSort;
            this.heapSort = heapSort;
            this.quickSort = quickSort;
        }

        /// <inheritdoc/>
        public override Task RunRange(IList<SortableElementViewModel> items, int start, int end, CancellationToken token)
        {
            int depthLimit = 2 * (int)Math.Log2(end - start);
            return this.RunIntroSort(items, start, end - 1, depthLimit, token);
        }

        /// <inheritdoc/>
        public override string ToString() => "Intro Sort";

        private async Task RunIntroSort(IList<SortableElementViewModel> items, int lo, int hi, int depthLimit, CancellationToken token)
        {
            int size = hi - lo;

            if (size < 16)
            {
                await this.insertionSort.RunRange(items, lo, hi + 1, token);

                for (int i = lo; i <= hi; i++)
                {
                    items[i].Status = SortableElementStatus.Sorted;
                }
            }
            else if (depthLimit < 1)
            {
                await this.heapSort.RunRange(items, lo, hi + 1, token);
                items[lo].Status = SortableElementStatus.Sorted;
            }
            else
            {
                int p = await this.quickSort.Partition(items, lo, hi, token);

                depthLimit--;
                await this.RunIntroSort(items, lo, p, depthLimit, token);
                await this.RunIntroSort(items, p + 1, hi, depthLimit, token);
            }
        }
    }
}
