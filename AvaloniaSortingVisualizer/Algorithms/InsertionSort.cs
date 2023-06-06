namespace AvaloniaSortingVisualizer.Algorithms
{
    using System.Collections.Generic;
    using System.Threading;
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
        public override Task RunRange(IList<SortableElementViewModel> items, int start, int end, CancellationToken token) => this.GappedInsertionSort(items, start, end, 1, token);

        /// <inheritdoc/>
        public override string ToString() => "Insertion Sort";

        /// <summary>
        /// Performs a gapped insertion sort in a range of items.
        /// </summary>
        /// <param name="items">The list of items.</param>
        /// <param name="start">Start index of the sequence (inclusive).</param>
        /// <param name="end">End index of the sequence (exclusive).</param>
        /// <param name="gap">Lenght of the gap.</param>
        /// <param name="token">Token to cancel the operation.</param>
        /// <returns>A <see cref="Task"/> representing the operation.</returns>
        protected async Task GappedInsertionSort(IList<SortableElementViewModel> items, int start, int end, int gap, CancellationToken token)
        {
            for (int i = start + gap; i < end; i++)
            {
                double tempValue = items[i].Value;

                int j;
                for (j = i; j >= start + gap && items[j - gap].Value > tempValue; j -= gap)
                {
                    items[j].Value = items[j - gap].Value;
                    await this.UpdateBox(items, j, token);
                }

                items[j].Value = tempValue;
                await this.UpdateBox(items, j, token);
            }
        }
    }
}
