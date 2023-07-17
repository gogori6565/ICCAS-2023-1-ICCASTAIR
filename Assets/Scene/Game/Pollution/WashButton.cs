using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class WashButton : MonoBehaviour
{
    private bool washPossible = true;
    public static int subtractPointIndex;
    public static int subtractPointSum = 0;
    public static int washCounting = 0;

    public void OnButtonClick()
    {
        if (washPossible)
        {
            washCounting++;
            subtractPointIndex = CursorChanger.cursorIndex;

            if (ResultScene.preScore < 3000)
            {
                subtractPointSum += ResultScene.subtractPoints[subtractPointIndex];
            }
            //Сп
            if (ResultScene.preScore >= 3000 && ResultScene.preScore < 6000)
            {
                if (washCounting > 1)
                {
                    subtractPointSum += ResultScene.subtractPoints[subtractPointIndex];
                }
            }
            //Лѓ
            if (ResultScene.preScore > 6000)
            {
                if (washCounting > 2)
                {
                    subtractPointSum += ResultScene.subtractPoints[subtractPointIndex];
                }
            }


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