using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //유니티에서 씬에 관련된 제어를 해주기 위해 추가

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
                GiveUpController.gameType = "Symmetry";
                SceneManager.LoadScene("SymmetryRule");
                break;

            case "Pollution_Compulsion":
                GiveUpController.gameType = "Pollution";
                SceneManager.LoadScene("PollutionRule");
                break;

            case "Confirmation_Compulsion":
                GiveUpController.gameType = "Confirmation";
                SceneManager.LoadScene("ConfirmationRule");
                break;

            case "Statistic":
                SceneManager.LoadScene("Statistic");
                break;

            case "MainMenu_Button":
                SceneManager.LoadScene("LogIn");
                break;


        }
    }
 
}
