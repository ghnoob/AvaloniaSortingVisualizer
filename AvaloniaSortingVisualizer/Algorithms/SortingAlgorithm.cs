namespace AvaloniaSortingVisualizer.Algorithms
{
    using System.Threading;
    using System.Threading.Tasks;
    using AvaloniaSortingVisualizer.Models;
    using AvaloniaSortingVisualizer.Services;

    /// <summary>
    /// Base class for sorting algorithms.
    /// </summary>
    public abstract class SortingAlgorithm : Algorithm
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SortingAlgorithm"/> class.
        /// </summary>
        /// <param name="soundService"></param>
        protected SortingAlgorithm(ISoundService soundService)
            : base(soundService) { }

        public async override Task Run(CancellationToken cancellationToken)
        {
            await base.Run(cancellationToken);
            await this.FinalSweep();
        }

        /// <summary>
        /// Performs a final sweep to visually mark all elements as sorted.
        /// </summary>
        /// <returns>A task representing the final sweep operation.</returns>
        private async Task FinalSweep()
        {
            for (int i = 0; i < this.itemsCount; i++)
            {
                await this.UpdateBox(i);
                this.Items[i].Status = SortableElementStatus.Sorted;
            }

            this.ClearAllStatuses();
        }
    }
}
