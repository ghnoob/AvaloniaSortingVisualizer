using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using AvaloniaSortingVisualizer.Services;

namespace AvaloniaSortingVisualizer.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty]
    private ObservableCollection<SortableElementViewModel> _items;

    public MainViewModel(ISortableElementService service)
    {
        IEnumerable<SortableElementViewModel> wrappedItems = service
            .GetItems()
            .Select(model => new SortableElementViewModel(model));

        Items = new ObservableCollection<SortableElementViewModel>(wrappedItems);
    }
}
