using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;

public class FirbaseInit : MonoBehaviour
{
    public string DBurl = "https://pattern-breaker-1cbe6-default-rtdb.firebaseio.com/";
    DatabaseReference reference;
    
    void Start()
    {
        FirebaseApp.DefaultInstance.Options.DatabaseUrl = new Uri(DBurl);
        PlayCntReadDB();
        WriteDB();
        ReadDB();
    }

    public void WriteDB()
    {
        reference = FirebaseDatabase.DefaultInstance.GetReference("GameData/Pollution");
        // admin 자식에 데이터 저장
        data d1 = new data(ChangeScene6.findDirty, ChangeScene6.remainTime, WashButton.washCounting, ResultScene.totalScore);

        if (ResultScene.totalScore >= 6000)
        {
            LoginController.myDiffData.PollutionGameDifficulty = 3;
        }
        else if (ResultScene.totalScore >= 3000)
        {
            LoginController.myDiffData.PollutionGameDifficulty = 2;
        }
        else
        {
            LoginController.myDiffData.PollutionGameDifficulty = 1;
        }

        string adminKey = ((LoginController.myPlayData.PollutionPlay)+1).ToString();
        string jsondata = JsonUtility.ToJson(d1);
        reference.Child(LoginController.myID).Child(adminKey).SetRawJsonValueAsync(jsondata);

        FirebaseDatabase.DefaultInstance.GetReference("UserData/" + LoginController.myID).Child("PollutionGameDifficulty").SetValueAsync(LoginController.myDiffData.PollutionGameDifficulty);
        // play 자식에 데이터 저장
        reference.Child(LoginController.myID).Child("play").SetValueAsync(LoginController.myPlayData.PollutionPlay);
    }

    public void ReadDB()
    {
        DatabaseReference adminRef = FirebaseDatabase.DefaultInstance.GetReference("GameData/Pollution/"+ LoginController.myID);
        adminRef.GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                foreach (DataSnapshot adminData in snapshot.Children)
                {
                    string adminKey = adminData.Key;
                    string jsondata = adminData.GetRawJsonValue();
                    data d1 = JsonUtility.FromJson<data>(jsondata);
                    Debug.Log("Found dirty things: " + d1.FoundDirtyThings);
                    Debug.Log("Remaining Time: " + d1.RemainingTime);
                    Debug.Log("Number of washings: " + d1.NumberOfWashings);
                    Debug.Log("Score: " + d1.Score);
                }
            }
        });
    }

    public class data
    {
        public int FoundDirtyThings = 0;
        public int RemainingTime = 0;
        public int NumberOfWashings = 0;
        public int Score = 0;

        public data(int x, int y, int z, int w)
        {
            this.FoundDirtyThings = x;
            this.RemainingTime = y;
            this.NumberOfWashings = z;
            this.Score = w;
        }
    }

    //Write - 유저의 플레이 횟수 +1 늘리고 저장
    public void PlayCntWriteDB()
    {
        LoginController.myPlayData.PollutionPlay += 1;

        string path = "GameData/Pollution/" + LoginController.myID + "/play";
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.GetReference(path);

        reference.SetValueAsync(LoginController.myPlayData.PollutionPlay);
    }

    //Read - 유저의 플레이 횟수 가져오기
    public void PlayCntReadDB()
    {
        string path = "GameData/Pollution/" + LoginController.myID + "/play";
        FirebaseDatabase.DefaultInstance
            .GetReference(path)
            .GetValueAsync().ContinueWith(task =>
            {
                if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;
                    if (snapshot.Exists)
                    {
                        LoginController.myPlayData.PollutionPlay = Convert.ToInt32(snapshot.Value);
                        UnityEngine.Debug.Log(LoginController.myPlayData.PollutionPlay);
                    }
                }
                PlayCntWriteDB(); // 플레이 횟수 +1 후 저장
            });
    }
}