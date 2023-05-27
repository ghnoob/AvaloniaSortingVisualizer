using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AvaloniaSortingVisualizer.Models;
using AvaloniaSortingVisualizer.ViewModels;

namespace AvaloniaSortingVisualizer.Algorithms
{
    /// <summary>
    /// Implementation of the bubble sort algorithm
    /// </summary>
    public class BubbleSort : SortingAlgorithm
    {
        public override async Task RunRange(int start, int end)
        {
            bool sorted;
            int i = start;

            do
            {
                sorted = true;

                for (int j = start; j < end - i - 1; j++)
                {
                    if (Compare(Items[j], Items[j + 1]) > 0)
                    {
                        await Swap(j, j + 1);
                        sorted = false;
                    }
                    else
                    {
                        await UpdateBox(j);
                    }
                }

                Items[end - i - 1].Status = SortableElementStatus.Sorted;
                i++;
            } while (!sorted);
        }

        public override string ToString() => "Bubble Sort";
    }
}
