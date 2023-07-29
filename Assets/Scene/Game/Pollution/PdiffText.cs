using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PdiffText : MonoBehaviour
{
    public GameObject TextObj;
    public GameObject CanvasTextP;
    private bool clickEvent = true;

    void Start()
    {
        TextObj.GetComponent<Text>().text = "level of difficulty \n\n" + "Pollution : " + LoginController.myDiffData.PollutionGameDifficulty.ToString();
    }

    void OnMouseDown()
    {
        clickEvent = !clickEvent;
        CanvasTextP.SetActive(clickEvent);

    }
}
