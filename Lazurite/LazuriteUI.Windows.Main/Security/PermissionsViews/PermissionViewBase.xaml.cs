﻿using System;
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

namespace LazuriteUI.Windows.Main.Security.PermissionsViews
{
    /// <summary>
    /// Логика взаимодействия для PermissionViewBase.xaml
    /// </summary>
    public partial class PermissionViewBase : UserControl
    {
        public PermissionViewBase()
        {
            InitializeComponent();
        }

        public virtual bool IsSelectButtonVisible
        {
            get
            {
                return true;
            }
        }

        public event Action<PermissionViewBase> RemoveClicked;
    }
}