using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WashButton : MonoBehaviour
{
    public void OnButtonClick()
    {
        CursorChanger cursorChanger = FindObjectOfType<CursorChanger>();
        if (cursorChanger != null)
        {
            cursorChanger.WashCursorChange();
        }

        //GamePlay gamePlay = FindObjectOfType<GamePlay>();
       // if (gamePlay != null)
       // {
        //    gamePlay.washFlag(true);
        //}
    }

}