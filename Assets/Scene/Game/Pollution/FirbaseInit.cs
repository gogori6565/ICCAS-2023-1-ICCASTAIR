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
    public static int playcnt = 0;

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
        string adminKey = (playcnt+1).ToString();
        string jsondata = JsonUtility.ToJson(d1);
        reference.Child("admin").Child(adminKey).SetRawJsonValueAsync(jsondata);

        // play 자식에 데이터 저장
        reference.Child("admin").Child("play").SetValueAsync(playcnt);
    }

    public void ReadDB()
    {
        DatabaseReference adminRef = FirebaseDatabase.DefaultInstance.GetReference("GameData/Pollution/admin");
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
                    Debug.Log("Admin Key: " + adminKey);
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
        playcnt += 1;

        string path = "GameData/Pollution/admin/play";
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.GetReference(path);

        reference.SetValueAsync(playcnt);
    }

    //Read - 유저의 플레이 횟수 가져오기
    public void PlayCntReadDB()
    {
        string path = "GameData/Pollution/admin/play";
        FirebaseDatabase.DefaultInstance
            .GetReference(path)
            .GetValueAsync().ContinueWith(task =>
            {
                if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;
                    if (snapshot.Exists)
                    {
                        playcnt = Convert.ToInt32(snapshot.Value);
                        UnityEngine.Debug.Log(playcnt);
                    }
                }
                PlayCntWriteDB(); // 플레이 횟수 +1 후 저장
            });
    }
}