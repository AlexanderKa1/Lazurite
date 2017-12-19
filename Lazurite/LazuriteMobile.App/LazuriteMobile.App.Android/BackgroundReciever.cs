﻿
using Android.App;
using Android.Content;

namespace LazuriteMobile.App.Droid
{
    [BroadcastReceiver(Enabled = true, Exported = true)]
    public class BackgroundReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            Application.Context.StartService(new Intent(Application.Context, typeof(LazuriteService)));
        }
    }
}