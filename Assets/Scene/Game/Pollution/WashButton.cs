using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WashButton : MonoBehaviour
{
    private bool washPossible = true;

    public void OnButtonClick()
    {
        if (washPossible)
        {
            CursorChanger cursorChanger = FindObjectOfType<CursorChanger>();
            if (cursorChanger != null)
            {
                cursorChanger.WashCursorChange();
            }
        }
        
    }

    public void Possible(bool possible)
    {
        washPossible = possible;
    }
}