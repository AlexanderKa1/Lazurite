﻿using Lazurite.Windows.Logging;
using System;
using System.Windows;

namespace LazuriteUI.Windows.Main.Journal
{
    /// <summary>
    /// Логика взаимодействия для JournalWindow.xaml
    /// </summary>
    public partial class JournalLightWindow : Window
    {
        private static JournalLightWindow _current;
        private static bool _closed;
        private static object _locker = new object();

        private JournalLightWindow()
        {
            InitializeComponent();
            Top = 5;
            Left = 5;
            _closed = false;
        }

        protected override void OnClosed(EventArgs e)
        {
            _closed = true;
            base.OnClosed(e);
        }

        public static void Show(string message, WarnType type)
        {
            if (!string.IsNullOrEmpty(message))
                Application.Current?.Dispatcher.BeginInvoke(new Action(() =>
                {
                    if (_current == null || _closed)
                    {
                        _current = new JournalLightWindow();
                        _current.Show();
                    }
                    _current.Set(message, type);
                }));
        }

        public static void CloseWindow()
        {
            Application.Current?.Dispatcher.BeginInvoke(new Action(() =>
            {
                if (_current != null || !_closed)
                    _current.Close();
            }));
        }

        private void Set(string message, WarnType type)
        {
            if (!string.IsNullOrEmpty(message))
            {
                if (stackPanel.Children.Count == 10)
                {
                    stackPanel.Children.Clear();
                    Height = 50;
                }
                var itemView = new JournaltemView();
                itemView.DataContext = new JournalItemViewModel(message, type);
                stackPanel.Children.Add(itemView);
                Height += itemView.Height;
            }
        }

        private void ItemView_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
