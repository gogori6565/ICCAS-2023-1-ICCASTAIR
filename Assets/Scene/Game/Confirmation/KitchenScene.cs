using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KitchenScene : MonoBehaviour
{
    public Button grObj, gvObj, lightObj;
    public Sprite grOFF, gvOFF, lightOFF;
    string grOFFPath, gvOFFpath, lightOFFpath;

    void Start()
    {
        grObj = GameObject.Find("GasRange").GetComponent<Button>();
        gvObj = GameObject.Find("GasValve").GetComponent<Button>();
        lightObj = GameObject.Find("Light_Kitchen").GetComponent<Button>();

        grOFFPath = "on_off/���������� off";
        grOFF = LoadSpriteFromPath(grOFFPath);

        gvOFFpath = "on_off/��������";
        gvOFF = LoadSpriteFromPath(gvOFFpath);

        lightOFFpath = "on_off/���� off";
        lightOFF = LoadSpriteFromPath(lightOFFpath);

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
        }
    }
}
