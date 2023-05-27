using System.Threading.Tasks;

namespace AvaloniaSortingVisualizer.Algorithms
{
    /// <summary>
    /// Implementation of the Fisher-Yates shuffle algorithm
    /// </summary>
    public class FisherYatesShuffle : RandomShuffle
    {
        public override async Task RunRange(int start, int end)
        {
            for (int i = start; i < end - 1; i++)
            {
                int j = Rng.Next(i, end);
                await Swap(i, j);
            }
        }

        public override string ToString() => "Fisher-Yates Shuffle";
    }
}
