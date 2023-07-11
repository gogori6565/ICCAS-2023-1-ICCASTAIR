﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ResultSceneController : MonoBehaviour
{
    public Text scoreText;
    public Text lowLevel;
    public Text highLevel;
    public Text symmetricTouch;
    public Text asymmetricTouch;
    public Text remainingTime;
    public PlaySceneController.gameData newGameData;
    public GameObject emptyGage;
    public GameObject fillGage;
    // Use this for initialization
    void Start()
    {
        newGameData = PlaySceneController.myGameData;

        int score = getScore(newGameData);
        scoreText.text = score + "";
        lowLevel.text = "Lv" + newGameData.diff;
        if(newGameData.diff != 3)
        {
            highLevel.text = "Lv" + (newGameData.diff + 1);
        }
        else
        {
            highLevel.text = "MAX";
        }
        symmetricTouch.text = "Symmetric object touch : " + newGameData.symmetryTouchCount;
        asymmetricTouch.text = "Asymmetric object touch : " + (newGameData.asymmetryTouchCount + newGameData.symmetryMoreTouchCount);
        remainingTime.text = "Remaining time : " + newGameData.timeRemain + " sec";

        setGageBar(newGameData.diff, score);
    }

    // 점수 설정
    public int getScore(PlaySceneController.gameData data)
    {
        int score = 0;
        if(data.diff == 1) // 난이도 하 일때
        {
            score += data.symmetryTouchCount * 1000;
            score += data.timeRemain * 10;
            score -= data.symmetryMoreTouchCount * 300;
            score -= data.asymmetryTouchCount * 400;
            if(score > 3500) // 난이도 하의 max score 3500
            {
                score = 3500;
            }
        }
        else if(data.diff == 2) // 난이도 중 일때
        {
            score += data.symmetryTouchCount * 1200;
            score += data.timeRemain * 10;
            score -= data.symmetryMoreTouchCount * 500;
            score -= data.asymmetryTouchCount * 700;
            if (score > 6500) // 난이도 중의 max score 6500
            {
                score = 6500;
            }
        }
        else if(data.diff == 3) // 난이도 상 일때
        {
            score += data.symmetryTouchCount * 1125;
            score += data.timeRemain * 25;
            score -= data.symmetryMoreTouchCount * 700;
            score -= data.asymmetryTouchCount * 900;
            if (score > 10000) // 난이도 상의 max score 10000
            {
                score = 10000;
            }
        }

        if (score < 0)
        {
            score = 0;
        }
        return score;
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
        if(diff == 1) // 난이도 하 일때
        {
            percent = score / 3000.0f;
        }
        else if(diff == 2) // 난이도 중 일때
        {
            percent = score / 6000.0f;
        }
        else if(diff == 3) // 난이도 상 일때
        {
            percent = score / 10000.0f;
        }

        if(percent > 1f)
        {
            percent = 1f;
        }

        gageSetting = true;
    }

    float temp = 0f;
    // Update is called once per frame
    void Update()
    {
        // 게이지 바 애니메이션 효과
        if (gageSetting) // 바 설정이 완료되었다면
        {
            if(temp >= percent) // 설정된 퍼센트값보다 크거나 같으면 애니메이션 종료
            {
                gageSetting = false;
            }
            fillGage.GetComponent<RectTransform>().anchoredPosition = new Vector3(-(width / 2) + width * temp / 2, y, 0); // 색깔 게이지바의 길이 설정
            fillGage.transform.localScale = new Vector3(temp, 1, 0); // 색깔 게이지바의 위치 설정
            temp += 0.005f;
        }
    }
}