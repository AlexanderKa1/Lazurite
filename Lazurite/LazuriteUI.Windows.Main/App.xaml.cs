﻿using Lazurite.IOC;
using Lazurite.MainDomain;
using Lazurite.Scenarios.ScenarioTypes;
using Lazurite.Windows.Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using static LazuriteUI.Windows.Main.TestWindow;

namespace LazuriteUI.Windows.Main
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            ShutdownMode = ShutdownMode.OnExplicitShutdown;


            var core = new LazuriteCore();
            core.Initialize();
            core.Server.StartAsync(null);
            Singleton.Add(core);

            //var scenario = new SingleActionScenario();
            //scenario.TargetAction = new ToggleTestAction();
            //scenario.Initialize(null);
            //scenario.Name = "Переключатель";
            //core.ScenariosRepository.AddScenario(scenario);

            //var scenario0 = new SingleActionScenario();
            //scenario0.TargetAction = new ToggleTestAction();
            //scenario0.Initialize(null);
            //scenario0.Name = "Свет в коридоре";
            //core.ScenariosRepository.AddScenario(scenario0);

            //var scenario3 = new SingleActionScenario();
            //scenario3.Name = "Свет в ванной";
            //scenario3.TargetAction = new StatusTestAction();
            //scenario3.Initialize(null);
            //core.ScenariosRepository.AddScenario(scenario3);

            //var scenario2 = new SingleActionScenario();
            //scenario2.TargetAction = new FloatTestAction();
            //scenario2.Initialize(null);
            //scenario2.Name = "Уровень звука";
            //core.ScenariosRepository.AddScenario(scenario2);
            //core.VisualSettingsRepository.Add(new UserVisualSettings() { ScenarioId = scenario2.Id, AddictionalData = new[] { "Sound2" } });

            //var scenario4 = new SingleActionScenario();
            //scenario4.Name = "Компьютер";
            //scenario4.TargetAction = new ButtonTestAction();
            //scenario4.Initialize(null);
            //core.ScenariosRepository.AddScenario(scenario4);
            //core.VisualSettingsRepository.Add(new UserVisualSettings() { ScenarioId = scenario4.Id, AddictionalData = new[] { "TvNews" } });

            //var scenario5 = new SingleActionScenario();
            //scenario5.Name = "Свет в ванной";
            //scenario5.TargetAction = new DateTimeTestAction();
            //scenario5.Initialize(null);
            //core.ScenariosRepository.AddScenario(scenario5);
            //core.VisualSettingsRepository.Add(new UserVisualSettings() { ScenarioId = scenario5.Id, AddictionalData = new[] { "TvNews" } });

        }
    }
}
