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

            //씬 전환(랜덤)
            case "GameStart_Button":
                SceneCall();
                break;
        }
    }
    //클리어 씬 전환

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
        yield return new WaitForSeconds(1f); // 1초 동안 딜레이
        Result();//결과창 
    }

    private int GetUniqueRandomIndex()
    {
        int randomIndex = Random.Range(0, scenes.Length);

        // 이전 인덱스와 동일한 경우 다시 랜덤한 인덱스 생성
        while (randomIndex == preIndex)
        {
            randomIndex = Random.Range(0, scenes.Length);
        }

        preIndex = randomIndex; // 이전 인덱스 갱신

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
        //로드 전 점수 계산
        if (LoginController.myDiffData.PollutionGameDifficulty == 1)
        {
            ResultScene.totalScore = (remainTime * 10) + (findDirty * 300) - WashButton.subtractPointSum; //하

        }
        else if (LoginController.myDiffData.PollutionGameDifficulty == 2)
        {
            ResultScene.totalScore = (remainTime * 15) + (findDirty * 400) - WashButton.subtractPointSum; //중

        }
        else if (LoginController.myDiffData.PollutionGameDifficulty == 3)
        {
            ResultScene.totalScore = (remainTime * 20) + (findDirty * 500) - WashButton.subtractPointSum; //상
        }

        SceneManager.LoadScene("Result_Pollution");
    }
}