using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LoginManager : SingletonBehaviour<LoginManager>
{
    public string CustomId { get; private set; }

    public void OnClickGuestLogin()
    {
        Logger.Log("Guest Login");

        Firebase.Auth.FirebaseAuth auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        auth.SignInAnonymouslyAsync().ContinueWith(task => {
            if (task.IsCanceled)
            {
                Logger.LogError("SignInAnonymouslyAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Logger.LogError("SignInAnonymouslyAsync encountered an error: " + task.Exception);
                return;
            }

            Firebase.Auth.AuthResult result = task.Result;
            CustomId = result.User.UserId;

            Logger.Log($"User signed in successfully: {result.User.DisplayName} ({result.User.UserId})");
        });
    }
}
