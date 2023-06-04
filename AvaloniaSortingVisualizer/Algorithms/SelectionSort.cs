namespace AvaloniaSortingVisualizer.Algorithms
{
    using System.Threading.Tasks;
    using AvaloniaSortingVisualizer.Models;
    using AvaloniaSortingVisualizer.Services;

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
        public override async Task RunRange(int start, int end)
        {
            for (int i = start; i < end - 1; i++)
            {
                int minIndex = i;
                this.Items[minIndex].Status = SortableElementStatus.Tracked;
                for (int j = i + 1; j < end; j++)
                {
                    await this.UpdateBox(j);
                    if (this.Compare(this.Items[j], this.Items[minIndex]) < 0)
                    {
                        this.Items[minIndex].Status = SortableElementStatus.Normal;
                        this.Items[j].Status = SortableElementStatus.Tracked;
                        minIndex = j;
                    }
                }

                this.Items[minIndex].Status = SortableElementStatus.Normal;
                if (minIndex != i)
                {
                    await this.Swap(i, minIndex);
                }

                this.Items[i].Status = SortableElementStatus.Sorted;
            }
        }

        /// <inheritdoc/>
        public override string ToString() => "Selection Sort";
    }
}
