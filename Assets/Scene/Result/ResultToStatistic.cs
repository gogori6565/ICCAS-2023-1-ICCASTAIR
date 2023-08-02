using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

public class ResultToStatistic : MonoBehaviour
{
    //Firebase Connect
    public string DBurl = "https://pattern-breaker-1cbe6-default-rtdb.firebaseio.com/";
    DatabaseReference reference;

    public GameObject Graph;

    public int cPlay, pPlay, sPlay;
    public int cSum, pSum, sSum;       //Score 합계
    public static float cAverage, pAverage, sAverage;       //Score 평균

    public void ToStatistic()
    {
        // 배열 요소를 0으로 초기화
        for (int i = 0; i < StatisticFirebase.cScore.Length; i++)
        {
            StatisticFirebase.cScore[i] = 0;
        }
        for (int i = 0; i < StatisticFirebase.pScore.Length; i++)
        {
            StatisticFirebase.pScore[i] = 0;
        }
        for (int i = 0; i < StatisticFirebase.sScore.Length; i++)
        {
            StatisticFirebase.sScore[i] = 0;
        }

        cPlay = LoginController.myPlayData.ConfirmationPlay;
        pPlay = LoginController.myPlayData.PollutionPlay;
        sPlay = LoginController.myPlayData.SymmetryPlay;

        cSum = 0;
        pSum = 0;
        sSum = 0;

        /*
         * 최근 7판 게임 정보 가져오기 (점수, 감점요인)
         */
        for (int i = 0; i < 7; i++)
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

        cPlay = LoginController.myPlayData.ConfirmationPlay;
        pPlay = LoginController.myPlayData.PollutionPlay;
        sPlay = LoginController.myPlayData.SymmetryPlay;

        /*
         * 각 게임 전체 스코어 합산
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

        SceneManager.LoadScene("Statistic");
    }

    //각 게임 전체 Score 합산
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

    //Score 평균 계산
    public void CalAverage(string game)
    {
        if (game == "Confirmation")
        {
            cAverage = (float)cSum / (float)LoginController.myPlayData.ConfirmationPlay;
            UnityEngine.Debug.Log("cAverage : " + cAverage);
        }
        else if (game == "Pollution")
        {
            pAverage = (float)pSum / (float)LoginController.myPlayData.PollutionPlay;
            UnityEngine.Debug.Log("pAverage : " + pAverage);
        }
        else if (game == "Symmetry")
        {
            sAverage = (float)sSum / (float)LoginController.myPlayData.SymmetryPlay;
            UnityEngine.Debug.Log("sAverage : " + sAverage);

            //Graph.GetComponent<StatisticGraph>().Graph(); //graph 그리기
        }
    }

    //확인 강박 게임 - 게임 정보 가져오기
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
                    StatisticFirebase.cScore[idx] = Int32.Parse(dictionary["Score"]);
                    StatisticFirebase.cUsedHint[idx] = Int32.Parse(dictionary["UsedHint"]);
                    StatisticFirebase.cWrongAnswer[idx] = Int32.Parse(dictionary["WrongAnswer"]);

                    UnityEngine.Debug.Log("cScore[" + idx + "] : " + StatisticFirebase.cScore[idx]);
                }
            });
    }

    //오염 강박 게임 - 게임 정보 가져오기
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
                    StatisticFirebase.pScore[idx] = Int32.Parse(dictionary["Score"]);
                    StatisticFirebase.pFoundDirtyThings[idx] = Int32.Parse(dictionary["FoundDirtyThings"]);
                    StatisticFirebase.pNumberOfWashings[idx] = Int32.Parse(dictionary["NumberOfWashings"]);
                    StatisticFirebase.pRemainingTime[idx] = Int32.Parse(dictionary["RemainingTime"]);

                    UnityEngine.Debug.Log("pScore[" + idx + "] : " + StatisticFirebase.pScore[idx]);
                    /*
                    UnityEngine.Debug.Log("pFoundDirtyThings[" + idx + "] : " + pFoundDirtyThings[idx]);
                    UnityEngine.Debug.Log("pNumberOfWashings[" + idx + "] : " + pNumberOfWashings[idx]);
                    UnityEngine.Debug.Log("pRemainingTime[" + idx + "] : " + pRemainingTime[idx]);
                    */
                }
            });
    }

    //대칭 강박 게임 - 게임 정보 가져오기
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
                    StatisticFirebase.sScore[idx] = Int32.Parse(dictionary["Score"]);
                    StatisticFirebase.sAsymmetryTouch[idx] = Int32.Parse(dictionary["AsymmetryTouch"]);
                    StatisticFirebase.sRemainTime[idx] = Int32.Parse(dictionary["RemainTime"]);
                    StatisticFirebase.sSymmetryTouch[idx] = Int32.Parse(dictionary["SymmetryTouch"]);

                    UnityEngine.Debug.Log("sScore[" + idx + "] : " + StatisticFirebase.sScore[idx]);
                }
            });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
