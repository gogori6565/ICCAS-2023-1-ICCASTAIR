using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KitchenScene : MonoBehaviour
{
    public Button grObj, gvObj, lightObj, faucetObj, windowObj, fakObj, tissueObj;
    public Sprite grOFF, gvOFF, lightOFF, faucetOFF, windowOFF;
    string grOFFPath, gvOFFpath, lightOFFpath, faucetOFFpath, windowOFFpath;

    Color fakCol, tissueCol;

    void Start()
    {
        grObj = GameObject.Find("GasRange").GetComponent<Button>();
        gvObj = GameObject.Find("GasValve").GetComponent<Button>();
        lightObj = GameObject.Find("Light_Kitchen").GetComponent<Button>();
        faucetObj = GameObject.Find("faucet").GetComponent<Button>();
        windowObj = GameObject.Find("Window_Kitchen").GetComponent<Button>();
        fakObj = GameObject.Find("FirstAidKit").GetComponent<Button>();
        tissueObj = GameObject.Find("Tissue").GetComponent<Button>();

        grOFFPath = "on_off/가스레인지 off";
        grOFF = LoadSpriteFromPath(grOFFPath);

        gvOFFpath = "on_off/가스벨브";
        gvOFF = LoadSpriteFromPath(gvOFFpath);

        lightOFFpath = "on_off/전등 off";
        lightOFF = LoadSpriteFromPath(lightOFFpath);

        faucetOFFpath = "on_off/수도꼭지 off";
        faucetOFF = LoadSpriteFromPath(faucetOFFpath);

        windowOFFpath = "on_off/창문 닫힘";
        windowOFF = LoadSpriteFromPath(windowOFFpath);

        if (GV.GasRange == 1)
        {
            UnityEngine.UI.Image grImage = grObj.image;
            grImage.sprite = grOFF;
        }
        if (GV.GasValve == 1)
        {
            UnityEngine.UI.Image gvImage = gvObj.image;
            gvImage.sprite = gvOFF;
        }
        if (GV.Light_Kitchen == 1)
        {
            UnityEngine.UI.Image lightImage = lightObj.image;
            lightImage.sprite = lightOFF;
        }
        if (GV.faucet == 1)
        {
            UnityEngine.UI.Image faucetImage = faucetObj.image;
            faucetImage.sprite = faucetOFF;
        }
        if (GV.Window_Kitchen == 1)
        {
            UnityEngine.UI.Image windowImage = windowObj.image;
            windowImage.sprite = windowOFF;
        }
        if (GV.FirstAidKit == 1)
        {
            fakCol = fakObj.GetComponent<UnityEngine.UI.Image>().color;
            fakCol.a = 0.3f;
            fakObj.GetComponent<UnityEngine.UI.Image>().color = fakCol;
        }
        if (GV.Tissue == 1)
        {
            tissueCol = tissueObj.GetComponent<UnityEngine.UI.Image>().color;
            tissueCol.a = 0.3f;
            tissueObj.GetComponent<UnityEngine.UI.Image>().color = tissueCol;
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
            case "GasRange":
                GV.GasRange = 1;
                break;
            case "GasValve":
                GV.GasValve = 1;
                break;
            case "Light_Kitchen":
                GV.Light_Kitchen = 1;
                break;
            case "faucet":
                GV.faucet = 1;
                break;
            case "Window_Kitchen":
                GV.Window_Kitchen = 1;
                break;
        }
    }
}
