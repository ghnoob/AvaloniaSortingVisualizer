namespace AvaloniaSortingVisualizer.Algorithms
{
    using System.Threading.Tasks;
    using AvaloniaSortingVisualizer.Models;
    using AvaloniaSortingVisualizer.Services;

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
        public override async Task RunRange(int start, int end)
        {
            bool sorted;
            int i = end;

            do
            {
                sorted = !(await this.BubbleFromLeftToRight(start, i));
                i--;
            }
            while (!sorted);
        }

        /// <inheritdoc/>
        public override string ToString() => "Bubble Sort";

        /// <summary>
        /// Performs the bubble sort algorithm by moving elements from left to right
        /// in the specified range.
        /// </summary>
        /// <param name="left">The starting index of the range (inclusive).</param>
        /// <param name="right">The ending index of the range (exclusive).</param>
        /// <returns>
        /// A task representing the asynchronous operation.
        /// The task result is a boolean value indicating whether any elements were changed
        /// during the sorting process.
        /// </returns>
        protected async Task<bool> BubbleFromLeftToRight(int left, int right)
        {
            bool changed = false;

            for (int i = left; i < right - 1; i++)
            {
                if (this.Compare(this.Items[i], this.Items[i + 1]) > 0)
                {
                    await this.Swap(i, i + 1);
                    changed = true;
                }
                else
                {
                    await this.UpdateBox(i);
                }
            }

            this.Items[right - 1].Status = SortableElementStatus.Sorted;

            return changed;
        }
    }
}
