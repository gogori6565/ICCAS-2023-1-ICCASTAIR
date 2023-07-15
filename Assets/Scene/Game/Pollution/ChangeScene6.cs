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

    private int preIndex;

    private IEnumerator DelayedSceneChange()
    {
        yield return new WaitForSeconds(1f); // 2초 동안 딜레이
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
        int randomIndex = GetUniqueRandomIndex();
        string randomScene = scenes[randomIndex];
        SceneManager.LoadScene(randomScene);
    }

    private void Result()
    {
        SceneManager.LoadScene("Result_Pollution");


    }

}