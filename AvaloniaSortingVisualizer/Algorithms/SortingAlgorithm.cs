using System.Collections.ObjectModel;
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
        /// <summary>
        /// Initializes a new instance of the <see cref="SortingAlgorithm"/> class.
        /// </summary>
        /// <param name="items">The list of items to be sorted.</param>
        public SortingAlgorithm(ObservableCollection<SortableElementViewModel> items)
            : base(items) { }

        public async override Task Run()
        {
            await base.Run();
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
