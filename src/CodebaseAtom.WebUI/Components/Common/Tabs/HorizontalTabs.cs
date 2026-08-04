namespace CodebaseAtom.WebUI.Components.Common.Tabs;

public class HorizontalTabs : MudTabs
{
    public HorizontalTabs()
    {
        Elevation = 25;
        Rounded = true;
        ApplyEffectsToContainer = true;
        TabHeaderClass = "horizontal-tabs-tab-header";
        ActiveTabClass = "horizontal-tabs-active-tab";
        TabButtonsClass = "horizontal-tabs-tab-buttons";
        TabPanelsClass = "horizontal-tabs-tab-panel";
        KeepPanelsAlive = true;
    }
}
