﻿using Lazurite.IOC;
using Lazurite.MainDomain;
using Lazurite.Scenarios.ScenarioTypes;
using Lazurite.Windows.Core;
using Lazurite.Windows.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Lazurite.Windows.Logging;
using System.Windows.Controls;
using LazuriteUI.Windows.Controls;
using static LazuriteUI.Windows.Main.TestWindow;
using Lazurite.ActionsDomain.ValueTypes;
using LazuriteUI.Windows.Main.Journal;
using Lazurite.ActionsDomain;
using Lazurite.CoreActions;
using Lazurite.Security;
using Lazurite.Data;

namespace LazuriteUI.Windows.Main
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public LazuriteCore Core { get; private set; }
        
        public App()
        {
            this.Exit += (o, e) => 
                Core.WarningHandler.Info("Lazurite closing at " + DateTime.Now.ToString());            

            ShutdownMode = ShutdownMode.OnExplicitShutdown;
            Core = new LazuriteCore();
            Core.WarningHandler.OnWrite += (o, e) =>
            {
                JournalManager.Set(e.Message, e.Type, e.Exception);
            };
            try
            {
                Core.Initialize();
                Core.Server.StartAsync(null);
                Singleton.Add(Core);
            }
            catch (Exception e)
            {
                Core.WarningHandler.Warn("Во время инициализации приложения возникла ошибка", e);
            }
            NotifyIconManager.Initialize();
            DuplicatedProcessesListener.Found += (processes) => NotifyIconManager.ShowMainWindow();
            DuplicatedProcessesListener.Start();
        }
    }
}