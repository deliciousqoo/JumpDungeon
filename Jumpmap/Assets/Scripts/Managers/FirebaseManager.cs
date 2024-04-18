using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using UnityEngine.UI;
using TMPro;

public class FirebaseManager : MonoBehaviour
{
    public static FirebaseManager instance = null;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (instance != this) { Destroy(this.gameObject); }
        }
    }

    private FirebaseAuth auth; // �α���, ȸ������ � ���
    private FirebaseUser user; // ������ �Ϸ�� ���� ����

    public TMP_InputField email;
    public TMP_InputField password;

    private void Start()
    {
        auth = FirebaseAuth.DefaultInstance;
    }

    public void Create()
    {
        auth.CreateUserWithEmailAndPasswordAsync(email.text, password.text).ContinueWith(task => 
        {
            if (task.IsCanceled)
            {
                Debug.Log("ȸ������ ���");
                return;
            }
            if(task.IsFaulted)
            {
                Debug.Log("ȸ������ ����");
                return;
            }

            AuthResult authResult = task.Result;
            FirebaseUser newUser = authResult.User;
            Debug.Log("ȸ������ ����");
        });
    }

    public void Login()
    {
        auth.SignInWithEmailAndPasswordAsync(email.text, password.text).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.Log("�α��� ���");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.Log("�α��� ����");
                return;
            }

            AuthResult authResult = task.Result;
            FirebaseUser newUser = authResult.User;
            Debug.Log("�α��� ����");
        });
    }

    public void LogOut()
    {
        auth.SignOut();
        Debug.Log("�α׾ƿ�");
    }
}
