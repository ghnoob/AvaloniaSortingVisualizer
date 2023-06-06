namespace AvaloniaSortingVisualizer.Algorithms
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AvaloniaSortingVisualizer.Services;
    using AvaloniaSortingVisualizer.ViewModels;

    /// <summary>
    /// Sorts the items in ascending order.
    /// </summary>
    public class SortedShuffle : OrderedShuffleBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SortedShuffle"/> class.
        /// </summary>
        /// <param name="soundService">Service to play sounds to help wih visualization.</param>
        public SortedShuffle(ISoundService soundService)
            : base(soundService)
        {
        }

        /// <inheritdoc/>
        public override Task RunRange(IList<SortableElementViewModel> items, int start, int end, CancellationToken token)
        {
            IOrderedEnumerable<SortableElementViewModel> sorted = items
                .Skip(start)
                .Take(end - start + 1)
                .OrderBy(x => x.Value);

            return this.CopySequence(sorted, items, start, token);
        }

        /// <inheritdoc/>
        public override string ToString() => "Already Sorted";
    }
}
