namespace RevitCollaborationHistory;

/// <summary>
/// Temp directory that will contain Journal Script, temp journals and Reports on Partition History
/// </summary>
public class TempDirectory : IDisposable
{
    public TempDirectory()
    {
        string tempDir = Path.GetTempPath();
        Dir = Path.Combine(tempDir, "RevitCollaborationHistory");
        Directory.CreateDirectory(Dir);
        Directory.CreateDirectory(ReportsDirectory);
    }

    /// <summary>
    /// Base temp directory
    /// </summary>
    private string Dir { get; }
    
    /// <summary>
    /// Path to Journal Script file
    /// </summary>
    public string Script => Path.Combine(Dir, "script.txt");
    
    /// <summary>
    /// Path to Reports subdirectory
    /// </summary>
    public string ReportsDirectory => Path.Combine(Dir, "Reports");
    
    /// <summary>
    /// Resulting reports path
    /// </summary>
    public IEnumerable<string> Reports => Directory.EnumerateFiles(ReportsDirectory);

    public void Dispose()
    {
        Directory.Delete(Dir, true);
    }
}