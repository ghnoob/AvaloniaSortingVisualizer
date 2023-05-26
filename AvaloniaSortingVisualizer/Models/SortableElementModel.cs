namespace AvaloniaSortingVisualizer.Models
{
    /// <summary>
    /// Represents a sortable element.
    /// </summary>
    public class SortableElementModel
    {
        /// <summary>
        /// Gets or sets the value of the sortable element.
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// Gets or sets the status of the sortable element.
        /// </summary>
        public SortableElementStatus Status { get; set; }
    }
}
