using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;
using System;
using UnityEngine.SceneManagement;

public class LoginController : MonoBehaviour
{
    private string DBurl = "https://pattern-breaker-1cbe6-default-rtdb.firebaseio.com/";
    DatabaseReference reference;

    public InputField idText;
    public InputField pwText;
    public Text errorText;

    private bool idCheck, pwCheck;

    public static string myID; // id�� ���������� ����
    private string myPW;
    
    void Start()
    {
        FirebaseApp.DefaultInstance.Options.DatabaseUrl = new Uri(DBurl);
        reference = FirebaseDatabase.DefaultInstance.GetReference("UserData");
    }

    public void LoginEvent() // login ��ư �̺�Ʈ
    {
        if (idCheck)
        {
            if(pwText.text == myPW)
            {
                errorText.text = "";
                SceneManager.LoadScene("Survey");
                return;
            }
        }
        errorText.text = "The username or password do not match";
    }

    public void IdChange() // id input field�� ���� ���Ҷ����� ȣ��
    {
        idCheck = false;
        DatabaseReference re = reference.Child(idText.text).Child("PassWord");
        re.GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                if (snapshot.Value == null)
                {
                    idCheck = false;
                }
                else
                {
                    myPW = snapshot.Value.ToString();
                    idCheck = true;
                }
            }
        });
    }
}
