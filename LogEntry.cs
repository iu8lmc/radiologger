using System;

public class LogEntry
{
    public string CallSign { get; set; } = string.Empty;
    public string Band { get; set; } = string.Empty;
    public string Mode { get; set; } = string.Empty;
    public DateTime QsoDate { get; set; }
    public string ReportSent { get; set; } = string.Empty;
    public string ReportReceived { get; set; } = string.Empty;

    // Existing methods...
}