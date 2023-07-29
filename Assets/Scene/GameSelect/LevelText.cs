using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelText : MonoBehaviour
{
    public GameObject TextObj;
    public GameObject CanvasText;
    private bool clickEvent = true;

    void Start()
    {
        TextObj.GetComponent<Text>().text = "level of difficulty \n\n" + "Symmetry : " + LoginController.myDiffData.SymmetryGameDifficulty.ToString() + "\n"
            + "Pollution : " + LoginController.myDiffData.PollutionGameDifficulty.ToString() + "\n" + "Confirmation : " + LoginController.myDiffData.ConfirmationGameDifficulty.ToString();
    }

    void OnMouseDown()
    {
        clickEvent = !clickEvent;
        CanvasText.SetActive(clickEvent);
       
    }
}
