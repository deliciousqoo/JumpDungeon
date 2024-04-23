using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LogInSystem : MonoBehaviour
{
    public TMP_InputField email;
    public TMP_InputField password;

    public TextMeshProUGUI outputText;

    public void Start()
    {
        FirebaseManager.Instance.LoginState += OnChangedState;
        FirebaseManager.Instance.Init();
    }

    private void OnChangedState(bool sign)
    {
        outputText.text = sign ? "로그인 : " : "로그아웃 : ";
        outputText.text += FirebaseManager.Instance.UserId;
    }

    public void Create()
    {
        string e = email.text;
        string p = password.text;

        FirebaseManager.instance.Create(e, p);
    }

    public void LogIn()
    {
        FirebaseManager.instance.Login(email.text, password.text);
    }

    public void LogOut()
    {
        FirebaseManager.instance.LogOut();
    }

    public void SignInWithGoogle()
    {

    }
}
