using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //유니티에서 씬에 관련된 제어를 해주기 위해 추가

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
        yield return new WaitForSeconds(2f); // 2초 동안 딜레이
        ResultScene();//결과창 
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

    private void ResultScene()
    {
        SceneManager.LoadScene("Result_Pollution");
    }
}