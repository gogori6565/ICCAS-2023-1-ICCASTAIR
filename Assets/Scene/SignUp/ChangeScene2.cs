using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //����Ƽ���� ���� ���õ� ��� ���ֱ� ���� �߰�

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
