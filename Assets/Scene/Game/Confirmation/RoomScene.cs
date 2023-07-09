using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Versioning;
using UnityEngine;
using UnityEngine.UI;

public class RoomScene : MonoBehaviour
{
    public Button comObj, psObj;
    public Sprite comOFF, psOFF;
    string comOFFPath, psOFFpath;

    void Start()
    {
        comObj = GameObject.Find("computer").GetComponent<Button>();
        psObj = GameObject.Find("PowerStrip_Room").GetComponent<Button>();

        comOFFPath = "on_off/��ǻ�� ����";
        comOFF = LoadSpriteFromPath(comOFFPath);

        psOFFpath = "on_off/��Ƽ�� off";
        psOFF = LoadSpriteFromPath(psOFFpath);

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
            case "computer":
                GV.computer = 1;
                break;
            case "PowerStrip_Room":
                GV.PowerStrip_Room = 1;
                break;
        }
    }
}

//UnityEngine.Debug.Log(GV.computer);