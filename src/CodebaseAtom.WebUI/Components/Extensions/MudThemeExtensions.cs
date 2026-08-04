using System.Text.Json;

namespace CodebaseAtom.WebUI.Components.Extensions;

public static class MudThemeExtensions
{
    private static readonly JsonSerializerOptions _options = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = false
    };

    public static MudTheme Clone(this MudTheme theme)
    {
        var json = JsonSerializer.Serialize(theme, _options);

        return JsonSerializer.Deserialize<MudTheme>(json, _options)!;
    }
}
