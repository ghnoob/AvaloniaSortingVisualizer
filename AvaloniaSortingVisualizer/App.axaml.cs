using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using AvaloniaSortingVisualizer.Algorithms;
using AvaloniaSortingVisualizer.Services;
using AvaloniaSortingVisualizer.ViewModels;
using AvaloniaSortingVisualizer.Views;
using Microsoft.Extensions.DependencyInjection;
using Splat;
using Splat.Microsoft.Extensions.DependencyInjection;

namespace AvaloniaSortingVisualizer;

public partial class App : Application
{
    /// <inheritdoc/>
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
        this.ConfigureServices();
    }

    /// <inheritdoc/>
    public override void OnFrameworkInitializationCompleted()
    {
        if (this.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
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
        services.AddSingleton<ISoundService, SoundService>();

        // Algorithms
        services.AddTransient<SortingAlgorithm, BubbleSort>();
        services.AddTransient<SortingAlgorithm, CocktailSort>();
        services.AddTransient<SortingAlgorithm, CombSort>();

        services.AddTransient<Shuffle, FisherYatesShuffle>();

        // ViewModels
        services.AddTransient<MainViewModel>();
    }
}
