using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class transparency : MonoBehaviour
{
    public Button btn; // ��ư ������Ʈ�� �����ϱ� ���� ����

    Color color;

    void Start()
    {
        //Ŭ�� �̺�Ʈ ����
        btn.onClick.AddListener(HideObject);
    }

    public void HideObject()
    {
        // ������Ʈ�� ��Ȱ��ȭ�Ͽ� ����
        //objectToHide.SetActive(false);

        color = btn.GetComponent<UnityEngine.UI.Image>().color;
        color.a = 0.3f;
        btn.GetComponent<UnityEngine.UI.Image>().color = color;

        switch (this.gameObject.name)
        {
            case "Carkey":
                GV.Carkey = 1;
                break;
            case "Housekey":
                GV.Housekey = 1;
                break;
            case "FirstAidKit":
                GV.FirstAidKit = 1;
                break;
            case "Tissue":
                GV.Tissue = 1;
                break;
            case "Phone":
                GV.Phone = 1;
                break;
            case "Wallet":
                GV.Wallet = 1;
                break;
        }
    }
}
