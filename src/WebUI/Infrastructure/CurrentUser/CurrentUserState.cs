namespace Vioren.CodebaseAtom.WebUI.Infrastructure.CurrentUser;

public class CurrentUserState
{
    public CurrentUserModel? User { get; private set; }

    public void SetUser(CurrentUserModel? user)
    {
        User = user;
    }
}

//public class CurrentUserState
//{
//    public CascadingValueSource<CurrentUserModel?> Source { get; }

//    public CurrentUserState()
//    {
//        Source = new CascadingValueSource<CurrentUserModel?>(name: "CurrentUser", value: null, isFixed: false);
//    }

//    public CurrentUserModel? User => Source.Value;

//    public Task SetUserAsync(CurrentUserModel? user)
//    {
//        return Source.NotifyChangedAsync(user);
//    }
//}

//public class CurrentUserState
//{
//    public CascadingValueSource<CurrentUserModel?> Source { get; }

//    public CurrentUserState()
//    {
//        Source = new CascadingValueSource<CurrentUserModel?>(name: "CurrentUser", value: null, isFixed: false);
//    }

//    public CurrentUserModel? User { get; private set; }

//    public Task SetUserAsync(CurrentUserModel? user)
//    {
//        User = user;

//        return Source.NotifyChangedAsync(User);
//    }
//}
