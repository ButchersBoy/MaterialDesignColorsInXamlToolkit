using System.Windows.Media;

namespace MaterialDesignThemes.Wpf
{
    public class MaterialDesignDarkTheme : IBaseTheme
    {
        public Color MaterialDesignValidationErrorColor { get; } = (Color)ColorConverter.ConvertFromString("#CF6679");
        public Color MaterialDesignBackground { get; } = (Color)ColorConverter.ConvertFromString("#FF000000");
        public Color MaterialDesignPaper { get; } = (Color)ColorConverter.ConvertFromString("#FF303030");
        public Color MaterialDesignPaperDark { get; } = (Color)ColorConverter.ConvertFromString("#FF242424");
        public Color MaterialDesignCardBackground { get; } = (Color)ColorConverter.ConvertFromString("#FF424242");
        public Color MaterialDesignToolBarBackground { get; } = (Color)ColorConverter.ConvertFromString("#FF212121");
        public Color MaterialDesignBody { get; } = (Color)ColorConverter.ConvertFromString("#DDFFFFFF");
        public Color MaterialDesignBodyLight { get; } = (Color)ColorConverter.ConvertFromString("#89FFFFFF");
        public Color MaterialDesignColumnHeader { get; } = (Color)ColorConverter.ConvertFromString("#BCFFFFFF");
        public Color MaterialDesignCheckBoxOff { get; } = (Color)ColorConverter.ConvertFromString("#89FFFFFF");
        public Color MaterialDesignCheckBoxDisabled { get; } = (Color)ColorConverter.ConvertFromString("#FF647076");
        public Color MaterialDesignTextBoxBorder { get; } = (Color)ColorConverter.ConvertFromString("#89FFFFFF");
        public Color MaterialDesignDivider { get; } = (Color)ColorConverter.ConvertFromString("#1FFFFFFF");
        public Color MaterialDesignSelection { get; } = (Color)ColorConverter.ConvertFromString("#757575");
        public Color MaterialDesignToolForeground { get; } = (Color)ColorConverter.ConvertFromString("#FF616161");
        public Color MaterialDesignToolBackground { get; } = (Color)ColorConverter.ConvertFromString("#FFe0e0e0");
        public Color MaterialDesignFlatButtonClick { get; } = (Color)ColorConverter.ConvertFromString("#19757575");
        public Color MaterialDesignFlatButtonRipple { get; } = (Color)ColorConverter.ConvertFromString("#FFB6B6B6");
        public Color MaterialDesignToolTipBackground { get; } = (Color)ColorConverter.ConvertFromString("#eeeeee");
        public Color MaterialDesignChipBackground { get; } = (Color)ColorConverter.ConvertFromString("#FF2E3C43");
        public Color MaterialDesignSnackbarBackground { get; } = (Color)ColorConverter.ConvertFromString("#FFCDCDCD");
        public Color MaterialDesignSnackbarMouseOver { get; } = (Color)ColorConverter.ConvertFromString("#FFB9B9BD");
        public Color MaterialDesignSnackbarRipple { get; } = (Color)ColorConverter.ConvertFromString("#FF494949");
        public Color MaterialDesignTextFieldBoxBackground { get; } = (Color)ColorConverter.ConvertFromString("#1AFFFFFF");
        public Color MaterialDesignTextFieldBoxHoverBackground { get; } = (Color)ColorConverter.ConvertFromString("#1FFFFFFF");
        public Color MaterialDesignTextFieldBoxDisabledBackground { get; } = (Color)ColorConverter.ConvertFromString("#0DFFFFFF");
        public Color MaterialDesignTextAreaBorder { get; } = (Color)ColorConverter.ConvertFromString("#BCFFFFFF");
        public Color MaterialDesignTextAreaInactiveBorder { get; } = (Color)ColorConverter.ConvertFromString("#29FFFFFF");
        public Color MaterialDesignDataGridRowHoverBackground { get; } = (Color)ColorConverter.ConvertFromString("#14FFFFFF");
    }
}
