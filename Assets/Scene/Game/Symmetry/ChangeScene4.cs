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
            case "ReStart_Button":
                SceneManager.LoadScene("SymmetryRule");
                break;

            case "MainMenu_Button":
                SceneManager.LoadScene("GameSelect");
                break;

            case "GameStart_Button":
                SceneManager.LoadScene("Show_Symmetry");
                break;

                //10�� ������ �Ѿ�� ȭ�� ��ȯ �ʿ�
                //���� �ð� ������ ��� ȭ������ ��ȯ �ʿ�

        }
    }
 
}
