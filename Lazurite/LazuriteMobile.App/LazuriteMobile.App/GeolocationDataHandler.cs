﻿using Lazurite.IOC;
using Lazurite.Logging;
using Lazurite.MainDomain;
using Lazurite.Shared;
using LazuriteMobile.MainDomain;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using System;
using System.Threading.Tasks;

namespace LazuriteMobile.App
{
    public class GeolocationDataHandler : IAddictionalDataHandler, IDisposable
    {
        private IGeolocationListener _listener = Singleton.Resolve<IGeolocationListener>();
        private static readonly ILogger Log = Singleton.Resolve<ILogger>();

        public void Handle(AddictionalData data, object tag)
        {
            //do nothing
        }

        public void Prepare(AddictionalData data, object tag)
        {
            data.Set(_listener.LastGeolocation);
        }

        public void Initialize()
        {
            _listener = Singleton.Resolve<IGeolocationListener>();
        }

        public void Dispose()
        {
            try
            {
                _listener.Stop();
            }
            catch (Exception e)
            {
                Log.Error("Error on dispose GeolocationDataHandler", e);
            }
        }
    }
}
