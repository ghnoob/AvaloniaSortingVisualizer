using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using AvaloniaSortingVisualizer.Models;
using AvaloniaSortingVisualizer.Services;

namespace AvaloniaSortingVisualizer.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty]
    private ObservableCollection<SortableElementModel> _items;

    public MainViewModel(ISortableElementService service)
    {
        Items = service.GetItems();
    }
}
