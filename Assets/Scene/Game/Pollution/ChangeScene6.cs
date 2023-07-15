using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public static int remainTime;
    public static int findDirty;

    public void ClearNextScene(int cleartime)
    {
        remainTime = cleartime;
        StartCoroutine(DelayedSceneChange());
    }
    public void ClickCnt(int click)
    {
        findDirty = click;
    }
   
    public void TimeOutNextScene()
    {
        remainTime = 0;
        StartCoroutine(DelayedSceneChange());
    }

    private int preIndex;

    private IEnumerator DelayedSceneChange()
    {
        yield return new WaitForSeconds(1f); // 2�� ���� ������
        Result();//���â 
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

    private void Result()
    {
        SceneManager.LoadScene("Result_Pollution");


    }

}