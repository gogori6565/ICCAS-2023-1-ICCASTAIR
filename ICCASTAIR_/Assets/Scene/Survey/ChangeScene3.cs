using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //����Ƽ���� ���� ���õ� ��� ���ֱ� ���� �߰�

public class ChangeScene3 : MonoBehaviour
{
    public void ChangeSceneBtn()
    {
        switch (this.gameObject.name)
        {
            case "MainMenu_Button":
                SceneManager.LoadScene("LogIn");
                break;

            case "SurveySummit_Button":
                SceneManager.LoadScene("SurveyResult");
                break;

            case "GameSelect_Button":
                SceneManager.LoadScene("GameSelect");
                break;


        }
    }
 
}
