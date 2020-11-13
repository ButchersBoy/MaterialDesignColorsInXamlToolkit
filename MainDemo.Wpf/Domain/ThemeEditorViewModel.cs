using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Media;

namespace MaterialDesignColors.WpfExample.Domain
{
    internal class ThemeEditorViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<BrushColor> _brushes = BrushColor.FromLight();
        public ObservableCollection<BrushColor> Brushes
        {
            get => _brushes;
            set
            {
                if (_brushes != value)
                {
                    _brushes = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Brushes)));
                }
            }
        }

        private Color _selectedColor;
        public Color SelectedColor
        {
            get => _selectedColor;
            set
            {
                if (_selectedColor != value)
                {
                    _selectedColor = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedColor)));
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
    }
}
