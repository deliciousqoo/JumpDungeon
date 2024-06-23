using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Google;
using Firebase.Auth;
using System;
using GooglePlayGames.BasicApi;
using GooglePlayGames;
using TMPro;

public class FirebaseManager : MonoBehaviour
{
    [SerializeField]
    TMP_Text text;

    private FirebaseAuth auth; // 로그인, 회원가입 등에 사용
    //private FirebaseUser user; // 인증이 완료된 유저 정보

    public void Init()
    {
        /*
        if (auth.CurrentUser != null)
        {
            LogOut();
        }
        auth.StateChanged += OnChanged;*/

        PlayGamesPlatform.InitializeInstance(new PlayGamesClientConfiguration.Builder()
            .RequestIdToken()
            .RequestEmail()
            .Build());
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();

        auth = FirebaseAuth.DefaultInstance;
    }
    /*
    private void OnChanged(object sender, EventArgs e)
    {
        if (auth.CurrentUser != user)
        {
            bool signed = (auth.CurrentUser != user && auth.CurrentUser != null);
            if (!signed && user != null)
            {
                Debug.Log("로그아웃");
                LoginState?.Invoke(false);
            }

            user = auth.CurrentUser;
            if (signed)
            {
                Debug.Log("로그인");
                LoginState?.Invoke(true);
            }
        }
    }

    public void Create(string email, string password)
    {
        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task => 
        {
            if (task.IsCanceled)
            {
                Debug.Log("회원가입 취소");
                return;
            }
            if(task.IsFaulted)
            {
                Debug.Log("회원가입 실패");
                return;
            }

            AuthResult authResult = task.Result;
            FirebaseUser newUser = authResult.User;
            Debug.Log("회원가입 성공");
        });
    }

    public void Login(string email, string password)
    {
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.Log("로그인 취소");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.Log("로그인 실패");
                return;
            }

            AuthResult authResult = task.Result;
            FirebaseUser newUser = authResult.User;
            Debug.Log("로그인 성공");
        });
    }

    public void LogOut()
    {
        auth.SignOut();
        Debug.Log("로그아웃");
    }*/

    public void LogInPlayGames()
    {
        if(!Social.localUser.authenticated)
        {
            PlayGamesPlatform.Instance.Authenticate(success =>
            {
                if (success)
                {
                    //Debug.Log("Google Success");
                    text.text = "Google Success";
                    StartCoroutine(TryFirebaseLogin());
                }
                else
                {
                    text.text = "Google Failure";
                }
            });
        }
    }

    public void LogOutPlayGames()
    {
        if(Social.localUser.authenticated)
        {
            PlayGamesPlatform.Instance.SignOut();
            auth.SignOut();
            text.text = "LogOut";
        }
    }

    private IEnumerator TryFirebaseLogin()
    {
        while(string.IsNullOrEmpty(((PlayGamesLocalUser)Social.localUser).GetIdToken()))
        {
            yield return null;
        }

        string idToken = ((PlayGamesLocalUser)Social.localUser).GetIdToken();
        text.text += idToken;

        Credential credential = GoogleAuthProvider.GetCredential(idToken, null);
        auth.SignInWithCredentialAsync(credential).ContinueWith(task =>
        {
            if(task.IsCanceled)
            {
                text.text = "Firebase Canceled";
                //Debug.Log("Firebase Canceled");
                return;
            }
            else if(task.IsFaulted)
            {
                text.text = "Firebase Faulted";
                //Debug.Log("Firebase Faulted");
                return;
            }

            text.text = "Firebase Success";
            //Debug.Log("Firebase Success");
        });
    }
}
