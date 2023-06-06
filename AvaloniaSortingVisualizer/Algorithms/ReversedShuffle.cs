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
    public class ReversedShuffle : Shuffle
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReversedShuffle"/> class.
        /// </summary>
        /// <param name="soundService">Service to play sounds to help wih visualization.</param>
        public ReversedShuffle(ISoundService soundService)
            : base(soundService)
        {
        }

        /// <inheritdoc/>
        public override async Task RunRange(IList<SortableElementViewModel> items, int start, int end, CancellationToken token)
        {
            IOrderedEnumerable<SortableElementViewModel> sorted = items
                .Skip(start)
                .Take(end - start + 1)
                .OrderByDescending(x => x.Value);

            int i = start;
            foreach (SortableElementViewModel vm in sorted)
            {
                int j = items.IndexOf(vm);
                await this.Swap(items, i, j, token);
                i++;
            }
        }

        /// <inheritdoc/>
        public override string ToString() => "Reversed";
    }
}
