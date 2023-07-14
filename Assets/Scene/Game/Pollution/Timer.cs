using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text TimerText;
    public Text GameOverText;
    public GamePlay gamePlay;
    private float setTime = 60f;
    private bool isGameOver = false;
    private bool isGameClear = false;
    
    void Start()
    {
        gamePlay = FindObjectOfType<GamePlay>();
        if (gamePlay == null)
        {
            Debug.LogError("GamePlay instance not found!");
        }
    }

    void Update()
    {
        if (!isGameOver && !isGameClear)
        {
            GameOverText.enabled = false;

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

        //게임 클리어
        if (isGameClear)
        {
            TimerText.enabled = false;
            gamePlay.SetClickable(false);
            WashButton washbtn = FindObjectOfType<WashButton>();
            washbtn.Possible(false);
        }
    }

    void GameOver()
    {
        isGameOver = true;
        Debug.Log("Time Over");
        TimerText.enabled = false;
        GameOverText.enabled = true;

        gamePlay.SetClickable(false);
    }

    public void GameClear(bool win)
    {
        isGameClear = win;
    }
}
