using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using System;
using UnityEngine.SceneManagement;

public class WriteFirebase : MonoBehaviour
{
    public string DBurl = "https://pattern-breaker-1cbe6-default-rtdb.firebaseio.com/";
    DatabaseReference reference;

    void Start()
    {
        FirebaseApp.DefaultInstance.Options.DatabaseUrl = new Uri(DBurl);
        reference = FirebaseDatabase.DefaultInstance.GetReference("UserData");
    }

    public void SubmitEvent(int score)
    {
        int diff = 1;
        if (score <= 43) // 정상
        {
            diff = 3;
        }
        else if (score <= 48)
        {
            diff = 2;
        }
        else if (score <= 60)
        {
            diff = 1;
        }

        string jsondata2 = JsonUtility.ToJson(diff);

        reference.Child(SignUpSceneController.myID).Child("ConfirmationGameDifficulty").SetValueAsync(diff);
        reference.Child(SignUpSceneController.myID).Child("PollutionGameDifficulty").SetValueAsync(diff);
        reference.Child(SignUpSceneController.myID).Child("SymmetryGameDifficulty").SetValueAsync(diff);

        SignUpSceneController.myID = ""; // id 초기화

        SceneManager.LoadScene("SurveyResult");
    }
}
