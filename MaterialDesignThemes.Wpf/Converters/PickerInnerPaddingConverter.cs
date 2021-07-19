using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace MaterialDesignThemes.Wpf.Converters
{
    public class PickerInnerPaddingConverter : IValueConverter, IMultiValueConverter
    {
        private readonly FloatingHintOffsetCalculationConverter _offsetCalculation = new FloatingHintOffsetCalculationConverter();

        /// <summary>
        /// Sets the left padding for the inner picker button to zero
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => value is Thickness padding
                ? new Thickness(Constants.TextBoxInnerButtonSpacing, padding.Top, padding.Right, padding.Bottom)
                : Binding.DoNothing;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotSupportedException();
        
        /// <summary>
        /// Adds the width of the inner picker button to the right of inner padding 
        /// </summary>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] is not Thickness padding)
            {
                return Binding.DoNothing;
            }

            switch (values.Length)
            {
                // add width of picker-button to textfield-padding
                case 2 when values[1] is double rightAddend:
                    return new Thickness(
                        padding.Left,
                        padding.Top,
                        padding.Right + rightAddend + Constants.TextBoxInnerButtonSpacing,
                        padding.Bottom);

                // calculate padding for picker-button (considering floating hint offset)
                case 4:
                    var offsetResult = _offsetCalculation.Convert(new[] { values[1], values[2], values[3], null }, typeof(Thickness), null, culture);
                    if (offsetResult is Thickness offset)
                    {
                        return new Thickness(
                            padding.Left + offset.Left,
                            padding.Top + offset.Top,
                            padding.Right + offset.Right,
                            padding.Bottom + offset.Bottom);
                    }

                    break;
            }
            return Binding.DoNothing;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) => throw new NotSupportedException();
    }
}