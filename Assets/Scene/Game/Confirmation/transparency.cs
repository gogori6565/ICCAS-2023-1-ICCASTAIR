using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;

public class transparency : MonoBehaviour
{
    public Button btn; // 버튼 컴포넌트를 참조하기 위한 변수

    Color color;

    private AudioSource bagSound;

    void Start()
    {
        setAudioSetting();

        //클릭 이벤트 연결
        btn.onClick.AddListener(HideObject);
    }

    // 사운드 소스 불러오기
    public void setAudioSetting()
    {
        GameObject obj = GameObject.Find("BagSound");
        bagSound = obj.GetComponent<AudioSource>();
    }

    public void HideObject()
    {
        // 오브젝트를 비활성화하여 숨김
        //objectToHide.SetActive(false);

        switch (this.gameObject.name)
        {
            case "Carkey":
                if (GV.randomNumbers.IndexOf(17) != -1)
                {
                    GV.Carkey = 1;
                    bagSound.Play();

                    color = btn.GetComponent<UnityEngine.UI.Image>().color;
                    color.a = 0.3f;
                    btn.GetComponent<UnityEngine.UI.Image>().color = color;
                }
                break;
            case "Housekey":
                if (GV.randomNumbers.IndexOf(18) != -1)
                {
                    GV.Housekey = 1;
                    bagSound.Play();

                    color = btn.GetComponent<UnityEngine.UI.Image>().color;
                    color.a = 0.3f;
                    btn.GetComponent<UnityEngine.UI.Image>().color = color;
                }
                break;
            case "FirstAidKit":
                if (GV.randomNumbers.IndexOf(20) != -1)
                {
                    GV.FirstAidKit = 1;
                    bagSound.Play();

                    color = btn.GetComponent<UnityEngine.UI.Image>().color;
                    color.a = 0.3f;
                    btn.GetComponent<UnityEngine.UI.Image>().color = color;
                }
                break;
            case "Tissue":
                if (GV.randomNumbers.IndexOf(19) != -1)
                {
                    GV.Tissue = 1;
                    bagSound.Play();

                    color = btn.GetComponent<UnityEngine.UI.Image>().color;
                    color.a = 0.3f;
                    btn.GetComponent<UnityEngine.UI.Image>().color = color;
                }
                break;
            case "Phone":
                if (GV.randomNumbers.IndexOf(16) != -1)
                {
                    GV.Phone = 1;
                    bagSound.Play();

                    color = btn.GetComponent<UnityEngine.UI.Image>().color;
                    color.a = 0.3f;
                    btn.GetComponent<UnityEngine.UI.Image>().color = color;
                }
                break;
            case "Wallet":
                if (GV.randomNumbers.IndexOf(15) != -1)
                {
                    GV.Wallet = 1;
                    bagSound.Play();

                    color = btn.GetComponent<UnityEngine.UI.Image>().color;
                    color.a = 0.3f;
                    btn.GetComponent<UnityEngine.UI.Image>().color = color;
                }
                break;
        }
    }
}
