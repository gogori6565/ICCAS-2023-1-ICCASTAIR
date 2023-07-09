using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivingRoomScene : MonoBehaviour
{
    public Button tvObj, psObj, lightObj;
    public Sprite tvOFF, psOFF, lightOFF;
    string tvOFFPath, psOFFpath, lightOFFpath;

    void Start()
    {
        tvObj = GameObject.Find("tv").GetComponent<Button>();
        psObj = GameObject.Find("PowerStrip_LivingRoom").GetComponent<Button>();
        lightObj = GameObject.Find("Light_LivingRoom").GetComponent<Button>();

        tvOFFPath = "on_off/tv off";
        tvOFF = LoadSpriteFromPath(tvOFFPath);

        psOFFpath = "on_off/��Ƽ�� off";
        psOFF = LoadSpriteFromPath(psOFFpath);

        lightOFFpath = "on_off/���� off";
        lightOFF = LoadSpriteFromPath(lightOFFpath);

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
            case "tv":
                GV.tv = 1;
                break;
            case "Light_LivingRoom":
                GV.Light_LivingRoom = 1;
                break;
            case "PowerStrip_LivingRoom":
                GV.PowerStrip_LivingRoom = 1;
                break;
        }
    }
}
