using System.Threading;
using System.Threading.Tasks;
using AvaloniaSortingVisualizer.Models;
using AvaloniaSortingVisualizer.ViewModels;

namespace AvaloniaSortingVisualizer.Algorithms
{
    /// <summary>
    /// Base class for sorting algorithms.
    /// </summary>
    public abstract class SortingAlgorithm : Algorithm
    {
        public async override Task Run(CancellationToken cancellationToken)
        {
            await base.Run(cancellationToken);
            await FinalSweep();
        }

        /// <summary>
        /// Performs a final sweep to visually mark all elements as sorted.
        /// </summary>
        /// <returns>A task representing the final sweep operation.</returns>
        private async Task FinalSweep()
        {
            foreach (SortableElementViewModel vm in Items)
            {
                vm.Status = SortableElementStatus.Sorted;
                await UpdateBox();
            }

            ClearAllStatuses();
        }
    }
}
