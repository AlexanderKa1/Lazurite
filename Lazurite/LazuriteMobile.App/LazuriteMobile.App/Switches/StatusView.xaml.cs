﻿using Lazurite.MainDomain;
using LazuriteMobile.App.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace LazuriteMobile.App.Switches
{
    public partial class StatusView : ContentView
    {
        public StatusView()
        {
            InitializeComponent();
        }

        public StatusView(ScenarioInfo scenario, UserVisualSettings visualSettings) : this()
        {
            this.BindingContext = new ScenarioModel(scenario, visualSettings);
            itemView.Click += ItemView_Click;
        }

        private void ItemView_Click(object sender, EventArgs e)
        {
            var statusSwitch = new StatusViewSwitch((ScenarioModel)this.BindingContext);
            var dialog = new DialogView(statusSwitch);
            statusSwitch.StateChanged += (o, e2) => dialog.Close();
            dialog.Show(Helper.GetLastParent(this));
        }
    }
}