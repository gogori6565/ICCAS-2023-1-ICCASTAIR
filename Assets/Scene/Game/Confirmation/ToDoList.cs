using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class ToDoList : MonoBehaviour
{
    public Text[] textObj;

    void Start()
    {
        textObj = new Text[11];

        for (int i = 0; i < 11; i++)
        {
            string objectName = "Text (" + i.ToString() + ")";
            GameObject obj = GameObject.Find(objectName);
            if (obj != null)
            {
                textObj[i] = obj.GetComponent<Text>();
            }
        }

        for (int j = 0; j < GV.ListNum; j++)
        {
            textObj[j+1].text = GV.ListSentences[GV.randomNumbers[j]];
        }

        // 가져온 Text 오브젝트들 출력
        foreach (Text textObject in textObj)
        {
            UnityEngine.Debug.Log(textObject.text);
        }
    }
}