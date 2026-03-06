using System.IO;

namespace RevitJournalAbuser;

/// <summary>
/// Various scripts to use in a Journal file
/// </summary>
public static class Scripts
{
    /// <summary>
    /// Generate script to create report on Partition History of a rvt model
    /// </summary>
    /// <param name="filePath">Path to rvt model file</param>
    /// <param name="outputDirectory">Directory to put report at</param>
    /// <returns>Script in string format</returns>
    public static string CreatePartitionHistory(this string filePath, string outputDirectory)
    {
        string fileName = Path.GetFileName(filePath);
        string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filePath);
        string reportName = $"{fileNameWithoutExtension}.txt";
        string reportPath = Path.Combine(outputDirectory, reportName);
        return  $"""
                 '
                 Jrn.Command "Ribbon"  , "Show usernames, times, and comments for successive saves of a file. , ID_PARTITIONS_SHOW_HISTORY" 
                 Jrn.Data  _
                           "FileDialog"  , "IDOK" , "{filePath}"  _
                           , "rvt" , "{fileName}" , "{fileNameWithoutExtension}" 
                 Jrn.Data  _
                           "FileType"  , "RVT Files (*.rvt)" 
                 Jrn.PushButton "Modal , History , Dialog_Revit_PartitionsHistory"  _
                               , "Export..., Control_Revit_FileExport" 
                 Jrn.Data  _
                             "FileDialog"  , "IDOK" , "{reportPath}"  _
                             , "txt" , "{reportName}" , "" 
                 Jrn.Data  _
                             "FileType"  , "Delimited text (*.txt)" 
                 Jrn.PushButton "Modal , History , Dialog_Revit_PartitionsHistory"  _
                               , "Close, IDCANCEL"
                 """;
    }
}