using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using XamlTest;
using Xunit;
using Xunit.Abstractions;

namespace MaterialDesignThemes.UITests.WPF.Buttons
{
    public class OutlineButtonTests : TestBase
    {
        public OutlineButtonTests(ITestOutputHelper output)
            : base(output)
        { }

        [Fact]
        public async Task OutlinedButton_UsesThemeColorForBorder()
        {
            await using var recorder = new TestRecorder(App);

            //Arrange
            IVisualElement<Button> button = await LoadXaml<Button>(
                @"<Button Content=""Button"" Style=""{StaticResource MaterialDesignOutlinedButton}""/>");
            Color midColor = await GetThemeColor("PrimaryHueMidBrush");
            IVisualElement<Border> internalBorder = await button.GetElement<Border>("border");

            //Act
            Color? borderColor = await button.GetBorderBrushColor();
            Color? internalBorderColor = await internalBorder.GetBorderBrushColor();

            //Assert
            Assert.Equal(midColor, borderColor);
            Assert.Equal(midColor, internalBorderColor);

            recorder.Success();
        }

        [Fact]
        public async Task OutlinedButton_BorderCanBeOverridden()
        {
            await using var recorder = new TestRecorder(App);

            //Arrange
            var button = await LoadXaml<Button>(
                @"<Button Content=""Button""
                          Style=""{StaticResource MaterialDesignOutlinedButton}""
                          BorderThickness=""5""
                          BorderBrush=""Red""
                    />");
            Color midColor = await GetThemeColor("PrimaryHueMidBrush");
            IVisualElement<Border> internalBorder = await button.GetElement<Border>("border");

            //Act
            Thickness borderThickness = await internalBorder.GetBorderThickness();
            SolidColorBrush? borderBrush = (await internalBorder.GetBorderBrush()) as SolidColorBrush;

            //Assert
            Assert.Equal(new Thickness(5), borderThickness);
            Assert.Equal(Colors.Red, borderBrush?.Color);

            recorder.Success();
        }

        [Fact]
        public async Task OutlinedButton_OnMouseOver_UsesThemeBrush()
        {
            await using var recorder = new TestRecorder(App);

            //Arrange
            IVisualElement<Button> button = await LoadXaml<Button>(
                @"<Button Content=""Button"" Style=""{StaticResource MaterialDesignOutlinedButton}""/>");
            Color midColor = await GetThemeColor("PrimaryHueMidBrush");
            IVisualElement<Border> internalBorder = await button.GetElement<Border>("border");

            //Act
            await button.MoveCursorTo(Position.Center);
            await Wait.For(async () =>
            {
                SolidColorBrush? internalBorderBackground = (await internalBorder.GetBackground()) as SolidColorBrush;

                //Assert
                Assert.Equal(midColor, internalBorderBackground?.Color);
            });
            
            recorder.Success();
        }
    }
}
