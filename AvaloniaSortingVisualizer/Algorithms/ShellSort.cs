namespace AvaloniaSortingVisualizer.Algorithms
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using AvaloniaSortingVisualizer.Services;
    using AvaloniaSortingVisualizer.ViewModels;

    /// <summary>
    /// Shell Sort implementation.
    /// </summary>
    public class ShellSort : InsertionSort
    {
        /// <summary>
        /// Ciura's shell sort gap sequence.
        /// </summary>
        private static readonly int[] Gaps = { 1750, 701, 301, 132, 57, 23, 10, 4, 1 };

        /// <summary>
        /// Initializes a new instance of the <see cref="ShellSort"/> class.
        /// </summary>
        /// <param name="soundService">Service that will be used for playing sounds.</param>
        public ShellSort(ISoundService soundService)
            : base(soundService)
        {
        }

        /// <inheritdoc/>
        public override async Task RunRange(IList<SortableElementViewModel> items, int start, int end, CancellationToken token)
        {
            foreach (int gap in ShellSort.Gaps)
            {
                await this.GappedInsertionSort(items, start, end, gap, token);
            }
        }

        /// <inheritdoc/>
        public override string ToString() => "Shell Sort";
    }
}
