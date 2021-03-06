﻿using Lazurite.ActionsDomain.ValueTypes;
using Lazurite.IOC;
using Lazurite.MainDomain;
using LazuriteMobile.App.Controls;
using LazuriteMobile.App.Switches.Bases;
using LazuriteMobile.MainDomain;
using System;

using Xamarin.Forms;

namespace LazuriteMobile.App.Switches
{
    public partial class GeolocationView : SwitchBase
    {
        public GeolocationView()
        {
            InitializeComponent();
        }

        public GeolocationView(ScenarioInfo scenario) : this()
        {
            BindingContext = new SwitchScenarioModel(scenario);
            itemView.Click += ItemView_Click;
        }

        private void ItemView_Click(object sender, EventArgs e)
        {
            var model = (SwitchScenarioModel)BindingContext;
            var data = GeolocationData.FromString(model.ScenarioValue);
            var label = data.DateTime.ToString() + "\r\n" + model.ScenarioName;
            Singleton.Resolve<IGeolocationView>()?
                .View(new Lazurite.Shared.Geolocation(data.Latitude, data.Longtitude, false), label);
        }
    }
}
