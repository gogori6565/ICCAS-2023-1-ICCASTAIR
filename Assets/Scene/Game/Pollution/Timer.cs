using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text TimerText;
    public Text GameOverText; // Game Over 텍스트를 연결할 변수
    float setTime = 60;
    bool isGameOver = false; // 게임 종료 여부를 나타내는 변수

    void Update()
    {
        if (!isGameOver)
        {
            GameOverText.enabled = false; // Game Over 텍스트 비활성화

            setTime -= Time.deltaTime;
            if (setTime <= 0)
            {
                GameOver();
            }
            if (setTime >= 10)
            {
                TimerText.text = "00:" + ((int)setTime).ToString();
            }
            else if (setTime < 10)
            {
                TimerText.text = "00:0" + ((int)setTime).ToString();
            }
        }
    }

    void GameOver()
    {
        isGameOver = true;
        Debug.Log("Time Over");
        TimerText.enabled = false; // 타이머 텍스트 비활성화
        GameOverText.enabled = true; // Game Over 텍스트 활성화
    }
}