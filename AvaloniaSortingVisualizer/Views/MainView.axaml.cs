using Avalonia.Controls;
using AvaloniaSortingVisualizer.ViewModels;
using Splat;

namespace AvaloniaSortingVisualizer.Views;

public partial class MainView : UserControl
{
    public MainView()
    {
        this.InitializeComponent();

        this.DataContext = Locator.Current.GetService<MainViewModel>();
    }
}
