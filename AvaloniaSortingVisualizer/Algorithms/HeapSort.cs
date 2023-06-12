namespace AvaloniaSortingVisualizer.Algorithms
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using AvaloniaSortingVisualizer.Services;
    using AvaloniaSortingVisualizer.ViewModels;

    /// <summary>
    /// Heap Sort implementation.
    /// </summary>
    public class HeapSort : SortingAlgorithm
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HeapSort"/> class.
        /// </summary>
        /// <param name="soundService">Service that will be used for playing sounds.</param>
        public HeapSort(ISoundService soundService)
            : base(soundService)
        {
        }

        /// <inheritdoc/>
        public override async Task RunRange(IList<SortableElementViewModel> items, int start, int end, CancellationToken token)
        {
            for (int i = ((start + end) / 2) - 1; i >= start; i--)
            {
                await this.Heapify(items, i, start, end, token);
            }

            for (int i = end - 1; i > start; i--)
            {
                await this.Swap(items, start, i, token);
                await this.Heapify(items, start, start, i, token);
            }
        }

        /// <inheritdoc/>
        public override string ToString() => "Heap Sort";

        /// <summary>
        /// Converts a list of items into a max heap.
        /// </summary>
        /// <param name="items">List of items to heapify.</param>
        /// <param name="parentNode">Index from where to start the heapification process.</param>
        /// <param name="start">Start index of the list (inclusive).</param>
        /// <param name="end">End index of the list (exclusive).</param>
        /// <param name="token">Cancelation token to stop the operation.</param>
        /// <returns>A <see cref="Task"/> representing the operation.</returns>
        private async Task Heapify(IList<SortableElementViewModel> items, int parentNode, int start, int end, CancellationToken token)
        {
            int i = parentNode,
                largest = i;

            bool done = false;
            while (!done)
            {
                int leftChild = (i * 2) + 1 - start,
                    rightChild = leftChild + 1;

                if (leftChild < end && this.Compare(items[leftChild], items[largest]) > 0)
                {
                    largest = leftChild;
                }

                if (rightChild < end && this.Compare(items[rightChild], items[largest]) > 0)
                {
                    largest = rightChild;
                }

                if (i != largest)
                {
                    await this.Swap(items, i, largest, token);
                    i = largest;
                }
                else
                {
                    done = true;
                }
            }
        }
    }
}
