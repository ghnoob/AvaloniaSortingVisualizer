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
        /// <param name="soundService"></param>
        public BubbleSort(ISoundService soundService)
            : base(soundService) { }

        public override async Task RunRange(int start, int end)
        {
            bool sorted;
            int i = start;

            do
            {
                sorted = true;

                for (int j = start; j < end - i - 1; j++)
                {
                    if (this.Compare(this.Items[j], this.Items[j + 1]) > 0)
                    {
                        await this.Swap(j, j + 1);
                        sorted = false;
                    }
                    else
                    {
                        await this.UpdateBox(j);
                    }
                }

                this.Items[end - i - 1].Status = SortableElementStatus.Sorted;
                i++;
            } while (!sorted);
        }

        public override string ToString() => "Bubble Sort";
    }
}
