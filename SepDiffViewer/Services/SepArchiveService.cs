using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using SepDiffTool.Models;

namespace SepDiffTool.Services;

public class SepArchiveService : IDisposable
{
    private readonly HttpClient _client;

    public SepArchiveService()
    {
        // TODO 2: Инициализируй HttpClient (гл. 6).
        // Добавь заголовок User-Agent, иначе SEP может вернуть 403 Forbidden.
        // Пример: _client.DefaultRequestHeaders.Add("User-Agent", "SepDiffTool/1.0");
    }

    public void Dispose()
    {
        // TODO 3: Реализуй IDisposable (гл. 6).
        // Освободи ресурсы HttpClient через _client.Dispose();
    }

    public string DownloadHtml(string url)
    {
        // TODO 4: Синхронно скачай HTML по URL (гл. 6–8).
        // Используй chain: _client.GetAsync(url).Result.Content.ReadAsStringAsync().Result
        // ⚠️ В продакшене так не делают (блокирует поток), но для учебной цели
        // до изучения async/await (гл. 12+) это допустимо.
        throw new NotImplementedException("Реализуй скачивание HTML");
    }

    public List<SepVersion> GetArchiveVersions(string slug)
    {
        // TODO 5: Собери URL главной страницы статьи: https://plato.stanford.edu/entries/{slug}/
        // Скачай HTML, вызови приватный метод парсинга и верни список.
        throw new NotImplementedException("Реализуй получение списка версий");
    }

    private List<SepVersion> ParseArchiveVersions(string html, string slug)
    {
        var versions = new List<SepVersion>();
        var doc = new HtmlDocument(); // Внешняя библиотека
        doc.LoadHtml(html);

        // TODO 6: Найди все ссылки на архивы (HtmlAgilityPack API).
        // XPath: "//a[contains(@href, '/archives/') and contains(@href, '/entries/')]"
        // Пройдись циклом foreach по найденным узлам.
        // Для каждого узла извлеки href,补齐 до абсолютного URL, отфильтруй по slug.
        // Извлек период из URL через Regex: @"/archives/([^/]+)/"
        // Преобразуй "fall2023" → "Fall 2023" (используй switch-выражение + паттерны, гл. 3–4).
        // Добавляй в список новые SepVersion(...).
        
        versions.Add(new SepVersion("Current (Live)", $"https://plato.stanford.edu/entries/{slug}"));

        // TODO 7: Отсортируй список. Архивные версии должны идти первыми, Current — последним.
        // Используй versions.Sort((a, b) => ...) + лямбды (гл. 6).
        
        return versions;
    }

    private static string FormatArchiveName(string period)
    {
        // TODO 8: Преформатируй "win2023" → "Winter 2023" и т.д.
        // Используй switch-выражение с when-guard (гл. 3–4).
        // Для года используй range-индексы: period[^4..] (гл. 3).
        throw new NotImplementedException("Реализуй форматирование имени архива");
    }
}