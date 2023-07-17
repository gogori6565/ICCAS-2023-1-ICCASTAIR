using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using System;
using System.Threading;

public class ConfirmationFirebase : MonoBehaviour
{
    public string DBurl = "https://pattern-breaker-1cbe6-default-rtdb.firebaseio.com/";
    DatabaseReference reference;

    public int playcnt; //������ �÷��� Ƚ�� - DB���� ������ �� ����

    //Write - PlayData ����
    public void WriteDB()
    {
        reference = FirebaseDatabase.DefaultInstance.RootReference;

        data PlayData = new data(GV.score, GV.Hintcnt, GV.fail); //score, UsedHint, WrongAnswer

        //json �������� ������ ���
        string jsondate1 = JsonUtility.ToJson(PlayData);

        //����
        reference.Child("GameData").Child("Confirmation").Child(LoginController.myID).Child(playcnt.ToString()).SetRawJsonValueAsync(jsondate1);
        //reference.Child("GameData").Child("Confirmation").Child("user1").Child(playcnt.ToString()).SetRawJsonValueAsync(jsondate1);
    }

    //Write - ������ �÷��� Ƚ�� +1 �ø��� ����
    public void PlayCntWriteDB()
    {
        playcnt += 1;
        LoginController.myPlayData.ConfirmationPlay++;

        string path = "GameData/Confirmation/" + LoginController.myID;
        //string path = "GameData/Confirmation/user1";
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.GetReference(path);

        reference.Child("play").SetValueAsync(playcnt);

        WriteDB();          //PlayData ����
    }

    //Read - ������ �÷��� Ƚ�� ��������
    public void PlayCntReadDB()
    {
        string path = "GameData/Confirmation/" + LoginController.myID + "/play";
        //string path = "GameData/Confirmation/user1/play";
        FirebaseDatabase.DefaultInstance
            .GetReference(path)
            .GetValueAsync().ContinueWith(task =>
            {
                if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;
                    playcnt = Int32.Parse(snapshot.Value.ToString());
                    UnityEngine.Debug.Log(playcnt);

                    PlayCntWriteDB(); //�÷��� Ƚ�� +1 �� ����
                }
            });
    }

    //Write - ������ Ȯ�� ���� ���� ���̵� ���� (����)
    public void DiffWriteDB()
    {
        string path = "UserData/"+ LoginController.myID;
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.GetReference(path);

        reference.Child("ConfirmationGameDifficulty").SetValueAsync(GV.diff);
    }

    //Read - ������ Ȯ�� ���� ���� ���̵� �������� (���� ���� �� ȣ��Ǿ�� �� -> ���̵��� ���� ���� ���̵� ����)
    public void DiffReadDB()
    {
        string path = "UserData/"+ LoginController.myID;
        //string path = "UserData/user1";
        FirebaseDatabase.DefaultInstance
            .GetReference(path)
            .GetValueAsync().ContinueWith(task =>
            {
                if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;
                    foreach (DataSnapshot data in snapshot.Children)
                    {
                        if (data.Key == "ConfirmationGameDifficulty")
                        {
                            GV.diff = Int32.Parse(data.Value.ToString());
                            UnityEngine.Debug.Log(GV.diff);
                        }
                    }
                }
            });
    }

    //Read - ���� �� ���� ����(����+��������) ��������
    public void DeductionReadDB(int pcnt)
    {
        Dictionary<string, string> dictionary = new Dictionary<string, string>();

        string path = "GameData/Confirmation/" + LoginController.myID + "/" + pcnt.ToString();
        //string path = "GameData/Confirmation/user1/" + pcnt.ToString();
        FirebaseDatabase.DefaultInstance
            .GetReference(path)
            .GetValueAsync().ContinueWith(task =>
            {
                if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;
                    foreach (DataSnapshot data in snapshot.Children)
                    {
                        dictionary.Add(data.Key, data.Value.ToString());
                    }
                    GV.PreScore = Int32.Parse(dictionary["score"]);
                    GV.PreUsedHint = Int32.Parse(dictionary["UsedHint"]);
                    GV.PreWrongAnswer = Int32.Parse(dictionary["WrongAnswer"]);
                }
            });
    }

    //Read ����
    public void ReadDB()
    {
        Dictionary<string, string> dictionary = new Dictionary<string, string>();

        string path = "GameData/Confirmation/user1/1";
        FirebaseDatabase.DefaultInstance
            .GetReference(path)
            .GetValueAsync().ContinueWith(task =>
            {
                if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;
                    foreach (DataSnapshot data in snapshot.Children)
                    {
                        dictionary.Add(data.Key, data.Value.ToString());
                    }
                    UnityEngine.Debug.Log(dictionary["score"]);
                    UnityEngine.Debug.Log(dictionary["UsedHint"]);
                    UnityEngine.Debug.Log(dictionary["WrongAnswer"]);
                }
            });
    }

    public class data
    {
        public int score = 0;
        public int UsedHint = 0;
        public int WrongAnswer = 0;

        public data(int score, int UsedHint, int WrongAnswer)
        {
            this.score = score;
            this.UsedHint = UsedHint;
            this.WrongAnswer = WrongAnswer;
        }
    }
}
