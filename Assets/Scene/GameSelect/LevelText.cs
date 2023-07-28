using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelText : MonoBehaviour
{
    public Text TextDiff;
    public GameObject CanvasText;
    private bool clickEvent = true;

    void Start()
    {
        TextDiff.text = "level of difficulty \n\n" + "Symmetry : " + LoginController.myDiffData.SymmetryGameDifficulty.ToString() +"\n"
            + "Pollution : " + LoginController.myDiffData.PollutionGameDifficulty.ToString() + "\n" + "Confirmation : " + LoginController.myDiffData.ConfirmationGameDifficulty.ToString();
    }

    void OnMouseDown()
    {
        clickEvent = !clickEvent;
        CanvasText.SetActive(clickEvent);
       
    }
}
