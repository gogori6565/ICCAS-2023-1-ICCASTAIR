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

            if (LoginController.myDiffData.PollutionGameDifficulty == 1)
            {
                subtractPointSum += ResultScene.subtractPoints[subtractPointIndex];
            }
            //Сп
            if (LoginController.myDiffData.PollutionGameDifficulty == 2)
            {
                if (washCounting > 1)
                {
                    subtractPointSum += ResultScene.subtractPoints[subtractPointIndex];
                }
            }
            //Лѓ
            if (LoginController.myDiffData.PollutionGameDifficulty == 3)
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