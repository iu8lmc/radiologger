using System;
using System.Collections.Generic;
using System.IO;

public class Logger
{
    private List<string> _logEntries;
    private string _filePath;

    public Logger(string filePath)
    {
        _filePath = filePath;
        _logEntries = new List<string>();
    }

    public void Log(string message)
    {
        _logEntries.Add($"{DateTime.Now}: {message}");
    }

    public void SaveLogs()
    {
        File.AppendAllLines(_filePath, _logEntries);
    }
}
