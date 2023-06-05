namespace AvaloniaSortingVisualizer.Algorithms
{
    using System.Linq;
    using System.Threading.Tasks;
    using AvaloniaSortingVisualizer.Services;
    using AvaloniaSortingVisualizer.ViewModels;

    /// <summary>
    /// Sorts the items in ascending order.
    /// </summary>
    public class SortedShuffle : Shuffle
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
        public override async Task RunRange(int start, int end)
        {
            IOrderedEnumerable<SortableElementViewModel> sorted = this.Items
                .Skip(start)
                .Take(end - start + 1)
                .OrderBy(x => x.Value);

            int i = start;
            foreach (SortableElementViewModel vm in sorted)
            {
                this.Items[i] = vm;
                await this.UpdateBox(i);
                i++;
            }
        }

        /// <inheritdoc/>
        public override string ToString() => "Already Sorted";
    }
}
