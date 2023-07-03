using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //유니티에서 씬에 관련된 제어를 해주기 위해 추가

public class ChangeScene5 : MonoBehaviour
{
    public void ChangeSceneBtn()
    {
        switch (this.gameObject.name)
        {
            case "MainMenu_Button":
                SceneManager.LoadScene("LogIn");
                break;

            case "OpenDoor_Button":
                SceneManager.LoadScene("Entrance");
                break;

            case "CloseDoor_Button":
                SceneManager.LoadScene("EntranceOpen");
                break;

            case "DownArrow_Entrance":
                SceneManager.LoadScene("LivingRoom");
                break;

            case "Outside_Button":
                SceneManager.LoadScene("Puzzle");
                break;

            case "RightArrow_Kitchen":
                SceneManager.LoadScene("Room_c");
                break;

            case "LeftArrow_Kitchen":
                SceneManager.LoadScene("LivingRoom");
                break;

            case "DownArrow_LivingRoom":
                SceneManager.LoadScene("EntranceOpen");
                break;

            case "RightArrow_LivingRoom":
                SceneManager.LoadScene("Kitchen_c");
                break;

            case "Hint_Button":
                SceneManager.LoadScene("LivingRoom");
                break;

            case "LeftArrow_Room":
                SceneManager.LoadScene("Kitchen_c");
                break;

            case "GameStart_Button":
                SceneManager.LoadScene("EntranceOpen");
                break;
        }
    }
 
}
