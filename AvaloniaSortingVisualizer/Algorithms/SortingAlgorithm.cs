﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AvaloniaSortingVisualizer.Models;

namespace AvaloniaSortingVisualizer.Algorithms
{
    /// <summary>
    /// Base class for sorting algorithms.
    /// </summary>
    public abstract class SortingAlgorithm : IComparer<SortableElementModel>
    {
        private const int MaxDelay = 2048;

        /// <summary>
        /// Gets the name of the algorithm.
        /// </summary>
        public string Name
        {
            get => ToString();
        }

        /// <summary>
        /// Gets the list of items to be sorted.
        /// </summary>
        public ObservableCollection<SortableElementModel> Items { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SortingAlgorithm"/> class.
        /// </summary>
        /// <param name="items">The list of items to be sorted.</param>
        public SortingAlgorithm(ObservableCollection<SortableElementModel> items)
        {
            Items = items;
        }

        /// <summary>
        /// Delays the execution of the sorting algorithm to visualize the changes.
        /// </summary>
        /// <returns>A task representing the delay.</returns>
        protected Task UpdateBox()
        {
            return Task.Delay(MaxDelay / Items.Count);
        }

        /// <summary>
        /// Updates the state of an element at the specified index and delays the execution.
        /// </summary>
        /// <param name="index">The index of the element to update.</param>
        /// <returns>A task representing the update and delay operation.</returns>
        protected async Task UpdateBox(int index)
        {
            SortableElementModel model = Items[index];
            SortableElementStatus tmpStatus = model.Status;

            model.Status = SortableElementStatus.Tracked;
            await UpdateBox();
            model.Status = tmpStatus;
        }

        /// <summary>
        /// Updates the state of two elements at the specified indices and delays the execution.
        /// </summary>
        /// <param name="indexA">The index of the first element to update.</param>
        /// <param name="indexB">The index of the second element to update.</param>
        /// <returns>A task representing the update and delay operation.</returns>
        protected async Task UpdateBox(int indexA, int indexB)
        {
            SortableElementModel model = Items[indexB];
            SortableElementStatus tmpStatus = model.Status;

            model.Status = SortableElementStatus.Tracked;
            await UpdateBox(indexA);
            model.Status = tmpStatus;
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
            SortableElementModel model = Items[indexC];
            SortableElementStatus tmpStatus = model.Status;

            model.Status = SortableElementStatus.Tracked;
            await UpdateBox(indexA, indexB);
            model.Status = tmpStatus;
        }

        /// <summary>
        /// Swaps two elements at the specified indices and updates their state.
        /// </summary>
        /// <param name="indexA">The index of the first element to swap.</param>
        /// <param name="indexB">The index of the second element to swap.</param>
        /// <returns>A task representing the swap and update operation.</returns>
        protected Task Swap(int indexA, int indexB)
        {
            (Items[indexA], Items[indexB]) = (Items[indexB], Items[indexA]);
            return UpdateBox(indexA, indexB);
        }

        /// <summary>
        /// Sorts a range of elements in the list.
        /// </summary>
        /// <param name="start">The start index (inclusive) of the range.</param>
        /// <param name="end">The end index (exclusive) of the range.</param>
        /// <returns>A task representing the sort operation.</returns>
        public abstract Task SortRange(int start, int end);

        /// <summary>
        /// Sorts all elements in the list.
        /// </summary>
        /// <returns>A task representing the sort operation.</returns>
        public Task Sort() => SortRange(0, Items.Count);

        public int Compare(SortableElementModel? x, SortableElementModel? y)
        {
            if (x == null)
                return y == null ? 0 : -1;
            if (y == null)
                return 1;

            return x.Value.CompareTo(y.Value);
        }

        public abstract override string ToString();
    }
}