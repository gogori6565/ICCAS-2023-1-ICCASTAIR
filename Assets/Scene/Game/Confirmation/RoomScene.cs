using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Versioning;
using UnityEngine;
using UnityEngine.UI;

public class RoomScene : MonoBehaviour
{
    public Button comObj, psObj, lightObj, fanObj, windowObj, phoneObj, walletObj, getbackObj;
    public Sprite comOFF, psOFF, lightOFF, fanOFF, windowOFF;
    string comOFFPath, psOFFpath, lightOFFpath, fanOFFpath, windowOFFpath;
    public Image darkObj;

    private GameObject ToDoListObj;
    private bool hasFoundToDoList = false;

    Color phoneCol, walletCol, darkCol;

    private AudioSource turnSound;

    void Start()
    {
        setAudioSetting();

        comObj = GameObject.Find("computer").GetComponent<Button>();
        psObj = GameObject.Find("PowerStrip_Room").GetComponent<Button>();
        lightObj = GameObject.Find("Light_Room").GetComponent<Button>();
        fanObj = GameObject.Find("Fan_Room").GetComponent<Button>();
        windowObj = GameObject.Find("Window_Room").GetComponent<Button>();
        phoneObj = GameObject.Find("Phone").GetComponent<Button>();
        walletObj = GameObject.Find("Wallet").GetComponent<Button>();
        getbackObj = GameObject.Find("Getback").GetComponent<Button>();
        darkObj = GameObject.Find("dark").GetComponent<UnityEngine.UI.Image>();

        if (GV.outside == 1)
        {
            Color color = getbackObj.GetComponent<UnityEngine.UI.Image>().color;
            color.a = 1f;
            getbackObj.GetComponent<UnityEngine.UI.Image>().color = color;
        }
        else
        {
            Color color = getbackObj.GetComponent<UnityEngine.UI.Image>().color;
            color.a = 0.3f;
            getbackObj.GetComponent<UnityEngine.UI.Image>().color = color;
        }

        comOFFPath = "on_off/컴퓨터 꺼짐";
        comOFF = LoadSpriteFromPath(comOFFPath);

        psOFFpath = "on_off/멀티탭 off";
        psOFF = LoadSpriteFromPath(psOFFpath);

        lightOFFpath = "on_off/방 전등 off";
        lightOFF = LoadSpriteFromPath(lightOFFpath);

        fanOFFpath = "on_off/선풍기 off";
        fanOFF = LoadSpriteFromPath(fanOFFpath);

        windowOFFpath = "on_off/창문 닫힘";
        windowOFF = LoadSpriteFromPath(windowOFFpath);

        if (GV.computer == 1)
        {
            UnityEngine.UI.Image comImage = comObj.image;
            comImage.sprite = comOFF;
        }
        if (GV.PowerStrip_Room == 1)
        {
            UnityEngine.UI.Image psImage = psObj.image;
            psImage.sprite = psOFF;
        }
        if (GV.Light_Room == 1)
        {
            UnityEngine.UI.Image lightImage = lightObj.image;
            lightImage.sprite = lightOFF;

            darkCol = darkObj.GetComponent<UnityEngine.UI.Image>().color;
            darkCol.a = 0.3f;
            darkObj.GetComponent<UnityEngine.UI.Image>().color = darkCol;
        }
        if (GV.Fan_Room == 1)
        {
            UnityEngine.UI.Image fanImage = fanObj.image;
            fanImage.sprite = fanOFF;
        }
        if (GV.Window_Room == 1)
        {
            UnityEngine.UI.Image windowImage = windowObj.image;
            windowImage.sprite = windowOFF;
        }
        if (GV.Phone == 1)
        {
            phoneCol = phoneObj.GetComponent<UnityEngine.UI.Image>().color;
            phoneCol.a = 0.3f;
            phoneObj.GetComponent<UnityEngine.UI.Image>().color = phoneCol;
        }
        if (GV.Wallet == 1)
        {
            walletCol = walletObj.GetComponent<UnityEngine.UI.Image>().color;
            walletCol.a = 0.3f;
            walletObj.GetComponent<UnityEngine.UI.Image>().color = walletCol;
        }
    }

    // 사운드 소스 불러오기
    public void setAudioSetting()
    {
        GameObject obj = GameObject.Find("TurnSound");
        turnSound = obj.GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!hasFoundToDoList && GV.outside == 1)
        {
            ToDoListObj = GameObject.Find("ToDoList");
            if (ToDoListObj != null)
            {
                ToDoListObj.SetActive(false); // 비활성화
                hasFoundToDoList = true;
            }
        }
    }

    /*이미지 경로 -> Sprite로 변경 함수*/
    Sprite LoadSpriteFromPath(string path)
    {
        Texture2D texture = Resources.Load<Texture2D>(path);
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);
        return sprite;
    }
    
    /*버튼 클릭 시 버튼 이미지 변경*/
    public Button btn;
    public Sprite sprites;

    public void ChangeImageBtn()
    {
        UnityEngine.UI.Image btnImage = btn.image;

        switch (this.gameObject.name)
        {
            case "computer":
                if (GV.randomNumbers.IndexOf(11) != -1)
                {
                    turnSound.Play();
                    GV.computer = 1;
                    btnImage.sprite = sprites;
                }
                break;
            case "Light_Room":
                if (GV.randomNumbers.IndexOf(2) != -1)
                {
                    turnSound.Play();
                    GV.Light_Room = 1;
                    btnImage.sprite = sprites;

                    darkCol = darkObj.GetComponent<UnityEngine.UI.Image>().color;
                    darkCol.a = 0.3f;
                    darkObj.GetComponent<UnityEngine.UI.Image>().color = darkCol;
                }
                break;
            case "PowerStrip_Room":
                if (GV.randomNumbers.IndexOf(6) != -1)
                {
                    turnSound.Play();
                    GV.PowerStrip_Room = 1;
                    btnImage.sprite = sprites;
                }
                break;
            case "Fan_Room":
                if (GV.randomNumbers.IndexOf(4) != -1)
                {
                    turnSound.Play();
                    GV.Fan_Room = 1;
                    btnImage.sprite = sprites;
                }
                break;
            case "Window_Room":
                if (GV.randomNumbers.IndexOf(14) != -1)
                {
                    turnSound.Play();
                    GV.Window_Room = 1;
                    btnImage.sprite = sprites;
                }
                break;
        }
    }
}

//UnityEngine.Debug.Log(GV.computer);