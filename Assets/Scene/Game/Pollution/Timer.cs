using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text TimerText;
    public Text GameOverText; // Game Over 텍스트를 연결할 변수
    public GamePlay gamePlay; // GamePlay 스크립트 참조를 위한 변수
    float setTime = 60;
    bool isGameOver = false; // 게임 종료 여부를 나타내는 변수

    void Start()
    {
        // GamePlay 스크립트의 인스턴스를 찾아서 gamePlay 변수에 할당
        gamePlay = FindObjectOfType<GamePlay>();
        if (gamePlay == null)
        {
            Debug.LogError("GamePlay instance not found!");
        }
    }

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

        // GamePlay 스크립트에 클릭 가능 여부를 전달하여 클릭 불가능하도록 설정
        gamePlay.SetClickable(false);
    }
}
