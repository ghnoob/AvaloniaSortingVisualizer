namespace AvaloniaSortingVisualizer.Algorithms
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using AvaloniaSortingVisualizer.Models;
    using AvaloniaSortingVisualizer.Services;
    using AvaloniaSortingVisualizer.ViewModels;

    /// <summary>
    /// Selection sort implementation.
    /// </summary>
    public class SelectionSort : SortingAlgorithm
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SelectionSort"/> class.
        /// </summary>
        /// <param name="soundService">
        /// Service to play sounds to complement the algorithm visualization.
        /// </param>
        public SelectionSort(ISoundService soundService)
            : base(soundService)
        {
        }

        /// <inheritdoc/>
        public override async Task RunRange(IList<SortableElementViewModel> items, int start, int end, CancellationToken token)
        {
            for (int i = start; i < end - 1; i++)
            {
                int minIndex = i;
                items[minIndex].Status = SortableElementStatus.Tracked;
                for (int j = i + 1; j < end; j++)
                {
                    await this.UpdateBox(items, j, token);
                    if (this.Compare(items[j], items[minIndex]) < 0)
                    {
                        items[minIndex].Status = SortableElementStatus.Normal;
                        items[j].Status = SortableElementStatus.Tracked;
                        minIndex = j;
                    }
                }

                items[minIndex].Status = SortableElementStatus.Normal;
                if (minIndex != i)
                {
                    await this.Swap(items, i, minIndex, token);
                }

                items[i].Status = SortableElementStatus.Sorted;
            }
        }

        /// <inheritdoc/>
        public override string ToString() => "Selection Sort";
    }
}
