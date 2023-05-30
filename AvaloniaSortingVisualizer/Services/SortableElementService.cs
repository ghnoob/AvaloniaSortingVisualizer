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
        public IEnumerable<SortableElementModel> GenerateItems(int length) =>
            Enumerable.Range(1, length).Select(i => new SortableElementModel { Value = i });
    }
}
