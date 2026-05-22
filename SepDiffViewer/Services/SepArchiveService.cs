using HtmlAgilityPack;
//using SepDiffTool.Models;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using SepDiffViewer.Models;

namespace SepDiffTool.Services;

public class SepArchiveService(HttpClient client)
{
    public async Task<List<SepVersion>> GetArchiveVersionsAsync(string slug, CancellationToken ct = default)
    {
        var url = $"https://plato.stanford.edu/entries/{slug}/";
        var html = await client.GetStringAsync(url, ct);
        return ParseArchiveVersions(html, slug);
    }

    private static List<SepVersion> ParseArchiveVersions(string html, string slug)
    {
        var doc = new HtmlDocument();
        doc.LoadHtml(html);
        var versions = new List<SepVersion>();

        var links = doc.DocumentNode.SelectNodes(
            "//a[contains(@href, '/archives/') and contains(@href, '/entries/')]");
        if (links != null)
        {
            foreach (var link in links)
            {
                var href = link.GetAttributeValue("href", "").Trim();
                if (!href.StartsWith("http"))
                    href = "https://plato.stanford.edu" + href;

                if (href.Contains($"/entries/{slug}/", StringComparison.OrdinalIgnoreCase))
                {
                    var match = Regex.Match(href, @"/archives/([^/]+)/");
                    if (match.Success)
                    {
                        versions.Add(new SepVersion(FormatArchiveName(match.Groups[1].Value), href.TrimEnd('/')));
                    }
                }
            }
        }

        versions.Add(new SepVersion("Current (Live)", $"https://plato.stanford.edu/entries/{slug}"));
        return versions.OrderBy(v => v.Url.Contains("/archives/")).ToList();
    }

    private static string FormatArchiveName(string period)
    {
        var dict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            {"win", "Winter"}, {"spr", "Spring"}, {"sum", "Summer"}, {"fall", "Fall"},
            {"winter", "Winter"}, {"spring", "Spring"}, {"summer", "Summer"}, {"autumn", "Fall"}
        };

        var match = Regex.Match(period, @"^(\w+)(\d{4})$");
        return match.Success && dict.TryGetValue(match.Groups[1].Value, out var season) 
            ? $"{season} {match.Groups[2].Value}" 
            : period;
    }
}