using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using Xunit;
using Xunit.Abstractions;

namespace MaterialDesignThemes.UITests.DemoApp
{
    public class DemoAppTests : TestBase
    {
        public DemoAppTests(ITestOutputHelper output)
            : base(output, App.DemoAppPath)
        {
            WindowsElement? element = Driver.GetElementByName("Material Design In XAML Toolkit");
            Assert.NotNull(element);
        }

        [Fact]
        public void CanOpenAllPagesOnTheDemoApp()
        {
            using var recorder = new TestRecorder(Driver, Output);

            var mainWindow = new MainWindow(Driver);

            foreach (AppiumWebElement? listItem in mainWindow.PageListItems)
            {
                var rect = mainWindow.PagesListBox.Rect;
                Driver.WaitFor(() => mainWindow.PagesListBox.Rect.Right <= 1);
                Driver.WaitFor(() => mainWindow.HamburgerToggleButton.Displayed);
                
                Driver.WaitFor(() =>
                {
                    mainWindow.HamburgerToggleButton.Click();
                    return mainWindow.PagesListBox.Rect.X >= 0;
                });
                listItem.Click();
            }

            recorder.Success();
        }
    }
}
