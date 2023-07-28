using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //유니티에서 씬에 관련된 제어를 해주기 위해 추가

public class ChangeScene2 : MonoBehaviour
{
    public void ChangeSceneBtn()
    {
        switch (this.gameObject.name)
        {
            case "MainMenu_Button":
                SceneManager.LoadScene("LogIn");
                break;

            case "SignUp_Button":
                SceneManager.LoadScene("LogIn");
                break;

            case "Statistics_Button":
                SceneManager.LoadScene("Statistic");
                break;

           
        }
    }
 
}
