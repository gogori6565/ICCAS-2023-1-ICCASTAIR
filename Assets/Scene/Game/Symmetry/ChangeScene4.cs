using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //유니티에서 씬에 관련된 제어를 해주기 위해 추가

public class ChangeScene4 : MonoBehaviour
{
    public void ChangeSceneBtn()
    {
        switch (this.gameObject.name)
        {
            case "MainMenu_Button":
                SceneManager.LoadScene("GameSelect");
                break;

            case "GameStart_Button":
                SceneManager.LoadScene("Show");
                break;

                //10초 지나면 넘어가는 화면 전환 필요
                //제한 시간 끝나면 결과 화면으로 전환 필요

        }
    }
 
}
