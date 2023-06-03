namespace AvaloniaSortingVisualizer.Converters
{
    using System;
    using System.Globalization;
    using Avalonia.Data;
    using Avalonia.Data.Converters;

    /// <summary>
    /// Converts a <see cref="double"/> value to an <see cref="int"/>.
    /// </summary>
    public class DoubleToIntConverter : IValueConverter
    {
        /// <inheritdoc/>
        public object? Convert(
            object? value,
            Type targetType,
            object? parameter,
            CultureInfo culture)
        {
            if (value is double val && targetType.IsAssignableFrom(typeof(int)))
            {
                return (int)val;
            }

            return new BindingNotification(new InvalidCastException(), BindingErrorType.Error);
        }

        /// <inheritdoc/>
        public object? ConvertBack(
            object? value,
            Type targetType,
            object? parameter,
            CultureInfo culture)
        {
            if (value is int val && targetType.IsAssignableFrom(typeof(double)))
            {
                return (double)val;
            }

            return new BindingNotification(new InvalidCastException(), BindingErrorType.Error);
        }
    }
}
