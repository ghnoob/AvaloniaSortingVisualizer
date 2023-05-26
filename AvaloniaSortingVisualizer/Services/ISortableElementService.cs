using System.Collections.Generic;
using AvaloniaSortingVisualizer.Models;

namespace AvaloniaSortingVisualizer.Services
{
    /// <summary>
    /// Represents a service that provides sortable element data.
    /// </summary>
    public interface ISortableElementService
    {
        /// <summary>
        /// Gets the collection of sortable elements.
        /// </summary>
        /// <returns>The collection of sortable elements.</returns>
        IEnumerable<SortableElementModel> GetItems();
    }
}
