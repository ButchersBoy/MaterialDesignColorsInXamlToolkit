using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using System.Xml;
using MaterialDesignColors.WpfExample.Domain;
using MaterialDesignThemes.Wpf;

namespace MaterialDesignDemo
{
    /// <summary>
    /// Interação lógica para ThemeEditor.xam
    /// </summary>
    public partial class ThemeEditor : UserControl
    {
        public ThemeEditor()
        {
            InitializeComponent();
        }

        private string _existingFilePath = null;

        private void NewProject_Button_Click(object sender, RoutedEventArgs e)
        {
            NewProjectDialog.IsOpen = true;
        }

        private void NewProjectDialog_DialogClosing(object sender, DialogClosingEventArgs eventArgs)
        {
            if (eventArgs.Parameter == null) { return; }
            ThemeEditorViewModel vm = (ThemeEditorViewModel)DataContext;

            if (newdarkbased.IsChecked ?? false)
            {
                vm.Brushes = BrushColor.FromDark();
            }
            else
            {
                vm.Brushes = BrushColor.FromLight();
            }
            CustomBaseColorTheme theme = BuildTheme();
            ApplyTheme(theme);
            _existingFilePath = null;
        }

        private void CreateFrom_Button_Click(object sender, RoutedEventArgs e)
        {
            ThemeEditorViewModel vm = (ThemeEditorViewModel)DataContext;
            PaletteHelper _paletteHelper = new PaletteHelper();
            var colors = _paletteHelper.GetTheme();
            vm.Brushes = BrushColor.FromLight();

            vm.Brushes[0].Color = colors.PrimaryMid.Color;
            vm.Brushes[1].Color = colors.PrimaryMid.ForegroundColor.Value;
            vm.Brushes[2].Color = colors.SecondaryMid.Color;
            vm.Brushes[3].Color = colors.SecondaryMid.ForegroundColor.Value;
            vm.Brushes[4].Color = colors.ValidationError;
            vm.Brushes[5].Color = colors.Background;
            vm.Brushes[6].Color = colors.Paper;
            vm.Brushes[7].Color = colors.CardBackground;
            vm.Brushes[8].Color = colors.ToolBarBackground;
            vm.Brushes[9].Color = colors.Body;
            vm.Brushes[10].Color = colors.BodyLight;
            vm.Brushes[11].Color = colors.ColumnHeader;
            vm.Brushes[12].Color = colors.CheckBoxOff;
            vm.Brushes[13].Color = colors.CheckBoxDisabled;
            vm.Brushes[14].Color = colors.TextBoxBorder;
            vm.Brushes[15].Color = colors.Divider;
            vm.Brushes[16].Color = colors.Selection;
            vm.Brushes[17].Color = colors.ToolForeground;
            vm.Brushes[18].Color = colors.ToolBackground;
            vm.Brushes[19].Color = colors.FlatButtonClick;
            vm.Brushes[20].Color = colors.FlatButtonRipple;
            vm.Brushes[21].Color = colors.ToolTipBackground;
            vm.Brushes[22].Color = colors.ChipBackground;
            vm.Brushes[23].Color = colors.SnackbarBackground;
            vm.Brushes[24].Color = colors.SnackbarMouseOver;
            vm.Brushes[25].Color = colors.SnackbarRipple;
            vm.Brushes[26].Color = colors.TextFieldBoxBackground;
            vm.Brushes[27].Color = colors.TextFieldBoxHoverBackground;
            vm.Brushes[28].Color = colors.TextFieldBoxDisabledBackground;
            vm.Brushes[29].Color = colors.TextAreaBorder;
            vm.Brushes[30].Color = colors.TextAreaInactiveBorder;
            vm.Brushes[31].Color = colors.DataGridRowHoverBackground;

            CustomBaseColorTheme theme = BuildTheme();
            ApplyTheme(theme);
            _existingFilePath = null;
        }

        private void OpenProject_Button_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();
            ofd.Filter = "XAML Files (*.xaml)|*.xaml";
            ofd.Multiselect = false;
            ofd.CheckFileExists = true;
            ofd.Title = "Select theme project file to edit...";
            if (ofd.ShowDialog() == false) { return; }

            _existingFilePath = ofd.FileName;
            try
            {
                using (FileStream fs = new FileStream(_existingFilePath, FileMode.Open))
                {
                    CustomBaseColorTheme project = (CustomBaseColorTheme)XamlReader.Load(fs);
                    fs.Close();
                    fs.Dispose();
                    ThemeEditorViewModel vm = (ThemeEditorViewModel)DataContext;
                    vm.Brushes = BrushColor.LoadFromXAML(project);
                    CustomBaseColorTheme theme = BuildTheme();
                    ApplyTheme(theme);
                }
            }
            catch //(Exception ex)
            {
                commonDialogMessage.Text = "Invalid XAML File";
                CommonDialog.IsOpen = true;
            }
        }

        private void SaveProject_Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(_existingFilePath))
            {
                SaveProjectAs_Button_Click(sender, e);
                return;
            }
            Flush();
        }

        private void SaveProjectAs_Button_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog sfd = new Microsoft.Win32.SaveFileDialog();
            sfd.Filter = "XAML Files (*.xaml)|*.xaml";
            sfd.Title = "Choose the location to save...";
            sfd.FileName = "YourOwnThemeColors.xaml";
            if (sfd.ShowDialog() == false) { return; }
            _existingFilePath = sfd.FileName;
            Flush();
        }

        private void ApplyToDemoApp_Button_Click(object sender, RoutedEventArgs e)
        {
            CustomBaseColorTheme theme = BuildTheme();
            ApplyThemeEntireApp(theme);
        }


        private void Flush()
        {
            using FileStream fs = new FileStream(_existingFilePath, FileMode.Create, FileAccess.ReadWrite);
            var th = BuildTheme();
            //XamlWriter.Save(th, fs);
            var xmlWriterSettings = new XmlWriterSettings() { Indent = true, NewLineOnAttributes = true };
            using (XmlWriter xmlWriter = XmlWriter.Create(fs, xmlWriterSettings))
            {
                th.Clear();
                System.Xaml.XamlServices.Save(xmlWriter, th);
            }
        }

        private bool _lockRefresh = true;
        private void EditorViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (_lockRefresh) { return; }
            if (e.PropertyName == nameof(ThemeEditorViewModel.Brushes))
            {
                CustomBaseColorTheme builtTheme = BuildTheme();
                ApplyTheme(builtTheme);
            }
        }

        private void EditColorButton_Click(object sender, RoutedEventArgs e)
        {
            ColorEditorDialogHost.IsOpen = true;

            ThemeEditorViewModel vm = (ThemeEditorViewModel)DataContext;
            BrushColor entry = (BrushColor)((Button)sender).DataContext;
            if (entry == null) { return; }

            ColorEditorDialogAcceptButton.CommandParameter = entry;
            vm.SelectedColor = entry.Color;
            _lockRefresh = true;
            alphaslider.Value = entry.Color.A;
            _lockRefresh = false;
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (_lockRefresh) { return; }
            ThemeEditorViewModel vm = (ThemeEditorViewModel)DataContext;

            vm.SelectedColor = new Color
            {
                A = (byte)e.NewValue,
                R = vm.SelectedColor.R,
                G = vm.SelectedColor.G,
                B = vm.SelectedColor.B
            };
        }


        private void AcceptColor(object sender, DialogClosingEventArgs eventArgs)
        {
            _lockRefresh = false;
            ThemeEditorViewModel vm = (ThemeEditorViewModel)DataContext;
            BrushColor entry = (BrushColor)eventArgs.Parameter;
            if (entry == null) { return; }
            entry.Color = vm.SelectedColor;
            _lockRefresh = true;
            CustomBaseColorTheme theme = BuildTheme();
            ApplyTheme(theme);
        }

        private CustomBaseColorTheme BuildTheme()
        {
            ThemeEditorViewModel vm = (ThemeEditorViewModel)DataContext;

            CustomBaseColorTheme result = new CustomBaseColorTheme();
            foreach (BrushColor entry in vm.Brushes)
            {
                result.GetType().GetProperty(entry.Name).SetValue(result, entry.Color);
            }
            return result;
        }

        private void ApplyTheme(CustomBaseColorTheme theme)
        {
            samplegrid.Resources.MergedDictionaries[0] = theme;
            theme.SetTheme();
        }

        private void ApplyThemeEntireApp(CustomBaseColorTheme theme)
        {
            Application.Current.Resources.MergedDictionaries[0] = theme;
            theme.SetTheme();
            Application.Current.MainWindow.UpdateLayout();
        }

    }

}
