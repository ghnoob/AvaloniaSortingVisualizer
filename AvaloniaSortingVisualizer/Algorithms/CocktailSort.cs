namespace AvaloniaSortingVisualizer.Algorithms
{
    using System.Threading.Tasks;
    using AvaloniaSortingVisualizer.Models;
    using AvaloniaSortingVisualizer.Services;

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
        public override async Task RunRange(int start, int end)
        {
            bool sorted = false;
            bool rightToLeft = false;
            int i = start,
                j = end;

            while (!sorted)
            {
                if (rightToLeft)
                {
                    sorted = !(await this.BubbleFromRightToLeft(i, j));
                    j--;
                    i++;
                }
                else
                {
                    sorted = !(await this.BubbleFromLeftToRight(i, j));
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
        /// <param name="left">The starting index of the range (inclusive).</param>
        /// <param name="right">The ending index of the range (exclusive).</param>
        /// <returns>
        /// A task representing the asynchronous operation.
        /// The task result is a boolean value indicating whether any elements were changed
        /// during the sorting process.
        /// </returns>
        private async Task<bool> BubbleFromRightToLeft(int left, int right)
        {
            bool changed = false;

            for (int i = right - 1; i > left; i--)
            {
                if (this.Compare(this.Items[i], this.Items[i - 1]) < 0)
                {
                    await this.Swap(i, i - 1);
                    changed = true;
                }
                else
                {
                    await this.UpdateBox(i);
                }
            }

            this.Items[left].Status = SortableElementStatus.Sorted;

            return changed;
        }
    }
}
