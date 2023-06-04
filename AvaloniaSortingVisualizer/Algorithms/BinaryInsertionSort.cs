namespace AvaloniaSortingVisualizer.Algorithms
{
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
        public async override Task RunRange(int start, int end)
        {
            for (int i = start + 1; i < end; i++)
            {
                SortableElementViewModel temp = this.Items[i];

                int target = await this.BinarySearch(temp, start, i);
                this.Items[target].Status = SortableElementStatus.Tracked;

                for (int j = i; j > target; j--)
                {
                    this.Items[j] = this.Items[j - 1];
                    await this.UpdateBox(j);
                }

                this.Items[target].Status = SortableElementStatus.Normal;
                this.Items[target] = temp;
                await this.UpdateBox(target);
            }
        }

        /// <inheritdoc/>
        public override string ToString() => "Binary Insertion Sort";

        private async Task<int> BinarySearch(SortableElementViewModel value, int start, int end)
        {
            int i = start,
                j = end - 1;

            while (i <= j)
            {
                int mid = (i + j) / 2;

                await this.UpdateBox(i, mid, j);

                if (this.Compare(value, this.Items[mid]) >= 0)
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
