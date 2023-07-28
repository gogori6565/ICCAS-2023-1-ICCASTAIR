using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KitchenScene : MonoBehaviour
{
    public Button grObj, gvObj, lightObj, faucetObj, windowObj, fakObj, tissueObj, getbackObj;
    public Sprite grOFF, gvOFF, lightOFF, faucetOFF, windowOFF;
    string grOFFPath, gvOFFpath, lightOFFpath, faucetOFFpath, windowOFFpath;
    public Image darkObj;

    private GameObject ToDoListObj;
    private bool hasFoundToDoList = false;

    Color fakCol, tissueCol, darkCol;

    private AudioSource turnSound, foucetSound;

    void Start()
    {
        setAudioSetting();

        grObj = GameObject.Find("GasRange").GetComponent<Button>();
        gvObj = GameObject.Find("GasValve").GetComponent<Button>();
        lightObj = GameObject.Find("Light_Kitchen").GetComponent<Button>();
        faucetObj = GameObject.Find("faucet").GetComponent<Button>();
        windowObj = GameObject.Find("Window_Kitchen").GetComponent<Button>();
        fakObj = GameObject.Find("FirstAidKit").GetComponent<Button>();
        tissueObj = GameObject.Find("Tissue").GetComponent<Button>();
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

            darkCol = darkObj.GetComponent<UnityEngine.UI.Image>().color;
            darkCol.a = 0.3f;
            darkObj.GetComponent<UnityEngine.UI.Image>().color = darkCol;
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

    // 사운드 소스 불러오기
    public void setAudioSetting()
    {
        GameObject obj = GameObject.Find("TurnSound");
        turnSound = obj.GetComponent<AudioSource>();

        GameObject fobj = GameObject.Find("FaucetSound");
        foucetSound = fobj.GetComponent<AudioSource>();
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
            case "GasRange":
                if (GV.randomNumbers.IndexOf(8) != -1)
                {
                    turnSound.Play();
                    GV.GasRange = 1;
                    btnImage.sprite = sprites;
                }
                break;
            case "GasValve":
                if (GV.randomNumbers.IndexOf(9) != -1)
                {
                    turnSound.Play();
                    GV.GasValve = 1;
                    btnImage.sprite = sprites;
                }
                break;
            case "Light_Kitchen":
                if (GV.randomNumbers.IndexOf(1) != -1)
                {
                    turnSound.Play();
                    GV.Light_Kitchen = 1;
                    btnImage.sprite = sprites;

                    darkCol = darkObj.GetComponent<UnityEngine.UI.Image>().color;
                    darkCol.a = 0.3f;
                    darkObj.GetComponent<UnityEngine.UI.Image>().color = darkCol;
                }
                break;
            case "faucet":
                if (GV.randomNumbers.IndexOf(10) != -1)
                {
                    foucetSound.Play();
                    GV.faucet = 1;
                    btnImage.sprite = sprites;
                }
                break;
            case "Window_Kitchen":
                if (GV.randomNumbers.IndexOf(13) != -1)
                {
                    turnSound.Play();
                    GV.Window_Kitchen = 1;
                    btnImage.sprite = sprites;
                }
                break;
        }
    }
}
