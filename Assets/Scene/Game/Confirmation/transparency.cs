using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class transparency : MonoBehaviour
{
    public Button btn; // 버튼 컴포넌트를 참조하기 위한 변수

    Color color;

    void Start()
    {
        //클릭 이벤트 연결
        btn.onClick.AddListener(HideObject);
    }

    public void HideObject()
    {
        // 오브젝트를 비활성화하여 숨김
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
