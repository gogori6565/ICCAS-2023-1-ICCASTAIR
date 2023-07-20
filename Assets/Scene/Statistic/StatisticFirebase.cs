using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using System;
using System.Threading;
using System.Threading.Tasks; 

public class StatisticFirebase : MonoBehaviour
{
    //�ֱ� 7�� Score ���� string �迭
    int[] cScore = new int[7]; //Ȯ�� score
    int[] pScore = new int[7]; //���� score
    int[] sScore = new int[7]; //��Ī score

    //Ȯ�� ��������
    int[] cUsedHint = new int[7];
    int[] cWrongAnswer = new int[7];

    //���� ���� ����
    int[] pFoundDirtyThings = new int[7];
    int[] pNumberOfWashings = new int[7];
    int[] pRemainingTime = new int[7];

    //��Ī ���� ����
    int[] sAsymmetryTouch = new int[7];
    int[] sRemainTime = new int[7];
    int[] sSymmetryTouch = new int[7];

    //Firebase Connect
    public string DBurl = "https://pattern-breaker-1cbe6-default-rtdb.firebaseio.com/";
    DatabaseReference reference;

    public int cPlay, pPlay, sPlay;
    public int cSum, pSum, sSum;       //Score �հ�
    public float cAverage, pAverage, sAverage;       //Score ���

    void Start()
    {
        cPlay = LoginController.myPlayData.ConfirmationPlay;
        pPlay = LoginController.myPlayData.PollutionPlay;
        sPlay = LoginController.myPlayData.SymmetryPlay;

        cSum = 0;
        pSum = 0;
        sSum = 0;
        
        UnityEngine.Debug.Log("�÷��� Ƚ��: " + cPlay);

        /*
         * �ֱ� 7�� ���� ���� �������� (����, ��������)
         */
        for(int i = 0; i < 7; i++)
        {
            if (cPlay <= 0) break;
            cReadDB(cPlay, i);
            cPlay--;
        }

        for (int i = 0; i < 7; i++)
        {
            if (pPlay <= 0) break;
            pReadDB(pPlay, i);
            pPlay--;
        }

        for (int i = 0; i < 7; i++)
        {
            if (sPlay <= 0) break;
            sReadDB(sPlay, i);
            sPlay--;
        }

        /*
         * �� ���� ��ü ���ھ� �ջ�
         */
        while (cPlay > 0)
        {
            AllScore("Confirmation", cPlay);
            cPlay--;
        }
        while (pPlay > 0)
        {
            AllScore("Pollution", pPlay);
            pPlay--;
        }
        while (sPlay > 0)
        {
            AllScore("Symmetry", sPlay);
            sPlay--;
        }
    }

    //�� ���� ��ü Score �ջ�
    async void AllScore(string game, int play)
    {
        string path = "GameData/" + game + "/" + LoginController.myID + "/" + play.ToString();
        //string path = "GameData/Confirmation/admin/1";
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.GetReference(path);

        DataSnapshot snapshot = await reference.Child("Score").GetValueAsync();
        if (snapshot.Exists)
        {
            int score = int.Parse(snapshot.Value.ToString());

            if (game == "Confirmation")
            {
                cSum += score;
            }
            else if (game == "Pollution")
            {
                pSum += score;
            }
            else if (game == "Symmetry")
            {
                sSum += score;
            }

            if (play == 1)
            {
                CalAverage(game);
            }
        }
    }

    //Score ��� ���
    public void CalAverage(string game)
    {
        if (game == "Confirmation")
        {
            cAverage = (float)cSum / (float)LoginController.myPlayData.ConfirmationPlay;
        }
        else if (game == "Pollution")
        {
            pAverage = (float)pSum / (float)LoginController.myPlayData.PollutionPlay;
        }
        else if (game == "Symmetry")
        {
            sAverage = (float)sSum / (float)LoginController.myPlayData.SymmetryPlay;
            /*
            UnityEngine.Debug.Log("��: " + sSum);
            UnityEngine.Debug.Log("������: " + LoginController.myPlayData.SymmetryPlay);
            UnityEngine.Debug.Log("���: " + sAverage);
            */
        }
    }

    //Ȯ�� ���� ���� - ���� ���� ��������
    public void cReadDB(int play, int idx)
    {
        Dictionary<string, string> dictionary = new Dictionary<string, string>();

        string path = "GameData/Confirmation/" + LoginController.myID + "/" + play.ToString();
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
                    cScore[idx] = Int32.Parse(dictionary["Score"]);
                    cUsedHint[idx] = Int32.Parse(dictionary["UsedHint"]);
                    cWrongAnswer[idx] = Int32.Parse(dictionary["WrongAnswer"]);
                }
            });
    }

    //���� ���� ���� - ���� ���� ��������
    public void pReadDB(int play, int idx)
    {
        Dictionary<string, string> dictionary = new Dictionary<string, string>();

        string path = "GameData/Pollution/" + LoginController.myID + "/" + play.ToString();
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
                    pScore[idx] = Int32.Parse(dictionary["Score"]);
                    pFoundDirtyThings[idx] = Int32.Parse(dictionary["FoundDirtyThings"]);
                    pNumberOfWashings[idx] = Int32.Parse(dictionary["NumberOfWashings"]);
                    pRemainingTime[idx] = Int32.Parse(dictionary["RemainingTime"]);

                    UnityEngine.Debug.Log("pScore[" + idx + "] : " + pScore[idx]);
                    /*
                    UnityEngine.Debug.Log("pFoundDirtyThings[" + idx + "] : " + pFoundDirtyThings[idx]);
                    UnityEngine.Debug.Log("pNumberOfWashings[" + idx + "] : " + pNumberOfWashings[idx]);
                    UnityEngine.Debug.Log("pRemainingTime[" + idx + "] : " + pRemainingTime[idx]);
                    */
                }
            });
    }

    //��Ī ���� ���� - ���� ���� ��������
    public void sReadDB(int play, int idx)
    {
        Dictionary<string, string> dictionary = new Dictionary<string, string>();

        string path = "GameData/Symmetry/" + LoginController.myID + "/" + play.ToString();
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
                    sScore[idx] = Int32.Parse(dictionary["Score"]);
                    sAsymmetryTouch[idx] = Int32.Parse(dictionary["AsymmetryTouch"]);
                    sRemainTime[idx] = Int32.Parse(dictionary["RemainTime"]);
                    sSymmetryTouch[idx] = Int32.Parse(dictionary["SymmetryTouch"]);
                }
            });
    }
}
