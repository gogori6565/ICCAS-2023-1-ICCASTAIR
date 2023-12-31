using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivingRoomScene : MonoBehaviour
{
    public Button tvObj, psObj, lightObj, fanObj, windowObj, carkeyObj, housekeyObj, getbackObj;
    public Sprite tvOFF, psOFF, lightOFF, fanOFF, windowOFF;
    string tvOFFPath, psOFFpath, lightOFFpath, fanOFFpath, windowOFFpath;
    public Image darkObj;

    private GameObject ToDoListObj;
    private bool hasFoundToDoList = false;

    Color carkeyCol, housekeyCol, darkCol;

    private AudioSource turnSound;

    void Start()
    {
        setAudioSetting();

        tvObj = GameObject.Find("tv").GetComponent<Button>();
        psObj = GameObject.Find("PowerStrip_LivingRoom").GetComponent<Button>();
        lightObj = GameObject.Find("Light_LivingRoom").GetComponent<Button>();
        fanObj = GameObject.Find("Fan_LivingRoom").GetComponent<Button>();
        windowObj = GameObject.Find("Window_LivingRoom").GetComponent<Button>();
        carkeyObj = GameObject.Find("Carkey").GetComponent<Button>();
        housekeyObj = GameObject.Find("Housekey").GetComponent<Button>();
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

        tvOFFPath = "on_off/tv off";
        tvOFF = LoadSpriteFromPath(tvOFFPath);

        psOFFpath = "on_off/멀티탭 off";
        psOFF = LoadSpriteFromPath(psOFFpath);

        lightOFFpath = "on_off/전등 off";
        lightOFF = LoadSpriteFromPath(lightOFFpath);

        fanOFFpath = "on_off/선풍기 off";
        fanOFF = LoadSpriteFromPath(fanOFFpath);

        windowOFFpath = "on_off/창문 닫힘";
        windowOFF = LoadSpriteFromPath(windowOFFpath);

        if (GV.tv == 1)
        {
            UnityEngine.UI.Image tvImage = tvObj.image;
            tvImage.sprite = tvOFF;
        }
        if (GV.PowerStrip_LivingRoom == 1)
        {
            UnityEngine.UI.Image psImage = psObj.image;
            psImage.sprite = psOFF;
        }
        if (GV.Light_LivingRoom == 1)
        {
            UnityEngine.UI.Image lightImage = lightObj.image;
            lightImage.sprite = lightOFF;

            darkCol = darkObj.GetComponent<UnityEngine.UI.Image>().color;
            darkCol.a = 0.3f;
            darkObj.GetComponent<UnityEngine.UI.Image>().color = darkCol;
        }
        if (GV.Fan_LivingRoom == 1)
        {
            UnityEngine.UI.Image fanImage = fanObj.image;
            fanImage.sprite = fanOFF;
        }
        if (GV.Window_LivingRoom == 1)
        {
            UnityEngine.UI.Image windowImage = windowObj.image;
            windowImage.sprite = windowOFF;
        }
        if (GV.Carkey == 1)
        {
            carkeyCol = carkeyObj.GetComponent<UnityEngine.UI.Image>().color;
            carkeyCol.a = 0.3f;
            carkeyObj.GetComponent<UnityEngine.UI.Image>().color = carkeyCol;
        }
        if (GV.Housekey == 1)
        {
            housekeyCol = housekeyObj.GetComponent<UnityEngine.UI.Image>().color;
            housekeyCol.a = 0.3f;
            housekeyObj.GetComponent<UnityEngine.UI.Image>().color = housekeyCol;
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
            case "tv":
                if(GV.randomNumbers.IndexOf(3) != -1)
                {
                    turnSound.Play();
                    GV.tv = 1;
                    btnImage.sprite = sprites;
                }
                break;
            case "Light_LivingRoom":
                if (GV.randomNumbers.IndexOf(0) != -1)
                {
                    turnSound.Play();
                    GV.Light_LivingRoom = 1;
                    btnImage.sprite = sprites;

                    darkCol = darkObj.GetComponent<UnityEngine.UI.Image>().color;
                    darkCol.a = 0.3f;
                    darkObj.GetComponent<UnityEngine.UI.Image>().color = darkCol;
                }
                break;
            case "PowerStrip_LivingRoom":
                if (GV.randomNumbers.IndexOf(7) != -1)
                {
                    turnSound.Play();
                    GV.PowerStrip_LivingRoom = 1;
                    btnImage.sprite = sprites;
                }
                break;
            case "Fan_LivingRoom":
                if (GV.randomNumbers.IndexOf(5) != -1)
                {
                    turnSound.Play();
                    GV.Fan_LivingRoom = 1;
                    btnImage.sprite = sprites;
                }
                break;
            case "Window_LivingRoom":
                if (GV.randomNumbers.IndexOf(12) != -1)
                {
                    turnSound.Play();
                    GV.Window_LivingRoom = 1;
                    btnImage.sprite = sprites;
                }
                break;
        }
    }
}
