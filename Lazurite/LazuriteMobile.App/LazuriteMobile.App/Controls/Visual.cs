﻿using Xamarin.Forms;

namespace LazuriteMobile.App.Controls
{
    public static class Visual
    {
        public static readonly Color ItemBackground = Color.FromRgb(37, 37, 45);
        public static readonly Color ItemSelection = Color.SlateBlue;
        public static readonly Color BrightItemBackground = Color.FromRgb(60, 60, 68);
        public static readonly Color Background = Color.FromRgb(26, 26, 34);
        public static readonly Color BackgroundLazurite = Color.FromRgb(29, 25, 39);
        public static readonly Color CaptionForeground = Color.SteelBlue;
        public static readonly Color Foreground = Color.LightGray;
        public static readonly int FontSize = 14;
        public static readonly int BigFontSize = 17;

        public static readonly string FontFamily = "sans-serif-light";

        public static readonly Color SwitchBackground = ItemBackground;
        public static readonly Color SwitchBackgroundReadonly = Color.FromRgb(0, 49, 83);

        public static readonly Color SelectedSwitchBackground = ItemSelection;
        public static readonly Color SelectedSwitchBackgroundReadonly = Color.FromRgb(0, 80, 130);
    }
}