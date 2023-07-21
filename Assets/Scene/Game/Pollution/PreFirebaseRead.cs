using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;

public class PreFirebaseRead : MonoBehaviour
{
    public string DBurl = "https://pattern-breaker-1cbe6-default-rtdb.firebaseio.com/";
    public static int preDirty = 0;
    public static int preWashing = 0;
    public static int preRemainTime = 0;
    public static int preTotalScore = 0;

    // Start is called before the first frame update
    void Start()
    {
        DatabaseReference adminRef = FirebaseDatabase.DefaultInstance.GetReference("GameData/Pollution/" + LoginController.myID);
        adminRef.GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                // 미리 저장된 플레이 기록의 갯수를 파악
                int playCount = (int)snapshot.ChildrenCount;

                // 가장 마지막에 플레이한 데이터의 키를 구함
                string lastPlayKey = (LoginController.myPlayData.PollutionPlay).ToString();
                if (snapshot.HasChild(lastPlayKey))
                {
                    DataSnapshot lastPlaySnapshot = snapshot.Child(lastPlayKey);
                    string jsondata = lastPlaySnapshot.GetRawJsonValue();
                    data d1 = JsonUtility.FromJson<data>(jsondata);

                    // 데이터 사용
                    Debug.Log("Found dirty things: " + d1.FoundDirtyThings);
                    Debug.Log("Remaining Time: " + d1.RemainingTime);
                    Debug.Log("Number of washings: " + d1.NumberOfWashings);
                    Debug.Log("Score: " + d1.Score);
                    preDirty = d1.FoundDirtyThings;
                    preWashing = d1.NumberOfWashings;
                    preRemainTime = d1.RemainingTime;
                   preTotalScore = d1.Score;

                }
                else
                {
                    Debug.Log("No data found for the last play.");
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
}
