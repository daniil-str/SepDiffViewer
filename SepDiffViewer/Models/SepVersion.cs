namespace SepDiffTool.Models;

// Почему record? Версии не должны меняться после создания (иммутабельность).
public record SepVersion(string DisplayName, string Url);