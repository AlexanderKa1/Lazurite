﻿using Lazurite.MainDomain;
using LazuriteMobile.App.Controls;
using System;

using Xamarin.Forms;

namespace LazuriteMobile.App
{
    public partial class SettingsView : ContentView
    {
        public SettingsView()
        {
        	InitializeComponent();

            tbHost.Completed += AnyTextBox_Completed;
            tbLogin.Completed += AnyTextBox_Completed;
            tbPassword.Completed += AnyTextBox_Completed;
            tbSecretCode.Completed += AnyTextBox_Completed;
            tbService.Completed += AnyTextBox_Completed;
            numPort.Completed += AnyTextBox_Completed;
        }

        // Navigate next control on OK pressed
        private void AnyTextBox_Completed(object sender, EventArgs e)
        {
            var sourceTb = sender as Entry;
            var index = gridMain.Children.IndexOf(sourceTb) + 1;
            View nextControl = null;
            while (!(nextControl is Entry || nextControl is ItemView) && index < gridMain.Children.Count)
                nextControl = gridMain.Children[index++];
            if (nextControl != null)
                nextControl.Focus();
        }

        public void SetCredentials(ConnectionCredentials credentials)
        {
            tbHost.Text = credentials.Host;
            tbLogin.Text = credentials.Login;
            tbPassword.Text = credentials.Password;
            tbSecretCode.Text = credentials.SecretKey;
            tbService.Text = credentials.ServiceName;
            numPort.Value = credentials.Port;
        }

        public ConnectionCredentials GetCredentials()
        {
            return new ConnectionCredentials() {
                Host = tbHost.Text,
                Login = tbLogin.Text,
                Password = tbPassword.Text,
                SecretKey = tbSecretCode.Text,
                Port = (ushort)numPort.Value,
                ServiceName = tbService.Text
            };
        }

        public void SetErrorMessage(string str)
        {
            lblErrorMessage.Text = str;
        }

        public void ClearErrorMessage()
        {
            SetErrorMessage("");
        }

        private void itemView_Click(object arg1, EventArgs arg2)
        {
            ConnectClicked?.Invoke(this);
        }

        public event Action<SettingsView> ConnectClicked;
    }
}
