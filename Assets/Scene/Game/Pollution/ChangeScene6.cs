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

    private static int preIndex;

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
        CursorChanger.cursorIndex = 0;
        WashButton.washCounting = 0;
        WashButton.subtractPointSum = 0;

        int randomIndex = GetUniqueRandomIndex();
        string randomScene = scenes[randomIndex];
        SceneManager.LoadScene(randomScene);
    }

    private void Result()
    {
        //�ε� �� ���� ���
        if (ResultScene.preScore < 3000)
        {
            ResultScene.totalScore = (remainTime * 10) + (findDirty * 300) - WashButton.subtractPointSum; //��

        }
        else if (ResultScene.preScore >= 3000 && ResultScene.preScore < 6000)
        {
            ResultScene.totalScore = (remainTime * 15) + (findDirty * 400) - WashButton.subtractPointSum; //��

        }
        else if (ResultScene.preScore >= 6000)
        {
            ResultScene.totalScore = (remainTime * 20) + (findDirty * 500) - WashButton.subtractPointSum; //��
        }

        SceneManager.LoadScene("Result_Pollution");
    }
}