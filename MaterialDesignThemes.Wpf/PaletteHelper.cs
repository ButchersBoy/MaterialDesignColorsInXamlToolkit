﻿using MaterialDesignColors;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Runtime.InteropServices;
namespace MaterialDesignThemes.Wpf
{
    public class PaletteHelper
    {
        /// <summary>
        /// The primary swatch
        /// </summary>
        public static Swatch Primary;
        /// <summary>
        /// The accent swatch
        /// </summary>
        public static Swatch Accent;

        /// <summary>
        /// Primary color generated from DWM color
        /// </summary>
        public static Swatch AutoPrimary
        {
            get
            {
                var color = (System.Drawing.Color.FromArgb((int)Microsoft.Win32.Registry.GetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM","ColorizationColor", Colors.Purple)));
                return GetClosestSwatch(Color.FromArgb(color.A, color.R, color.G, color.B), false);
            }
        }
        /// <summary>
        /// Accent color generated from DWM color
        /// </summary>
        public static Swatch AutoAccent
        {
            get
            {
                var color = (System.Drawing.Color.FromArgb((int)Microsoft.Win32.Registry.GetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM","ColorizationColor", Colors.Green)));
                return GetClosestSwatch(Color.FromArgb(color.A, color.R, color.G, color.B), true);
            }
        }
        
        /// <summary>
        /// Matches a non material color to a material color
        /// </summary>
        /// <param name="baseColor">The color to match</param>
        /// <param name="accent">If the color is accent</param>
        /// <returns></returns>
        public static Swatch GetClosestSwatch(Color baseColor, bool accent)
        {
            var colors = new SwatchesProvider().Swatches.Select(x => new {Value = x, Diff = GetDiff(x.ExemplarHue.Color, baseColor)}).ToList();
            var min = colors.Min(x => x.Diff);
            var color = colors.FindIndex(x => x.Diff == min);
            return accent ? new SwatchesProvider().Swatches.ElementAtOrDefault(color + 1) != null ? new SwatchesProvider().Swatches.ElementAtOrDefault(color + 1) : new SwatchesProvider().Swatches.ElementAtOrDefault(color - 1) : new SwatchesProvider().Swatches.ElementAt(color);
        }
        private static int GetDiff(Color color, Color baseColor)
        {
            int a = color.A - baseColor.A,
                r = color.R - baseColor.R,
                g = color.G - baseColor.G,
                b = color.B - baseColor.B;
            return a*a + r*r + g*g + b*b;
        }
        public void SetLightDark(bool? isDark)
        {
            var existingResourceDictionary = Application.Current.Resources.MergedDictionaries
                .Where(rd => rd.Source != null)
                .SingleOrDefault(rd => Regex.Match(rd.Source.AbsolutePath, @"(\/MaterialDesignThemes.Wpf;component\/Themes\/MaterialDesignTheme\.)((Light)|(Dark))").Success);
            if (existingResourceDictionary == null)
                throw new ApplicationException("Unable to find Light/Dark base theme in Application resources.");

            var source =
                $"pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.{(isDark.Value ? "Dark" : "Light")}.xaml";
            var newResourceDictionary = new ResourceDictionary() { Source = new Uri(source) };

            Application.Current.Resources.MergedDictionaries.Remove(existingResourceDictionary);
            Application.Current.Resources.MergedDictionaries.Add(newResourceDictionary);

            var existingMahAppsResourceDictionary = Application.Current.Resources.MergedDictionaries
                .Where(rd => rd.Source != null)
                .SingleOrDefault(rd => Regex.Match(rd.Source.AbsolutePath, @"(\/MahApps.Metro;component\/Styles\/Accents\/)((BaseLight)|(BaseDark))").Success);
            if (existingMahAppsResourceDictionary == null) return;

            source =
                $"pack://application:,,,/MahApps.Metro;component/Styles/Accents/{(isDark.Value ? "BaseDark" : "BaseLight")}.xaml";
            var newMahAppsResourceDictionary = new ResourceDictionary { Source = new Uri(source) };

            Application.Current.Resources.MergedDictionaries.Remove(existingMahAppsResourceDictionary);
            Application.Current.Resources.MergedDictionaries.Add(newMahAppsResourceDictionary);
        }

        public void ReplacePrimaryColor(Swatch swatch)
        {
            if (swatch == null) throw new ArgumentNullException(nameof(swatch));

            Primary = swatch;

            var list = swatch.PrimaryHues.ToList();
            var light = list[2];
            var mid = swatch.ExemplarHue;
            var dark = list[7];

            foreach (var color in swatch.PrimaryHues)
            {
                ReplaceEntry(color.Name, color.Color);
                ReplaceEntry(color.Name + "Foreground", color.Foreground);
            }

            ReplaceEntry("PrimaryHueLightBrush", new SolidColorBrush(light.Color));
            ReplaceEntry("PrimaryHueLightForegroundBrush", new SolidColorBrush(light.Foreground));
            ReplaceEntry("PrimaryHueMidBrush", new SolidColorBrush(mid.Color));
            ReplaceEntry("PrimaryHueMidForegroundBrush", new SolidColorBrush(mid.Foreground));
            ReplaceEntry("PrimaryHueDarkBrush", new SolidColorBrush(dark.Color));
            ReplaceEntry("PrimaryHueDarkForegroundBrush", new SolidColorBrush(dark.Foreground));

            //mahapps brushes            
            ReplaceEntry("HighlightBrush", new SolidColorBrush(dark.Color));
            ReplaceEntry("AccentColorBrush", new SolidColorBrush(list[5].Color));
            ReplaceEntry("AccentColorBrush2", new SolidColorBrush(list[4].Color));
            ReplaceEntry("AccentColorBrush3", new SolidColorBrush(list[3].Color));
            ReplaceEntry("AccentColorBrush4", new SolidColorBrush(list[2].Color));
            ReplaceEntry("WindowTitleColorBrush", new SolidColorBrush(dark.Color));
            ReplaceEntry("AccentSelectedColorBrush", new SolidColorBrush(list[5].Foreground));
            ReplaceEntry("ProgressBrush", new LinearGradientBrush(dark.Color, list[3].Color, 90.0));
            ReplaceEntry("CheckmarkFill", new SolidColorBrush(list[5].Color));
            ReplaceEntry("RightArrowFill", new SolidColorBrush(list[5].Color));
            ReplaceEntry("IdealForegroundColorBrush", new SolidColorBrush(list[5].Foreground));
            ReplaceEntry("IdealForegroundDisabledBrush", new SolidColorBrush(dark.Color) { Opacity = .4 });                   
        }

        public void ReplacePrimaryColor(string name)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));

            var swatch = new SwatchesProvider().Swatches.FirstOrDefault(
                s => string.Compare(s.Name, name, StringComparison.InvariantCultureIgnoreCase) == 0);

            if (swatch == null)
                throw new ArgumentException($"No such swatch '{name}'", nameof(name));

            ReplacePrimaryColor(swatch);
        }

        public void ReplaceAccentColor(Swatch swatch)
        {
            if (swatch == null) throw new ArgumentNullException(nameof(swatch));

            Accent = swatch;

            foreach (var color in swatch.AccentHues)
            {
                ReplaceEntry(color.Name, color.Color);
                ReplaceEntry(color.Name + "Foreground", color.Foreground);
            }

            ReplaceEntry("SecondaryAccentBrush", new SolidColorBrush(swatch.AccentExemplarHue.Color));
            ReplaceEntry("SecondaryAccentForegroundBrush", new SolidColorBrush(swatch.AccentExemplarHue.Foreground));
        }

        public void ReplaceAccentColor(string name)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));

            var swatch = new SwatchesProvider().Swatches.FirstOrDefault(
                s => string.Compare(s.Name, name, StringComparison.InvariantCultureIgnoreCase) == 0 && s.IsAccented);

            if (swatch == null)
                throw new ArgumentException($"No such accented swatch '{name}'", nameof(name));

            ReplaceAccentColor(swatch);
        }
        
        /// <summary>
        /// Replaces a certain entry anywhere in the parent dictionary and its merged dictionaries
        /// </summary>
        /// <param name="entryName">The entry to replace</param>
        /// <param name="newValue">The new entry value</param>
        /// <param name="parentDictionary">The root dictionary to start searching at. Null means using Application.Current.Resources</param>
        private static void ReplaceEntry(object entryName, object newValue, ResourceDictionary parentDictionary = null)
        {            
            if (parentDictionary == null)
                parentDictionary = Application.Current.Resources;
            
            if (parentDictionary.Contains(entryName))
            {
                var brush = parentDictionary[entryName] as SolidColorBrush;
                if (brush != null && !brush.IsFrozen)
                {                 
                    var animation = new ColorAnimation
                    {
                        From = ((SolidColorBrush)parentDictionary[entryName]).Color,
                        To = ((SolidColorBrush)newValue).Color,
                        Duration = new Duration(TimeSpan.FromMilliseconds(300))
                    };
                    brush.BeginAnimation(SolidColorBrush.ColorProperty, animation);
                }
                else
                    parentDictionary[entryName] = newValue; //Set value normally
            }

            foreach (var dictionary in parentDictionary.MergedDictionaries)
                ReplaceEntry(entryName, newValue, dictionary);
        }
    }    
}
