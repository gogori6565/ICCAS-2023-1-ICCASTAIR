using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivingRoomScene : MonoBehaviour
{
    public Button tvObj, psObj, lightObj, fanObj, windowObj;
    public Sprite tvOFF, psOFF, lightOFF, fanOFF, windowOFF;
    string tvOFFPath, psOFFpath, lightOFFpath, fanOFFpath, windowOFFpath;

    void Start()
    {
        tvObj = GameObject.Find("tv").GetComponent<Button>();
        psObj = GameObject.Find("PowerStrip_LivingRoom").GetComponent<Button>();
        lightObj = GameObject.Find("Light_LivingRoom").GetComponent<Button>();
        fanObj = GameObject.Find("Fan_LivingRoom").GetComponent<Button>();
        windowObj = GameObject.Find("Window_LivingRoom").GetComponent<Button>();

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
        btnImage.sprite = sprites;

        switch (this.gameObject.name)
        {
            case "tv":
                GV.tv = 1;
                break;
            case "Light_LivingRoom":
                GV.Light_LivingRoom = 1;
                break;
            case "PowerStrip_LivingRoom":
                GV.PowerStrip_LivingRoom = 1;
                break;
            case "Fan_LivingRoom":
                GV.Fan_LivingRoom = 1;
                break;
            case "Window_LivingRoom":
                GV.Window_LivingRoom = 1;
                break;
        }
    }
}
