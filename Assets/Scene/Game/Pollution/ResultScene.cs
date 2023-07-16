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

    public static int preScore = 0;
    public int totalScore;

    private void Start()
    {
        objectText = GameObject.Find("objectText").GetComponent<Text>();
        timeText = GameObject.Find("timeText").GetComponent<Text>();
        scoreText = GameObject.Find("scoreText").GetComponent<Text>();
        washText = GameObject.Find("washText").GetComponent<Text>();


        if (preScore < 3000)
        {
            totalScore = (ChangeScene6.remainTime * 10) + (ChangeScene6.findDirty * 300) - WashButton.subtractPointSum; //ÇÏ
        }
        else if (preScore >= 3000 && preScore < 6000)
        {
            totalScore = (ChangeScene6.remainTime * 15) + (ChangeScene6.findDirty * 400) - WashButton.subtractPointSum; //Áß
        }else if(preScore >= 6000)
        {
            totalScore = (ChangeScene6.remainTime * 20) + (ChangeScene6.findDirty * 500) - WashButton.subtractPointSum; //»ó
        }
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
                preScore = totalScore;
                SceneManager.LoadScene("GameSelect");
                break;
        }
    }
}
