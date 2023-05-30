using AvaloniaSortingVisualizer.Models;

namespace AvaloniaSortingVisualizer.ViewModels
{
    /// <summary>
    /// View model for a sortable element.
    /// </summary>
    public class SortableElementViewModel : ViewModelBase
    {
        private readonly SortableElementModel _model;

        /// <summary>
        /// Gets the value of the sortable element.
        /// </summary>
        public double Value => _model.Value;

        /// <summary>
        /// Gets or sets the status of the sortable element.
        /// </summary>
        public SortableElementStatus Status
        {
            get => _model.Status;
            set =>
                SetProperty(
                    _model.Status,
                    value,
                    _model,
                    (model, status) => model.Status = status,
                    nameof(Status)
                );
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SortableElementViewModel"/> class.
        /// </summary>
        /// <param name="model">The underlying model of the sortable element.</param>
        public SortableElementViewModel(SortableElementModel model)
        {
            _model = model;
        }
    }
}
