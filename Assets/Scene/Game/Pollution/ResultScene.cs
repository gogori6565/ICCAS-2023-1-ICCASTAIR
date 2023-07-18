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

        objectText.text = "Found dirty things: " + ChangeScene6.findDirty.ToString();
        timeText.text = "Remaining Time : " + ChangeScene6.remainTime.ToString() + "sec";
        washText.text = "Number of washings : " + WashButton.washCounting.ToString();
        scoreText.text = "" + totalScore.ToString();
    }

    private bool gageSetting = false;
    private float percent = 0f;
    float width, y;
    // ������ ����
    public void setGageBar(int diff, int score)
    {
        width = emptyGage.GetComponent<RectTransform>().rect.width; // ����ִ� ���������� ����
        y = emptyGage.GetComponent<RectTransform>().anchoredPosition.y; // ����ִ� ���������� y��

        percent = 0f;
        if (diff == 1) // ���̵� �� �϶�
        {
            percent = score / 3000.0f;
        }
        else if (diff == 2) // ���̵� �� �϶�
        {
            percent = score / 6000.0f;
        }
        else if (diff == 3) // ���̵� �� �϶�
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
        if (gageSetting) // �� ������ �Ϸ�Ǿ��ٸ�
        {
            if (temp >= percent) // ������ �ۼ�Ʈ������ ũ�ų� ������ �ִϸ��̼� ����
            {
                gageSetting = false;
            }
            fillGage.GetComponent<RectTransform>().anchoredPosition = new Vector3(-(width / 2) + width * temp / 2, y, 0); // ���� ���������� ���� ����
            fillGage.transform.localScale = new Vector3(temp, 1, 0); // ���� ���������� ��ġ ����
            temp += 0.005f;
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
