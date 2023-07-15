using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //����Ƽ���� ���� ���õ� ��� ���ֱ� ���� �߰�

public class ChangeScene6 : MonoBehaviour
{
    string[] scenes = { "Kitchen_p", "Restroom", "Room_p" };

    public void ChangeSceneBtn()
    {
        switch (this.gameObject.name)
        {
            case "MainMenu_Button":
                SceneManager.LoadScene("PollutionRule");
                break;

            //�� ��ȯ(����)
            case "GameStart_Button":
                SceneCall();
                break;
        }
    }
    //Ŭ���� �� ��ȯ
    public void ClearNextScene()
    {
        StartCoroutine(DelayedSceneChange());
    }

    public void TimeOutNextScene()
    {
        StartCoroutine(DelayedSceneChange());
    }

    private int preIndex;

    private IEnumerator DelayedSceneChange()
    {
        yield return new WaitForSeconds(2f); // 2�� ���� ������
        ResultScene();//���â 
    }

    private int GetUniqueRandomIndex()
    {
        int randomIndex = Random.Range(0, scenes.Length);

        // ���� �ε����� ������ ��� �ٽ� ������ �ε��� ����
        while (randomIndex == preIndex)
        {
            randomIndex = Random.Range(0, scenes.Length);
        }

        preIndex = randomIndex; // ���� �ε��� ����

        return randomIndex;
    }

    private void SceneCall()
    {
        int randomIndex = GetUniqueRandomIndex();
        string randomScene = scenes[randomIndex];
        SceneManager.LoadScene(randomScene);
    }

    private void ResultScene()
    {
        SceneManager.LoadScene("Result_Pollution");
    }
}