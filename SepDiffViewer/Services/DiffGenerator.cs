using DiffPlex.DiffBuilder;
using DiffPlex.DiffBuilder.Model;
using System.Text;

namespace SepDiffTool.Services;

public class DiffGenerator
{
    private const int MaxUnchanged = 3;

    public string GenerateConsoleDiff(string oldText, string newText)
    {
        var builder = new InlineDiffBuilder();
        var diff = builder.BuildDiffModel(oldText, newText);

        var sb = new StringBuilder();
        int unchangedCount = 0;

        foreach (var line in diff.Lines)
        {
            if (line.Type == ChangeType.Unchanged)
            {
                if (unchangedCount < MaxUnchanged)
                    sb.AppendLine($"    {line.Text}");
                else if (unchangedCount == MaxUnchanged)
                    sb.AppendLine("    ... (скрыто несколько строк без изменений) ...");
                unchangedCount++;
            }
            else
            {
                unchangedCount = 0;
                string prefix = line.Type switch
                {
                    ChangeType.Inserted => "[+] ",
                    ChangeType.Deleted  => "[-] ",
                    ChangeType.Modified => "[~] ",
                    _ => "    "
                };
                sb.AppendLine($"{prefix}{line.Text}");
            }
        }
        return sb.ToString();
    }
}