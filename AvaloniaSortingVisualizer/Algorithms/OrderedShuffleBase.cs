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
    /// Base class for ordered shuffles.
    /// </summary>
    public abstract class OrderedShuffleBase : Shuffle
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OrderedShuffleBase"/> class.
        /// </summary>
        /// <param name="soundService">Service to play sounds to help wih visualization.</param>
        protected OrderedShuffleBase(ISoundService soundService)
            : base(soundService)
        {
        }

        /// <summary>
        /// Swaps the elements in a list so they match the order in a source enumerable.
        /// </summary>
        /// <param name="source">Enumerable that wil be used as a pattern to order the list.</param>
        /// <param name="destination">List that will be ordered.</param>
        /// <param name="startIndex">Offset to start ordering the items.</param>
        /// <param name="token">Cancellation token that can be used to stop the operation.</param>
        /// <returns>A <see cref="Task"/> representing the operation.</returns>
        protected async Task CopySequence(IEnumerable<SortableElementViewModel> source, IList<SortableElementViewModel> destination, int startIndex, CancellationToken token)
        {
            int i = startIndex;
            foreach (SortableElementViewModel vm in source)
            {
                if (i >= destination.Count)
                {
                    return;
                }

                int j = destination.IndexOf(vm);
                if (j >= 0 && i != j)
                {
                    await this.Swap(destination, i, j, token);
                }

                i++;
            }
        }
    }
}
