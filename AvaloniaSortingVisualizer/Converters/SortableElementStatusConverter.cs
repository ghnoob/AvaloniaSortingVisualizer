using System;
using System.Globalization;
using Avalonia.Data;
using Avalonia.Data.Converters;
using Avalonia.Media;
using AvaloniaSortingVisualizer.Models;

namespace AvaloniaSortingVisualizer.Converters
{
    /// <summary>
    /// Converts the <see cref="SortableElementStatus"/> to a corresponding <see cref="ISolidColorBrush"/> value.
    /// </summary>
    public class SortableElementStatusConverter : IValueConverter
    {
        /// <inheritdoc/>
        public object? Convert(
            object? value,
            Type targetType,
            object? parameter,
            CultureInfo culture
        )
        {
            if (
                value is SortableElementStatus status
                && targetType.IsAssignableFrom(typeof(ISolidColorBrush))
            )
            {
                switch (status)
                {
                    case SortableElementStatus.Normal:
                        return Brushes.White;
                    case SortableElementStatus.Tracked:
                        return Brushes.Red;
                    case SortableElementStatus.Sorted:
                        return Brushes.Lime;
                    default:
                        break;
                }
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
            if (
                value is ISolidColorBrush brush
                && targetType.IsAssignableTo(typeof(SortableElementStatus))
            )
            {
                if (value == Brushes.White)
                    return SortableElementStatus.Normal;
                if (value == Brushes.Red)
                    return SortableElementStatus.Tracked;
                if (value == Brushes.Lime)
                    return SortableElementStatus.Sorted;
            }

            return new BindingNotification(new InvalidCastException(), BindingErrorType.Error);
        }
    }
}
