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


    private void Start()
    {
        objectText = GameObject.Find("objectText").GetComponent<Text>();
        timeText = GameObject.Find("timeText").GetComponent<Text>();
        scoreText = GameObject.Find("scoreText").GetComponent<Text>();
        washText = GameObject.Find("washText").GetComponent<Text>();
    }

    private void Update()
    {
        objectText.text = "Found dirty things: " + ChangeScene6.findDirty.ToString();
        timeText.text = "Remaining Time : " + ChangeScene6.remainTime.ToString() + "sec";
        washText.text = "Number of washings : " + WashButton.washCounting.ToString();
        scoreText.text = "" + totalScore.ToString();
    }

    public void ChangeSceneBtn()
    {
        switch (this.gameObject.name)
        {
            case "MainMenu_Button":

                SceneManager.LoadScene("GameSelect");
                break;
        }
    }
}
