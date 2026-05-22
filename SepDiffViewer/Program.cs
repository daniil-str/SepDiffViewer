//using SepDiffTool.Services;
//using SepDiffTool.Models;
using System.Net.Http;
using SepDiffTool.Services;
using SepDiffViewer.Models;
using SepDiffViewer.Services;

namespace SepDiffTool;

class Program
{
    static async Task Main(string[] args)
    {
        using var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.UserAgent.ParseAdd(
            "Mozilla/5.0 (compatible; SepDiffTool/1.0; +https://github.com/yourname/sepdifftool)");

        var archiveService = new SepArchiveService(httpClient);
        var contentExtractor = new SepContentExtractor();
        var diffGenerator = new DiffGenerator();

        Console.WriteLine("📖 Stanford Encyclopedia of Philosophy Diff Tool");
        Console.Write("Введите slug статьи (например, 'knowledge-analysis', 'ethics'): ");
        string slug = Console.ReadLine()?.Trim() ?? "";
        if (string.IsNullOrWhiteSpace(slug))
        {
            Console.WriteLine("❌ Slug не может быть пустым.");
            return;
        }

        Console.WriteLine($"\n🌐 Загрузка архивных версий...");
        List<SepVersion> versions;
        try
        {
            versions = await archiveService.GetArchiveVersionsAsync(slug);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Ошибка загрузки: {ex.Message}");
            return;
        }

        if (versions.Count == 0)
        {
            Console.WriteLine("⚠️ Не удалось найти архивные версии.");
            return;
        }

        Console.WriteLine("\n📅 Доступные версии:");
        for (int i = 0; i < versions.Count; i++)
            Console.WriteLine($"[{i,2}] {versions[i].DisplayName}");

        Console.Write("\n🔹 Индекс версии 1 (базовая): ");
        if (!int.TryParse(Console.ReadLine(), out int idx1) || idx1 < 0 || idx1 >= versions.Count)
        { Console.WriteLine("❌ Неверный выбор."); return; }

        Console.Write("🔹 Индекс версии 2 (сравнение): ");
        if (!int.TryParse(Console.ReadLine(), out int idx2) || idx2 < 0 || idx2 >= versions.Count)
        { Console.WriteLine("❌ Неверный выбор."); return; }

        Console.WriteLine($"\n⬇️ Загрузка версий: {versions[idx1].DisplayName} и {versions[idx2].DisplayName}...");
        
        string html1, html2;
        try
        {
            html1 = await httpClient.GetStringAsync(versions[idx1].Url);
            await Task.Delay(500); // Вежливый интервал между запросами
            html2 = await httpClient.GetStringAsync(versions[idx2].Url);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Ошибка загрузки версий: {ex.Message}");
            return;
        }

        string text1 = contentExtractor.ExtractCleanText(html1);
        string text2 = contentExtractor.ExtractCleanText(html2);

        Console.WriteLine("\n🔍 Генерация различий (v1 → v2):");
        Console.WriteLine("Легенда: [+] Добавлено  [-] Удалено  [~] Изменено  [ ] Без изменений\n");
        
        Console.WriteLine(diffGenerator.GenerateConsoleDiff(text1, text2));
        
        Console.WriteLine("\n✅ Готово. Нажмите Enter для выхода.");
        Console.ReadLine();
    }
}