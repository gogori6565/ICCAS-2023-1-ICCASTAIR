using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelText : MonoBehaviour
{
    public GameObject TextObj;
    public GameObject CanvasText;
    public GameObject donggleObj;
    private bool clickEvent = true;

    void Start()
    {
        TextObj.GetComponent<Text>().text = "level of difficulty \n\n" + "Symmetry : " + LoginController.myDiffData.SymmetryGameDifficulty.ToString() + "\n"
            + "Pollution : " + LoginController.myDiffData.PollutionGameDifficulty.ToString() + "\n" + "Confirmation : " + LoginController.myDiffData.ConfirmationGameDifficulty.ToString();
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
        CanvasText.SetActive(clickEvent);
       
    }
}
