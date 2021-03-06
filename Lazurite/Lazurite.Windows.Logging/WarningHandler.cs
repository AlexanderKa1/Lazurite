﻿using log4net;
using log4net.Appender;
using log4net.Core;
using log4net.Layout;
using log4net.Repository.Hierarchy;
using System;
using System.IO;

namespace Lazurite.Windows.Logging
{
    public class WarningHandler : WarningHandlerBase
    {
        private static ILog _logger;

        static WarningHandler()
        {
            var hierarchy = (Hierarchy)LogManager.GetRepository();
            hierarchy.Name = "main";
            var patternLayout = new PatternLayout();
            patternLayout.ConversionPattern = "%d [%2%t] %-5p %m%n%n";
            patternLayout.ActivateOptions();

            var roller = new RollingFileAppender();
            roller.AppendToFile = true;
            roller.File = @"Logs\Log.txt";
            roller.Layout = patternLayout;
            roller.MaxSizeRollBackups = 50;
            roller.RollingStyle = RollingFileAppender.RollingMode.Date;
            roller.StaticLogFileName = true;
            roller.LockingModel = new FileAppender.MinimalLock();
            roller.ActivateOptions();
            hierarchy.Root.AddAppender(roller);
            
            hierarchy.Root.Level = Level.All;
            hierarchy.Configured = true;

            _logger = LogManager.GetLogger("main");
        }

        public WarningHandler()
        {
            //do nothing
        }

        public override void InternalWrite(WarnType type, string message = null, Exception exception = null)
        {
            try
            {
                switch (type)
                {
                    case WarnType.Debug:
                        _logger.Debug(message, exception);
                        break;
                    case WarnType.Error:
                        _logger.Error(message, exception);
                        break;
                    case WarnType.Fatal:
                        {
                            ExtremeLog(message, exception);
                            _logger.Fatal(message, exception);
                            LogManager.Flush(0);
                        }
                        break;
                    case WarnType.Info:
                        _logger.Info(message, exception);
                        break;
                    case WarnType.Warn:
                        _logger.Warn(message, exception);
                        break;
                }
            }
            catch (Exception e)
            {
                ExtremeLog(message + " /// " + exception.Message, e);
            }
        }

        public static void ExtremeLog(string message, Exception e=null)
        {
            File.AppendAllLines("extremeLog_"+Guid.NewGuid().ToString()+".txt", new[] { DateTime.Now.ToString(), message, e.Message, e.StackTrace });
        }
    }
}