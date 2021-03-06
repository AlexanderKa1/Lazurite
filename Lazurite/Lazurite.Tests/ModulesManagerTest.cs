﻿using Lazurite.ActionsDomain;
using Lazurite.Data;
using Lazurite.IOC;
using Lazurite.Windows.Modules;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.Linq;

namespace Lazurite.Tests
{
    [TestClass]
    public class ModulesManagerTest
    {
        [TestMethod]
        public void CreatePluginTest()
        {
            Singleton.Add(new FileSavior());
            var sourcePluginFolder = @"D:\Programming\Lazurite_2\LazuriteTestModules\LazuriteTestModules\LazuriteTestModules\bin\Debug\";
            var targetFile = @"D:\Temporary\Lazurite_test.pyp";
            PluginsCreator.CreatePluginFile(sourcePluginFolder, targetFile);
        }

        [TestMethod]
        public void LoadPluginTest()
        {
            Singleton.Add(new FileSavior());
            var modulesManager = new PluginsManager();
            var targetFile = @"D:\Temporary\Lazurite_test.pyp";
            modulesManager.AddPlugin(targetFile);
            if (!modulesManager.GetModules().Any(x => x.Name.Contains("TestAction")))
                throw new Exception();
        }

        [TestMethod]
        public void RemoveLibTest()
        {
            Singleton.Add(new FileSavior());
            var modulesManager = new PluginsManager();
            modulesManager.RemovePlugin(modulesManager.GetPlugins().First().Name);
            if (modulesManager.GetModules().Any(x => x.Name.Contains("TestAction")))
                throw new Exception();
        }

        [TestMethod]
        public void TestExtModulesAcrossSerializing_part1()
        {
            var savior = new FileSavior();
            Singleton.Add(savior);
            var manager = new PluginsManager();
            IAction testAction = manager.CreateInstance(manager.GetModules().First(), null);
            testAction.SetValue(null, DateTime.Now.ToString());
            savior.Set("testAction", testAction);
        }

        [TestMethod]
        public void TestExtModulesAcrossSerializing_part2()
        {
            var savior = new FileSavior();
            Singleton.Add(savior);
            var modulesManager = new PluginsManager();
            IAction testAction = savior.Get<IAction>("testAction");
            Debug.WriteLine(testAction.GetValue(null));
            if (!testAction.GetType().Equals(modulesManager.GetModules().First()))
                throw new Exception();
        }        
    }
}
