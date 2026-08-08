namespace Vioren.CodebaseAtom.WebUI.Infrastructure.CurrentUser;

public sealed class CurrentUserState
{
    public event EventHandler? OnChanged;

    public CurrentUserModel? CurrentUser
    {
        get;
        private set
        {
            if (field != value)
            {
                field = value;
                NotifyStateChanged();
            }
        }
    }

    private void NotifyStateChanged()
    {
        OnChanged?.Invoke(this, EventArgs.Empty);
    }

    public void SetUser(CurrentUserModel? user)
    {
        CurrentUser = user;
    }

    public void UpdateDisplayName(string newDisplayName)
    {
        if (CurrentUser is not null)
        {
            CurrentUser = CurrentUser with { DisplayName = newDisplayName };
        }
    }

    public void UpdateUser(Action<CurrentUserModel> update)
    {
        if (CurrentUser is null)
        {
            return;
        }

        update(CurrentUser);
        NotifyStateChanged();
    }
}
