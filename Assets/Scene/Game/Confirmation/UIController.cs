using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private GameObject resultPanel;
    [SerializeField]
    private Board board;

    public UnityEngine.UI.Text timerText;

    void Update()
    {
        OffResultPanel();

        GV.elapsedTime = Time.time - GV.startTime;

        // 초단위로 표시할 문자열 형식으로 변환
        string minutes = ((int)GV.elapsedTime / 60).ToString("00");
        string seconds = (GV.elapsedTime % 60).ToString("00");

        // 타이머를 텍스트 오브젝트에 표시
        timerText.text = minutes + ":" + seconds;
    }

    public void OnResultPanel()
    {
        resultPanel.SetActive(true);
    }

    public void OffResultPanel()
    {
        if (GV.UIControllerOnce)
        {
            resultPanel.SetActive(false);
        }
    }

    public void OnClickRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

