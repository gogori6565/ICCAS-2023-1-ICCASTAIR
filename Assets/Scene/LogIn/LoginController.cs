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

    public static string myID; // id�� ���������� ����
    private string myPW;

    public static DiffData myDiffData; // �� ������ ���̵��� ���� �ڷᱸ�� (��������)
    private int cDiff, pDiff, sDiff;

    public static PlayData myPlayData; // �� ���Ӻ� �÷��� Ƚ���� ���� �ڷᱸ�� (��������)
    private int cPlay, pPlay, sPlay;

    void Start()
    {
        FirebaseApp.DefaultInstance.Options.DatabaseUrl = new Uri(DBurl);
        reference = FirebaseDatabase.DefaultInstance.GetReference("UserData");
        reference2 = FirebaseDatabase.DefaultInstance.GetReference("GameData");
    }

    public void LoginEvent() // login ��ư �̺�Ʈ
    {
        if (idCheck)
        {
            if(pwText.text == myPW)
            {
                errorText.text = "";
                // id �����ϱ�
                myID = idText.text;

                if (taskfinish[0] && taskfinish[1] && taskfinish[2] && taskfinish[3])
                {
                    myDiffData = new DiffData(cDiff, pDiff, sDiff);
                    myPlayData = new PlayData(cPlay, pPlay, sPlay);
                    Debug.Log("���̵� - Ȯ�� : " + cDiff + " ���� : " + pDiff + " ��Ī : " + sDiff);
                    Debug.Log("�÷��� Ƚ�� - Ȯ�� : " + cPlay + " ���� : " + pPlay + " ��Ī : " + sPlay);
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

    public void IdChange() // id input field�� ���� ���Ҷ����� ȣ��
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
        // task �۾� �̹Ƿ� Update�� ����
        if(idCheck && pwText.text == myPW)
        {
            if (flag)
            {
                // �� ������ ���̵� ���ϱ�
                DatabaseReference re = reference.Child(idText.text);
                re.GetValueAsync().ContinueWith(task =>
                {
                    taskfinish[0] = false;
                    if (task.IsCompleted)
                    {
                        DataSnapshot snapshot = task.Result;
                        foreach (DataSnapshot data in snapshot.Children)
                        {
                            if (data.Key == "ConfirmationGameDifficulty") // Ȯ�ΰ��� ���̵� ����
                            {
                                cDiff = Convert.ToInt32(data.Value);
                            }
                            else if (data.Key == "PollutionGameDifficulty") // �������� ���̵� ����
                            {
                                pDiff = Convert.ToInt32(data.Value);
                            }
                            else if (data.Key == "SymmetryGameDifficulty") // ��Ī���� ���̵� ����
                            {
                                sDiff = Convert.ToInt32(data.Value);
                            }
                        }
                        taskfinish[0] = true;
                        return;
                    }
                });
                // �� ���Ӻ� �÷��� Ƚ�� ���ϱ�
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

    public class DiffData // �� ������ ���̵��� ���� �ڷᱸ��
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

    public class PlayData // �� ���Ӻ� �÷��� Ƚ���� ���� �ڷᱸ��
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
