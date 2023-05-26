using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using AvaloniaSortingVisualizer.Models;

namespace AvaloniaSortingVisualizer.Services
{
    /// <summary>
    /// Represents a service that provides sortable element data.
    /// </summary>
    public class SortableElementService : ISortableElementService
    {
        private ObservableCollection<SortableElementModel> _items;

        /// <summary>
        /// Initializes a new instance of the <see cref="SortableElementService"/> class.
        /// </summary>
        public SortableElementService()
        {
            _items = GenerateItems();
        }

        public ObservableCollection<SortableElementModel> GetItems()
        {
            return _items;
        }

        /// <summary>
        /// Generates the collection of sortable elements.
        /// </summary>
        /// <returns>The collection of sortable elements.</returns>
        private ObservableCollection<SortableElementModel> GenerateItems()
        {
            // TODO: dynamic length
            IEnumerable<SortableElementModel> items = Enumerable
                .Range(1, 128)
                .Select(i => new SortableElementModel { Value = i });

            return new ObservableCollection<SortableElementModel>(items);
        }
    }
}
