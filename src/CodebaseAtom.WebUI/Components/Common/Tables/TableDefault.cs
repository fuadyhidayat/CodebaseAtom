namespace CodebaseAtom.WebUI.Components.Common.Tables;

public class TableDefault<T> : MudTable<T>
{
    public TableDefault()
    {
        Elevation = 25;
        Outlined = true;
        Bordered = true;
        Striped = true;
        Hover = true;
    }
}
