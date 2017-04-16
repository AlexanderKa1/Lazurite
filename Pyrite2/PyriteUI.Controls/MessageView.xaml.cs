﻿using PyriteUI.Icons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PyriteUI.Controls
{
    /// <summary>
    /// Логика взаимодействия для MessageView.xaml
    /// </summary>
    public partial class MessageView : UserControl
    {
        public MessageView()
        {
            InitializeComponent();
        }

        public void Show(Panel parentElement)
        {
            parentElement.Children.Add(this);
            Panel.SetZIndex(this, 999);
        }

        public void Close()
        {
            ((Panel)Parent).Children.Remove(this);
        }

        public void SetItems(MessageItemInfo[] itemsInfos)
        {
            itemsView.Children.Clear();
            foreach (var item in itemsInfos)
            {
                var itemView = new ItemView() {
                    Content = item.Text,
                    Icon = item.Icon ?? Icon.Add,
                    IconVisibility = item.Icon != null ? Visibility.Visible : Visibility.Collapsed
                };
                itemView.MinWidth = 140;
                itemView.Margin = new Thickness(2,0,0,0);
                itemView.Click += (o, e) => item.Click?.Invoke(this);
                itemsView.Children.Add(itemView);
            }
        }

        public bool IsItemsEnabled
        {
            get
            {
                return itemsView.IsEnabled;
            }
            set
            {
                itemsView.IsEnabled = value;
            }
        }

        public string ContentText
        {
            get
            {
                return tbText.Text;
            }
            set
            {
                tbText.Text = value;
            }
        }

        public string HeaderText
        {
            get
            {
                return captionView.Content.ToString();
            }
            set
            {
                captionView.Content = value;
            }
        }

        public bool IsIconVisible
        {
            get
            {
                return iconView.Visibility != Visibility.Collapsed;
            }
            set
            {
                iconView.Visibility = value ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public Icon Icon
        {
            get
            {
                return iconView.Icon;
            }
            set
            {
                iconView.Icon = value;
            }
        }

        public void StartAnimateProgress()
        {
            captionView.StartAnimateProgress();
        }

        public void StopAnimateProgress()
        {
            captionView.StopAnimateProgress();
        }
    }
}
