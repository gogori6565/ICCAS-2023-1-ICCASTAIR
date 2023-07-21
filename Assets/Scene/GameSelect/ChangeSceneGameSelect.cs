using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //����Ƽ���� ���� ���õ� ��� ���ֱ� ���� �߰�

public class ChangeSceneGameSelect : MonoBehaviour
{
    public void ChangeSceneBtn()
    {
        switch (this.gameObject.name)
        {
            case "Login_Button":
                SceneManager.LoadScene("Survey");
                break;

            case "Symmetry_Compulsion":
                SceneManager.LoadScene("SymmetryRule");
                break;

            case "Pollution_Compulsion":
                SceneManager.LoadScene("PollutionRule");
                break;

            case "Confirmation_Compulsion":
                SceneManager.LoadScene("ConfirmationRule");
                break;

            case "Storage_Compulsion":
                SceneManager.LoadScene("StorageRule");
                break;

            case "SButton":
                SceneManager.LoadScene("Statistic");
                break;
        }
    }
 
}
