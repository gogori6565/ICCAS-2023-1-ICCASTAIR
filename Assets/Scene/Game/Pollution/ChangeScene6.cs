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
                int randomIndex = Random.Range(0, scenes.Length);
                string randomScene = scenes[randomIndex]; // 랜덤 인덱스에 해당하는 Scene 가져오기
                SceneManager.LoadScene(randomScene); // Scene 전환
                break;
        }
    }
 
}
