using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using AvaloniaSortingVisualizer.Algorithms;
using AvaloniaSortingVisualizer.Services;

namespace AvaloniaSortingVisualizer.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty]
    private ObservableCollection<SortableElementViewModel> _items;

    [ObservableProperty]
    private SortingAlgorithm _selectedAlgorithm;

    public SortingAlgorithm[] Algorithms { get; }

    public MainViewModel(ISortableElementService service)
    {
        IEnumerable<SortableElementViewModel> wrappedItems = service
            .GetItems()
            .Select(model => new SortableElementViewModel(model));

        Items = new ObservableCollection<SortableElementViewModel>(wrappedItems);
        Algorithms = GenerateAlgorithms();
        SelectedAlgorithm = Algorithms[0];
    }

    private SortingAlgorithm[] GenerateAlgorithms() =>
        new SortingAlgorithm[1] { new BubbleSort(Items) };

    [RelayCommand]
    private Task RunSortAsync() => SelectedAlgorithm.Sort();
}
