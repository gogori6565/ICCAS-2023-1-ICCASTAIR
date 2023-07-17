using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private GameObject resultPanel;
    [SerializeField]
    private Board board;

    public UnityEngine.UI.Text timerText;

    void Update()
    {
        GV.elapsedTime = Time.time - GV.startTime;

        // �ʴ����� ǥ���� ���ڿ� �������� ��ȯ
        string minutes = ((int)GV.elapsedTime / 60).ToString("00");
        string seconds = (GV.elapsedTime % 60).ToString("00");

        // Ÿ�̸Ӹ� �ؽ�Ʈ ������Ʈ�� ǥ��
        timerText.text = minutes + ":" + seconds;
    }

    public void OnResultPanel()
    {
        resultPanel.SetActive(true);
    }

    public void OnClickRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
