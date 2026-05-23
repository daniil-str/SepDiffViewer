using System;
using System.Text;

namespace SepDiffTool.Services;

public class DiffService
{
    public string GenerateDiff(string oldText, string newText)
    {
        // TODO 13: Разбей оба текста на массивы строк через .Split() (гл. 3).
        // StringSplitOptions.None, разделители: "\r\n", "\n"

        var sb = new StringBuilder();
        int unchangedCount = 0;
        int maxUnchanged = 3;
        int maxLen = Math.Max(/* длины массивов */);

        // TODO 14: Пройдись циклом for от 0 до maxLen.
        // Безопасно извлеки старую и новую строку (проверь индексы на выход за границы).
        // Используй tuple-паттерн в switch для определения префикса:
        // (null, not null) → "[+] "
        // (not null, null) → "[-] "
        // (not null, not null) when old == new → null (неизменено)
        // _ → "[~] "
        // Логика скрытия длинных блоков без изменений:
        // если префикс null → увеличивай счётчик, выводи первые 3 строки, 
        // на 4-й выведи "... (скрыто) ...", дальше пропускай.
        // если префикс не null → сбрось счётчик, выведи строку с префиксом.

        return sb.ToString();
    }
}