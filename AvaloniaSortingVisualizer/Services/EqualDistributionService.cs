namespace AvaloniaSortingVisualizer.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using AvaloniaSortingVisualizer.Models;

    /// <summary>
    /// Represents a service that provides sortable element data.
    /// </summary>
    public class EqualDistributionService : ISortableElementService
    {
        /// <inheritdoc/>
        public string Name => "All Equal";

        /// <inheritdoc/>
        public IEnumerable<SortableElementModel> GenerateItems(int length) => Enumerable
            .Repeat(length / 2, length)
            .Select(x => new SortableElementModel
            {
                Status = SortableElementStatus.Normal,
                Value = x,
            });
    }
}
