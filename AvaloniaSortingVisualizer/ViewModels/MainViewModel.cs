﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Drawing.Geometries;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using AvaloniaSortingVisualizer.Algorithms;
using AvaloniaSortingVisualizer.Services;

namespace AvaloniaSortingVisualizer.ViewModels
{
    public partial class MainViewModel : ViewModelBase
    {
        public int DefaultArrayLength
        {
            get => 128;
        }

        private ISortableElementService _sortableElementService;

        /// <summary>
        /// Gets or sets the collection of sortable element view models.
        /// </summary>
        private ObservableCollection<SortableElementViewModel> _items;

        /// <summary>
        /// Indicates if an algorithm is running
        /// </summary>
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(ChangeArrayLengthCommand))]
        private bool _isRunning;

        /// <summary>
        /// Gets the shuffle algorithm
        /// </summary>
        public Shuffle Shuffler { get; }

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
        /// Gets a collection of cartesian chart series
        /// </summary>
        public ObservableCollection<ISeries> Series { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MainViewModel"/> class.
        /// </summary>
        /// <param name="service">The sortable element service.</param>
        /// <param name="algorithms">The collection of sorting algorithms.</param>
        public MainViewModel(
            ISortableElementService service,
            IEnumerable<SortingAlgorithm> algorithms,
            Shuffle shuffler
        )
        {
            _sortableElementService = service;
            _items = GenerateObservableCollection(DefaultArrayLength);
            XAxes = new Axis[] { new Axis { IsVisible = false } };
            YAxes = new Axis[] { new Axis { IsVisible = false } };
            Series = ConfigureSeries();
            SortingAlgorithms = algorithms.OrderBy(alg => alg.Name);
            Shuffler = shuffler;
            IsRunning = false;
        }

        /// <summary>
        /// Creates a collection of cartesian chart series
        /// </summary>
        /// <returns>The created collection</returns>
        private ObservableCollection<ISeries> ConfigureSeries() =>
            new ObservableCollection<ISeries>
            {
                new ColumnSeries<SortableElementViewModel, RectangleGeometry>
                {
                    Values = _items,
                    MaxBarWidth = double.PositiveInfinity,
                    Padding = 0,
                    Fill = new SolidColorPaint(SKColors.White),
                    Mapping = (vm, point) =>
                    {
                        point.PrimaryValue = vm.Value;
                        point.SecondaryValue = point.Context.Entity.EntityIndex;

                        if (point.Context.Visual is RectangleGeometry rect)
                            rect.Fill = vm.GetColor();
                    }
                }
            };

        /// <summary>
        /// Runs an algorithm asynchronously.
        /// </summary>
        /// <param name="algorithm">The algorithm to run</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        [RelayCommand(IncludeCancelCommand = true)]
        private async Task RunAlgorithmAsync(Algorithm algorithm, CancellationToken token)
        {
            IsRunning = true;
            try
            {
                algorithm.SetItems(_items);
                await algorithm.Run(token);
            }
            catch (OperationCanceledException)
            {
                algorithm.ClearAllStatuses();
            }
            finally
            {
                IsRunning = false;
            }
        }

        /// <summary>
        /// Converts the <see cref="IEnumerable"/> of <see cref="SortableElement"/>
        /// generated by the service to a <see cref="ObservableColletion"/>
        /// </summary>
        /// <param name="length">Length of the collection</param>
        /// <returns>The generated collection</returns>
        private ObservableCollection<SortableElementViewModel> GenerateObservableCollection(
            int length
        )
        {
            IEnumerable<SortableElementViewModel> items = _sortableElementService
                .GenerateItems(length)
                .Select(model => new SortableElementViewModel(model));

            return new ObservableCollection<SortableElementViewModel>(items);
        }

        private bool CanChangeArrayLength(int length) => !IsRunning;

        [RelayCommand(CanExecute = nameof(CanChangeArrayLength))]
        private void ChangeArrayLength(int length)
        {
            _items = GenerateObservableCollection(length);
            Series[0].Values = _items;
        }
    }
}
