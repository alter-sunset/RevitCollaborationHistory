using System.Collections.Generic;
using System.IO;
using System.Linq;
using RevitJournalAbuser;

namespace RevitPartitionHistory;

public class ReportsTempDirectory: TempDirectory
{
    /// <summary>
    /// Path to Reports subdirectory
    /// </summary>
    private string ReportsDirectory => Path.Combine(Dir, "Reports");
    
    /// <summary>
    /// Resulting reports path
    /// </summary>
    private IEnumerable<string> Reports => Directory.EnumerateFiles(ReportsDirectory);
    
    /// <summary>
    /// Creates temp environment with journal script to export partition history of provided RVT files
    /// </summary>
    /// <param name="files">RVT files path</param>
    /// <returns>Path to the resulting temp environment</returns>
    public ReportsTempDirectory (IEnumerable<string> files)
    {
        Directory.CreateDirectory(ReportsDirectory);
        
        string[] fileScripts = files
            .Select(file => file.CreatePartitionHistory(ReportsDirectory))
            .ToArray();
        
        using Journal journal = new(Script, fileScripts);
    }
    
    /// <summary>
    /// Start Revit process with a given Journal Script
    /// </summary>
    /// <param name="version">Revit version (eg 2022)</param>
    public IEnumerable<Report> GetReports(int version)
    {
        using Abuser abuser = new(version, this);
        
        abuser.RunJournalScript();
        abuser.WaitForExit();

        return Reports
            .Select(r => new Report(r));
    }
}