namespace AvaloniaSortingVisualizer.Algorithms
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using AvaloniaSortingVisualizer.Models;
    using AvaloniaSortingVisualizer.Services;
    using AvaloniaSortingVisualizer.ViewModels;

    /// <summary>
    /// Base class for sorting algorithms.
    /// </summary>
    public abstract class SortingAlgorithm : Algorithm
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SortingAlgorithm"/> class.
        /// </summary>
        /// <param name="soundService">
        /// Service to play sounds to complement the algorithm visualization.
        /// </param>
        protected SortingAlgorithm(ISoundService soundService)
            : base(soundService)
        {
        }

        /// <inheritdoc/>
        public async override Task Run(IList<SortableElementViewModel> items, CancellationToken token)
        {
            await base.Run(items, token);
            await this.FinalSweep(items, token);
        }

        /// <summary>
        /// Performs a final sweep to visually mark all elements as sorted.
        /// </summary>
        /// <param name="items">Items to mark.</param>
        /// <param name="token">
        /// A cancellation token that can be used to cancel the operation.
        /// </param>
        /// <returns>A task representing the final sweep operation.</returns>
        private async Task FinalSweep(IList<SortableElementViewModel> items, CancellationToken token)
        {
            for (int i = 0; i < items.Count; i++)
            {
                await this.UpdateBox(items, i, token);
                items[i].Status = SortableElementStatus.Sorted;
            }

            this.ClearAllStatuses(items);
        }
    }
}
