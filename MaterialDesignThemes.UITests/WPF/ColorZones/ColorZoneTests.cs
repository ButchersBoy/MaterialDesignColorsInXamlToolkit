using System.Threading.Tasks;
using System.Windows.Media;
using MaterialDesignThemes.Wpf;
using XamlTest;
using Xunit;
using Xunit.Abstractions;

namespace MaterialDesignThemes.UITests.WPF.ColorZones
{
    public class ColorZoneTests : TestBase
    {
        public ColorZoneTests(ITestOutputHelper output)
            : base(output)
        {

        }

        [Theory]
        [InlineData(ColorZoneMode.Standard, "MaterialDesignPaper", "MaterialDesignBody")]
        [InlineData(ColorZoneMode.Inverted, "MaterialDesignBody", "MaterialDesignPaper")]
        [InlineData(ColorZoneMode.PrimaryLight, "PrimaryHueLightBrush", "PrimaryHueLightForegroundBrush")]
        [InlineData(ColorZoneMode.PrimaryMid, "PrimaryHueMidBrush", "PrimaryHueMidForegroundBrush")]
        [InlineData(ColorZoneMode.PrimaryDark, "PrimaryHueDarkBrush", "PrimaryHueDarkForegroundBrush")]
        [InlineData(ColorZoneMode.SecondaryLight, "SecondaryHueLightBrush", "SecondaryHueLightForegroundBrush")]
        [InlineData(ColorZoneMode.SecondaryMid, "SecondaryHueMidBrush", "SecondaryHueMidForegroundBrush")]
        [InlineData(ColorZoneMode.SecondaryDark, "SecondaryHueDarkBrush", "SecondaryHueDarkForegroundBrush")]
        [InlineData(ColorZoneMode.Light, "MaterialDesignLightBackground", "MaterialDesignLightForeground")]
        [InlineData(ColorZoneMode.Dark, "MaterialDesignDarkBackground", "MaterialDesignDarkForeground")]
        public async Task Mode_SetsThemeColors(ColorZoneMode mode, string backgroundBrush, string foregroundBrush)
        {
            await using var recorder = new TestRecorder(App);

            IVisualElement<ColorZone> colorZone = await LoadXaml<ColorZone>(@$"
<materialDesign:ColorZone Mode=""{mode}""/>
");
            Color background = await GetThemeColor(backgroundBrush);
            Color foreground = await GetThemeColor(foregroundBrush);

            Assert.Equal(background, await colorZone.GetBackgroundColor());
            Assert.Equal(foreground, await colorZone.GetForegroundColor());

            recorder.Success();
        }
    }
}
