using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; //유니티에서 씬에 관련된 제어를 해주기 위해 추가
using System;

public class ChangeScene5 : MonoBehaviour
{
    public Button outsideBtn;
    private int complete;

    public void ChangeSceneBtn()
    {
        switch (this.gameObject.name)
        {
            case "MainMenu_Button":
                SceneManager.LoadScene("LogIn");
                break;

            case "OpenDoor_Button":
                SceneManager.LoadScene("Entrance");
                break;

            case "CloseDoor_Button":
                SceneManager.LoadScene("EntranceOpen");
                break;

            case "DownArrow_Entrance":
                SceneManager.LoadScene("LivingRoom");
                break;

            case "Outside_Button":
                ActiveOutside();
                if (complete == 1) //ToDoList complete?
                {
                    SceneManager.LoadScene("Puzzle");
                }
                break;

            case "RightArrow_Kitchen":
                SceneManager.LoadScene("Room_c");
                break;

            case "LeftArrow_Kitchen":
                SceneManager.LoadScene("LivingRoom");
                break;

            case "DownArrow_LivingRoom":
                SceneManager.LoadScene("EntranceOpen");
                break;

            case "RightArrow_LivingRoom":
                SceneManager.LoadScene("Kitchen_c");
                break;

            case "Hint_Button":
                SceneManager.LoadScene("LivingRoom");
                break;

            case "LeftArrow_Room":
                SceneManager.LoadScene("Kitchen_c");
                break;

            case "GameStart_Button":
                SceneManager.LoadScene("EntranceOpen");
                break;
        }
    }

    public void ActiveOutside()
    {
        int completeNum = 0;

        for (int j = 0; j < GV.ListNum; j++)
        {
            switch (GV.randomNumbers[j])
            {
                case 0:
                    if (GV.Light_LivingRoom == 1) completeNum += 1;
                    break;
                case 1:
                    if (GV.Light_Kitchen == 1) completeNum +=1;
                    break;
                case 2:
                    if (GV.Light_Room == 1) completeNum +=1;
                    break;
                case 3:
                    if (GV.tv == 1) completeNum +=1;
                    break;
                case 4:
                    if (GV.Fan_Room == 1) completeNum +=1;
                    break;
                case 5:
                    if (GV.Fan_LivingRoom == 1) completeNum +=1;
                    break;
                case 6:
                    if (GV.PowerStrip_Room == 1) completeNum +=1;
                    break;
                case 7:
                    if (GV.PowerStrip_LivingRoom == 1) completeNum +=1;
                    break;
                case 8:
                    if (GV.GasRange == 1) completeNum +=1;
                    break;
                case 9:
                    if (GV.GasValve == 1) completeNum +=1;
                    break;
                case 10:
                    if (GV.faucet == 1) completeNum +=1;
                    break;
                case 11:
                    if (GV.computer == 1) completeNum +=1;
                    break;
                case 12:
                    if (GV.Window_LivingRoom == 1) completeNum +=1;
                    break;
                case 13:
                    if (GV.Window_Kitchen == 1) completeNum +=1;
                    break;
                case 14:
                    if (GV.Window_Room == 1) completeNum +=1;
                    break;
                case 15:
                    if (GV.Wallet == 1) completeNum +=1;
                    break;
                case 16:
                    if (GV.Phone == 1) completeNum +=1;
                    break;
                case 17:
                    if (GV.Carkey == 1) completeNum +=1;
                    break;
                case 18:
                    if (GV.HouseKey == 1) completeNum +=1;
                    break;
                case 19:
                    if (GV.Tissue == 1) completeNum +=1;
                    break;
                case 20:
                    if (GV.FirstAidKit == 1) completeNum +=1;
                    break;
            }

            if(GV.ListNum == completeNum)
            {
                complete = 1;
            }
            else
            {
                complete = 0;
            }
        }
    }
}
