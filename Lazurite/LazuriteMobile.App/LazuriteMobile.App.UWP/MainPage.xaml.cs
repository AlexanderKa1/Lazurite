﻿namespace LazuriteMobile.App.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();

            LoadApplication(new LazuriteMobile.App.App());
        }
    }
}
