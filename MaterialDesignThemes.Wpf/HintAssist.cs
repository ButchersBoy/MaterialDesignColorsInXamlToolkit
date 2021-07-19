using System.Windows;
using System.Windows.Media;

namespace MaterialDesignThemes.Wpf
{
    public static class HintAssist
    {
        private const double DefaultFloatingScale = 0.74;
        private const double DefaultHintOpacity = 0.56;
        internal static readonly Point DefaultFloatingOffset = new(0, -16);
        private static readonly Brush DefaultBackground = new SolidColorBrush(Colors.Transparent);
        private static readonly double DefaultHelperTextFontSize = 10;

        #region AttachedProperty : IsFloatingProperty
        public static readonly DependencyProperty IsFloatingProperty
            = DependencyProperty.RegisterAttached("IsFloating", typeof(bool), typeof(HintAssist),
                new FrameworkPropertyMetadata(default(bool), FrameworkPropertyMetadataOptions.Inherits));

        public static bool GetIsFloating(DependencyObject element)
            => (bool)element.GetValue(IsFloatingProperty);
        public static void SetIsFloating(DependencyObject element, bool value)
            => element.SetValue(IsFloatingProperty, value);
        #endregion

        #region AttachedProperty : FloatingScaleProperty
        public static readonly DependencyProperty FloatingScaleProperty
            = DependencyProperty.RegisterAttached("FloatingScale", typeof(double), typeof(HintAssist),
                new FrameworkPropertyMetadata(DefaultFloatingScale, FrameworkPropertyMetadataOptions.Inherits));

        public static double GetFloatingScale(DependencyObject element)
            => (double)element.GetValue(FloatingScaleProperty);
        public static void SetFloatingScale(DependencyObject element, double value)
            => element.SetValue(FloatingScaleProperty, value);
        #endregion

        #region AttachedProperty : FloatingOffsetProperty
        public static readonly DependencyProperty FloatingOffsetProperty
            = DependencyProperty.RegisterAttached("FloatingOffset", typeof(Point), typeof(HintAssist),
                new FrameworkPropertyMetadata(DefaultFloatingOffset, FrameworkPropertyMetadataOptions.Inherits));

        public static Point GetFloatingOffset(DependencyObject element)
            => (Point)element.GetValue(FloatingOffsetProperty);

        public static void SetFloatingOffset(DependencyObject element, Point value)
            => element.SetValue(FloatingOffsetProperty, value);
        #endregion

        #region AttachedProperty : HintProperty
        public static readonly DependencyProperty HintProperty
            = DependencyProperty.RegisterAttached("Hint", typeof(object), typeof(HintAssist),
                new FrameworkPropertyMetadata(default, FrameworkPropertyMetadataOptions.Inherits));

        public static object GetHint(DependencyObject element)
            => element.GetValue(HintProperty);
        public static void SetHint(DependencyObject element, object value)
            => element.SetValue(HintProperty, value);
        #endregion

        #region AttachedProperty : HintOpacityProperty
        public static readonly DependencyProperty HintOpacityProperty
            = DependencyProperty.RegisterAttached("HintOpacity", typeof(double), typeof(HintAssist),
                new PropertyMetadata(DefaultHintOpacity));

        public static double GetHintOpacityProperty(DependencyObject element)
            => (double)element.GetValue(HintOpacityProperty);
        public static void SetHintOpacity(DependencyObject element, double value)
            => element.SetValue(HintOpacityProperty, value);
        #endregion

        #region AttachedProperty : HintFontFamilyProperty
        public static readonly DependencyProperty FontFamilyProperty
            = DependencyProperty.RegisterAttached("FontFamily", typeof(FontFamily), typeof(HintAssist),
                new PropertyMetadata(default));

        public static FontFamily GetFontFamily(DependencyObject element)
            => (FontFamily)element.GetValue(FontFamilyProperty);
        public static void SetFontFamily(DependencyObject element, FontFamily value)
            => element.SetValue(FontFamilyProperty, value);
        #endregion

        #region AttachedProperty : ForegroundProperty
        public static readonly DependencyProperty ForegroundProperty
            = DependencyProperty.RegisterAttached("Foreground", typeof(Brush), typeof(HintAssist), new PropertyMetadata(default(Brush)));

        public static Brush GetForeground(DependencyObject element)
            => (Brush)element.GetValue(ForegroundProperty);
        public static void SetForeground(DependencyObject element, Brush value)
            => element.SetValue(ForegroundProperty, value);
        #endregion

        #region AttachedProperty : BackgroundProperty
        public static readonly DependencyProperty BackgroundProperty
            = DependencyProperty.RegisterAttached("Background", typeof(Brush), typeof(HintAssist), new PropertyMetadata(DefaultBackground));

        public static Brush GetBackground(DependencyObject element)
            => (Brush)element.GetValue(BackgroundProperty);
        public static void SetBackground(DependencyObject element, Brush value)
            => element.SetValue(BackgroundProperty, value);
        #endregion

        #region AttachedProperty : HelperTextProperty
        public static readonly DependencyProperty HelperTextProperty
            = DependencyProperty.RegisterAttached("HelperText", typeof(string), typeof(HintAssist),
                new FrameworkPropertyMetadata(default(string), FrameworkPropertyMetadataOptions.Inherits));

        public static object GetHelperText(DependencyObject element)
             => element.GetValue(HelperTextProperty);
        public static void SetHelperText(DependencyObject element, object value)
            => element.SetValue(HelperTextProperty, value);
        #endregion

        #region AttachedProperty : HelperTextFontSizeProperty
        public static readonly DependencyProperty HelperTextFontSizeProperty
            = DependencyProperty.RegisterAttached("HelperTextFontSize", typeof(double), typeof(HintAssist),
                new FrameworkPropertyMetadata(DefaultHelperTextFontSize, FrameworkPropertyMetadataOptions.Inherits));

        public static double GetHelperTextFontSize(DependencyObject element) =>
            (double)element.GetValue(HelperTextFontSizeProperty);
        public static void SetHelperTextFontSize(DependencyObject element, double value) =>
            element.SetValue(HelperTextFontSizeProperty, value);

        #endregion

        #region AttachedProperty : HelperTextStyleProperty
        public static readonly DependencyProperty HelperTextStyleProperty
            = DependencyProperty.RegisterAttached("HelperTextStyle", typeof(Style), typeof(HintAssist),
                new PropertyMetadata(null));

        public static Style? GetHelperTextStyle(DependencyObject element) =>
            (Style?)element.GetValue(HelperTextStyleProperty);
        public static void SetHelperTextStyle(DependencyObject element, Style? value) =>
            element.SetValue(HelperTextStyleProperty, value);

        #endregion
    }
}