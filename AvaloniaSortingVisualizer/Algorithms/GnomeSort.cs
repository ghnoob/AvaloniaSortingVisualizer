namespace AvaloniaSortingVisualizer.Algorithms
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using AvaloniaSortingVisualizer.Services;
    using AvaloniaSortingVisualizer.ViewModels;

    /// <summary>
    /// Implementation of the Gnome Sort algorithm.
    /// </summary>
    public class GnomeSort : SortingAlgorithm
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GnomeSort"/> class.
        /// </summary>
        /// <param name="soundService">Service that will be used for playing sounds.</param>
        public GnomeSort(ISoundService soundService)
            : base(soundService)
        {
        }

        /// <inheritdoc/>
        public override async Task RunRange(IList<SortableElementViewModel> items, int start, int end, CancellationToken token)
        {
            int i = start;
            while (i < end - 1)
            {
                if (i >= start && this.Compare(items[i], items[i + 1]) > 0)
                {
                    await this.Swap(items, i, i + 1, token);
                    i--;
                }
                else
                {
                    i++;
                    await this.UpdateBox(items, i, token);
                }
            }
        }

        /// <inheritdoc/>
        public override string ToString() => "Gnome Sort";
    }
}
