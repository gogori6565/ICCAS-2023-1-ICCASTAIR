using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text TimerText;
    public Text GameOverText; // Game Over �ؽ�Ʈ�� ������ ����
    public GamePlay gamePlay; // GamePlay ��ũ��Ʈ ������ ���� ����
    float setTime = 60;
    bool isGameOver = false; // ���� ���� ���θ� ��Ÿ���� ����

    void Start()
    {
        // GamePlay ��ũ��Ʈ�� �ν��Ͻ��� ã�Ƽ� gamePlay ������ �Ҵ�
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
            GameOverText.enabled = false; // Game Over �ؽ�Ʈ ��Ȱ��ȭ

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
        TimerText.enabled = false; // Ÿ�̸� �ؽ�Ʈ ��Ȱ��ȭ
        GameOverText.enabled = true; // Game Over �ؽ�Ʈ Ȱ��ȭ

        // GamePlay ��ũ��Ʈ�� Ŭ�� ���� ���θ� �����Ͽ� Ŭ�� �Ұ����ϵ��� ����
        gamePlay.SetClickable(false);
    }
}
