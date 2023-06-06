namespace AvaloniaSortingVisualizer.Algorithms
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using AvaloniaSortingVisualizer.Models;
    using AvaloniaSortingVisualizer.Services;
    using AvaloniaSortingVisualizer.ViewModels;

    /// <summary>
    /// Implementation of the Cocktail Sort algorithm.
    /// </summary>
    public class CocktailSort : BubbleSort
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CocktailSort"/> class.
        /// </summary>
        /// <param name="soundService">Service that will be used for playing sounds.</param>
        public CocktailSort(ISoundService soundService)
            : base(soundService)
        {
        }

        /// <inheritdoc/>
        public override async Task RunRange(IList<SortableElementViewModel> items, int start, int end, CancellationToken token)
        {
            bool sorted = false;
            bool rightToLeft = false;
            int i = start,
                j = end;

            while (!sorted)
            {
                if (rightToLeft)
                {
                    sorted = !(await this.BubbleFromRightToLeft(items, i, j, token));
                    j--;
                    i++;
                }
                else
                {
                    sorted = !(await this.BubbleFromLeftToRight(items, i, j, token));
                }

                rightToLeft = !rightToLeft;
            }
        }

        /// <inheritdoc/>
        public override string ToString() => "Cocktail Sort";

        /// <summary>
        /// Performs the bubble sort algorithm by moving elements from right to left
        /// in the specified range.
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
        private async Task<bool> BubbleFromRightToLeft(IList<SortableElementViewModel> items, int left, int right, CancellationToken token)
        {
            bool changed = false;

            for (int i = right - 1; i > left; i--)
            {
                if (this.Compare(items[i], items[i - 1]) < 0)
                {
                    await this.Swap(items, i, i - 1, token);
                    changed = true;
                }
                else
                {
                    await this.UpdateBox(items, i, token);
                }
            }

            items[left].Status = SortableElementStatus.Sorted;

            return changed;
        }
    }
}
