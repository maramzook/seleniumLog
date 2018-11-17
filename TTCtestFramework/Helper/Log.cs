using log4net;
using OpenQA.Selenium;
using TTCtestFramework.Utility;
using System;

namespace TTCtestFramework.Helper
{
    /// <summary>
    /// This class will provide logging functionality.
    /// </summary>
    public class Log
    {
        private readonly ILog logger;

        /// <summary>
        /// The Log constructor. 
        /// </summary>
        /// <param name="type">IWebDriver used to pass TakeScreenshot </param>
        public Log(Type type)
        {
            log4net.Config.XmlConfigurator.Configure();
            logger = LogManager.GetLogger(type.Name);
        }

        /// <summary>
        /// Logs a message object with DEBUG level.
        /// </summary>
        /// <param name="message">Text message to log</param>
        public void Debug(string message)
        {
            logger.Debug(message);
        }

        /// <summary>
        /// Logs a message object with DEBUG level including the stack trace of the Exception passed as argument.
        /// </summary>
        /// <param name="message">Text message to log</param>
        /// <param name="e">The exception to log, including its stack trace.</param>
        public void Debug(string message, Exception e)
        {
            logger.Debug(message, e);
        }

        /// <summary>
        /// Logs a formated message object with DEBUG level.
        /// </summary>
        /// <param name="format">A string containing zero or more format items</param>
        /// <param name="args">Object array containing zero or more objects to format</param>
        public void Debug(string format, params object[] args)
        {
            logger.DebugFormat(format, args);
        }

        /// <summary>
        /// Logs a message object with INFO level.
        /// </summary>
        /// <param name="message">Text message to log</param>
        public void Info(string message)
        {
            logger.Info(message);
        }

        /// <summary>
        /// Logs a message object with INFO level including the stack trace of the Exception passed as argument.
        /// </summary>
        /// <param name="message">Text message to log</param>
        /// <param name="e">The exception to log, including its stack trace.</param>
        public void Info(string message, Exception e)
        {
            logger.Info(message, e);
        }

        /// <summary>
        /// Logs a formated message object with INFO level.
        /// </summary>
        /// <param name="format">A string containing zero or more format items</param>
        /// <param name="args">Object array containing zero or more objects to format</param>
        public void Info(string format, params object[] args)
        {
            logger.InfoFormat(format, args);
        }

        /// <summary>
        /// Logs a message object with WARN level.
        /// </summary>
        /// <param name="message">Text message to log</param>
        public void Warn(string message)
        {
            logger.Warn(message);
        }

        /// <summary>
        /// Logs a message object with Warn level including the stack trace of the Exception passed as argument.
        /// </summary>
        /// <param name="message">Text message to log</param>
        /// <param name="e">The exception to log, including its stack trace.</param>
        public void Warn(string message, Exception e)
        {
            logger.Warn(message, e);
        }

        /// <summary>
        /// Logs a formated message object with Warn level.
        /// </summary>
        /// <param name="format">A string containing zero or more format items</param>
        /// <param name="args">Object array containing zero or more objects to format</param>
        public void Warn(string format, params object[] args)
        {
            logger.WarnFormat(format, args);
        }

        /// <summary>
        /// Logs a message object with ERROR level.
        /// </summary>
        /// <param name="message">Text message to log</param>
        public void Error(string message)
        {
            logger.Error(message);
        }

        /// <summary>
        /// Logs a message object with ERROR level including the stack trace of the Exception passed as argument.
        /// </summary>
        /// <param name="message">Text message to log</param>
        /// <param name="e">The exception to log, including its stack trace.</param>
        public void Error(string message, Exception e)
        {
            logger.Error(message, e);
        }

        /// <summary>
        /// Logs a formated message object with ERROR level.
        /// </summary>
        /// <param name="format">A string containing zero or more format items</param>
        /// <param name="args">Object array containing zero or more objects to format</param>
        public void Error(string format, params object[] args)
        {
            logger.ErrorFormat(format, args);
        }
    }
}
