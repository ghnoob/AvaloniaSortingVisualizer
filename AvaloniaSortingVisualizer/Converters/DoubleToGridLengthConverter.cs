using System;
using System.Globalization;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Data.Converters;

namespace AvaloniaSortingVisualizer.Converters
{
    /// <summary>
    /// Converts a <see cref="double"/> value to a <see cref="GridLength"/>.
    /// </summary>
    public class DoubleToGridLengthConverter : IValueConverter
    {
        /// <inheritdoc/>
        public object? Convert(
            object? value,
            Type targetType,
            object? parameter,
            CultureInfo culture
        )
        {
            if (value is double val && targetType.IsAssignableFrom(typeof(GridLength)))
            {
                if (val >= 0)
                    return new GridLength(val, GridUnitType.Star);

                ArgumentOutOfRangeException error = new ArgumentOutOfRangeException(
                    "Value must be greater than 0"
                );
                GridLength fallback = new GridLength(0, GridUnitType.Auto);
                return new BindingNotification(
                    error,
                    BindingErrorType.DataValidationError,
                    fallback
                );
            }

            return new BindingNotification(new InvalidCastException(), BindingErrorType.Error);
        }

        /// <inheritdoc/>
        public object? ConvertBack(
            object? value,
            Type targetType,
            object? parameter,
            CultureInfo culture
        )
        {
            if (value is GridLength length && targetType.IsAssignableFrom(typeof(double)))
            {
                return length.Value;
            }

            return new BindingNotification(new InvalidCastException(), BindingErrorType.Error);
        }
    }
}
