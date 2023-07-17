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
    DatabaseReference reference, reference2;

    public InputField idText;
    public InputField pwText;
    public Text errorText;

    private bool idCheck, pwCheck;

    public static string myID; // id는 전역변수로 저장
    private string myPW;

    public static DiffData myDiffData; // 각 게임의 난이도를 담을 자료구조 (전역변수)
    private int cDiff, pDiff, sDiff;

    public static PlayData myPlayData; // 각 게임별 플레이 횟수를 담을 자료구조 (전역변수)
    private int cPlay, pPlay, sPlay;

    void Start()
    {
        FirebaseApp.DefaultInstance.Options.DatabaseUrl = new Uri(DBurl);
        reference = FirebaseDatabase.DefaultInstance.GetReference("UserData");
        reference2 = FirebaseDatabase.DefaultInstance.GetReference("GameData");
    }

    public void LoginEvent() // login 버튼 이벤트
    {
        if (idCheck)
        {
            if(pwText.text == myPW)
            {
                errorText.text = "";
                // id 저장하기
                myID = idText.text;

                if (taskfinish[0] && taskfinish[1] && taskfinish[2] && taskfinish[3])
                {
                    myDiffData = new DiffData(cDiff, pDiff, sDiff);
                    myPlayData = new PlayData(cPlay, pPlay, sPlay);
                    Debug.Log("난이도 - 확인 : " + cDiff + " 오염 : " + pDiff + " 대칭 : " + sDiff);
                    Debug.Log("플레이 횟수 - 확인 : " + cPlay + " 오염 : " + pPlay + " 대칭 : " + sPlay);
                    SceneManager.LoadScene("Survey");
                }
                else
                {
                    errorText.text = "Please try again in a few minutes";
                }
            }
            else
            {
                errorText.text = "The username or password do not match";
            }
        }
        else
        {
            errorText.text = "The username or password do not match";
        }
        
    }

    public void IdChange() // id input field의 값이 변할때마다 호출
    {
        flag = true;
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

    public void PwChange()
    {
        flag = true;
    }

    private bool[] taskfinish = new bool[4];
    private bool flag = true;
    private void Update()
    {
        // task 작업 이므로 Update에 구현
        if(idCheck && pwText.text == myPW)
        {
            if (flag)
            {
                // 각 게임의 난이도 구하기
                DatabaseReference re = reference.Child(idText.text);
                re.GetValueAsync().ContinueWith(task =>
                {
                    taskfinish[0] = false;
                    if (task.IsCompleted)
                    {
                        DataSnapshot snapshot = task.Result;
                        foreach (DataSnapshot data in snapshot.Children)
                        {
                            if (data.Key == "ConfirmationGameDifficulty") // 확인강박 난이도 저장
                            {
                                cDiff = Convert.ToInt32(data.Value);
                            }
                            else if (data.Key == "PollutionGameDifficulty") // 오염강박 난이도 저장
                            {
                                pDiff = Convert.ToInt32(data.Value);
                            }
                            else if (data.Key == "SymmetryGameDifficulty") // 대칭강박 난이도 저장
                            {
                                sDiff = Convert.ToInt32(data.Value);
                            }
                        }
                        taskfinish[0] = true;
                        return;
                    }
                });
                // 각 게임별 플레이 횟수 구하기
                findPlayTime("Confirmation",1);
                findPlayTime("Pollution",2);
                findPlayTime("Symmetry",3);
                flag = false;
            }
        }
    }

    public void findPlayTime(string gameType, int index)
    {
        DatabaseReference re = reference2.Child(gameType).Child(idText.text).Child("play");
        re.GetValueAsync().ContinueWith(task =>
        {
            taskfinish[index] = false;
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                if(gameType == "Confirmation")
                {
                    cPlay = Convert.ToInt32(snapshot.Value);
                    taskfinish[index] = true;
                    return;
                }
                else if(gameType == "Pollution")
                {
                    pPlay = Convert.ToInt32(snapshot.Value);
                    taskfinish[index] = true;
                    return;
                }
                else if(gameType == "Symmetry")
                {
                    sPlay = Convert.ToInt32(snapshot.Value);
                    taskfinish[index] = true;
                    return;
                }
            }
        });
    }

    public class DiffData // 각 게임의 난이도를 담을 자료구조
    {
        public int ConfirmationGameDifficulty;
        public int PollutionGameDifficulty;
        public int SymmetryGameDifficulty;
        public DiffData(int CDiff, int PDiff, int SDiff)
        {
            ConfirmationGameDifficulty = CDiff;
            PollutionGameDifficulty = PDiff;
            SymmetryGameDifficulty = SDiff;
        }
    }

    public class PlayData // 각 게임별 플레이 횟수를 담을 자료구조
    {
        public int ConfirmationPlay;
        public int PollutionPlay;
        public int SymmetryPlay;
        public PlayData(int cPlay, int pPlay, int sPlay)
        {
            ConfirmationPlay = cPlay;
            PollutionPlay = pPlay;
            SymmetryPlay = sPlay;
        }
    }
}
