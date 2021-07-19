using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using MaterialDesignThemes.UITests.Samples.DialogHost;
using XamlTest;
using Xunit;
using Xunit.Abstractions;

namespace MaterialDesignThemes.UITests.WPF.DialogHosts
{
    public class DialogHostTests : TestBase
    { 
        public DialogHostTests(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public async Task OnOpenDialog_OverlayCoversContent()
        {
            await using var recorder = new TestRecorder(App);

            IVisualElement dialogHost = await LoadUserControl<WithCounter>();
            var overlay = await dialogHost.GetElement<Grid>("PART_ContentCoverGrid");

            var resultTextBlock = await dialogHost.GetElement<TextBlock>("ResultTextBlock");
            await Wait.For(async () => await resultTextBlock.GetText() == "Clicks: 0");

            var testOverlayButton = await dialogHost.GetElement<Button>("TestOverlayButton");
            await testOverlayButton.LeftClick();
            await Wait.For(async () => await resultTextBlock.GetText() == "Clicks: 1");

            var showDialogButton = await dialogHost.GetElement<Button>("ShowDialogButton");
            await showDialogButton.LeftClick();

            var closeDialogButton = await dialogHost.GetElement<Button>("CloseDialogButton");
            await Wait.For(async () => await closeDialogButton.GetIsVisible() == true);

            await testOverlayButton.LeftClick();
            await Wait.For(async () => await resultTextBlock.GetText() == "Clicks: 1");
            await closeDialogButton.LeftClick();

            var retry = new Retry(5, TimeSpan.FromSeconds(5));
            await Wait.For(async () => await overlay.GetVisibility() != Visibility.Visible, retry);
            await testOverlayButton.LeftClick();
            await Wait.For(async () => Assert.Equal("Clicks: 2", await resultTextBlock.GetText()), retry);
        }

        [Fact]
        [Description("Issue 2282")]
        public async Task ClosingDialogWithIsOpenProperty_ShouldRaiseDialogClosingEvent()
        {
            await using var recorder = new TestRecorder(App);

            IVisualElement dialogHost = await LoadUserControl<ClosingEventCounter>();
            var showButton = await dialogHost.GetElement<Button>("ShowDialogButton");
            var closeButton = await dialogHost.GetElement<Button>("CloseButton");
            var resultTextBlock = await dialogHost.GetElement<TextBlock>("ResultTextBlock");

            await showButton.LeftClick();
            await Wait.For(async () => await closeButton.GetIsVisible());
            await Task.Delay(300);
            await closeButton.LeftClick();

            await Wait.For(async () => Assert.Equal("1", await resultTextBlock.GetText()));
            recorder.Success();
        }
    }
}
