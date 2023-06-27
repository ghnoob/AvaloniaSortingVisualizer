namespace AvaloniaSortingVisualizer.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AvaloniaSortingVisualizer.Models;

    /// <summary>
    /// Represents a service that provides sortable element data.
    /// </summary>
    public class SineWaveDistributionService : ISortableElementService
    {
        /// <inheritdoc/>
        public string Name => "Sine Wave";

        /// <inheritdoc/>
        public IEnumerable<SortableElementModel> GenerateItems(int length) =>
            Enumerable.Range(1, length).Select(i => new SortableElementModel { Value = (((length / 2) - 1) * Math.Sin((2 * Math.PI) * ((2d / length) * i))) + ((length / 2) + 1) });
    }
}
