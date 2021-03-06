﻿using Lazurite.IOC;
using Lazurite.Logging;
using System;
using System.Diagnostics;
using System.IO;

namespace LazuriteUI.Windows.Preparator
{
    public static class Utils
    {
        public static readonly string LazuriteProcessName = "LazuriteUI.Windows.Main";
        private static readonly string VcRedistDir = "VcRedist";

        public static void KillAllLazuriteProcesses()
        {
            foreach (var process in Process.GetProcessesByName(LazuriteProcessName))
                process.Kill();
        }

        private static void VcRedistInstall(string filePath)
        {
            var log = Singleton.Resolve<ILogger>();
            try
            {
                var result = Lazurite.Windows.Utils.Utils.ExecuteProcess(filePath, "/install /quiet /norestart /log vcredist_installation_log.txt", false, true);
                log.InfoFormat("{0} installed with result: [{1}]", filePath, result);
            }
            catch (Exception e)
            {
                log.InfoFormat("{0} installation error", e);
            }
        }

        public static void VcRedistInstallAll()
        {
            var log = Singleton.Resolve<ILogger>();
            try
            {
                var basePath = Lazurite.Windows.Utils.Utils.GetAssemblyFolder(typeof(Utils).Assembly);
                var vcRedistDir = Path.Combine(basePath, VcRedistDir);
                foreach (var filePath in Directory.GetFiles(vcRedistDir))
                    VcRedistInstall(filePath);
            }
            catch (Exception e)
            {
                log.Info("VcRedist x64 installation error", e);
            }
        }

        public static void FirewallSettings()
        {
            var log = Singleton.Resolve<ILogger>();
            try
            {
                var basePath = Lazurite.Windows.Utils.Utils.GetAssemblyFolder(typeof(Utils).Assembly);
                var fullPath = Path.Combine(basePath, LazuriteProcessName + ".exe");
                var command = string.Format("firewall add allowedprogram \"{0}\" LazuriteAccess{1} ENABLE", 
                    fullPath,
                    Lazurite.Windows.Utils.Utils.GetCurrentLazuriteUniqueHash());
                Lazurite.Windows.Utils.Utils.ExecuteProcess(Path.Combine(Environment.SystemDirectory, "netsh.exe"), command);
            }
            catch (Exception e)
            {
                log.Info("Firewall settings error", e);
            }
        }
    }
}