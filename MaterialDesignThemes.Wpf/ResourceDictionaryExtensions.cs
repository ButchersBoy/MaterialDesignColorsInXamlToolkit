using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using MaterialDesignColors;
using MaterialDesignColors.ColorManipulation;

namespace MaterialDesignThemes.Wpf
{
    public static class ResourceDictionaryExtensions
    {
        private const string CurrentThemeKey = nameof(MaterialDesignThemes) + "." + nameof(CurrentThemeKey);
        private const string ThemeManagerKey = nameof(MaterialDesignThemes) + "." + nameof(ThemeManagerKey);

        public static void SetTheme(this ResourceDictionary resourceDictionary, ITheme theme)
            => SetTheme(resourceDictionary, theme, null);

        public static void SetTheme(this ResourceDictionary resourceDictionary, ITheme theme, ColorAdjustment? colorAdjustment)
        {
            if (resourceDictionary is null) throw new ArgumentNullException(nameof(resourceDictionary));

            if (theme is Theme internalTheme)
            {
                colorAdjustment ??= internalTheme.ColorAdjustment;
                internalTheme.ColorAdjustment = colorAdjustment;
            }

            Color primaryLight = theme.PrimaryLight.Color;
            Color primaryMid = theme.PrimaryMid.Color;
            Color primaryDark = theme.PrimaryDark.Color;

            Color secondaryLight = theme.SecondaryLight.Color;
            Color secondaryMid = theme.SecondaryMid.Color;
            Color secondaryDark = theme.SecondaryDark.Color;

            if (colorAdjustment is { })
            {
                if (colorAdjustment.Colors.HasFlag(ColorSelection.Primary))
                {
                    AdjustColors(theme.Paper, colorAdjustment, ref primaryLight, ref primaryMid, ref primaryDark);
                }
                if (colorAdjustment.Colors.HasFlag(ColorSelection.Secondary))
                {
                    AdjustColors(theme.Paper, colorAdjustment, ref secondaryLight, ref secondaryMid, ref secondaryDark);
                }
            }

            SetSolidColorBrush(resourceDictionary, "PrimaryHueLightBrush", primaryLight);
            SetSolidColorBrush(resourceDictionary, "PrimaryHueLightForegroundBrush", theme.PrimaryLight.ForegroundColor ?? primaryLight.ContrastingForegroundColor());
            SetSolidColorBrush(resourceDictionary, "PrimaryHueMidBrush", primaryMid);
            SetSolidColorBrush(resourceDictionary, "PrimaryHueMidForegroundBrush", theme.PrimaryMid.ForegroundColor ?? primaryMid.ContrastingForegroundColor());
            SetSolidColorBrush(resourceDictionary, "PrimaryHueDarkBrush", primaryDark);
            SetSolidColorBrush(resourceDictionary, "PrimaryHueDarkForegroundBrush", theme.PrimaryDark.ForegroundColor ?? primaryDark.ContrastingForegroundColor());

            SetSolidColorBrush(resourceDictionary, "SecondaryHueLightBrush", secondaryLight);
            SetSolidColorBrush(resourceDictionary, "SecondaryHueLightForegroundBrush", theme.SecondaryLight.ForegroundColor ?? secondaryLight.ContrastingForegroundColor());
            SetSolidColorBrush(resourceDictionary, "SecondaryHueMidBrush", secondaryMid);
            SetSolidColorBrush(resourceDictionary, "SecondaryHueMidForegroundBrush", theme.SecondaryMid.ForegroundColor ?? secondaryMid.ContrastingForegroundColor());
            SetSolidColorBrush(resourceDictionary, "SecondaryHueDarkBrush", secondaryDark);
            SetSolidColorBrush(resourceDictionary, "SecondaryHueDarkForegroundBrush", theme.SecondaryDark.ForegroundColor ?? secondaryDark.ContrastingForegroundColor());

            SetSolidColorBrush(resourceDictionary, "MaterialDesignValidationErrorBrush", theme.ValidationError);
            resourceDictionary["MaterialDesignValidationErrorColor"] = theme.ValidationError;

            SetSolidColorBrush(resourceDictionary, "MaterialDesignBackground", theme.Background);
            SetSolidColorBrush(resourceDictionary, "MaterialDesignPaper", theme.Paper);
            SetSolidColorBrush(resourceDictionary, "MaterialDesignPaperDark", theme.PaperDark);
            SetSolidColorBrush(resourceDictionary, "MaterialDesignCardBackground", theme.CardBackground);
            SetSolidColorBrush(resourceDictionary, "MaterialDesignToolBarBackground", theme.ToolBarBackground);
            SetSolidColorBrush(resourceDictionary, "MaterialDesignBody", theme.Body);
            SetSolidColorBrush(resourceDictionary, "MaterialDesignBodyLight", theme.BodyLight);
            SetSolidColorBrush(resourceDictionary, "MaterialDesignColumnHeader", theme.ColumnHeader);
            SetSolidColorBrush(resourceDictionary, "MaterialDesignCheckBoxOff", theme.CheckBoxOff);
            SetSolidColorBrush(resourceDictionary, "MaterialDesignCheckBoxDisabled", theme.CheckBoxDisabled);
            SetSolidColorBrush(resourceDictionary, "MaterialDesignTextBoxBorder", theme.TextBoxBorder);
            SetSolidColorBrush(resourceDictionary, "MaterialDesignDivider", theme.Divider);
            SetSolidColorBrush(resourceDictionary, "MaterialDesignSelection", theme.Selection);
            SetSolidColorBrush(resourceDictionary, "MaterialDesignToolForeground", theme.ToolForeground);
            SetSolidColorBrush(resourceDictionary, "MaterialDesignToolBackground", theme.ToolBackground);
            SetSolidColorBrush(resourceDictionary, "MaterialDesignFlatButtonClick", theme.FlatButtonClick);
            SetSolidColorBrush(resourceDictionary, "MaterialDesignFlatButtonRipple", theme.FlatButtonRipple);
            SetSolidColorBrush(resourceDictionary, "MaterialDesignToolTipBackground", theme.ToolTipBackground);
            SetSolidColorBrush(resourceDictionary, "MaterialDesignChipBackground", theme.ChipBackground);
            SetSolidColorBrush(resourceDictionary, "MaterialDesignSnackbarBackground", theme.SnackbarBackground);
            SetSolidColorBrush(resourceDictionary, "MaterialDesignSnackbarMouseOver", theme.SnackbarMouseOver);
            SetSolidColorBrush(resourceDictionary, "MaterialDesignSnackbarRipple", theme.SnackbarRipple);
            SetSolidColorBrush(resourceDictionary, "MaterialDesignTextFieldBoxBackground", theme.TextFieldBoxBackground);
            SetSolidColorBrush(resourceDictionary, "MaterialDesignTextFieldBoxHoverBackground", theme.TextFieldBoxHoverBackground);
            SetSolidColorBrush(resourceDictionary, "MaterialDesignTextFieldBoxDisabledBackground", theme.TextFieldBoxDisabledBackground);
            SetSolidColorBrush(resourceDictionary, "MaterialDesignTextAreaBorder", theme.TextAreaBorder);
            SetSolidColorBrush(resourceDictionary, "MaterialDesignTextAreaInactiveBorder", theme.TextAreaInactiveBorder);
            SetSolidColorBrush(resourceDictionary, "MaterialDesignDataGridRowHoverBackground", theme.DataGridRowHoverBackground);

            if (!(resourceDictionary.GetThemeManager() is ThemeManager themeManager))
            {
                resourceDictionary[ThemeManagerKey] = themeManager = new ThemeManager(resourceDictionary);
            }
            ITheme oldTheme = resourceDictionary.GetTheme();
            resourceDictionary[CurrentThemeKey] = theme;

            themeManager.OnThemeChange(oldTheme, theme);
        }

        public static ITheme GetTheme(this ResourceDictionary resourceDictionary)
        {
            if (resourceDictionary is null) throw new ArgumentNullException(nameof(resourceDictionary));
            if (resourceDictionary[CurrentThemeKey] is ITheme theme)
            {
                return theme;
            }

            Color secondaryMid = GetColor("SecondaryHueMidBrush");
            Color secondaryMidForeground = GetColor("SecondaryHueMidForegroundBrush");

            if (!TryGetColor("SecondaryHueLightBrush", out Color secondaryLight))
            {
                secondaryLight = secondaryMid.Lighten();
            }
            if (!TryGetColor("SecondaryHueLightForegroundBrush", out Color secondaryLightForeground))
            {
                secondaryLightForeground = secondaryLight.ContrastingForegroundColor();
            }

            if (!TryGetColor("SecondaryHueDarkBrush", out Color secondaryDark))
            {
                secondaryDark = secondaryMid.Darken();
            }
            if (!TryGetColor("SecondaryHueDarkForegroundBrush", out Color secondaryDarkForeground))
            {
                secondaryDarkForeground = secondaryDark.ContrastingForegroundColor();
            }

            //Attempt to simply look up the appropriate resources
            return new Theme
            {
                PrimaryLight = new ColorPair(GetColor("PrimaryHueLightBrush"), GetColor("PrimaryHueLightForegroundBrush")),
                PrimaryMid = new ColorPair(GetColor("PrimaryHueMidBrush"), GetColor("PrimaryHueMidForegroundBrush")),
                PrimaryDark = new ColorPair(GetColor("PrimaryHueDarkBrush"), GetColor("PrimaryHueDarkForegroundBrush")),

                SecondaryLight = new ColorPair(secondaryLight, secondaryLightForeground),
                SecondaryMid = new ColorPair(secondaryMid, secondaryMidForeground),
                SecondaryDark = new ColorPair(secondaryDark, secondaryDarkForeground),

                Background = GetColor("MaterialDesignBackground"),
                Body = GetColor("MaterialDesignBody"),
                BodyLight = GetColor("MaterialDesignBodyLight"),
                CardBackground = GetColor("MaterialDesignCardBackground"),
                CheckBoxDisabled = GetColor("MaterialDesignCheckBoxDisabled"),
                CheckBoxOff = GetColor("MaterialDesignCheckBoxOff"),
                ChipBackground = GetColor("MaterialDesignChipBackground"),
                ColumnHeader = GetColor("MaterialDesignColumnHeader"),
                DataGridRowHoverBackground = GetColor("MaterialDesignDataGridRowHoverBackground"),
                Divider = GetColor("MaterialDesignDivider"),
                FlatButtonClick = GetColor("MaterialDesignFlatButtonClick"),
                FlatButtonRipple = GetColor("MaterialDesignFlatButtonRipple"),
                Selection = GetColor("MaterialDesignSelection"),
                SnackbarBackground = GetColor("MaterialDesignSnackbarBackground"),
                SnackbarMouseOver = GetColor("MaterialDesignSnackbarMouseOver"),
                SnackbarRipple = GetColor("MaterialDesignSnackbarRipple"),
                TextAreaBorder = GetColor("MaterialDesignTextAreaBorder"),
                TextAreaInactiveBorder = GetColor("MaterialDesignTextAreaInactiveBorder"),
                TextBoxBorder = GetColor("MaterialDesignTextBoxBorder"),
                TextFieldBoxBackground = GetColor("MaterialDesignTextFieldBoxBackground"),
                TextFieldBoxDisabledBackground = GetColor("MaterialDesignTextFieldBoxDisabledBackground"),
                TextFieldBoxHoverBackground = GetColor("MaterialDesignTextFieldBoxHoverBackground"),
                ToolBackground = GetColor("MaterialDesignToolBackground"),
                ToolBarBackground = GetColor("MaterialDesignToolBarBackground"),
                ToolForeground = GetColor("MaterialDesignToolForeground"),
                ToolTipBackground = GetColor("MaterialDesignToolTipBackground"),
                Paper = GetColor("MaterialDesignPaper"),
                PaperDark = GetColor("MaterialDesignPaperDark"),
                ValidationError = GetColor("MaterialDesignValidationErrorBrush")
            };

            Color GetColor(params string[] keys)
            {
                foreach (string key in keys)
                {
                    if (TryGetColor(key, out Color color))
                    {
                        return color;
                    }
                }
                throw new InvalidOperationException($"Could not locate required resource with key(s) '{string.Join(", ", keys)}'");
            }

            bool TryGetColor(string key, out Color color)
            {
                if (resourceDictionary[key] is SolidColorBrush brush)
                {
                    color = brush.Color;
                    return true;
                }
                color = default;
                return false;
            }
        }

        public static IThemeManager? GetThemeManager(this ResourceDictionary resourceDictionary)
        {
            if (resourceDictionary is null) throw new ArgumentNullException(nameof(resourceDictionary));
            return resourceDictionary[ThemeManagerKey] as IThemeManager;
        }

        internal static void SetSolidColorBrush(this ResourceDictionary sourceDictionary, string name, Color value)
        {
            if (sourceDictionary is null) throw new ArgumentNullException(nameof(sourceDictionary));
            if (name is null) throw new ArgumentNullException(nameof(name));

            sourceDictionary[name + "Color"] = value;

            if (sourceDictionary[name] is SolidColorBrush brush)
            {
                if (brush.Color == value) return;

                if (!brush.IsFrozen)
                {
                    var animation = new ColorAnimation
                    {
                        From = brush.Color,
                        To = value,
                        Duration = new Duration(TimeSpan.FromMilliseconds(300))
                    };
                    brush.BeginAnimation(SolidColorBrush.ColorProperty, animation);
                    return;
                }
            }

            var newBrush = new SolidColorBrush(value);
            newBrush.Freeze();
            sourceDictionary[name] = newBrush;
        }

        private static void AdjustColors(Color background, ColorAdjustment colorAdjustment, ref Color light, ref Color mid, ref Color dark)
        {
            double offset;
            switch (colorAdjustment.Contrast)
            {
                case Contrast.Low:
                    if (background.IsLightColor())
                    {
                        dark = dark.EnsureContrastRatio(background, colorAdjustment.DesiredContrastRatio, out offset);
                        if (Math.Abs(offset) > 0.0)
                        {
                            mid = mid.ShiftLightness(offset);
                            light = light.ShiftLightness(offset);
                        }
                    }
                    else
                    {
                        light = light.EnsureContrastRatio(background, colorAdjustment.DesiredContrastRatio, out offset);
                        if (Math.Abs(offset) > 0.0)
                        {
                            mid = mid.ShiftLightness(offset);
                            dark = dark.ShiftLightness(offset);
                        }
                    }
                    break;
                case Contrast.Medium:
                    mid = mid.EnsureContrastRatio(background, colorAdjustment.DesiredContrastRatio, out offset);
                    if (Math.Abs(offset) > 0.0)
                    {
                        dark = dark.ShiftLightness(offset);
                        light = light.ShiftLightness(offset);
                    }
                    break;
                case Contrast.High:
                    if (background.IsLightColor())
                    {
                        light = light.EnsureContrastRatio(background, colorAdjustment.DesiredContrastRatio, out offset);
                        if (Math.Abs(offset) > 0.0)
                        {
                            mid = mid.ShiftLightness(offset);
                            dark = dark.ShiftLightness(offset);
                        }
                    }
                    else
                    {
                        dark = dark.EnsureContrastRatio(background, colorAdjustment.DesiredContrastRatio, out offset);
                        if (Math.Abs(offset) > 0.0)
                        {
                            light = light.ShiftLightness(offset);
                            mid = mid.ShiftLightness(offset);
                        }
                    }
                    break;
            }
        }

        private class ThemeManager : IThemeManager
        {
            private ResourceDictionary _ResourceDictionary;

            public ThemeManager(ResourceDictionary resourceDictionary)
                => _ResourceDictionary = resourceDictionary ?? throw new ArgumentNullException(nameof(resourceDictionary));

            public event EventHandler<ThemeChangedEventArgs>? ThemeChanged;

            public void OnThemeChange(ITheme oldTheme, ITheme newTheme)
                => ThemeChanged?.Invoke(this, new ThemeChangedEventArgs(_ResourceDictionary, oldTheme, newTheme));
        }
    }
}