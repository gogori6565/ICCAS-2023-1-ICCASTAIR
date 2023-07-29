using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SdiffText : MonoBehaviour
{
    public GameObject TextObj;
    public GameObject CanvasTextS;
    public GameObject donggleObj;
    private bool clickEvent = true;

    void Start()
    {
        TextObj.GetComponent<Text>().text = "level of difficulty \n\n" + "Symmetry : " + LoginController.myDiffData.SymmetryGameDifficulty.ToString();
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
        CanvasTextS.SetActive(clickEvent);

    }
}
