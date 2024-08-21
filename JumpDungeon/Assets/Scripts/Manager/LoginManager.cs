using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class LoginManager : SingletonBehaviour<LoginManager>
{
    public string UserId { get; private set; }

    public void SetUserId(string userId)
    {
        UserId = userId;
    }

    public async Task OnClickGuestLogin()
    {
        Logger.Log("Guest Login");

        Firebase.Auth.FirebaseAuth auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        try
        {
            Firebase.Auth.AuthResult result = await auth.SignInAnonymouslyAsync();
            UserId = result.User.UserId;

            Logger.Log($"User signed in successfully: {result.User.DisplayName} ({result.User.UserId})");
        }
        catch (System.Exception e)
        {
            Logger.LogError("SignInAnonymouslyAsync encountered an error: " + e);
        }

    }
}
