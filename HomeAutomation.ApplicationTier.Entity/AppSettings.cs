﻿namespace HomeAutomation.ApplicationTier.Entity
{
    public class AppSettings
    {
        public static string? ConnectionString { get; private set; }
        public static string[]? CORS { get; private set; }
    }
}
