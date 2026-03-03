using System;
using System.Collections.Generic;
using System.IO;

namespace RevitCollaborationHistory;

public class TempDirectory : IDisposable
{
    public TempDirectory()
    {
        string tempDir = System.IO.Path.GetTempPath();
        Path = System.IO.Path.Combine(tempDir, "RevitCollaborationHistory");
        Directory.CreateDirectory(Path);
    }
    public string Path { get; }
    public string Script => System.IO.Path.Combine(Path, "script.txt");
    
    public string ReportsDirectory => System.IO.Path.Combine(Path, "Reports");
    public IEnumerable<string> Reports => Directory.EnumerateFiles(ReportsDirectory);

    public void Dispose()
    {
        Directory.Delete(Path, true);
    }
}