using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using AvaloniaSortingVisualizer.Algorithms;
using AvaloniaSortingVisualizer.Services;

namespace AvaloniaSortingVisualizer.ViewModels
{
    public partial class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Gets or sets the collection of sortable element view models.
        /// </summary>
        [ObservableProperty]
        private ObservableCollection<SortableElementViewModel> _items;

        /// <summary>
        /// Gets or sets the selected sorting algorithm.
        /// </summary>
        [ObservableProperty]
        private SortingAlgorithm _selectedAlgorithm;

        /// <summary>
        /// Gets the shuffle algorithm
        /// </summary>
        private Shuffle Shuffler { get; }

        /// <summary>
        /// Gets the ordered enumerable of sorting algorithms.
        /// </summary>
        public IOrderedEnumerable<SortingAlgorithm> SortingAlgorithms { get; }

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
            // Wrap the sortable element models in view models
            IEnumerable<SortableElementViewModel> wrappedItems = service
                .GetItems()
                .Select(model => new SortableElementViewModel(model));

            // Create an ObservableCollection from the wrapped view models
            Items = new ObservableCollection<SortableElementViewModel>(wrappedItems);

            // Configure the sorting algorithms
            SortingAlgorithms = ConfigureAlgorithms(algorithms);

            // Set the selected algorithm to the first one in the ordered list
            SelectedAlgorithm = SortingAlgorithms.First();

            // Configure the shuffler
            Shuffler = ConfigureShuffler(shuffler);
        }

        /// <summary>
        /// Configures the suffler
        /// </summary>
        /// <param name="shuffler">The shuffler to configure</param>
        /// <returns>The configured shuffler</returns>
        private Shuffle ConfigureShuffler(Shuffle shuffler)
        {
            shuffler.SetItems(Items);
            return shuffler;
        }

        /// <summary>
        /// Configures the sorting algorithms with the items collection.
        /// </summary>
        /// <param name="algorithms">The collection of sorting algorithms.</param>
        /// <returns>An ordered enumerable of sorting algorithms.</returns>
        private IOrderedEnumerable<SortingAlgorithm> ConfigureAlgorithms(
            IEnumerable<SortingAlgorithm> algorithms
        )
        {
            foreach (SortingAlgorithm alg in algorithms)
            {
                // Set the items collection for each algorithm
                alg.SetItems(Items);
            }

            // Order the algorithms by name
            return algorithms.OrderBy(alg => alg.Name);
        }

        /// <summary>
        /// Runs the selected sorting algorithm asynchronously.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        [RelayCommand]
        private Task RunSortAsync() => SelectedAlgorithm.Run();

        /// <summary>
        /// Runs the shuffle algorithm asynchronously.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        [RelayCommand]
        private Task RunShuffleAsync() => Shuffler.Run();
    }
}
