namespace AvaloniaSortingVisualizer.Algorithms
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using AvaloniaSortingVisualizer.Models;
    using AvaloniaSortingVisualizer.Services;
    using AvaloniaSortingVisualizer.ViewModels;

    /// <summary>
    /// Binary Insertion Sort implementation.
    /// </summary>
    public class BinaryInsertionSort : SortingAlgorithm
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BinaryInsertionSort"/> class.
        /// </summary>
        /// <param name="soundService">Service that will be used for playing sounds.</param>
        public BinaryInsertionSort(ISoundService soundService)
            : base(soundService)
        {
        }

        /// <inheritdoc/>
        public override async Task RunRange(IList<SortableElementViewModel> items, int start, int end, CancellationToken token)
        {
            for (int i = start + 1; i < end; i++)
            {
                double tempValue = items[i].Value;

                int target = await this.BinarySearch(items, items[i], start, i, token);
                SortableElementViewModel tmpTarget = items[target];
                tmpTarget.Status = SortableElementStatus.Tracked;

                for (int j = i; j > target; j--)
                {
                    items[j].Value = items[j - 1].Value;
                    await this.UpdateBox(items, j, token);
                }

                tmpTarget.Status = SortableElementStatus.Normal;
                items[target].Value = tempValue;
                await this.UpdateBox(items, target, token);
            }
        }

        /// <inheritdoc/>
        public override string ToString() => "Binary Insertion Sort";

        /// <summary>
        /// Gets the index of the collection where the value should be inserted.
        /// </summary>
        /// <param name="items">Collection to search into.</param>
        /// <param name="value">Value to search for.</param>
        /// <param name="start">Start index of the search (inclusive).</param>
        /// <param name="end">End index of the search (exclusive).</param>
        /// <param name="token">
        /// A cancellation token that can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A see <see cref="Task"/> that contains the index of the found position.
        /// </returns>
        private async Task<int> BinarySearch(IList<SortableElementViewModel> items, SortableElementViewModel value, int start, int end, CancellationToken token)
        {
            int i = start,
                j = end - 1;

            while (i <= j)
            {
                int mid = (i + j) / 2;

                await this.UpdateBox(items, i, mid, j, token);

                if (this.Compare(value, items[mid]) >= 0)
                {
                    i = mid + 1;
                }
                else
                {
                    j = mid - 1;
                }
            }

            return i;
        }
    }
}
