using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace MaterialDesignThemes.Wpf
{
    public static class DataGridAssist
    {
        private static DataGrid _suppressComboAutoDropDown;

        public static readonly DependencyProperty AutoGeneratedCheckBoxStyleProperty = DependencyProperty
            .RegisterAttached(
                "AutoGeneratedCheckBoxStyle", typeof(Style), typeof(DataGridAssist),
                new PropertyMetadata(default(Style), AutoGeneratedCheckBoxStylePropertyChangedCallback));

        private static void AutoGeneratedCheckBoxStylePropertyChangedCallback(DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            ((DataGrid)dependencyObject).AutoGeneratingColumn += (sender, args) =>
           {
               var dataGridCheckBoxColumn = args.Column as DataGridCheckBoxColumn;
               if (dataGridCheckBoxColumn == null) return;

               dataGridCheckBoxColumn.ElementStyle = GetAutoGeneratedCheckBoxStyle(dependencyObject);
           };
        }

        public static void SetAutoGeneratedCheckBoxStyle(DependencyObject element, Style value)
        {
            element.SetValue(AutoGeneratedCheckBoxStyleProperty, value);
        }

        public static Style GetAutoGeneratedCheckBoxStyle(DependencyObject element)
        {
            return (Style)element.GetValue(AutoGeneratedCheckBoxStyleProperty);
        }

        public static readonly DependencyProperty AutoGeneratedEditingCheckBoxStyleProperty = DependencyProperty
            .RegisterAttached(
                "AutoGeneratedEditingCheckBoxStyle", typeof(Style), typeof(DataGridAssist),
                new PropertyMetadata(default(Style), AutoGeneratedEditingCheckBoxStylePropertyChangedCallback));

        private static void AutoGeneratedEditingCheckBoxStylePropertyChangedCallback(DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            ((DataGrid)dependencyObject).AutoGeneratingColumn += (sender, args) =>
           {
               var dataGridCheckBoxColumn = args.Column as DataGridCheckBoxColumn;
               if (dataGridCheckBoxColumn == null) return;

               dataGridCheckBoxColumn.EditingElementStyle = GetAutoGeneratedEditingCheckBoxStyle(dependencyObject);
           };
        }

        public static void SetAutoGeneratedEditingCheckBoxStyle(DependencyObject element, Style value)
        {
            element.SetValue(AutoGeneratedEditingCheckBoxStyleProperty, value);
        }

        public static Style GetAutoGeneratedEditingCheckBoxStyle(DependencyObject element)
        {
            return (Style)element.GetValue(AutoGeneratedEditingCheckBoxStyleProperty);
        }

        public static readonly DependencyProperty AutoGeneratedEditingTextStyleProperty = DependencyProperty
            .RegisterAttached(
                "AutoGeneratedEditingTextStyle", typeof(Style), typeof(DataGridAssist),
                new PropertyMetadata(default(Style), AutoGeneratedEditingTextStylePropertyChangedCallback));

        private static void AutoGeneratedEditingTextStylePropertyChangedCallback(DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            ((DataGrid)dependencyObject).AutoGeneratingColumn += (sender, args) =>
           {
               var dataGridTextColumn = args.Column as DataGridTextColumn;
               if (dataGridTextColumn == null) return;

               dataGridTextColumn.EditingElementStyle = GetAutoGeneratedEditingTextStyle(dependencyObject);
           };
        }

        public static void SetAutoGeneratedEditingTextStyle(DependencyObject element, Style value)
        {
            element.SetValue(AutoGeneratedEditingTextStyleProperty, value);
        }

        public static Style GetAutoGeneratedEditingTextStyle(DependencyObject element)
        {
            return (Style)element.GetValue(AutoGeneratedEditingTextStyleProperty);
        }

        public static readonly DependencyProperty CellPaddingProperty = DependencyProperty.RegisterAttached(
            "CellPadding", typeof(Thickness), typeof(DataGridAssist),
            new FrameworkPropertyMetadata(new Thickness(13, 8, 8, 8), FrameworkPropertyMetadataOptions.Inherits));

        public static void SetCellPadding(DependencyObject element, Thickness value)
        {
            element.SetValue(CellPaddingProperty, value);
        }

        public static Thickness GetCellPadding(DependencyObject element)
        {
            return (Thickness)element.GetValue(CellPaddingProperty);
        }

        public static readonly DependencyProperty ColumnHeaderPaddingProperty = DependencyProperty.RegisterAttached(
            "ColumnHeaderPadding", typeof(Thickness), typeof(DataGridAssist),
            new FrameworkPropertyMetadata(new Thickness(8), FrameworkPropertyMetadataOptions.Inherits));

        public static void SetColumnHeaderPadding(DependencyObject element, Thickness value)
        {
            element.SetValue(ColumnHeaderPaddingProperty, value);
        }

        public static Thickness GetColumnHeaderPadding(DependencyObject element)
        {
            return (Thickness)element.GetValue(ColumnHeaderPaddingProperty);
        }


        public static readonly DependencyProperty EnableEditBoxAssistProperty = DependencyProperty.RegisterAttached(
            "EnableEditBoxAssist", typeof(bool), typeof(DataGridAssist),
            new PropertyMetadata(default(bool), EnableCheckBoxAssistPropertyChangedCallback));

        public static void SetEnableEditBoxAssist(DependencyObject element, bool value)
        {
            element.SetValue(EnableEditBoxAssistProperty, value);
        }

        public static bool GetEnableEditBoxAssist(DependencyObject element)
        {
            return (bool)element.GetValue(EnableEditBoxAssistProperty);
        }

        private static void EnableCheckBoxAssistPropertyChangedCallback(DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var dataGrid = dependencyObject as DataGrid;
            if (dataGrid == null) return;

            if ((bool)dependencyPropertyChangedEventArgs.NewValue)
            {
                dataGrid.PreviewMouseLeftButtonDown += DataGridOnPreviewMouseLeftButtonDown;
                dataGrid.KeyDown += DataGridOnKeyDown;
            }
            else
            {
                dataGrid.PreviewMouseLeftButtonDown -= DataGridOnPreviewMouseLeftButtonDown;
                dataGrid.KeyDown -= DataGridOnKeyDown;
            }
        }

        private static void DataGridOnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space &&
                e.OriginalSource is DataGridCell cell &&
                cell.IsReadOnly == false &&
                cell.Column is DataGridComboBoxColumn &&
                sender is DataGrid dataGrid)
            {
                dataGrid.BeginEdit();
            }
        }

        private static void DataGridOnPreviewMouseLeftButtonDown(object sender,
            MouseButtonEventArgs mouseButtonEventArgs)
        {
            var dataGrid = (DataGrid)sender;

            var inputHitTest =
                dataGrid.InputHitTest(mouseButtonEventArgs.GetPosition((DataGrid)sender)) as DependencyObject;

            while (inputHitTest != null)
            {
                var dataGridCell = inputHitTest as DataGridCell;
                if (dataGridCell != null && dataGrid.Equals(dataGridCell.GetVisualAncestry().OfType<DataGrid>().FirstOrDefault()))
                {
                    if (dataGridCell.IsReadOnly) return;

                    ToggleButton toggleButton;
                    ComboBox comboBox;
                    if (IsDirectHitOnEditComponent(dataGridCell, mouseButtonEventArgs, out toggleButton))
                    {
                        dataGrid.CurrentCell = new DataGridCellInfo(dataGridCell);
                        dataGrid.BeginEdit();
                        toggleButton.SetCurrentValue(ToggleButton.IsCheckedProperty, !toggleButton.IsChecked);
                        dataGrid.CommitEdit();
                        mouseButtonEventArgs.Handled = true;
                    }
                    else if (IsDirectHitOnEditComponent(dataGridCell, mouseButtonEventArgs, out comboBox))
                    {
                        if (_suppressComboAutoDropDown != null) return;

                        dataGrid.CurrentCell = new DataGridCellInfo(dataGridCell);
                        dataGrid.BeginEdit();
                        //check again, as we move to  the edit  template
                        if (IsDirectHitOnEditComponent(dataGridCell, mouseButtonEventArgs, out comboBox))
                        {
                            _suppressComboAutoDropDown = dataGrid;
                            comboBox.DropDownClosed += ComboBoxOnDropDownClosed;
                            comboBox.IsDropDownOpen = true;
                        }
                        mouseButtonEventArgs.Handled = true;
                    }

                    return;
                }

                inputHitTest = (inputHitTest is Visual || inputHitTest is Visual3D)
                    ? VisualTreeHelper.GetParent(inputHitTest)
                    : null;
            }
        }

        private static void ComboBoxOnDropDownClosed(object sender, EventArgs eventArgs)
        {
            _suppressComboAutoDropDown.CommitEdit();
            _suppressComboAutoDropDown = null;
            ((ComboBox)sender).DropDownClosed -= ComboBoxOnDropDownClosed;
        }

        private static bool IsDirectHitOnEditComponent<TControl>(ContentControl contentControl, MouseEventArgs mouseButtonEventArgs, out TControl control)
            where TControl : Control
        {
            control = contentControl.Content as TControl;
            if (control == null) return false;

            var frameworkElement = VisualTreeHelper.GetChild(contentControl, 0) as FrameworkElement;
            if (frameworkElement == null) return false;

            var transformToAncestor = (MatrixTransform)control.TransformToAncestor(frameworkElement);
            var rect = new Rect(
                new Point(transformToAncestor.Value.OffsetX, transformToAncestor.Value.OffsetY),
                new Size(control.ActualWidth, control.ActualHeight));

            return rect.Contains(mouseButtonEventArgs.GetPosition(frameworkElement));
        }
    }
}