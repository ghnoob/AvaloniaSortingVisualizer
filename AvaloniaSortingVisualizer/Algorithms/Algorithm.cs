namespace AvaloniaSortingVisualizer.Algorithms
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AvaloniaSortingVisualizer.Models;
    using AvaloniaSortingVisualizer.Services;
    using AvaloniaSortingVisualizer.ViewModels;

    /// <summary>
    /// Base class for algorithms.
    /// </summary>
    public abstract class Algorithm : IComparer<SortableElementViewModel>
    {
        /// <summary>
        /// Maximum time in ms between algorithm steps.
        /// </summary>
        private const int MaxDelay = 1024;

        /// <summary>
        /// Service to play sounds to complement the algorithm visualization.
        /// </summary>
        private readonly ISoundService soundService;

        /// <summary>
        /// Initializes a new instance of the <see cref="Algorithm"/> class.
        /// </summary>
        /// <param name="soundService">
        /// Service to play sounds to complement the algorithm visualization.
        /// </param>
        public Algorithm(ISoundService soundService)
        {
            this.soundService = soundService;
        }

        /// <summary>
        /// Gets the name of the algorithm.
        /// </summary>
        public string Name => this.ToString();

        /// <summary>
        /// Runs the algorithm on a range of elements of a list of items.
        /// </summary>
        /// <param name="items">The list of items.</param>
        /// <param name="start">The start index (inclusive) of the range.</param>
        /// <param name="end">The end index (exclusive) of the range.</param>
        /// <param name="token">Token to cancel the operation.</param>
        /// <returns>A task representing the sort operation.</returns>
        public abstract Task RunRange(IList<SortableElementViewModel> items, int start, int end, CancellationToken token);

        /// <summary>
        /// Runs the algorithm on all elements in the list.
        /// </summary>
        /// <param name="items">The list of items.</param>
        /// <param name="token">
        /// A cancellation token that can be used to cancel the operation.
        /// </param>
        /// <returns>A task representing the sort operation.</returns>
        public virtual async Task Run(IList<SortableElementViewModel> items, CancellationToken token)
        {
            await this.RunRange(items, 0, items.Count, token);
            this.ClearAllStatuses(items);
        }

        /// <inheritdoc/>
        public int Compare(SortableElementViewModel? x, SortableElementViewModel? y)
        {
            if (x == null)
            {
                return y == null ? 0 : -1;
            }

            if (y == null)
            {
                return 1;
            }

            return x.Value.CompareTo(y.Value);
        }

        /// <summary>
        /// Clears the status of all elements in the collection.
        /// </summary>
        /// <param name="items">Collection to clear.</param>
        public void ClearAllStatuses(IList<SortableElementViewModel> items)
        {
            foreach (
                SortableElementViewModel vm in items.Where(
                    item => item.Status != SortableElementStatus.Normal))
            {
                vm.Status = SortableElementStatus.Normal;
            }
        }

        /// <inheritdoc/>
        public abstract override string ToString();

        /// <summary>
        /// Delays the execution of the algorithm to visualize the changes.
        /// </summary>
        /// <param name="items">Collection to visualize.</param>
        /// <param name="token">Token that can be used to cancel the operation.</param>
        /// <returns>A task representing the delay.</returns>
        protected Task UpdateBox(IList<SortableElementViewModel> items, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            return Task.Delay(MaxDelay / items.Count);
        }

        /// <summary>
        /// Updates the state of an element at the specified index and delays the execution.
        /// </summary>
        /// <param name="items">Collection to visualize.</param>
        /// <param name="index">The index of the element to update.</param>
        /// <param name="token">Token that can be used to cancel the operation.</param>
        /// <returns>A task representing the update and delay operation.</returns>
        protected async Task UpdateBox(IList<SortableElementViewModel> items, int index, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            SortableElementViewModel vm = items[index];
            SortableElementStatus tmpStatus = vm.Status;

            vm.Status = SortableElementStatus.Tracked;

            int count = items.Count;

            MidiNotes note = this.soundService.CalculateNote(vm.Value, count);
            await this.soundService.PlayNoteAsync(note, MaxDelay / count);
            vm.Status = tmpStatus;
        }

        /// <summary>
        /// Updates the state of three elements at the specified indices and delays the execution.
        /// </summary>
        /// <param name="items">Collection to visualize.</param>
        /// <param name="indexA">The index of the first element to update.</param>
        /// <param name="indexB">The index of the second element to update.</param>
        /// <param name="token">Token that can be used to cancel the operation.</param>
        /// <returns>A task representing the update and delay operation.</returns>
        protected async Task UpdateBox(IList<SortableElementViewModel> items, int indexA, int indexB, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            SortableElementViewModel vmA = items[indexA];
            SortableElementStatus tmpStatusA = vmA.Status;
            SortableElementViewModel vmB = items[indexB];
            SortableElementStatus tmpStatusB = vmB.Status;

            vmA.Status = SortableElementStatus.Tracked;
            vmB.Status = SortableElementStatus.Tracked;

            int count = items.Count;
            MidiNotes noteA = this.soundService.CalculateNote(vmA.Value, count);
            MidiNotes noteB = this.soundService.CalculateNote(vmB.Value, count);

            await this.soundService.PlayNotesAsync(noteA, noteB, MaxDelay / count);

            vmA.Status = tmpStatusA;
            vmB.Status = tmpStatusB;
        }

        /// <summary>
        /// Updates the state of three elements at the specified indices and delays the execution.
        /// </summary>
        /// <param name="items">Collection to visualize.</param>
        /// <param name="indexA">The index of the first element to update.</param>
        /// <param name="indexB">The index of the second element to update.</param>
        /// <param name="indexC">The index of the third element to update.</param>
        /// <param name="token">Token that can be used to cancel the operation.</param>
        /// <returns>A task representing the update and delay operation.</returns>
        protected async Task UpdateBox(IList<SortableElementViewModel> items, int indexA, int indexB, int indexC, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            SortableElementViewModel vmA = items[indexA];
            SortableElementStatus tmpStatusA = vmA.Status;
            SortableElementViewModel vmB = items[indexB];
            SortableElementStatus tmpStatusB = vmB.Status;
            SortableElementViewModel vmC = items[indexC];
            SortableElementStatus tmpStatusC = vmC.Status;

            vmA.Status = SortableElementStatus.Tracked;
            vmB.Status = SortableElementStatus.Tracked;
            vmC.Status = SortableElementStatus.Tracked;

            int count = items.Count;
            MidiNotes noteA = this.soundService.CalculateNote(vmA.Value, count);
            MidiNotes noteB = this.soundService.CalculateNote(vmB.Value, count);
            MidiNotes noteC = this.soundService.CalculateNote(vmC.Value, count);

            await this.soundService.PlayNotesAsync(noteA, noteB, noteC, MaxDelay / count);

            vmA.Status = tmpStatusA;
            vmB.Status = tmpStatusB;
            vmC.Status = tmpStatusC;
        }

        /// <summary>
        /// Swaps two elements at the specified indices and updates their state.
        /// </summary>
        /// <param name="items">Collection to update.</param>
        /// <param name="indexA">The index of the first element to swap.</param>
        /// <param name="indexB">The index of the second element to swap.</param>
        /// <param name="token">Token that can be used to cancel the operation.</param>
        /// <returns>A task representing the swap and update operation.</returns>
        protected Task Swap(IList<SortableElementViewModel> items, int indexA, int indexB, CancellationToken token)
        {
            (items[indexA], items[indexB]) = (items[indexB], items[indexA]);
            return this.UpdateBox(items, indexA, indexB, token);
        }
    }
}
