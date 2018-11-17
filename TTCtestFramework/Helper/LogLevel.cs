namespace TTCtestFramework.Helper
{
    /// <summary>
    /// The LogLevel enum was implemented to support the filter of the 
    /// screenshot class as it is called from the Log class. The LogLevel
    /// enum will follow the implementation of the log4net as described below.
    /// 
    /// A log request of level p in a logger with level q is enabled if p >= q. 
    /// This rule is at the heart of log4net. It assumes that levels are ordered. 
    /// For the standard levels, we have ALL < DEBUG < INFO < WARN < ERROR < FATAL < OFF. 
    /// </summary>
    public enum LogLevel
    {
        All,
        Debug,
        Info,
        Warn,
        Error,
        Fatal,
        Off,
    }

}
