namespace CodebaseAtom.WebUI.Components.Statics;

public static class ThemeFor
{
    private static readonly string[] _fontFamily =
    [
        "Public Sans",
        "Segoe UI",
        "sans-serif"
    ];

    private static readonly PaletteLight _paletteLight = new()
    {
        Primary = "#3A8A4A",
        Secondary = "#F4B942",
        Tertiary = "#5E9F63",
        Info = "#5B8DEF",
        Success = "#43A047",
        AppbarBackground = "#0D3B30",
        AppbarText = "#FFFFFF",
        Background = "#FAFBF7",
        Surface = "#FFFFFF",
        DrawerBackground = "#FFFFFF",
        DrawerText = "#2D3748",
        TextPrimary = "#1F2937",
        TextSecondary = "#6B7280",
        LinesDefault = "#E5E7EB",
        LinesInputs = "#D1D5DB",
        TableLines = "#E5E7EB",
        BackgroundGray = "#F3F4F6",
        ActionDefault = "#4B5563",
        ActionDisabled = "#C7CDD6",
        ActionDisabledBackground = "#F3F4F6",
        Divider = "#E5E7EB"
    };

    private static readonly Typography _typography = new()
    {
        Default = new DefaultTypography
        {
            FontFamily = _fontFamily,
            FontWeight = "400",
            FontSize = "0.900rem",
            LineHeight = "1.5",
            LetterSpacing = "0"
        },
        H1 = new H1Typography
        {
            FontFamily = _fontFamily,
            FontWeight = "700",
            FontSize = "2rem",
            LineHeight = "1.2",
            LetterSpacing = "-0.02em"
        },
        H2 = new H2Typography
        {
            FontFamily = _fontFamily,
            FontWeight = "700",
            FontSize = "1.5rem",
            LineHeight = "1.25",
            LetterSpacing = "-0.02em"
        },
        H3 = new H3Typography
        {
            FontFamily = _fontFamily,
            FontWeight = "600",
            FontSize = "1.25rem",
            LineHeight = "1.3",
            LetterSpacing = "-0.01em"
        },
        H4 = new H4Typography
        {
            FontFamily = _fontFamily,
            FontWeight = "600",
            FontSize = "1.125rem",
            LineHeight = "1.35"
        },
        H5 = new H5Typography
        {
            FontFamily = _fontFamily,
            FontWeight = "600",
            FontSize = "1rem",
            LineHeight = "1.4"
        },
        H6 = new H6Typography
        {
            FontFamily = _fontFamily,
            FontWeight = "600",
            FontSize = "0.95rem",
            LineHeight = "1.45"
        },
        Subtitle1 = new Subtitle1Typography
        {
            FontFamily = _fontFamily,
            FontWeight = "500",
            FontSize = "1rem",
            LineHeight = "1.5"
        },
        Body1 = new Body1Typography
        {
            FontFamily = _fontFamily,
            FontWeight = "400",
            FontSize = "0.900rem",
            LineHeight = "1.6"
        },
        Body2 = new Body2Typography
        {
            FontFamily = _fontFamily,
            FontWeight = "400",
            FontSize = "0.800rem",
            LineHeight = "1.6"
        },
        Button = new ButtonTypography
        {
            FontFamily = _fontFamily,
            FontWeight = "600",
            FontSize = "0.875rem",
            LineHeight = "1.5",
            LetterSpacing = "0.01em",
            TextTransform = "none"
        }
    };

    public static readonly MudTheme Default = new()
    {
        PaletteLight = _paletteLight,
        Typography = _typography
    };

    public static readonly MudTheme NoAppBar = new()
    {
        PaletteLight = _paletteLight,
        LayoutProperties = new LayoutProperties
        {
            AppbarHeight = "0px"
        },
        Typography = _typography
    };
}
