namespace AvaloniaSortingVisualizer.Algorithms
{
    using System.Threading.Tasks;
    using AvaloniaSortingVisualizer.Services;

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
        public override async Task RunRange(int start, int end)
        {
            int i = start;
            while (i < end - 1)
            {
                if (i >= start && this.Compare(this.Items[i], this.Items[i + 1]) > 0)
                {
                    await this.Swap(i, i + 1);
                    i--;
                }
                else
                {
                    i++;
                    await this.UpdateBox(i);
                }
            }
        }

        /// <inheritdoc/>
        public override string ToString() => "Gnome Sort";
    }
}
