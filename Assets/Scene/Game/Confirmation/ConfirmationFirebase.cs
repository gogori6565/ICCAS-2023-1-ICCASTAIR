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

    private int playcnt; //������ �÷��� Ƚ�� - DB���� ������ �� ����

    //Result Page �ε�Ǹ� ȣ��
    void Start()
    {
        FirebaseApp.DefaultInstance.Options.DatabaseUrl = new Uri(DBurl);
        PlayCntReadDB(); //���� play Ƚ�� �������� �� ���� ù ��°
        //-> playcnt ���� ȣ�� -> playdata ���� ȣ��
    }

    //Write - PlayData ����
    public void WriteDB()
    {
        reference = FirebaseDatabase.DefaultInstance.RootReference;

        data PlayData = new data(GV.score, GV.Hintcnt, GV.fail); //score, UsedHint, WrongAnswer

        //json �������� ������ ���
        string jsondate1 = JsonUtility.ToJson(PlayData);

        //����
        reference.Child("GameData").Child("Confirmation").Child("user1").Child(playcnt.ToString()).SetRawJsonValueAsync(jsondate1);
    }

    //Write - ������ �÷��� Ƚ�� +1 �ø��� ����
    public void PlayCntWriteDB()
    {
        playcnt += 1;
        string path = "GameData/Confirmation/user1";
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.GetReference(path);

        reference.Child("play").SetValueAsync(playcnt);

        WriteDB();
    }

    //Read - ������ �÷��� Ƚ�� ��������
    public void PlayCntReadDB()
    {
        string path = "GameData/Confirmation/user1/play";
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

    //Read - ������ Ȯ�� ���� ���� ���̵� �������� (���� ���� �� ȣ��Ǿ�� �� -> ���̵��� ���� ���� ���̵� ����)
    public void DiffReadDB()
    {
        
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
