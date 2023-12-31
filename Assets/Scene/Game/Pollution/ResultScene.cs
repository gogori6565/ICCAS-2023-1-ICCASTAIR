using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultScene : MonoBehaviour
{
    public Text objectText;
    public Text timeText;
    public Text scoreText;
    public Text washText;
    public static int[] subtractPoints;

    public static int totalScore;
    public static int ScoreForFirebase;

    public Text lowLevel;
    public Text highLevel;
    public GameObject emptyGage;
    public GameObject fillGage;

    public GameObject gageSound;
    public AudioSource gageAudio;

    private void Start()
    {
        objectText = GameObject.Find("objectText").GetComponent<Text>();
        timeText = GameObject.Find("timeText").GetComponent<Text>();
        scoreText = GameObject.Find("scoreText").GetComponent<Text>();
        washText = GameObject.Find("washText").GetComponent<Text>();

        lowLevel.text = "Lv" + LoginController.myDiffData.PollutionGameDifficulty;
        if (LoginController.myDiffData.PollutionGameDifficulty != 3)
        {
            highLevel.text = "Lv" + (LoginController.myDiffData.PollutionGameDifficulty + 1);
        }
        else
        {
            highLevel.text = "MAX";
        }
        setGageBar(LoginController.myDiffData.PollutionGameDifficulty, totalScore);

        string resultText1;
        string resultText2;
        string resultText3;
        string resultText4;

        if ((ChangeScene6.findDirty - PreFirebaseRead.preDirty) > 0)
        {
            resultText1 = "( +" + (ChangeScene6.findDirty - PreFirebaseRead.preDirty).ToString() + ")";
        }
        else if ((ChangeScene6.findDirty - PreFirebaseRead.preDirty) < 0)
        {
            resultText1 = "(" + (ChangeScene6.findDirty - PreFirebaseRead.preDirty).ToString() + ")";
        }
        else
        {
            resultText1 = "(-)";
        }

        if ((ChangeScene6.remainTime - PreFirebaseRead.preRemainTime) > 0)
        {
            resultText2 = "( +" + (ChangeScene6.remainTime - PreFirebaseRead.preRemainTime).ToString() + ")";
        }
        else if ((ChangeScene6.remainTime - PreFirebaseRead.preRemainTime) < 0)
        {
            resultText2 = "(" + (ChangeScene6.remainTime - PreFirebaseRead.preRemainTime).ToString() + ")";
        }
        else
        {
            resultText2 = "(-)";
        }

        if ((WashButton.washCounting - PreFirebaseRead.preWashing) > 0)
        {
            resultText3 = "( +" + (WashButton.washCounting - PreFirebaseRead.preWashing).ToString() + ")";
        }
        else if ((WashButton.washCounting - PreFirebaseRead.preWashing) < 0)
        {
            resultText3 = "(" + (WashButton.washCounting - PreFirebaseRead.preWashing).ToString() + ")";
        }
        else
        {
            resultText3 = "(-)";
        }

        if ((totalScore - PreFirebaseRead.preTotalScore) > 0)
        {
            resultText4 = "( +" + (totalScore - PreFirebaseRead.preTotalScore).ToString() + ")";
        }
        else if ((totalScore - PreFirebaseRead.preTotalScore) < 0)
        {
            resultText4 = "(" + (totalScore - PreFirebaseRead.preTotalScore).ToString() + ")";
        }
        else
        {
            resultText4 = "(-)";
        }

        gageAudio = gageSound.GetComponent<AudioSource>();

        objectText.text = "Found dirty things: " + ChangeScene6.findDirty.ToString() + "  " + resultText1;
        timeText.text = "Remaining Time : " + ChangeScene6.remainTime.ToString() + "sec  " + resultText2;
        washText.text = "Number of washings : " + WashButton.washCounting.ToString() + "  " + resultText3;
        scoreText.text = "" + totalScore.ToString() + "  " + resultText4;


    }

    private bool gageSetting = false;
    private float percent = 0f;
    float width, y;
    // 점수바 설정
    public void setGageBar(int diff, int score)
    {
        width = emptyGage.GetComponent<RectTransform>().rect.width; // 비어있는 게이지바의 길이
        y = emptyGage.GetComponent<RectTransform>().anchoredPosition.y; // 비어있는 게이지바의 y값

        percent = 0f;
        if (diff == 1) // 난이도 하 일때
        {
            percent = score / 3000.0f;
        }
        else if (diff == 2) // 난이도 중 일때
        {
            percent = score / 6000.0f;
        }
        else if (diff == 3) // 난이도 상 일때
        {
            percent = score / 10000.0f;
        }

        if (percent > 1f)
        {
            percent = 1f;
        }

        gageSetting = true;
    }

    float temp = 0f;
    private void Update()
    {
        if (gageSetting) // 바 설정이 완료되었다면
        {
            if (temp >= percent) // 설정된 퍼센트값보다 크거나 같으면 애니메이션 종료
            {
              
                gageSetting = false;
            }
            fillGage.GetComponent<RectTransform>().anchoredPosition = new Vector3(-(width / 2) + width * temp / 2, y, 0); // 색깔 게이지바의 길이 설정
            fillGage.transform.localScale = new Vector3(temp, 1, 0); // 색깔 게이지바의 위치 설정
            temp += 0.005f;

            if(temp >= 1f)
            {
                gageAudio.Play();
            }
        }
    }

    public void ChangeSceneBtn()
    {
        switch (this.gameObject.name)
        {
            case "MainMenu_Button":

                SceneManager.LoadScene("GameSelect");
                break;

            case "Restart_Button":

                SceneManager.LoadScene("PollutionRule");
                break;
        }
    }

  

}