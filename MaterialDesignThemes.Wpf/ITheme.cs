using System.Windows.Media;
using MaterialDesignColors;

namespace MaterialDesignThemes.Wpf
{
    public interface ITheme
    {
        ColorPair PrimaryLight { get; set; }
        ColorPair PrimaryMid { get; set; }
        ColorPair PrimaryDark { get; set; }

        ColorPair SecondaryLight { get; set; }
        ColorPair SecondaryMid { get; set; }
        ColorPair SecondaryDark { get; set; }

        Color ValidationError { get; set; }
        Color Background { get; set; }
        Color Paper { get; set; }
        Color PaperDark { get; set; }
        Color CardBackground { get; set; }
        Color ToolBarBackground { get; set; }
        Color Body { get; set; }
        Color BodyLight { get; set; }
        Color ColumnHeader { get; set; }

        Color CheckBoxOff { get; set; }
        Color CheckBoxDisabled { get; set; }

        Color Divider { get; set; }
        Color Selection { get; set; }
        
        Color ToolForeground { get; set; }
        Color ToolBackground { get; set; }

        Color FlatButtonClick { get; set; }
        Color FlatButtonRipple { get; set; }

        Color ToolTipBackground { get; set; }
        Color ChipBackground { get; set; }

        Color SnackbarBackground { get; set; }
        Color SnackbarMouseOver { get; set; }
        Color SnackbarRipple { get; set; }

        Color TextBoxBorder { get; set; }

        Color TextFieldBoxBackground { get; set; }
        Color TextFieldBoxHoverBackground { get; set; }
        Color TextFieldBoxDisabledBackground { get; set; }
        Color TextAreaBorder { get; set; }
        Color TextAreaInactiveBorder { get; set; }
        
        Color DataGridRowHoverBackground { get; set; }
    }
}