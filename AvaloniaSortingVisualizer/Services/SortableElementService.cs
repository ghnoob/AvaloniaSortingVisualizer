using System.Collections.Generic;
using System.Linq;
using AvaloniaSortingVisualizer.Models;

namespace AvaloniaSortingVisualizer.Services
{
    /// <summary>
    /// Represents a service that provides sortable element data.
    /// </summary>
    public class SortableElementService : ISortableElementService
    {
        private IEnumerable<SortableElementModel> _items;

        /// <summary>
        /// Initializes a new instance of the <see cref="SortableElementService"/> class.
        /// </summary>
        public SortableElementService()
        {
            _items = GenerateItems();
        }

        public IEnumerable<SortableElementModel> GetItems()
        {
            return _items;
        }

        /// <summary>
        /// Generates the collection of sortable elements.
        /// </summary>
        /// <returns>The collection of sortable elements.</returns>
        private IEnumerable<SortableElementModel> GenerateItems()
        {
            // TODO: dynamic length
            return Enumerable.Range(1, 128).Select(i => new SortableElementModel { Value = i });
        }
    }
}
