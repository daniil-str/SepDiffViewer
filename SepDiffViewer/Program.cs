using System;
using System.Collections.Generic;
using SepDiffTool.Models;
using SepDiffTool.Services;

namespace SepDiffTool;

class Program
{
    static void Main(string[] args)
    {
        // TODO 15: Выведи приветствие, запроси slug через Console.ReadLine().
        // Проверь на null/пустоту, обрежь пробелы (гл. 1–2).

        Console.WriteLine("🌐 Загрузка архивных версий...");
        try
        {
            // TODO 16: Объяви using var archiveService = new SepArchiveService(); (гл. 6)
            // Вызови GetArchiveVersions(slug). Если список пуст → выведи предупреждение и выйди.
            // Выведи пронумерованный список версий циклом for.

            // TODO 17: Запроси два индекса через Console.ReadLine() + int.TryParse().
            // Проверь границы массива.

            // TODO 18: Скачай HTML обеих версий через archiveService.DownloadHtml().
            // Добавь Thread.Sleep(500) между запросами (вежливость к серверу).

            // TODO 19: Создай экземпляры SepContentExtractor и DiffService.
            // Извлеки чистый текст из обоих HTML, сгенерируй diff.
            // Выведи результат в консоль с легендой.

            Console.ReadLine();
        }
        catch (Exception ex)
        {
            // TODO 20: Обработай исключения (гл. 7). Выведи ex.Message.
        }
    }
}