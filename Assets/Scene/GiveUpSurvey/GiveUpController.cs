using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;
using System;
using UnityEngine.SceneManagement;

public class GiveUpController : MonoBehaviour
{
    public GameObject[] surveyDiff = new GameObject[5];
    public GameObject surveyText;
    public GameObject popup, popup2;
    public Text text;
    public string DBurl = "https://pattern-breaker-1cbe6-default-rtdb.firebaseio.com/";
    DatabaseReference reference;
    public static string gameType = "";

    void Start()
    {
        popup.SetActive(false);
        popup2.SetActive(false);
        text = surveyText.GetComponent<Text>();
        FirebaseApp.DefaultInstance.Options.DatabaseUrl = new Uri(DBurl);
        reference = FirebaseDatabase.DefaultInstance.GetReference("UserData");
    }

    public void setGameType(string name)
    {
        gameType = name;
    }

    public void SubmitEvent()
    {
        int index = -1;

        for(int i=0; i<5; i++) // üũ�Ǿ��ִ� ���̵� ���ϱ�
        {
            if (surveyDiff[i].GetComponent<Toggle>().isOn)
            {
                index = i;
            }
        }

        if(index >= 3) // ����� �Ǵ� �ſ� ������� ����������
        {
            if(gameType == "Symmetry" && LoginController.myDiffData.SymmetryGameDifficulty == 1 ||
                gameType == "Pollution" && LoginController.myDiffData.PollutionGameDifficulty == 1 ||
                gameType == "Confirmation" && LoginController.myDiffData.ConfirmationGameDifficulty == 1)
            {
                popup2.SetActive(true);
            }
            else
            {
                popup.SetActive(true);
            }
        }
        else
        {
            if (text.text != "")
            {
                DatabaseReference re = reference.Child(LoginController.myID).Child("Survey").Child(gameType).Child(DateTime.Now.ToString());
                re.SetValueAsync(text.text);
            }
            SceneManager.LoadScene("GameSelect");
        }
    }

    bool levelDownComplete = false;
    public void DownLevel()
    {
        DatabaseReference re = reference.Child(LoginController.myID).Child("Survey");
        if (gameType == "Symmetry")
        {
            LoginController.myDiffData.SymmetryGameDifficulty--;
            reference.Child(LoginController.myID).Child("SymmetryGameDifficulty").SetValueAsync(LoginController.myDiffData.SymmetryGameDifficulty);
            if (text.text != "")
            {
                re.Child(gameType).Child(DateTime.Now.ToString()).SetValueAsync(text.text);
            }
        }
        else if(gameType == "Pollution")
        {
            LoginController.myDiffData.PollutionGameDifficulty--;
            reference.Child(LoginController.myID).Child("PollutionGameDifficulty").SetValueAsync(LoginController.myDiffData.PollutionGameDifficulty);
            if (text.text != "")
            {
                re.Child(gameType).Child(DateTime.Now.ToString()).SetValueAsync(text.text);
            }
        }
        else if(gameType == "Confirmation")
        {
            LoginController.myDiffData.ConfirmationGameDifficulty--;
            reference.Child(LoginController.myID).Child("ConfirmationGameDifficulty").SetValueAsync(LoginController.myDiffData.ConfirmationGameDifficulty);
            if (text.text != "")
            {
                re.Child(gameType).Child(DateTime.Now.ToString()).SetValueAsync(text.text);
            }
        }

        SceneManager.LoadScene("GameSelect");
    }

    public void NoChangeLevel()
    {
        if (text.text != "")
        {
            DatabaseReference re = reference.Child(LoginController.myID).Child("Survey").Child(gameType).Child(DateTime.Now.ToString());
            re.SetValueAsync(text.text);
        }

        SceneManager.LoadScene("GameSelect");
    }

    void Update()
    {
        
    }
}
