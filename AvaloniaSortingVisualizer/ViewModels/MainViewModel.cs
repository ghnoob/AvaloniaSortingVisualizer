namespace AvaloniaSortingVisualizer.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AvaloniaSortingVisualizer.Algorithms;
    using AvaloniaSortingVisualizer.Services;
    using CommunityToolkit.Mvvm.ComponentModel;
    using CommunityToolkit.Mvvm.Input;
    using LiveChartsCore;
    using LiveChartsCore.SkiaSharpView;
    using LiveChartsCore.SkiaSharpView.Drawing.Geometries;
    using LiveChartsCore.SkiaSharpView.Painting;
    using SkiaSharp;

    /// <summary>
    /// View model for the <see cref="Views.MainView"/> class.
    /// </summary>
    public partial class MainViewModel : ViewModelBase
    {
        private ISortableElementService sortableElementService;

        /// <summary>
        /// Gets or sets the collection of sortable element view models.
        /// </summary>
        private ObservableCollection<SortableElementViewModel> items;

        /// <summary>
        /// Indicates if an algorithm is running.
        /// </summary>
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(ChangeArrayLengthCommand))]
        private bool isRunning;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainViewModel"/> class.
        /// </summary>
        /// <param name="service">The sortable element service.</param>
        /// <param name="sortingAlgorithms">The collection of sorting algorithms.</param>
        /// <param name="shufflers">The shuffling algorithms.</param>
        public MainViewModel(
            ISortableElementService service,
            IEnumerable<SortingAlgorithm> sortingAlgorithms,
            IEnumerable<Shuffle> shufflers)
        {
            this.sortableElementService = service;
            this.items = this.GenerateObservableCollection(this.DefaultArrayLength);
            this.XAxes = new Axis[] { new Axis { IsVisible = false } };
            this.YAxes = new Axis[] { new Axis { IsVisible = false } };
            this.Series = this.ConfigureSeries();
            this.SortingAlgorithms = sortingAlgorithms.OrderBy(alg => alg.Name);
            this.Shufflers = shufflers;
            this.IsRunning = false;
        }

        /// <summary>
        /// Gets the default array length.
        /// </summary>
        public int DefaultArrayLength
        {
            get => 128;
        }

        /// <summary>
        /// Gets the shuffle algorithms.
        /// </summary>
        public IEnumerable<Shuffle> Shufflers { get; }

        /// <summary>
        /// Gets the ordered enumerable of sorting algorithms.
        /// </summary>
        public IOrderedEnumerable<SortingAlgorithm> SortingAlgorithms { get; }

        /// <summary>
        /// Gets the x axes.
        /// </summary>
        public IList<Axis> XAxes { get; }

        /// <summary>
        /// Gets the y axes.
        /// </summary>
        public IList<Axis> YAxes { get; }

        /// <summary>
        /// Gets a collection of cartesian chart series.
        /// </summary>
        public ObservableCollection<ISeries> Series { get; }

        /// <summary>
        /// Creates a collection of cartesian chart series.
        /// </summary>
        /// <returns>The created collection.</returns>
        private ObservableCollection<ISeries> ConfigureSeries() =>
            new ObservableCollection<ISeries>
            {
                new ColumnSeries<SortableElementViewModel, RectangleGeometry>
                {
                    Values = this.items,
                    MaxBarWidth = double.PositiveInfinity,
                    Padding = 0,
                    Fill = new SolidColorPaint(SKColors.White),
                    Mapping = (vm, point) =>
                    {
                        point.PrimaryValue = vm.Value;
                        point.SecondaryValue = point.Context.Entity.EntityIndex;

                        if (point.Context.Visual is RectangleGeometry rect)
                        {
                            rect.Fill = vm.GetColor();
                        }
                    },
                },
            };

        /// <summary>
        /// Runs an algorithm asynchronously.
        /// </summary>
        /// <param name="algorithm">The algorithm to run.</param>
        /// <param name="token">Token to cancel the operation.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        [RelayCommand(IncludeCancelCommand = true)]
        private async Task RunAlgorithmAsync(Algorithm algorithm, CancellationToken token)
        {
            this.IsRunning = true;
            try
            {
                await algorithm.Run(this.items, token);
            }
            catch (OperationCanceledException)
            {
                algorithm.ClearAllStatuses(this.items);
            }
            finally
            {
                this.IsRunning = false;
            }
        }

        /// <summary>
        /// Creates a sortable collection.
        /// </summary>
        /// <param name="length">Length of the collection.</param>
        /// <returns>The generated collection.</returns>
        private ObservableCollection<SortableElementViewModel> GenerateObservableCollection(
            int length)
        {
            IEnumerable<SortableElementViewModel> items = this.sortableElementService
                .GenerateItems(length)
                .Select(model => new SortableElementViewModel(model));

            return new ObservableCollection<SortableElementViewModel>(items);
        }

        private bool CanChangeArrayLength(int length) => !this.IsRunning;

        [RelayCommand(CanExecute = nameof(CanChangeArrayLength))]
        private void ChangeArrayLength(int length)
        {
            this.items = this.GenerateObservableCollection(length);
            this.Series[0].Values = this.items;
        }
    }
}
