namespace AvaloniaSortingVisualizer.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AvaloniaSortingVisualizer.Models;

    /// <summary>
    /// Represents a service that provides sortable element data.
    /// </summary>
    public class RandomDistributionService : ISortableElementService
    {
        private readonly Random rng = new Random();

        /// <inheritdoc/>
        public string Name => "Random";

        /// <inheritdoc/>
        public IEnumerable<SortableElementModel> GenerateItems(int length)
        {
            for (int i = 0; i < length; i++)
            {
                yield return new SortableElementModel
                {
                    Status = SortableElementStatus.Normal,
                    Value = this.rng.Next(1, length + 1),
                };
            }
        }
    }
}
