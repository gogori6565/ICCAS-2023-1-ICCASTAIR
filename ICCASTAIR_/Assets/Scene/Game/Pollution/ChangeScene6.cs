using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //����Ƽ���� ���� ���õ� ��� ���ֱ� ���� �߰�

public class ChangeScene6 : MonoBehaviour
{
    public void ChangeSceneBtn()
    {
        switch (this.gameObject.name)
        {
            case "MainMenu_Button":
                SceneManager.LoadScene("LogIn");
                break;

            case "GameStart_Button":
                SceneManager.LoadScene("Kitchen_p"); //�ϴ� �ξ����� ������ѳ����Կ�
                break;

        }
    }
 
}
