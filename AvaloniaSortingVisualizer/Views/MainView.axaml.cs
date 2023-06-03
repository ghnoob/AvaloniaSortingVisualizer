namespace AvaloniaSortingVisualizer.Views
{
    using Avalonia.Controls;
    using AvaloniaSortingVisualizer.ViewModels;
    using Splat;

    /// <summary>
    /// Main view of the application.
    /// </summary>
    public partial class MainView : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainView"/> class.
        /// </summary>
        public MainView()
        {
            this.InitializeComponent();

            this.DataContext = Locator.Current.GetService<MainViewModel>();
        }
    }
}
