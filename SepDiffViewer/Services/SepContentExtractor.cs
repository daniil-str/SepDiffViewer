using System;
using System.Text;
using System.Text.RegularExpressions;
using HtmlAgilityPack;

namespace SepDiffTool.Services;

public class SepContentExtractor
{
    public string ExtractCleanText(string html)
    {
        var doc = new HtmlDocument(); // Внешняя библиотека
        doc.LoadHtml(html);

        // TODO 9: Найди основной контент. Попробуй сначала "#content", если null → "main".
        // Если оба null → верни string.Empty.

        // TODO 10: Удали из найденного узла всё лишнее через XPath:
        // ".//script|.//style|.//nav|.//footer|.//header"
        // Пройдись циклом и вызови node.Remove() для каждого найденного.

        var sb = new StringBuilder();
        ExtractTextWithStructure(/* найденный узел */, sb);
        string result = sb.ToString().Trim();
        
        // TODO 11: Убери множественные пустые строки через Regex.Replace (гл. 3).
        // Паттерн: @"\n{3,}" → заменить на "\n\n"
        return result;
    }

    private static void ExtractTextWithStructure(HtmlNode node, StringBuilder sb)
    {
        // TODO 12: Рекурсивно обойди дочерние узлы (foreach в node.ChildNodes).
        // Для каждого child проверь child.Name (гл. 3, 5):
        // • "#text" → добавь текст + пробел, если не пустой.
        // • "p", "li", "blockquote", "pre" → добавь текст + \n\n.
        // • "h1".."h6" → добавь "# " + текст + \n\n.
        // • "br" → добавь \n.
        // • Иначе → если у узла есть дочерние, вызови этот же метод рекурсивно.
        // Подсказка: используй or-паттерн: child.Name is "p" or "li" ...
    }
}