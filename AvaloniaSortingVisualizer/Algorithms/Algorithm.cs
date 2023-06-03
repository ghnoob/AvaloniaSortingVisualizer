namespace AvaloniaSortingVisualizer.Algorithms
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
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
        /// Token to cancel to operation.
        /// </summary>
        private CancellationToken cancellationToken;

        /// <summary>
        /// Time in ms between algorithm steps.
        /// </summary>
        private int delay;

        /// <summary>
        /// Initializes a new instance of the <see cref="Algorithm"/> class.
        /// </summary>
        /// <param name="soundService">
        /// Service to play sounds to complement the algorithm visualization.
        /// </param>
        public Algorithm(ISoundService soundService)
        {
            this.Items = new ObservableCollection<SortableElementViewModel>();
            this.soundService = soundService;
        }

        /// <summary>
        /// Gets the name of the algorithm.
        /// </summary>
        public string Name => this.ToString();

        /// <summary>
        /// Gets or sets amount of items to operate on.
        /// </summary>
        protected int ItemsCount { get; set; }

        /// <summary>
        /// Gets the list of items the algorithm will modify.
        /// </summary>
        protected ObservableCollection<SortableElementViewModel> Items { get; private set; }

        /// <summary>
        /// Sets the collection of sortable element view models for the algorithm to operate on.
        /// </summary>
        /// <param name="items">The collection of sortable element view models.</param>
        public void SetItems(ObservableCollection<SortableElementViewModel> items)
        {
            this.Items = items;
        }

        /// <summary>
        /// Runs the algorithm on a range of elements in the list.
        /// </summary>
        /// <param name="start">The start index (inclusive) of the range.</param>
        /// <param name="end">The end index (exclusive) of the range.</param>
        /// <returns>A task representing the sort operation.</returns>
        public abstract Task RunRange(int start, int end);

        /// <summary>
        /// Runs the algorithm on all elements in the list.
        /// </summary>
        /// <param name="cancellationToken">
        /// A cancellation token that can be used to cancel the operation.
        /// </param>
        /// <returns>A task representing the sort operation.</returns>
        public virtual async Task Run(CancellationToken cancellationToken)
        {
            this.cancellationToken = cancellationToken;
            this.ItemsCount = this.Items.Count;
            this.delay = MaxDelay / this.ItemsCount;

            await this.RunRange(0, this.ItemsCount);
            this.ClearAllStatuses();
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
        public void ClearAllStatuses()
        {
            foreach (
                SortableElementViewModel vm in this.Items.Where(
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
        /// <returns>A task representing the delay.</returns>
        protected Task UpdateBox()
        {
            this.cancellationToken.ThrowIfCancellationRequested();

            return Task.Delay(this.delay);
        }

        /// <summary>
        /// Updates the state of an element at the specified index and delays the execution.
        /// </summary>
        /// <param name="index">The index of the element to update.</param>
        /// <returns>A task representing the update and delay operation.</returns>
        protected async Task UpdateBox(int index)
        {
            this.cancellationToken.ThrowIfCancellationRequested();

            SortableElementViewModel vm = this.Items[index];
            SortableElementStatus tmpStatus = vm.Status;

            vm.Status = SortableElementStatus.Tracked;

            MidiNotes note = this.soundService.CalculateNote(vm.Value, this.ItemsCount);
            await this.soundService.PlayNoteAsync(note, this.delay);
            vm.Status = tmpStatus;
        }

        /// <summary>
        /// Updates the state of two elements at the specified indices and delays the execution.
        /// </summary>
        /// <param name="indexA">The index of the first element to update.</param>
        /// <param name="indexB">The index of the second element to update.</param>
        /// <returns>A task representing the update and delay operation.</returns>
        protected async Task UpdateBox(int indexA, int indexB)
        {
            SortableElementViewModel vmA = this.Items[indexA];
            SortableElementStatus tmpStatusA = vmA.Status;
            SortableElementViewModel vmB = this.Items[indexB];
            SortableElementStatus tmpStatusB = vmB.Status;

            vmA.Status = SortableElementStatus.Tracked;
            vmB.Status = SortableElementStatus.Tracked;

            MidiNotes noteA = this.soundService.CalculateNote(vmA.Value, this.ItemsCount);
            MidiNotes noteB = this.soundService.CalculateNote(vmB.Value, this.ItemsCount);

            await this.soundService.PlayNotesAsync(noteA, noteB, this.delay);

            vmA.Status = tmpStatusA;
            vmB.Status = tmpStatusB;
        }

        /// <summary>
        /// Updates the state of three elements at the specified indices and delays the execution.
        /// </summary>
        /// <param name="indexA">The index of the first element to update.</param>
        /// <param name="indexB">The index of the second element to update.</param>
        /// <param name="indexC">The index of the third element to update.</param>
        /// <returns>A task representing the update and delay operation.</returns>
        protected async Task UpdateBox(int indexA, int indexB, int indexC)
        {
            SortableElementViewModel vmA = this.Items[indexA];
            SortableElementStatus tmpStatusA = vmA.Status;
            SortableElementViewModel vmB = this.Items[indexB];
            SortableElementStatus tmpStatusB = vmB.Status;
            SortableElementViewModel vmC = this.Items[indexC];
            SortableElementStatus tmpStatusC = vmC.Status;

            vmA.Status = SortableElementStatus.Tracked;
            vmB.Status = SortableElementStatus.Tracked;
            vmC.Status = SortableElementStatus.Tracked;

            MidiNotes noteA = this.soundService.CalculateNote(vmA.Value, this.ItemsCount);
            MidiNotes noteB = this.soundService.CalculateNote(vmB.Value, this.ItemsCount);
            MidiNotes noteC = this.soundService.CalculateNote(vmC.Value, this.ItemsCount);

            await this.soundService.PlayNotesAsync(noteA, noteB, noteC, this.delay);

            vmA.Status = tmpStatusA;
            vmB.Status = tmpStatusB;
            vmC.Status = tmpStatusC;
        }

        /// <summary>
        /// Swaps two elements at the specified indices and updates their state.
        /// </summary>
        /// <param name="indexA">The index of the first element to swap.</param>
        /// <param name="indexB">The index of the second element to swap.</param>
        /// <returns>A task representing the swap and update operation.</returns>
        protected Task Swap(int indexA, int indexB)
        {
            (this.Items[indexA], this.Items[indexB]) = (this.Items[indexB], this.Items[indexA]);
            return this.UpdateBox(indexA, indexB);
        }
    }
}
