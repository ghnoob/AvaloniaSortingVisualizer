using System;
using System.Collections.Generic;
using System.Globalization;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data.Converters;

namespace AvaloniaSortingVisualizer.Converters
{
    /// <summary>
    /// A converter that calculates the proportional height for a row in a Grid based on the value and item count.
    /// </summary>
    public class ProportionalHeightConverter : IMultiValueConverter
    {
        /// <inheritdoc/>
        public object? Convert(
            IList<object?> values,
            Type targetType,
            object? parameter,
            CultureInfo culture
        )
        {
            if (
                targetType.IsAssignableFrom(typeof(GridLength))
                && values != null
                && values.Count >= 2
                && values[0] is double value
                && values[1] is int itemsCount
                && itemsCount >= value
            )
            {
                // Calculate the proportional height using the item count and the provided value.
                return new GridLength(itemsCount - value, GridUnitType.Star);
            }

            return AvaloniaProperty.UnsetValue;
        }
    }
}
