using System;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using MaterialDesignThemes.Wpf;
using XamlTest;
using Xunit;
using Xunit.Abstractions;

[assembly: CollectionBehavior(DisableTestParallelization = true)]
[assembly: GenerateHelpers(typeof(SmartHint))]
[assembly: GenerateHelpers(typeof(TimePicker))]

namespace MaterialDesignThemes.UITests
{
    public abstract class TestBase : IDisposable
    {
        protected ITestOutputHelper Output { get; }

        protected IApp App { get; }

        public TestBase(ITestOutputHelper output)
        {
            Output = output ?? throw new ArgumentNullException(nameof(output));

            App = XamlTest.App.StartRemote();
        }

        protected async Task<Color> GetThemeColor(string name)
        {
            IResource resource = await App.GetResource(name);
            return resource.GetAs<Color?>() ?? throw new Exception($"Failed to convert resource '{name}' to color");
        }

        protected async Task<IVisualElement<T>> LoadXaml<T>(string xaml)
        {
            await App.InitialzeWithMaterialDesign();
            return await App.CreateWindowWith<T>(xaml);
        }

        protected async Task<IVisualElement> LoadUserControl<TControl>()
            where TControl : UserControl
        {
            await App.InitialzeWithMaterialDesign();
            return await App.CreateWindowWithUserControl<TControl>();
        }

        public virtual void Dispose() => App.Dispose();
    }
}
