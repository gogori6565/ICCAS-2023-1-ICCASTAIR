using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SdiffText : MonoBehaviour
{
    public Text STextDiff;
    public GameObject CanvasTextS;
    private bool clickEvent = true;

    void Start()
    {
        STextDiff.text = "level of difficulty \n\n" + "Symmetry : " + LoginController.myDiffData.SymmetryGameDifficulty.ToString();
    }

    void OnMouseDown()
    {
        clickEvent = !clickEvent;
        CanvasTextS.SetActive(clickEvent);

    }
}
