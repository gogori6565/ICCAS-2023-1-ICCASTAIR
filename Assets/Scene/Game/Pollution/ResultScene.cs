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
    public static int[] subtractPoints = { 0, 600, 400, 200, 0 }; //하 


    private int totalScore = (ChangeScene6.remainTime * 10) + (ChangeScene6.findDirty * 300) - WashButton.subtractPointSum; //하
    
    private void Start()
    {
        objectText = GameObject.Find("objectText").GetComponent<Text>();
        timeText = GameObject.Find("timeText").GetComponent<Text>();
        scoreText = GameObject.Find("scoreText").GetComponent<Text>();
        washText = GameObject.Find("washText").GetComponent<Text>();

        // 텍스트 컴포넌트들을 찾아 할당합니다.
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
