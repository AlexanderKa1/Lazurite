﻿using LazuriteMobile.App.Controls;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace LazuriteMobile.App.Switches
{
    public class Background_BoolToColor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value)
                return Visual.SwitchBackground;
            else
                return Visual.SwitchBackgroundReadonly;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
