using System;
using System.Globalization;
using System.IO;
using System.Linq;

namespace RevitCollaborationHistory;

public class Report
{
    public string FileName { get; }
    public DateTime TimeStamp { get; }
    public string? UserName { get; }
    public string? Comment { get; }
    public string ReportLine => $"{FileName}|{TimeStamp}|{UserName}|{Comment}";
    
    public Report(string path)
    {
        FileName = Path.GetFileNameWithoutExtension(path);
        
        string? secondLine = File.ReadLines(path)
            .Skip(1)
            .FirstOrDefault();
        
        string[]? columns = secondLine?.Split('\t');

        if (columns is null || columns.Length < 3) return;
        
        TimeStamp = DateTime.ParseExact(
            columns[0],
            "dd/MM/yyyy HH:mm:ss",
            CultureInfo.InvariantCulture);

        UserName = columns[1];
        Comment = columns[2];
    }
}