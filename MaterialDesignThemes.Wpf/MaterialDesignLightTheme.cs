using System.Windows.Media;

namespace MaterialDesignThemes.Wpf
{
    public class MaterialDesignLightTheme : IBaseTheme
    {
        public Color MaterialDesignValidationErrorColor { get; } = (Color)ColorConverter.ConvertFromString("#B00020");
        public Color MaterialDesignBackground { get; } = (Color)ColorConverter.ConvertFromString("#FFFFFFFF");
        public Color MaterialDesignPaper { get; } = (Color)ColorConverter.ConvertFromString("#FFFAFAFA");
        public Color MaterialDesignPaperDark { get; } = (Color)ColorConverter.ConvertFromString("#FFEEEEEE");
        public Color MaterialDesignCardBackground { get; } = (Color)ColorConverter.ConvertFromString("#FFFFFFFF");
        public Color MaterialDesignToolBarBackground { get; } = (Color)ColorConverter.ConvertFromString("#FFF5F5F5");
        public Color MaterialDesignBody { get; } = (Color)ColorConverter.ConvertFromString("#DD000000");
        public Color MaterialDesignBodyLight { get; } = (Color)ColorConverter.ConvertFromString("#89000000");
        public Color MaterialDesignColumnHeader { get; } = (Color)ColorConverter.ConvertFromString("#BC000000");
        public Color MaterialDesignCheckBoxOff { get; } = (Color)ColorConverter.ConvertFromString("#89000000");
        public Color MaterialDesignCheckBoxDisabled { get; } = (Color)ColorConverter.ConvertFromString("#FFBDBDBD");
        public Color MaterialDesignTextBoxBorder { get; } = (Color)ColorConverter.ConvertFromString("#89000000");
        public Color MaterialDesignDivider { get; } = (Color)ColorConverter.ConvertFromString("#1F000000");
        public Color MaterialDesignSelection { get; } = (Color)ColorConverter.ConvertFromString("#FFDEDEDE");
        public Color MaterialDesignToolForeground { get; } = (Color)ColorConverter.ConvertFromString("#FF616161");
        public Color MaterialDesignToolBackground { get; } = (Color)ColorConverter.ConvertFromString("#FFE0E0E0");
        public Color MaterialDesignFlatButtonClick { get; } = (Color)ColorConverter.ConvertFromString("#FFDEDEDE");
        public Color MaterialDesignFlatButtonRipple { get; } = (Color)ColorConverter.ConvertFromString("#FFB6B6B6");
        public Color MaterialDesignToolTipBackground { get; } = (Color)ColorConverter.ConvertFromString("#757575");
        public Color MaterialDesignChipBackground { get; } = (Color)ColorConverter.ConvertFromString("#12000000");
        public Color MaterialDesignSnackbarBackground { get; } = (Color)ColorConverter.ConvertFromString("#FF323232");
        public Color MaterialDesignSnackbarMouseOver { get; } = (Color)ColorConverter.ConvertFromString("#FF464642");
        public Color MaterialDesignSnackbarRipple { get; } = (Color)ColorConverter.ConvertFromString("#FFB6B6B6");
        public Color MaterialDesignTextFieldBoxBackground { get; } = (Color)ColorConverter.ConvertFromString("#0F000000");
        public Color MaterialDesignTextFieldBoxHoverBackground { get; } = (Color)ColorConverter.ConvertFromString("#14000000");
        public Color MaterialDesignTextFieldBoxDisabledBackground { get; } = (Color)ColorConverter.ConvertFromString("#08000000");
        public Color MaterialDesignTextAreaBorder { get; } = (Color)ColorConverter.ConvertFromString("#BC000000");
        public Color MaterialDesignTextAreaInactiveBorder { get; } = (Color)ColorConverter.ConvertFromString("#29000000");
        public Color MaterialDesignDataGridRowHoverBackground { get; } = (Color)ColorConverter.ConvertFromString("#0A000000");
    }
}
