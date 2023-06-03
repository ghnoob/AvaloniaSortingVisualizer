namespace AvaloniaSortingVisualizer.Algorithms
{
    using System.Threading.Tasks;
    using AvaloniaSortingVisualizer.Services;

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
                    if (this.Compare(this.Items[i], this.Items[i - gap]) < 0)
                    {
                        await this.Swap(i, i - gap);
                    }
                    else
                    {
                        await this.UpdateBox(i);
                    }
                }
            }

            await base.RunRange(start, end);
        }

        public override string ToString() => "Comb Sort";
    }
}
