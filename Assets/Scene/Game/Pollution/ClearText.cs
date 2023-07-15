using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearText : MonoBehaviour
{
    public Text clearText;

    private void Start()
    {
        clearText.enabled = false;
    }

    public void CheckClear(int clickCount, int numElementsToShow)
    {
        if (clickCount == numElementsToShow)
        {
            clearText.enabled = true;

            Timer timer = FindObjectOfType<Timer>();
            if (timer != null)
            {
                timer.GameClear(true);
            }
        }
    }
}
