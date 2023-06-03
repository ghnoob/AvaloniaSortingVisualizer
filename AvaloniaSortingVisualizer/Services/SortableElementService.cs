namespace AvaloniaSortingVisualizer.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using AvaloniaSortingVisualizer.Models;

    /// <summary>
    /// Represents a service that provides sortable element data.
    /// </summary>
    public class SortableElementService : ISortableElementService
    {
        /// <inheritdoc/>
        public IEnumerable<SortableElementModel> GenerateItems(int length) =>
            Enumerable.Range(1, length).Select(i => new SortableElementModel { Value = i });
    }
}
