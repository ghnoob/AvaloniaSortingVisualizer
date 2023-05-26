namespace AvaloniaSortingVisualizer.Models
{
    /// <summary>
    /// Represents the status of a sortable element.
    /// </summary>
    public enum SortableElementStatus
    {
        /// <summary>
        /// The element is in a normal state.
        /// </summary>
        Normal,

        /// <summary>
        /// The element is currently being tracked or observed.
        /// </summary>
        Tracked,

        /// <summary>
        /// The element has been sorted.
        /// </summary>
        Sorted
    }
}
