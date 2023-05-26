using Avalonia.Controls;
using AvaloniaSortingVisualizer.Services;
using AvaloniaSortingVisualizer.ViewModels;

namespace AvaloniaSortingVisualizer.Views;

public partial class MainView : UserControl
{
    public MainView()
    {
        InitializeComponent();

        // TODO: dependency injection
        ISortableElementService service = new SortableElementService();
        MainViewModel vm = new MainViewModel(service);

        DataContext = vm;
    }
}
