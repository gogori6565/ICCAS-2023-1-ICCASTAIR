using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmDiffText : MonoBehaviour
{
    public Text CTextDiff;
    public GameObject CanvasTextC;
    private bool clickEvent = true;

    void Start()
    {
        TextDiff.text = "level of difficulty \n\n" + "Confirmation : " + LoginController.myDiffData.ConfirmationGameDifficulty.ToString();
    }

    void OnMouseDown()
    {
        clickEvent = !clickEvent;
        CanvasText.SetActive(clickEvent);

    }
}
