using System.Threading.Tasks;
using AvaloniaSortingVisualizer.Services;

namespace AvaloniaSortingVisualizer.Algorithms
{
    public class CombSort : BubbleSort
    {
        private const double ShrinkFactor = 1.3;

        public CombSort(ISoundService soundService)
            : base(soundService) { }

        public override async Task RunRange(int start, int end)
        {
            for (
                int gap = (int)((end - start) / ShrinkFactor);
                gap > 1;
                gap = (int)(gap / ShrinkFactor)
            )
            {
                for (int i = start + gap; i < end; i++)
                {
                    if (Compare(Items[i], Items[i - gap]) < 0)
                        await Swap(i, i - gap);
                    else
                        await UpdateBox(i);
                }
            }

            await base.RunRange(start, end);
        }

        public override string ToString() => "Comb Sort";
    }
}
