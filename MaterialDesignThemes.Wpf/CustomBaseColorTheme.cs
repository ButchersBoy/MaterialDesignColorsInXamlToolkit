using System.Windows;
using System.Windows.Media;
using MaterialDesignColors;

namespace MaterialDesignThemes.Wpf
{
    public class CustomBaseColorTheme : ResourceDictionary, IBaseTheme
    {
        private Color? _primaryBackgroundColor = Colors.Purple;
        public Color? PrimaryBackgroundColor
        {
            get { return _primaryBackgroundColor; }
            set
            {
                if (_primaryBackgroundColor != value)
                {
                    _primaryBackgroundColor = value;
                }
            }
        }

        private Color? _primaryForegroundColor = Colors.White;
        public Color? PrimaryForegroundColor
        {
            get { return _primaryForegroundColor; }
            set
            {
                if (_primaryForegroundColor != value)
                {
                    _primaryForegroundColor = value;
                }
            }
        }


        private Color? _secondaryBackgroundColor = Colors.Lime;
        public Color? SecondaryBackgroundColor
        {
            get { return _secondaryBackgroundColor; }
            set
            {
                if (_secondaryBackgroundColor != value)
                {
                    _secondaryBackgroundColor = value;
                }
            }
        }

        private Color? _secondaryForegroundColor = Colors.Black;
        public Color? SecondaryForegroundColor
        {
            get { return _secondaryForegroundColor; }
            set
            {
                if (_secondaryForegroundColor != value)
                {
                    _secondaryForegroundColor = value;
                }
            }
        }

        private Color _validationErrorColor = (Color)ColorConverter.ConvertFromString("#f44336");
        public Color ValidationErrorColor
        { 
            get { return _validationErrorColor; }
            set
            {
                if (_validationErrorColor != value)
                {
                    _validationErrorColor = value;
                }
                SetTheme();
            }
        }

        public Color MaterialDesignBackground  { get; set; } = (Color) ColorConverter.ConvertFromString("#FFFFFFFF");
        public Color MaterialDesignPaper  { get; set; } = (Color) ColorConverter.ConvertFromString("#FFFAFAFA");
        public Color MaterialDesignCardBackground  { get; set; } = (Color) ColorConverter.ConvertFromString("#FFFFFFFF");
        public Color MaterialDesignToolBarBackground  { get; set; } = (Color) ColorConverter.ConvertFromString("#FFF5F5F5");
        public Color MaterialDesignBody  { get; set; } = (Color) ColorConverter.ConvertFromString("#DD000000");
        public Color MaterialDesignBodyLight  { get; set; } = (Color) ColorConverter.ConvertFromString("#89000000");
        public Color MaterialDesignColumnHeader  { get; set; } = (Color) ColorConverter.ConvertFromString("#BC000000");
        public Color MaterialDesignCheckBoxOff  { get; set; } = (Color) ColorConverter.ConvertFromString("#89000000");
        public Color MaterialDesignCheckBoxDisabled  { get; set; } = (Color) ColorConverter.ConvertFromString("#FFBDBDBD");
        public Color MaterialDesignTextBoxBorder  { get; set; } = (Color) ColorConverter.ConvertFromString("#89000000");
        public Color MaterialDesignDivider  { get; set; } = (Color) ColorConverter.ConvertFromString("#1F000000");
        public Color MaterialDesignSelection  { get; set; } = (Color) ColorConverter.ConvertFromString("#FFDEDEDE");
        public Color MaterialDesignToolForeground  { get; set; } = (Color) ColorConverter.ConvertFromString("#FF616161");
        public Color MaterialDesignToolBackground  { get; set; } = (Color) ColorConverter.ConvertFromString("#FFE0E0E0");
        public Color MaterialDesignFlatButtonClick  { get; set; } = (Color) ColorConverter.ConvertFromString("#FFDEDEDE");
        public Color MaterialDesignFlatButtonRipple  { get; set; } = (Color) ColorConverter.ConvertFromString("#FFB6B6B6");
        public Color MaterialDesignToolTipBackground  { get; set; } = (Color) ColorConverter.ConvertFromString("#757575");
        public Color MaterialDesignChipBackground  { get; set; } = (Color) ColorConverter.ConvertFromString("#12000000");
        public Color MaterialDesignSnackbarBackground  { get; set; } = (Color) ColorConverter.ConvertFromString("#FF323232");
        public Color MaterialDesignSnackbarMouseOver  { get; set; } = (Color) ColorConverter.ConvertFromString("#FF464642");
        public Color MaterialDesignSnackbarRipple  { get; set; } = (Color) ColorConverter.ConvertFromString("#FFB6B6B6");
        public Color MaterialDesignTextFieldBoxBackground  { get; set; } = (Color) ColorConverter.ConvertFromString("#0F000000");
        public Color MaterialDesignTextFieldBoxHoverBackground  { get; set; } = (Color) ColorConverter.ConvertFromString("#14000000");
        public Color MaterialDesignTextFieldBoxDisabledBackground  { get; set; } = (Color) ColorConverter.ConvertFromString("#08000000");
        public Color MaterialDesignTextAreaBorder  { get; set; } = (Color) ColorConverter.ConvertFromString("#BC000000");
        public Color MaterialDesignTextAreaInactiveBorder  { get; set; } = (Color) ColorConverter.ConvertFromString("#0F000000");
        public Color MaterialDesignDataGridRowHoverBackground  { get; set; } = (Color) ColorConverter.ConvertFromString("#0A000000");


        public void SetTheme()
        {
            var theme = Theme.Create(this, _primaryBackgroundColor ?? Colors.Purple, _secondaryBackgroundColor ?? Colors.Lime);
            theme.PrimaryLight = new ColorPair(theme.PrimaryLight.Color, _primaryForegroundColor);
            theme.PrimaryMid = new ColorPair(theme.PrimaryMid.Color, _primaryForegroundColor);
            theme.PrimaryDark = new ColorPair(theme.PrimaryDark.Color, _primaryForegroundColor);
            theme.SecondaryLight = new ColorPair(theme.SecondaryLight.Color, _secondaryForegroundColor);
            theme.SecondaryMid = new ColorPair(theme.SecondaryMid.Color, _secondaryForegroundColor);
            theme.SecondaryDark = new ColorPair(theme.SecondaryDark.Color, _secondaryForegroundColor);
            ApplyTheme(theme);
        }

        protected virtual void ApplyTheme(ITheme theme)
        {
            this.SetTheme(theme);
        }


    }
}