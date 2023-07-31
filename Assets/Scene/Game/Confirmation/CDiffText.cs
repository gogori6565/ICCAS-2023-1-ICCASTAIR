using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CDiffText : MonoBehaviour
{
    public GameObject TextObj;
    public GameObject CanvasTextC;
    public GameObject donggleObj;
    private bool clickEvent = true;

    void Start()
    {
        TextObj.GetComponent<Text>().text = "level of difficulty \n\n" + "Checking : " + LoginController.myDiffData.ConfirmationGameDifficulty.ToString();
    }

    void OnMouseDown()
    {
        clickEvent = !clickEvent;
        if (clickEvent)
        {
            donggleObj.GetComponent<Animator>().speed = 1f;
        }
        else
        {
            donggleObj.GetComponent<Animator>().speed = 0f;
        }
        CanvasTextC.SetActive(clickEvent);

    }
}

