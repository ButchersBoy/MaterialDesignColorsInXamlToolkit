using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using XamlTest;
using Xunit;
using Xunit.Abstractions;

namespace MaterialDesignThemes.UITests.WPF.ListBoxes
{
    public class ListBoxTests : TestBase
    {
        public ListBoxTests(ITestOutputHelper output)
            : base(output)
        { }

        [Fact]
        public async Task OnMouseOver_BackgroundIsSet()
        {
            await using var recorder = new TestRecorder(App);

            var listBox = await LoadXaml<ListBox>(@"
<ListBox MinWidth=""200"">
    <ListBoxItem Content=""Item1"" />
    <ListBoxItem Content=""Item2"" />
    <ListBoxItem Content=""Item3"" />
    <ListBoxItem Content=""Item4"" />
</ListBox>
");

            var listBoxItem = await listBox.GetElement<ListBoxItem>("/ListBoxItem[2]");
            Assert.Equal("Item3", await listBoxItem.GetContent());
            var mouseOverBorder = await listBoxItem.GetElement<Border>("MouseOverBorder");

            await listBox.MoveCursorTo(Position.TopLeft);
            await Wait.For(async () => Assert.Equal(0.0, await mouseOverBorder.GetOpacity()));

            await mouseOverBorder.MoveCursorTo();
            await Wait.For(async () =>
            {
                double opacity = await mouseOverBorder.GetOpacity();
                Output.WriteLine($"Got opacity {opacity}");
                Assert.Equal(0.1, opacity);
            });

            Color effectiveBackground = await mouseOverBorder.GetEffectiveBackground();
            Color? foreground = await listBoxItem.GetForegroundColor();
            foreground = foreground?.FlattenOnto(effectiveBackground);

            float? contrastRatio = foreground?.ContrastRatio(effectiveBackground);
            Assert.True(contrastRatio >= MaterialDesignSpec.MinimumContrastSmallText);

            recorder.Success();
        }

        [Theory]
        [Description("Issue 1994")]
        [InlineData("MaterialDesignFilterChipListBox")]
        [InlineData("MaterialDesignFilterChipPrimaryListBox")]
        [InlineData("MaterialDesignFilterChipAccentListBox")]
        [InlineData("MaterialDesignFilterChipOutlineListBox")]
        [InlineData("MaterialDesignFilterChipPrimaryOutlineListBox")]
        [InlineData("MaterialDesignFilterChipAccentOutlineListBox")]
        [InlineData("MaterialDesignChoiceChipListBox")]
        [InlineData("MaterialDesignChoiceChipPrimaryListBox")]
        [InlineData("MaterialDesignChoiceChipAccentListBox")]
        [InlineData("MaterialDesignChoiceChipOutlineListBox")]
        [InlineData("MaterialDesignChoiceChipPrimaryOutlineListBox")]
        [InlineData("MaterialDesignChoiceChipAccentOutlineListBox")]
        public async Task OnClickChoiceChipListBox_ChangesSelectedItem(string listBoxStyle)
        {
            await using var recorder = new TestRecorder(App);

            var listBox = await LoadXaml<ListBox>($@"
<ListBox x:Name=""ChipsListBox"" Style=""{{StaticResource {listBoxStyle}}}"">
    <ListBoxItem>Mercury</ListBoxItem>
    <ListBoxItem>Venus</ListBoxItem>
    <ListBoxItem>Earth</ListBoxItem>
    <ListBoxItem>Pluto</ListBoxItem>
</ListBox>
");
            var earth = await listBox.GetElement<ListBoxItem>("/ListBoxItem[2]");
            await earth.LeftClick();

            await Wait.For(async () => Assert.Equal(2, await listBox.GetSelectedIndex()));

            recorder.Success();
        }

        [Fact]
        public async Task ScrollBarAssist_ButtonsVisibility_HidesButtonsOnMinimalistStyle()
        {
            await using var recorder = new TestRecorder(App);

            string xaml = @"<ListBox Height=""300"" Width=""300""
materialDesign:ScrollBarAssist.ButtonsVisibility=""Collapsed"" 
ScrollViewer.HorizontalScrollBarVisibility=""Visible"" 
ScrollViewer.VerticalScrollBarVisibility=""Visible"">
<ListBox.Resources>
    <Style BasedOn=""{StaticResource MaterialDesignScrollBarMinimal}"" TargetType=""{x:Type ScrollBar}"" />
</ListBox.Resources>
";
            for(int i = 0; i < 50; i++)
            {
                xaml += $"    <ListBoxItem>This is a pretty long meaningless text just to make horizontal scrollbar visibile</ListBoxItem>{Environment.NewLine}";
            }
            xaml += "</ListBox>";

            var listBox = await LoadXaml<ListBox>(xaml);
            var verticalScrollBar = await listBox.GetElement<ScrollBar>("PART_VerticalScrollBar");
            var horizontalScrollBar = await listBox.GetElement<ScrollBar>("PART_HorizontalScrollBar");

            Assert.Equal(17, await verticalScrollBar.GetActualWidth());
            var verticalThumb = await verticalScrollBar.GetElement<Border>("/Thumb~border");
            Assert.Equal(10, await verticalThumb.GetActualWidth());
            var upButton = await verticalScrollBar.GetElement<RepeatButton>("PART_LineUpButton");
            Assert.False(await upButton.GetIsVisible());
            var downButton = await verticalScrollBar.GetElement<RepeatButton>("PART_LineDownButton");
            Assert.False(await downButton.GetIsVisible());

            Assert.Equal(17, await horizontalScrollBar.GetActualHeight());
            var horizontalThumb = await horizontalScrollBar.GetElement<Border>("/Thumb~border");
            Assert.Equal(10, await horizontalThumb.GetActualHeight());
            var leftButton = await horizontalScrollBar.GetElement<RepeatButton>("PART_LineLeftButton");
            Assert.False(await leftButton.GetIsVisible());
            var rightButton = await horizontalScrollBar.GetElement<RepeatButton>("PART_LineRightButton");
            Assert.False(await rightButton.GetIsVisible());

            recorder.Success();
        }
    }
}
