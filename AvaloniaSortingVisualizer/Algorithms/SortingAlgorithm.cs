using System.Threading;
using System.Threading.Tasks;
using AvaloniaSortingVisualizer.Models;
using AvaloniaSortingVisualizer.Services;
using AvaloniaSortingVisualizer.ViewModels;

namespace AvaloniaSortingVisualizer.Algorithms
{
    /// <summary>
    /// Base class for sorting algorithms.
    /// </summary>
    public abstract class SortingAlgorithm : Algorithm
    {
        protected SortingAlgorithm(ISoundService soundService)
            : base(soundService) { }

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
            for (int i = 0; i < itemsCount; i++)
            {
                SortableElementViewModel vm = Items[i];

                vm.Status = SortableElementStatus.Sorted;
                MidiNotes note = _soundService.CalculateNote(vm.Value, itemsCount);
                await _soundService.PlayNoteAsync(note, 1);
            }

            ClearAllStatuses();
        }
    }
}
