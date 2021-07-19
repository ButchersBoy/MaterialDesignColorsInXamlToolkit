using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Controls;
using MaterialDesignColors;
using MaterialDesignThemes.Wpf;
using XamlTest;

namespace MaterialDesignThemes.UITests
{
    public static class XamlTestMixins
    {
        public static async Task InitialzeWithMaterialDesign(this IApp app,
            BaseTheme baseTheme = BaseTheme.Light,
            PrimaryColor primary = PrimaryColor.DeepPurple,
            SecondaryColor secondary = SecondaryColor.Lime,
            ColorAdjustment? colorAdjustment = null)
        {
            string colorAdjustString = "";
            if (colorAdjustment != null)
            {
                colorAdjustString = "ColorAdjustment=\"{materialDesign:ColorAdjustment}\"";
            }

            string applicationResourceXaml = $@"<ResourceDictionary 
xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation""
xmlns:x=""http://schemas.microsoft.com/winfx/2006/xaml""
xmlns:materialDesign=""http://materialdesigninxaml.net/winfx/xaml/themes"">
    <ResourceDictionary.MergedDictionaries>
        <materialDesign:BundledTheme BaseTheme=""{baseTheme}"" PrimaryColor=""{primary}"" SecondaryColor=""{secondary}"" {colorAdjustString}/>

        <ResourceDictionary Source = ""pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Defaults.xaml"" />
    </ResourceDictionary.MergedDictionaries>
</ResourceDictionary>";

            await app.Initialize(applicationResourceXaml,
                Path.GetFullPath("MaterialDesignColors.dll"),
                Path.GetFullPath("MaterialDesignThemes.Wpf.dll"),
                Assembly.GetExecutingAssembly().Location);
        }

        public static async Task<IVisualElement<T>> CreateWindowWith<T>(this IApp app, string xaml)
        {
            string windowXaml = @$"<Window
        xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation""
        xmlns:x=""http://schemas.microsoft.com/winfx/2006/xaml""
        xmlns:d=""http://schemas.microsoft.com/expression/blend/2008""
        xmlns:mc=""http://schemas.openxmlformats.org/markup-compatibility/2006""
        xmlns:materialDesign=""http://materialdesigninxaml.net/winfx/xaml/themes""
        mc:Ignorable=""d""
        Height=""800"" Width=""1100""
        TextElement.Foreground=""{{DynamicResource MaterialDesignBody}}""
        TextElement.FontWeight=""Regular""
        TextElement.FontSize=""13""
        TextOptions.TextFormattingMode=""Ideal"" 
        TextOptions.TextRenderingMode=""Auto""
        Background=""{{DynamicResource MaterialDesignPaper}}""
        FontFamily=""{{materialDesign:MaterialDesignFont}}"" 
        Title=""Test Window""
        Topmost=""True""
        WindowStartupLocation=""CenterScreen"">
        {xaml}
</Window>";
            IWindow window = await app.CreateWindow(windowXaml);
            return await window.GetElement<T>(".Content");
        }

        public static async Task<IVisualElement> CreateWindowWith(this IApp app, string xaml)
        {
            string windowXaml = @$"<Window
        xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation""
        xmlns:x=""http://schemas.microsoft.com/winfx/2006/xaml""
        xmlns:d=""http://schemas.microsoft.com/expression/blend/2008""
        xmlns:mc=""http://schemas.openxmlformats.org/markup-compatibility/2006""
        xmlns:materialDesign=""http://materialdesigninxaml.net/winfx/xaml/themes""
        mc:Ignorable=""d""
        Height=""800"" Width=""1100""
        TextElement.Foreground=""{{DynamicResource MaterialDesignBody}}""
        TextElement.FontWeight=""Regular""
        TextElement.FontSize=""13""
        TextOptions.TextFormattingMode=""Ideal"" 
        TextOptions.TextRenderingMode=""Auto""
        Background=""{{DynamicResource MaterialDesignPaper}}""
        FontFamily=""{{materialDesign:MaterialDesignFont}}"" 
        Title=""Test Window""
        Topmost=""True""
        WindowStartupLocation=""CenterScreen"">
        {xaml}
</Window>";
            IWindow window = await app.CreateWindow(windowXaml);
            return await window.GetElement(".Content");
        }

        public static async Task<IVisualElement> CreateWindowWithUserControl<TControl>(this IApp app)
            where TControl : UserControl
        {
            string windowXaml = @$"<Window
        xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation""
        xmlns:x=""http://schemas.microsoft.com/winfx/2006/xaml""
        xmlns:d=""http://schemas.microsoft.com/expression/blend/2008""
        xmlns:mc=""http://schemas.openxmlformats.org/markup-compatibility/2006""
        xmlns:materialDesign=""http://materialdesigninxaml.net/winfx/xaml/themes""
        xmlns:local=""clr-namespace:{typeof(TControl).Namespace};assembly={typeof(TControl).Assembly.GetName().Name}""
        mc:Ignorable=""d""
        Height=""800"" Width=""1100""
        TextElement.Foreground=""{{DynamicResource MaterialDesignBody}}""
        TextElement.FontWeight=""Regular""
        TextElement.FontSize=""13""
        TextOptions.TextFormattingMode=""Ideal"" 
        TextOptions.TextRenderingMode=""Auto""
        Background=""{{DynamicResource MaterialDesignPaper}}""
        FontFamily=""{{materialDesign:MaterialDesignFont}}"" 
        Title=""Test Window""
        Topmost=""True""
        WindowStartupLocation=""CenterScreen"">
        <local:{typeof(TControl).Name} />
</Window>";
            IWindow window = await app.CreateWindow(windowXaml);
            return await window.GetElement(".Content.Content");
        }
    }
}
