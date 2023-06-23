namespace AvaloniaSortingVisualizer.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using AvaloniaSortingVisualizer.Models;

    /// <summary>
    /// Represents a service that provides sortable element data.
    /// </summary>
    public class AlmostEqualDistributionService : ISortableElementService
    {
        /// <inheritdoc/>
        public string Name => "Almost All Equal";

        /// <inheritdoc/>
        public IEnumerable<SortableElementModel> GenerateItems(int length) => Enumerable
            .Repeat(length / 4, 1)
            .Concat(Enumerable.Repeat(length / 2, length - 2))
            .Append(length)
            .Select(x => new SortableElementModel
            {
                Status = SortableElementStatus.Normal,
                Value = x,
            });
    }
}
