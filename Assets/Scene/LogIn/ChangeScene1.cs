using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //����Ƽ���� ���� ���õ� ��� ���ֱ� ���� �߰�

public class ChangeScene1 : MonoBehaviour
{
    public void ChangeSceneBtn()
    {
        switch (this.gameObject.name)
        {
            case "Login_Button":
                SceneManager.LoadScene("Survey");
                break;

            case "SignUp_Button":
                SceneManager.LoadScene("SignUp");
                break;

           
        }
    }
 
}
