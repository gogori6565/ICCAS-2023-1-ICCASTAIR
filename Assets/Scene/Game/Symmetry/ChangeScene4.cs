using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //����Ƽ���� ���� ���õ� ��� ���ֱ� ���� �߰�

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

                //10�� ������ �Ѿ�� ȭ�� ��ȯ �ʿ�
                //���� �ð� ������ ��� ȭ������ ��ȯ �ʿ�

        }
    }
 
}
