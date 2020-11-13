using System;
using System.Windows.Media;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace MaterialDesignColors.WpfExample.Domain
{
    class BrushColor : INotifyPropertyChanged
    {
        public static ObservableCollection<BrushColor> FromDark()
        {
            ObservableCollection<BrushColor> result = new ObservableCollection<BrushColor>();
            result.Add(new BrushColor("PrimaryBackgroundColor", Colors.Purple));
            result.Add(new BrushColor("PrimaryForegroundColor", Colors.White));
            result.Add(new BrushColor("SecondaryBackgroundColor", Colors.Lime));
            result.Add(new BrushColor("SecondaryForegroundColor", Colors.Black));
            result.Add(new BrushColor("ValidationErrorColor", (Color)ColorConverter.ConvertFromString("#f44336")));
            result.Add(new BrushColor("MaterialDesignBackground", (Color)ColorConverter.ConvertFromString("#FF000000")));
            result.Add(new BrushColor("MaterialDesignPaper", (Color)ColorConverter.ConvertFromString("#FF303030")));
            result.Add(new BrushColor("MaterialDesignCardBackground", (Color)ColorConverter.ConvertFromString("#FF424242")));
            result.Add(new BrushColor("MaterialDesignToolBarBackground", (Color)ColorConverter.ConvertFromString("#FF212121")));
            result.Add(new BrushColor("MaterialDesignBody", (Color)ColorConverter.ConvertFromString("#DDFFFFFF")));
            result.Add(new BrushColor("MaterialDesignBodyLight", (Color)ColorConverter.ConvertFromString("#89FFFFFF")));
            result.Add(new BrushColor("MaterialDesignColumnHeader", (Color)ColorConverter.ConvertFromString("#BCFFFFFF")));
            result.Add(new BrushColor("MaterialDesignCheckBoxOff", (Color)ColorConverter.ConvertFromString("#89FFFFFF")));
            result.Add(new BrushColor("MaterialDesignCheckBoxDisabled", (Color)ColorConverter.ConvertFromString("#FF647076")));
            result.Add(new BrushColor("MaterialDesignTextBoxBorder", (Color)ColorConverter.ConvertFromString("#89FFFFFF")));
            result.Add(new BrushColor("MaterialDesignDivider", (Color)ColorConverter.ConvertFromString("#1FFFFFFF")));
            result.Add(new BrushColor("MaterialDesignSelection", (Color)ColorConverter.ConvertFromString("#757575")));
            result.Add(new BrushColor("MaterialDesignToolForeground", (Color)ColorConverter.ConvertFromString("#FF616161")));
            result.Add(new BrushColor("MaterialDesignToolBackground", (Color)ColorConverter.ConvertFromString("#FFe0e0e0")));
            result.Add(new BrushColor("MaterialDesignFlatButtonClick", (Color)ColorConverter.ConvertFromString("#19757575")));
            result.Add(new BrushColor("MaterialDesignFlatButtonRipple", (Color)ColorConverter.ConvertFromString("#FFB6B6B6")));
            result.Add(new BrushColor("MaterialDesignToolTipBackground", (Color)ColorConverter.ConvertFromString("#eeeeee")));
            result.Add(new BrushColor("MaterialDesignChipBackground", (Color)ColorConverter.ConvertFromString("#FF2E3C43")));
            result.Add(new BrushColor("MaterialDesignSnackbarBackground", (Color)ColorConverter.ConvertFromString("#FFCDCDCD")));
            result.Add(new BrushColor("MaterialDesignSnackbarMouseOver", (Color)ColorConverter.ConvertFromString("#FFB9B9BD")));
            result.Add(new BrushColor("MaterialDesignSnackbarRipple", (Color)ColorConverter.ConvertFromString("#FF494949")));
            result.Add(new BrushColor("MaterialDesignTextFieldBoxBackground", (Color)ColorConverter.ConvertFromString("#1AFFFFFF")));
            result.Add(new BrushColor("MaterialDesignTextFieldBoxHoverBackground", (Color)ColorConverter.ConvertFromString("#1FFFFFFF")));
            result.Add(new BrushColor("MaterialDesignTextFieldBoxDisabledBackground", (Color)ColorConverter.ConvertFromString("#0DFFFFFF")));
            result.Add(new BrushColor("MaterialDesignTextAreaBorder", (Color)ColorConverter.ConvertFromString("#BCFFFFFF")));
            result.Add(new BrushColor("MaterialDesignTextAreaInactiveBorder", (Color)ColorConverter.ConvertFromString("#1AFFFFFF")));
            result.Add(new BrushColor("MaterialDesignDataGridRowHoverBackground", (Color)ColorConverter.ConvertFromString("#14FFFFFF")));
            return result;
        }

        public static ObservableCollection<BrushColor> FromLight()
        {
            ObservableCollection<BrushColor> result = new ObservableCollection<BrushColor>();
            result.Add(new BrushColor("PrimaryBackgroundColor", Colors.Purple));
            result.Add(new BrushColor("PrimaryForegroundColor", Colors.White));
            result.Add(new BrushColor("SecondaryBackgroundColor", Colors.Lime));
            result.Add(new BrushColor("SecondaryForegroundColor", Colors.Black));
            result.Add(new BrushColor("ValidationErrorColor", (Color)ColorConverter.ConvertFromString("#f44336")));
            result.Add(new BrushColor("MaterialDesignBackground", (Color)ColorConverter.ConvertFromString("#FFFFFFFF")));
            result.Add(new BrushColor("MaterialDesignPaper", (Color)ColorConverter.ConvertFromString("#FFFAFAFA")));
            result.Add(new BrushColor("MaterialDesignCardBackground", (Color)ColorConverter.ConvertFromString("#FFFFFFFF")));
            result.Add(new BrushColor("MaterialDesignToolBarBackground", (Color)ColorConverter.ConvertFromString("#FFF5F5F5")));
            result.Add(new BrushColor("MaterialDesignBody", (Color)ColorConverter.ConvertFromString("#DD000000")));
            result.Add(new BrushColor("MaterialDesignBodyLight", (Color)ColorConverter.ConvertFromString("#89000000")));
            result.Add(new BrushColor("MaterialDesignColumnHeader", (Color)ColorConverter.ConvertFromString("#BC000000")));
            result.Add(new BrushColor("MaterialDesignCheckBoxOff", (Color)ColorConverter.ConvertFromString("#89000000")));
            result.Add(new BrushColor("MaterialDesignCheckBoxDisabled", (Color)ColorConverter.ConvertFromString("#FFBDBDBD")));
            result.Add(new BrushColor("MaterialDesignTextBoxBorder", (Color)ColorConverter.ConvertFromString("#89000000")));
            result.Add(new BrushColor("MaterialDesignDivider", (Color)ColorConverter.ConvertFromString("#1F000000")));
            result.Add(new BrushColor("MaterialDesignSelection", (Color)ColorConverter.ConvertFromString("#FFDEDEDE")));
            result.Add(new BrushColor("MaterialDesignToolForeground", (Color)ColorConverter.ConvertFromString("#FF616161")));
            result.Add(new BrushColor("MaterialDesignToolBackground", (Color)ColorConverter.ConvertFromString("#FFE0E0E0")));
            result.Add(new BrushColor("MaterialDesignFlatButtonClick", (Color)ColorConverter.ConvertFromString("#FFDEDEDE")));
            result.Add(new BrushColor("MaterialDesignFlatButtonRipple", (Color)ColorConverter.ConvertFromString("#FFB6B6B6")));
            result.Add(new BrushColor("MaterialDesignToolTipBackground", (Color)ColorConverter.ConvertFromString("#757575")));
            result.Add(new BrushColor("MaterialDesignChipBackground", (Color)ColorConverter.ConvertFromString("#12000000")));
            result.Add(new BrushColor("MaterialDesignSnackbarBackground", (Color)ColorConverter.ConvertFromString("#FF323232")));
            result.Add(new BrushColor("MaterialDesignSnackbarMouseOver", (Color)ColorConverter.ConvertFromString("#FF464642")));
            result.Add(new BrushColor("MaterialDesignSnackbarRipple", (Color)ColorConverter.ConvertFromString("#FFB6B6B6")));
            result.Add(new BrushColor("MaterialDesignTextFieldBoxBackground", (Color)ColorConverter.ConvertFromString("#0F000000")));
            result.Add(new BrushColor("MaterialDesignTextFieldBoxHoverBackground", (Color)ColorConverter.ConvertFromString("#14000000")));
            result.Add(new BrushColor("MaterialDesignTextFieldBoxDisabledBackground", (Color)ColorConverter.ConvertFromString("#08000000")));
            result.Add(new BrushColor("MaterialDesignTextAreaBorder", (Color)ColorConverter.ConvertFromString("#BC000000")));
            result.Add(new BrushColor("MaterialDesignTextAreaInactiveBorder", (Color)ColorConverter.ConvertFromString("#0F000000")));
            result.Add(new BrushColor("MaterialDesignDataGridRowHoverBackground", (Color)ColorConverter.ConvertFromString("#0A000000")));
            return result;
        }

        public static ObservableCollection<BrushColor> LoadFromXAML(MaterialDesignThemes.Wpf.CustomBaseColorTheme dictionary)
        {
            ObservableCollection<BrushColor> result = new ObservableCollection<BrushColor>();
            result.Add(new BrushColor("PrimaryBackgroundColor", dictionary.PrimaryBackgroundColor.Value));
            result.Add(new BrushColor("PrimaryForegroundColor", dictionary.PrimaryForegroundColor.Value));
            result.Add(new BrushColor("SecondaryBackgroundColor", dictionary.SecondaryBackgroundColor.Value));
            result.Add(new BrushColor("SecondaryForegroundColor", dictionary.SecondaryForegroundColor.Value));
            result.Add(new BrushColor("ValidationErrorColor", dictionary.ValidationErrorColor));
            result.Add(new BrushColor("MaterialDesignBackground", dictionary.MaterialDesignBackground));
            result.Add(new BrushColor("MaterialDesignPaper", dictionary.MaterialDesignPaper));
            result.Add(new BrushColor("MaterialDesignCardBackground", dictionary.MaterialDesignCardBackground));
            result.Add(new BrushColor("MaterialDesignToolBarBackground", dictionary.MaterialDesignToolBarBackground));
            result.Add(new BrushColor("MaterialDesignBody", dictionary.MaterialDesignBody));
            result.Add(new BrushColor("MaterialDesignBodyLight", dictionary.MaterialDesignBodyLight));
            result.Add(new BrushColor("MaterialDesignColumnHeader", dictionary.MaterialDesignColumnHeader));
            result.Add(new BrushColor("MaterialDesignCheckBoxOff", dictionary.MaterialDesignCheckBoxOff));
            result.Add(new BrushColor("MaterialDesignCheckBoxDisabled", dictionary.MaterialDesignCheckBoxDisabled));
            result.Add(new BrushColor("MaterialDesignTextBoxBorder", dictionary.MaterialDesignTextBoxBorder));
            result.Add(new BrushColor("MaterialDesignDivider", dictionary.MaterialDesignDivider));
            result.Add(new BrushColor("MaterialDesignSelection", dictionary.MaterialDesignSelection));
            result.Add(new BrushColor("MaterialDesignToolForeground", dictionary.MaterialDesignToolForeground));
            result.Add(new BrushColor("MaterialDesignToolBackground", dictionary.MaterialDesignToolBackground));
            result.Add(new BrushColor("MaterialDesignFlatButtonClick", dictionary.MaterialDesignFlatButtonClick));
            result.Add(new BrushColor("MaterialDesignFlatButtonRipple", dictionary.MaterialDesignFlatButtonRipple));
            result.Add(new BrushColor("MaterialDesignToolTipBackground", dictionary.MaterialDesignToolTipBackground));
            result.Add(new BrushColor("MaterialDesignChipBackground", dictionary.MaterialDesignChipBackground));
            result.Add(new BrushColor("MaterialDesignSnackbarBackground", dictionary.MaterialDesignSnackbarBackground));
            result.Add(new BrushColor("MaterialDesignSnackbarMouseOver", dictionary.MaterialDesignSnackbarMouseOver));
            result.Add(new BrushColor("MaterialDesignSnackbarRipple", dictionary.MaterialDesignSnackbarRipple));
            result.Add(new BrushColor("MaterialDesignTextFieldBoxBackground", dictionary.MaterialDesignTextFieldBoxBackground));
            result.Add(new BrushColor("MaterialDesignTextFieldBoxHoverBackground", dictionary.MaterialDesignTextFieldBoxHoverBackground));
            result.Add(new BrushColor("MaterialDesignTextFieldBoxDisabledBackground", dictionary.MaterialDesignTextFieldBoxDisabledBackground));
            result.Add(new BrushColor("MaterialDesignTextAreaBorder", dictionary.MaterialDesignTextAreaBorder));
            result.Add(new BrushColor("MaterialDesignTextAreaInactiveBorder", dictionary.MaterialDesignTextAreaInactiveBorder));
            result.Add(new BrushColor("MaterialDesignDataGridRowHoverBackground", dictionary.MaterialDesignDataGridRowHoverBackground));
            return result;
        }

        public BrushColor(string name, Color color)
        {
            Name = name;
            Color = color;
        }

        private string _name = "BrushName";
        public string Name
        {
            get { return _name; }
            set 
            { 
                _name = value;
                if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs("Name")); }
            }
        }

        private Color _color = Colors.Black;
        public Color Color
        {
            get { return _color; }
            set
            {
                _color = value;
                if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs("Color")); }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
    }
}
