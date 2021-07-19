using System;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using MaterialDesignThemes.Wpf;
using XamlTest;

namespace MaterialDesignThemes.UITests.WPF.TimePickers
{
    public static class MaterialDesignTimePicker
    {
        public static async Task PickClock(this IVisualElement timePicker, int hour, int minute)
        {
            var button = await timePicker.GetElement<Button>("PART_Button");
            await button.LeftClick();

            var popup = await timePicker.GetElement<Popup>("PART_Popup");
            await popup.ClickHour(hour);
            await popup.ClickMinute(minute);
        }

        private static async Task ClickHour(this IVisualElement<Popup> popup, int hour)
            => await popup.ClickButton(Clock.HoursCanvasPartName, (hour + 11) % 12);

        private static async Task ClickMinute(this IVisualElement<Popup> popup, int minute)
            => await popup.ClickButton(Clock.MinutesCanvasPartName, (minute % 5 == 0 ? (minute / 5 + 11) % 12 : minute - minute / 5 + 11));

        private static async Task ClickButton(this IVisualElement<Popup> popup, string partName, int index)
        {
            const int delay = 200;

            await Task.Delay(delay);
            var canvas = await Wait.For(async () => await popup.GetElement(partName));
            var button = await canvas.GetElement<ClockItemButton>($"/ClockItemButton[{index}]");
            await Task.Delay(delay);
            await Wait.For(async () =>
            {
                await button.LeftClick();
                await Task.Delay(delay);
                return await button.GetIsChecked() == true;
            });
        }
    }
}
