using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class ToDoList : MonoBehaviour
{
    public Text[] textObj;

    private Boolean complete; //List 완수 하였는가?

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

        // 가져온 Text 오브젝트들 출력
        foreach (Text textObject in textObj)
        {
            UnityEngine.Debug.Log(textObject.text);
        }
    }

    //Update() - 프레임 마다 한 번씩 호출됨
    void Update()
    {
        for (int j = 0; j < GV.ListNum; j++)
        {
            complete = false;

            switch (GV.randomNumbers[j])
            {
                case 0:
                    if (GV.Light_LivingRoom == 1) complete = true;
                    break;
                case 1:
                    if (GV.Light_Kitchen == 1) complete = true;
                    break;
                case 2:
                    if (GV.Light_Room == 1) complete = true;
                    break;
                case 3:
                    if (GV.tv == 1) complete = true;
                    break;
                case 4:
                    if (GV.Fan_Room == 1) complete = true;
                    break;
                case 5:
                    if (GV.Fan_LivingRoom == 1) complete = true;
                    break;
                case 6:
                    if (GV.PowerStrip_Room == 1) complete = true;
                    break;
                case 7:
                    if (GV.PowerStrip_LivingRoom == 1) complete = true;
                    break;
                case 8:
                    if (GV.GasRange == 1) complete = true;
                    break;
                case 9:
                    if (GV.GasValve == 1) complete = true;
                    break;
                case 10:
                    if (GV.faucet == 1) complete = true;
                    break;
                case 11:
                    if (GV.computer == 1) complete = true;
                    break;
                case 12:
                    if (GV.Window_LivingRoom == 1) complete = true;
                    break;
                case 13:
                    if (GV.Window_Kitchen == 1) complete = true;
                    break;
                case 14:
                    if (GV.Window_Room == 1) complete = true;
                    break;
                case 15:
                    if (GV.Wallet == 1) complete = true;
                    break;
                case 16:
                    if (GV.Phone == 1) complete = true;
                    break;
                case 17:
                    if (GV.Carkey == 1) complete = true;
                    break;
                case 18:
                    if (GV.HouseKey == 1) complete = true;
                    break;
                case 19:
                    if (GV.Tissue == 1) complete = true;
                    break;
                case 20:
                    if (GV.FirstAidKit == 1) complete = true;
                    break;
            }

            if (complete)
            {
                textObj[j + 1].text = "<color=\"#808080\">" + GV.ListSentences[GV.randomNumbers[j]] + "</color>";
            }
            else
            {
                textObj[j + 1].text = GV.ListSentences[GV.randomNumbers[j]];
            }
        }
    }
}