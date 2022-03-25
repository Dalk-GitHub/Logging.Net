﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logging.Net.Loggers.MB
{
    /// <summary>
    /// a logger which loggs like blazor
    /// </summary>
    public class MBLogger : ILogger
    {
        /// <summary>
        /// configuration for debug logs
        /// </summary>
        public LoggingConfiguration DebugConfiguration { get; set; } = new MBDebugConfiguration();
        /// <summary>
        /// configuration for info logs
        /// </summary>
        public LoggingConfiguration InfoConfiguration { get; set; } = new MBInfoConfiguration();
        /// <summary>
        /// configuration for warn logs
        /// </summary>
        public LoggingConfiguration WarnConfiguration { get; set; } = new MBWarnConfiguration();
        /// <summary>
        /// configuration for error logs
        /// </summary>
        public LoggingConfiguration ErrorConfiguration { get; set; } = new MBErrorConfiguration();

        /// <summary>
        /// additional logging operation
        /// </summary>
        public ILoggingAddition Addition { get; set; } = new NoAddition();

        private void LogForConfiguration(LoggingConfiguration c, string s)
        {
            string logged = "";
            var prefix = c.GetPrefix(c.GetTimePrefix());
            logged += prefix;
            LoggingHelper.ForegroundColorWrite(prefix, c.Color);
            Console.Write(": ");
            var lines = s.Replace("\r\n", "\n").Split('\n');
            int i = 0;
            string LZ()
            {
                string r = "";
                while (r.Length < prefix.Length + 2)
                {
                    r += " ";
                }
                return r;
            }
            lines.ToList().ForEach(line =>
            {
                if (i == 0)
                {
                    logged += line;
                    Console.WriteLine(line);
                }
                else
                {
                    var wl = LZ() + line;
                    logged += "\n" + wl;
                    Console.WriteLine(wl);
                }
                i++;
            });
            Addition.ProcessMessage(logged, c.Color);
        }


        /// <summary>
        /// logs a string as debug
        /// </summary>
        /// <param name="s">string to log</param>
        public void Debug(string s)
        {
            LogForConfiguration(DebugConfiguration, s);
        }

        /// <summary>
        /// logs a string as error
        /// </summary>
        /// <param name="s">string to log</param>
        public void Error(string s)
        {
            LogForConfiguration(ErrorConfiguration, s);
        }


        /// <summary>
        /// gets addition
        /// </summary>
        /// <returns>addition</returns>
        public ILoggingAddition GetAddition()
        {
            return Addition;
        }

        /// <summary>
        /// gets debug configuration
        /// </summary>
        /// <returns>debug configuration</returns>
        public LoggingConfiguration GetDebugConfiguration()
        {
            return DebugConfiguration;
        }

        /// <summary>
        /// gets error configuration
        /// </summary>
        /// <returns>error configuration</returns>
        public LoggingConfiguration GetErrorConfiguration()
        {
            return ErrorConfiguration;
        }

        /// <summary>
        /// gets info configuration
        /// </summary>
        /// <returns>info configuration</returns>
        public LoggingConfiguration GetInfoConfiguration()
        {
            return InfoConfiguration;
        }

        /// <summary>
        /// gets warn configuration
        /// </summary>
        /// <returns>warn configuration</returns>
        public LoggingConfiguration GetWarnConfiguration()
        {
            return WarnConfiguration;
        }

        /// <summary>
        /// logs a string as info
        /// </summary>
        /// <param name="s">string to log</param>
        public void Info(string s)
        {
            LogForConfiguration(InfoConfiguration, s);
        }


        /// <summary>
        /// sets addition
        /// </summary>
        /// <param name="addition">new value</param>
        public void SetAddition(ILoggingAddition addition)
        {
            Addition = addition;
        }

        /// <summary>
        /// sets new debug configuration
        /// </summary>
        /// <param name="configuration">new value</param>
        public void SetDebugConfiguration(LoggingConfiguration configuration)
        {
            DebugConfiguration = configuration;
        }

        /// <summary>
        /// sets new error configuration
        /// </summary>
        /// <param name="configuration">new value</param>
        public void SetErrorConfiguration(LoggingConfiguration configuration)
        {
            ErrorConfiguration = configuration;
        }

        /// <summary>
        /// sets new info configuration
        /// </summary>
        /// <param name="configuration">new value</param>
        public void SetInfoConfiguration(LoggingConfiguration configuration)
        {
            InfoConfiguration = configuration;
        }

        /// <summary>
        /// sets new warn configuration
        /// </summary>
        /// <param name="configuration">new value</param>
        public void SetWarnConfiguration(LoggingConfiguration configuration)
        {
            WarnConfiguration = configuration;
        }

        /// <summary>
        /// logs a string as warn
        /// </summary>
        /// <param name="s">string to log</param>
        public void Warn(string s)
        {
            LogForConfiguration(WarnConfiguration, s);
        }

        /// <summary>
        /// log an exception as info
        /// </summary>
        /// <param name="ex">exception to log</param>
        public void InfoEx(Exception ex)
        {
            LogExForConfiguration(InfoConfiguration, ex);
        }

        /// <summary>
        /// log an exception as debug
        /// </summary>
        /// <param name="ex">exception to log</param>
        public void DebugEx(Exception ex)
        {
            LogExForConfiguration(DebugConfiguration, ex);
        }

        /// <summary>
        /// log an exception as warn
        /// </summary>
        /// <param name="ex">exception to log</param>
        public void WarnEx(Exception ex)
        {
            LogExForConfiguration(WarnConfiguration, ex);
        }

        /// <summary>
        /// log an exception as error
        /// </summary>
        /// <param name="ex">exception to log</param>
        public void ErrorEx(Exception ex)
        {
            LogExForConfiguration(ErrorConfiguration, ex);
        }

        private void LogExForConfiguration(LoggingConfiguration conf, Exception ex)
        {
            var ln = ex.ToString().Replace("\r\n", "\n").Split('\n');
            foreach (var l in ln)
            {
                LogForConfiguration(conf, l);
            }
        }
    }
}
