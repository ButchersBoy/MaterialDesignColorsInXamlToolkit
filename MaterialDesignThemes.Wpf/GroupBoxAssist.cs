using System.Windows;
using System.Windows.Media;

namespace MaterialDesignThemes.Wpf
{
    public static class GroupBoxAssist
    {
        private static readonly CornerRadius DefaultCornerRadius = new CornerRadius(0d);

        #region AttachedProperty : CornerRadiusProperty
        /// <summary>
        /// Controls the corner radius of the surrounding box.
        /// </summary>
        public static readonly DependencyProperty CornerRadiusProperty
            = DependencyProperty.RegisterAttached("CornerRadius", typeof(CornerRadius), typeof(GroupBoxAssist), new PropertyMetadata(DefaultCornerRadius));

        public static CornerRadius GetCornerRadius(DependencyObject element) => (CornerRadius)element.GetValue(CornerRadiusProperty);
        public static void SetCornerRadius(DependencyObject element, CornerRadius value) => element.SetValue(CornerRadiusProperty, value);
        #endregion

        #region AttachedProperty : HeaderCornerRadiusProperty
        /// <summary>
        /// Controls the corner radius of the surrounding box.
        /// </summary>
        public static readonly DependencyProperty HeaderCornerRadiusProperty
            = DependencyProperty.RegisterAttached("HeaderCornerRadius", typeof(CornerRadius), typeof(GroupBoxAssist), new PropertyMetadata(DefaultCornerRadius));

        public static CornerRadius GetHeaderCornerRadius(DependencyObject element) => (CornerRadius)element.GetValue(HeaderCornerRadiusProperty);
        public static void SetHeaderCornerRadius(DependencyObject element, CornerRadius value) => element.SetValue(HeaderCornerRadiusProperty, value);
        #endregion

        #region AttachedProperty : HeaderBorderBrushProperty
        public static readonly DependencyProperty HeaderBorderBrushProperty = DependencyProperty.RegisterAttached("HeaderBorderBrush", typeof(Brush), typeof(GroupBoxAssist), new FrameworkPropertyMetadata(default(Brush)));

        public static void SetHeaderBorderBrush(DependencyObject element, Brush value) => element.SetValue(HeaderBorderBrushProperty, value);

        public static Brush GetHeaderBorderBrush(DependencyObject element) => (Brush)element.GetValue(HeaderBorderBrushProperty);

        #endregion

        #region AttachedProperty : HeaderBorderThicknessProperty
        public static readonly DependencyProperty HeaderBorderThicknessProperty = DependencyProperty.RegisterAttached("HeaderBorderThickness", typeof(Thickness), typeof(GroupBoxAssist), new FrameworkPropertyMetadata(default(Thickness)));

        public static void SetHeaderBorderThickness(DependencyObject element, Thickness value) => element.SetValue(HeaderBorderThicknessProperty, value);

        public static Thickness GetHeaderBorderThickness(DependencyObject element) => (Thickness)element.GetValue(HeaderBorderThicknessProperty);
        #endregion

        #region AttachedProperty : HeaderBackgroundProperty
        public static readonly DependencyProperty HeaderBackgroundProperty = DependencyProperty.RegisterAttached("HeaderBackground", typeof(Brush), typeof(GroupBoxAssist), new FrameworkPropertyMetadata(default(Brush)));

        public static void SetHeaderBackground(DependencyObject element, Brush value) => element.SetValue(HeaderBackgroundProperty, value);

        public static Brush GetHeaderBackground(DependencyObject element) => (Brush)element.GetValue(HeaderBackgroundProperty);
        #endregion

        #region AttachedProperty : HeaderForegroundProperty
        public static readonly DependencyProperty HeaderForegroundProperty = DependencyProperty.RegisterAttached("HeaderForeground", typeof(Brush), typeof(GroupBoxAssist), new FrameworkPropertyMetadata(default(Brush)));

        public static void SetHeaderForeground(DependencyObject element, Brush value) => element.SetValue(HeaderForegroundProperty, value);

        public static Brush GetHeaderForeground(DependencyObject element) => (Brush)element.GetValue(HeaderForegroundProperty);
        #endregion
    }
}
