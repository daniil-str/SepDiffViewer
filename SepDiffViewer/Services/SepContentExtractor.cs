using HtmlAgilityPack;
using System.Text;
using System.Text.RegularExpressions;

namespace SepDiffViewer.Services;

public class SepContentExtractor
{
    public string ExtractCleanText(string html)
    {
        var doc = new HtmlDocument();
        doc.LoadHtml(html);

        var contentNode = doc.DocumentNode.SelectSingleNode("//div[@id='content']")
                          ?? doc.DocumentNode.SelectSingleNode("//main");

        if (contentNode == null) return string.Empty;

        var removeNodes = contentNode.SelectNodes(
            ".//script|.//style|.//nav|.//footer|.//header|.//div[@class='sep-nav']");
        if (removeNodes != null)
        {
            foreach (var node in removeNodes.ToList()) node.Remove();
        }

        var sb = new StringBuilder();
        ExtractTextWithStructure(contentNode, sb);
        return Regex.Replace(sb.ToString().Trim(), @"\n{3,}", "\n\n");
    }

    private static void ExtractTextWithStructure(HtmlNode node, StringBuilder sb)
    {
        foreach (var child in node.ChildNodes)
        {
            if (child.Name == "#text")
            {
                var text = child.InnerText.Trim();
                if (!string.IsNullOrEmpty(text))
                    sb.Append(text).Append(' ');
            }
            else if (child.Name is "p" or "li" or "blockquote" or "pre")
            {
                var inner = child.InnerText.Trim();
                if (!string.IsNullOrEmpty(inner))
                {
                    sb.AppendLine(inner);
                    sb.AppendLine();
                }
            }
            else if (child.Name.StartsWith("h"))
            {
                var inner = child.InnerText.Trim();
                if (!string.IsNullOrEmpty(inner))
                {
                    sb.AppendLine($"# {inner}");
                    sb.AppendLine();
                }
            }
            else if (child.Name == "br")
            {
                sb.AppendLine();
            }
            else if (child.ChildNodes.Count > 0)
            {
                ExtractTextWithStructure(child, sb);
            }
        }
    }
}