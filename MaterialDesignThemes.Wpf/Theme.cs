using System;
using System.Windows.Media;
using MaterialDesignColors;
using Microsoft.Win32;

namespace MaterialDesignThemes.Wpf
{
    public class Theme : ITheme
    {
        public static IBaseTheme Light { get; } = new MaterialDesignLightTheme();
        public static IBaseTheme Dark { get; } = new MaterialDesignDarkTheme();

        /// <summary>
        /// Get the current Windows theme.
        /// Based on ControlzEx
        /// https://github.com/ControlzEx/ControlzEx/blob/48230bb023c588e1b7eb86ea83f7ddf7d25be735/src/ControlzEx/Theming/WindowsThemeHelper.cs#L19
        /// </summary>
        /// <returns></returns>
        public static BaseTheme? GetSystemTheme()
        {
            try
            {
                var registryValue = Registry.GetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Themes\Personalize", "AppsUseLightTheme", null);

                if (registryValue is null)
                {
                    return null;
                }

                return Convert.ToBoolean(registryValue) ? BaseTheme.Light : BaseTheme.Dark;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static Theme Create(IBaseTheme baseTheme, Color primary, Color accent)
        {
            if (baseTheme is null) throw new ArgumentNullException(nameof(baseTheme));
            var theme = new Theme();

            theme.SetBaseTheme(baseTheme);
            theme.SetPrimaryColor(primary);
            theme.SetSecondaryColor(accent);

            return theme;
        }

        public ColorPair SecondaryLight { get; set; }
        public ColorPair SecondaryMid { get; set; }
        public ColorPair SecondaryDark { get; set; }

        public ColorPair PrimaryLight { get; set; }
        public ColorPair PrimaryMid { get; set; }
        public ColorPair PrimaryDark { get; set; }

        public Color ValidationError { get; set; }
        public Color Background { get; set; }
        public Color Paper { get; set; }
        public Color PaperDark { get; set; }
        public Color CardBackground { get; set; }
        public Color ToolBarBackground { get; set; }
        public Color Body { get; set; }
        public Color BodyLight { get; set; }
        public Color ColumnHeader { get; set; }
        public Color CheckBoxOff { get; set; }
        public Color CheckBoxDisabled { get; set; }
        public Color Divider { get; set; }
        public Color Selection { get; set; }
        public Color ToolForeground { get; set; }
        public Color ToolBackground { get; set; }
        public Color FlatButtonClick { get; set; }
        public Color FlatButtonRipple { get; set; }
        public Color ToolTipBackground { get; set; }
        public Color ChipBackground { get; set; }
        public Color SnackbarBackground { get; set; }
        public Color SnackbarMouseOver { get; set; }
        public Color SnackbarRipple { get; set; }
        public Color TextBoxBorder { get; set; }
        public Color TextFieldBoxBackground { get; set; }
        public Color TextFieldBoxHoverBackground { get; set; }
        public Color TextFieldBoxDisabledBackground { get; set; }
        public Color TextAreaBorder { get; set; }
        public Color TextAreaInactiveBorder { get; set; }
        public Color DataGridRowHoverBackground { get; set; }

        public ColorAdjustment? ColorAdjustment { get; set; }
    }
}