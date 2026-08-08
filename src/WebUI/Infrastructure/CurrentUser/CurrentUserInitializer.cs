//using System.Security.Claims;
//using Microsoft.AspNetCore.Components.Authorization;

//namespace Vioren.CodebaseAtom.WebUI.Infrastructure.CurrentUser;

//public class CurrentUserInitializer
//{
//    private readonly AuthenticationStateProvider _authStateProvider;
//    private readonly CurrentUserService _currentUserService;
//    private readonly CurrentUserState _userState;

//    public CurrentUserInitializer(
//        AuthenticationStateProvider authStateProvider,
//        CurrentUserService userFetcher,
//        CurrentUserState userState)
//    {
//        _authStateProvider = authStateProvider;
//        _currentUserService = userFetcher;
//        _userState = userState;
//    }

//    public async Task InitializeAsync()
//    {
//        var authState = await _authStateProvider.GetAuthenticationStateAsync();
//        var user = authState.User;

//        if (user.Identity is not null && user.Identity.IsAuthenticated)
//        {
//            var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier);

//            if (userIdClaim is not null && Guid.TryParse(userIdClaim.Value, out var userId))
//            {
//                var currentUser = await _currentUserService.GetCurrentUserAsync(userId);
//                await _userState.SetUserAsync(currentUser);

//                return;
//            }
//        }

//        await _userState.SetUserAsync(null);
//    }
//}
