namespace AvaloniaSortingVisualizer.Algorithms
{
    using System.Threading.Tasks;
    using AvaloniaSortingVisualizer.Services;
    using AvaloniaSortingVisualizer.ViewModels;

    /// <summary>
    /// Insertion Sort implementation.
    /// </summary>
    public class InsertionSort : SortingAlgorithm
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InsertionSort"/> class.
        /// </summary>
        /// <param name="soundService">Service that will be used for playing sounds.</param>
        public InsertionSort(ISoundService soundService)
            : base(soundService)
        {
        }

        /// <inheritdoc/>
        public override Task RunRange(int start, int end) => this.GappedInsertionSort(start, end, 1);

        /// <inheritdoc/>
        public override string ToString() => "Insertion Sort";

        /// <summary>
        /// Performs a gapped insertion sort in a range of items.
        /// </summary>
        /// <param name="start">Start index of the sequence (inclusive).</param>
        /// <param name="end">End index of the sequence (exclusive).</param>
        /// <param name="gap">Lenght of the gap.</param>
        /// <returns>A <see cref="Task"/> representing the operation.</returns>
        protected async Task GappedInsertionSort(int start, int end, int gap = 1)
        {
            for (int i = start + gap; i < end; i++)
            {
                SortableElementViewModel temp = this.Items[i];

                int j;
                for (j = i; j >= start + gap && this.Compare(this.Items[j - gap], temp) > 0; j -= gap)
                {
                    this.Items[j] = this.Items[j - gap];
                    await this.UpdateBox(j);
                }

                this.Items[j] = temp;
                await this.UpdateBox(j);
            }
        }
    }
}
