using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CDiffText : MonoBehaviour
{
    public GameObject TextObj;
    public GameObject CanvasTextC;
    private bool clickEvent = true;

    void Start()
    {
        TextObj.GetComponent<Text>().text = "level of difficulty \n\n" + "Confirmation : " + LoginController.myDiffData.ConfirmationGameDifficulty.ToString();
    }

    void OnMouseDown()
    {
        clickEvent = !clickEvent;
        CanvasTextC.SetActive(clickEvent);

    }
}

