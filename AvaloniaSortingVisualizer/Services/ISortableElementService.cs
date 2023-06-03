namespace AvaloniaSortingVisualizer.Services
{
    using System.Collections.Generic;
    using AvaloniaSortingVisualizer.Models;

    /// <summary>
    /// Represents a service that provides sortable element data.
    /// </summary>
    public interface ISortableElementService
    {
        /// <summary>
        /// Generates a collection of sortable elements.
        /// </summary>
        /// <param name="length">Length of the collection.</param>
        /// <returns>The generated collection.</returns>
        IEnumerable<SortableElementModel> GenerateItems(int length);
    }
}
