using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KitchenScene : MonoBehaviour
{
    public Button grObj, gvObj, lightObj, faucetObj, windowObj, fakObj, tissueObj;
    public Sprite grOFF, gvOFF, lightOFF, faucetOFF, windowOFF;
    string grOFFPath, gvOFFpath, lightOFFpath, faucetOFFpath, windowOFFpath;
    
    private GameObject ToDoListObj;
    private bool hasFoundToDoList = false;

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

        grOFFPath = "on_off/���������� off";
        grOFF = LoadSpriteFromPath(grOFFPath);

        gvOFFpath = "on_off/��������";
        gvOFF = LoadSpriteFromPath(gvOFFpath);

        lightOFFpath = "on_off/���� off";
        lightOFF = LoadSpriteFromPath(lightOFFpath);

        faucetOFFpath = "on_off/�������� off";
        faucetOFF = LoadSpriteFromPath(faucetOFFpath);

        windowOFFpath = "on_off/â�� ����";
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

    void Update()
    {
        if (!hasFoundToDoList && GV.outside == 1)
        {
            ToDoListObj = GameObject.Find("ToDoList");
            if (ToDoListObj != null)
            {
                ToDoListObj.SetActive(false); // ��Ȱ��ȭ
                hasFoundToDoList = true;
            }
        }
    }

    /*�̹��� ��� -> Sprite�� ���� �Լ�*/
    Sprite LoadSpriteFromPath(string path)
    {
        Texture2D texture = Resources.Load<Texture2D>(path);
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);
        return sprite;
    }

    /*��ư Ŭ�� �� ��ư �̹��� ����*/
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
                    GV.GasRange = 1;
                    btnImage.sprite = sprites;
                }
                break;
            case "GasValve":
                if (GV.randomNumbers.IndexOf(9) != -1)
                {
                    GV.GasValve = 1;
                    btnImage.sprite = sprites;
                }
                break;
            case "Light_Kitchen":
                if (GV.randomNumbers.IndexOf(1) != -1)
                {
                    GV.Light_Kitchen = 1;
                    btnImage.sprite = sprites;
                }
                break;
            case "faucet":
                if (GV.randomNumbers.IndexOf(10) != -1)
                {
                    GV.faucet = 1;
                    btnImage.sprite = sprites;
                }
                break;
            case "Window_Kitchen":
                if (GV.randomNumbers.IndexOf(13) != -1)
                {
                    GV.Window_Kitchen = 1;
                    btnImage.sprite = sprites;
                }
                break;
        }
    }
}
