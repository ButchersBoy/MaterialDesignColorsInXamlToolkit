using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using XamlTest;
using Xunit;
using Xunit.Abstractions;

namespace MaterialDesignThemes.UITests.WPF
{
    public class TextFieldDefaultHeightTests : TestBase
    {
        public TextFieldDefaultHeightTests(ITestOutputHelper output) : base(output) { }

        private const int Precision = 3;

        [Fact]
        public async Task SameHeightWithDefaultStyle()
        {
            await using var recorder = new TestRecorder(App);

            var stackPanel = await LoadXaml<StackPanel>(@"
<StackPanel>
    <TextBox />
    <PasswordBox />
    <ComboBox />
    <DatePicker />
    <materialDesign:TimePicker />
</StackPanel>");

            var height = await GetHeight(stackPanel, "TextBox");
            Assert.Equal(height, await GetHeight(stackPanel, "PasswordBox"), Precision);
            Assert.Equal(height, await GetHeight(stackPanel, "ComboBox"), Precision);
            Assert.Equal(height, await GetHeight(stackPanel, "DatePicker"), Precision);
            Assert.Equal(height, await GetHeight(stackPanel, "TimePicker"), Precision);

            recorder.Success();
        }

        [Fact]
        public async Task SameHeightWithFloatingHintStyle()
        {
            await using var recorder = new TestRecorder(App);

            var stackPanel = await LoadXaml<StackPanel>(@"
<StackPanel>
    <TextBox materialDesign:HintAssist.IsFloating=""True"" />
    <PasswordBox materialDesign:HintAssist.IsFloating=""True"" />
    <ComboBox materialDesign:HintAssist.IsFloating=""True"" />
    <DatePicker materialDesign:HintAssist.IsFloating=""True"" />
    <materialDesign:TimePicker materialDesign:HintAssist.IsFloating=""True"" />
</StackPanel>");

            var height = await GetHeight(stackPanel, "TextBox");
            Assert.True(height > 0);
            Assert.Equal(height, await GetHeight(stackPanel, "PasswordBox"), Precision);
            Assert.Equal(height, await GetHeight(stackPanel, "ComboBox"), Precision);
            Assert.Equal(height, await GetHeight(stackPanel, "DatePicker"), Precision);
            Assert.Equal(height, await GetHeight(stackPanel, "TimePicker"), Precision);

            recorder.Success();
        }

        [Fact]
        public async Task SameHeightWithFilledStyle()
        {
            await using var recorder = new TestRecorder(App);

            var stackPanel = await LoadXaml<StackPanel>(@"
<StackPanel>
    <TextBox Style=""{StaticResource MaterialDesignFilledTextBox}"" />
    <PasswordBox Style=""{StaticResource MaterialDesignFilledPasswordBox}"" />
    <ComboBox Style=""{StaticResource MaterialDesignFilledComboBox}"" />
    <DatePicker Style=""{StaticResource MaterialDesignFilledDatePicker}"" />
    <materialDesign:TimePicker Style=""{StaticResource MaterialDesignFilledTimePicker}"" />
</StackPanel>");

            var height = await GetHeight(stackPanel, "TextBox");
            Assert.True(height > 0);
            Assert.Equal(height, await GetHeight(stackPanel, "PasswordBox"), Precision);
            Assert.Equal(height, await GetHeight(stackPanel, "ComboBox"), Precision);
            Assert.Equal(height, await GetHeight(stackPanel, "DatePicker"), Precision);
            Assert.Equal(height, await GetHeight(stackPanel, "TimePicker"), Precision);

            recorder.Success();
        }
        
        [Fact]
        public async Task SameHeightWithOutlinedStyle()
        {
            await using var recorder = new TestRecorder(App);

            var stackPanel = await LoadXaml<StackPanel>(@"
<StackPanel>
    <TextBox Style=""{StaticResource MaterialDesignOutlinedTextBox}"" />
    <PasswordBox Style=""{StaticResource MaterialDesignOutlinedPasswordBox}"" />
    <DatePicker Style=""{StaticResource MaterialDesignOutlinedDatePicker}"" />
    <materialDesign:TimePicker Style=""{StaticResource MaterialDesignOutlinedTimePicker}"" />
</StackPanel>");

            var height = await GetHeight(stackPanel, "TextBox");
            Assert.True(height > 0);
            Assert.Equal(height, await GetHeight(stackPanel, "PasswordBox"), Precision);
            Assert.Equal(height, await GetHeight(stackPanel, "DatePicker"), Precision);
            Assert.Equal(height, await GetHeight(stackPanel, "TimePicker"), Precision);

            recorder.Success();
        }

        private static async Task<double> GetHeight(IVisualElement container, string type)
        {
            var element = await container.GetElement<FrameworkElement>("/" + type);
            var height = await element.GetActualHeight();
            Assert.True(height > 0);
            return height;
        }
    }
}