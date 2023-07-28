using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PdiffText : MonoBehaviour
{
    public Text PTextDiff;
    public GameObject CanvasTextP;
    private bool clickEvent = true;

    void Start()
    {
        PTextDiff.text = "level of difficulty \n\n" + "Pollution : " + LoginController.myDiffData.PollutionGameDifficulty.ToString();
    }

    void OnMouseDown()
    {
        clickEvent = !clickEvent;
        CanvasTextP.SetActive(clickEvent);

    }
}
