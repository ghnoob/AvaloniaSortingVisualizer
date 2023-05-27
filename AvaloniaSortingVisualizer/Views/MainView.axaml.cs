using Avalonia.Controls;
using Splat;
using AvaloniaSortingVisualizer.ViewModels;

namespace AvaloniaSortingVisualizer.Views;

public partial class MainView : UserControl
{
    public MainView()
    {
        InitializeComponent();

        DataContext = Locator.Current.GetService<MainViewModel>();
    }
}
