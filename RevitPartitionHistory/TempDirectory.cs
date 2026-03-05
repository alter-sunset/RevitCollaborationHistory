using System.Collections.Generic;
using System.IO;

namespace RevitPartitionHistory;

public class TempDirectory: RevitJournalAbuser.TempDirectory
{
    /// <summary>
    /// Path to Reports subdirectory
    /// </summary>
    public string ReportsDirectory => Path.Combine(Dir, "Reports");
    
    /// <summary>
    /// Resulting reports path
    /// </summary>
    public IEnumerable<string> Reports => Directory.EnumerateFiles(ReportsDirectory);

    public TempDirectory(bool createReportsDirectory)
    {
        Directory.CreateDirectory(ReportsDirectory);
    }
}