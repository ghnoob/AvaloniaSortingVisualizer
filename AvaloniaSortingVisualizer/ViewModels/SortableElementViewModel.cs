namespace AvaloniaSortingVisualizer.ViewModels
{
    using AvaloniaSortingVisualizer.Models;
    using LiveChartsCore.SkiaSharpView.Painting;
    using SkiaSharp;

    /// <summary>
    /// View model for a sortable element.
    /// </summary>
    public class SortableElementViewModel : ViewModelBase
    {
        private readonly SortableElementModel model;

        /// <summary>
        /// Initializes a new instance of the <see cref="SortableElementViewModel"/> class.
        /// </summary>
        /// <param name="model">The underlying model of the sortable element.</param>
        public SortableElementViewModel(SortableElementModel model)
        {
            this.model = model;
        }

        /// <summary>
        /// Gets or sets the value of the sortable element.
        /// </summary>
        public double Value
        {
            get => this.model.Value;
            set =>
                this.SetProperty(
                    this.model.Value,
                    value,
                    this.model,
                    (model, val) => model.Value = val,
                    nameof(this.Value));
        }

        /// <summary>
        /// Gets or sets the status of the sortable element.
        /// </summary>
        public SortableElementStatus Status
        {
            get => this.model.Status;
            set =>
                this.SetProperty(
                    this.model.Status,
                    value,
                    this.model,
                    (model, status) => model.Status = status,
                    nameof(this.Status));
        }

        /// <summary>
        /// Gets a color representing the current Status.
        /// </summary>
        /// <returns>
        /// A <see cref="SolidColorPaint"/> representing the current status.
        /// </returns>
        public SolidColorPaint GetColor()
        {
            if (this.Status == SortableElementStatus.Normal)
            {
                return new SolidColorPaint(SKColors.White);
            }

            if (this.Status == SortableElementStatus.Tracked)
            {
                return new SolidColorPaint(SKColors.Red);
            }

            return new SolidColorPaint(SKColors.Green);
        }
    }
}
