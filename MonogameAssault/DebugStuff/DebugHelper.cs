using System;

namespace MonogameAssault.DebugStuff;
internal static class DebugHelper
{
    internal static void Log(LogLevel logLevel, string message)
    {
        switch (logLevel)
        {
            case LogLevel.Info:
                Console.WriteLine($"INFO: {message}");
                break;
            case LogLevel.Warning:
                Console.WriteLine($"WARNING: {message}");
                break;
            case LogLevel.Error:
                Console.WriteLine($"ERROR: {message}");
                break;
            default:
                Console.WriteLine($"loglevel is unknown: {message}");
                break;
        }
    }
}
