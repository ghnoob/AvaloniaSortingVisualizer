namespace AvaloniaSortingVisualizer.Services
{
    using System.Collections.Generic;
    using AvaloniaSortingVisualizer.Models;

    /// <summary>
    /// Represents a service that provides a distribution of sortable elements.
    /// </summary>
    public interface ISortableElementService
    {
        /// <summary>
        /// Gets the name of the distribution.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Generates a collection of sortable elements.
        /// </summary>
        /// <param name="length">Length of the collection.</param>
        /// <returns>The generated collection.</returns>
        public IEnumerable<SortableElementModel> GenerateItems(int length);
    }
}
