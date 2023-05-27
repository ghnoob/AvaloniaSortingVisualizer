using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using Splat;
using Splat.Microsoft.Extensions.DependencyInjection;
using AvaloniaSortingVisualizer.Services;
using AvaloniaSortingVisualizer.ViewModels;
using AvaloniaSortingVisualizer.Views;

namespace AvaloniaSortingVisualizer;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
        ConfigureServices();
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            // Line below is needed to remove Avalonia data validation.
            // Without this line you will get duplicate validations from both Avalonia and CT
            ExpressionObserver.DataValidators.RemoveAll(x => x is DataAnnotationsValidationPlugin);
            desktop.MainWindow = new MainWindow();
        }

        base.OnFrameworkInitializationCompleted();
    }

    /// <summary>
    /// Configures the services for the application.
    /// </summary>
    private void ConfigureServices()
    {
        IServiceCollection services = new ServiceCollection();
        services.UseMicrosoftDependencyResolver();
        Locator.CurrentMutable.InitializeSplat();

        // Services
        services.AddSingleton<ISortableElementService, SortableElementService>();

        // ViewModels
        services.AddTransient<MainViewModel>();
    }
}
