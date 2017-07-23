﻿using Lazurite.MainDomain;
using LazuriteUI.Windows.Controls;
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

namespace LazuriteUI.Windows.Main.Security
{
    /// <summary>
    /// Логика взаимодействия для GroupsSelectView.xaml
    /// </summary>
    public partial class UsersSelectView : UserControl
    {
        public UsersSelectView(UserBase[] selectedUsers)
        {
            InitializeComponent();
            this.usersView.SelectedUsers = selectedUsers;
        }

        public UserBase[] SelectedGroups
        {
            get
            {
                return this.usersView.SelectedUsers;
            }
        }

        private void ItemView_Click(object sender, RoutedEventArgs e)
        {
            ApplyClicked?.Invoke();
        }

        public event Action ApplyClicked;

        public static void Show(Action<UserBase[]> callback, UserBase[] selectedUsers)
        {
            var control = new UsersSelectView(selectedUsers);
            var dialogView = new DialogView(control);
            dialogView.Caption = "Выберите пользователей";
            control.ApplyClicked += () => {
                callback?.Invoke(control.SelectedGroups);
                dialogView.Close();
            };
            dialogView.Show();
        }
    }
}