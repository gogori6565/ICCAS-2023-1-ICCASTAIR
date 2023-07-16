using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Result : MonoBehaviour
{
    public Text Score, LeftLV, RightLV, UsedHints, WrongAnswer;
    public GameObject emptyGage;
    public GameObject fillGage;

    public GameObject cf;

    private string leftlv, rightlv;

    void Start()
    {
        Score = GameObject.Find("Score").GetComponent<UnityEngine.UI.Text>();
        LeftLV = GameObject.Find("LeftLV").GetComponent<UnityEngine.UI.Text>();
        RightLV = GameObject.Find("RightLV").GetComponent<UnityEngine.UI.Text>();
        UsedHints = GameObject.Find("UsedHints").GetComponent<UnityEngine.UI.Text>();
        WrongAnswer = GameObject.Find("WrongAnswer").GetComponent<UnityEngine.UI.Text>();

        GV.score = 0; //����

        CalculateScore();
        PrintText();

        setGageBar();

        CalculateLevel(); //���̵� ��� - ������

        //���� play Ƚ�� �������� -> (+1) Ƚ�� ���� -> playdata ����
        cf.GetComponent<ConfirmationFirebase>().PlayCntReadDB(); 

        //������ Ȯ�� ���� ���� ���̵� ����
        cf.GetComponent<ConfirmationFirebase>().DiffWriteDB(); 
        
    }

    private void PrintText()
    {
        Score.text = GV.score.ToString();
        LeftLV.text = leftlv;
        RightLV.text = rightlv;
        UsedHints.text = "Used Hints : " + GV.Hintcnt + " Times";
        WrongAnswer.text = "Wrong Answer : " + GV.fail + " Times";
    }

    //���� ��� & ���� text
    private void CalculateScore()
    {
        if (GV.diff == 1) //��
        {
            leftlv = "Lv1"; rightlv = "Lv2";
            GV.score = 3500 - ((GV.Hintcnt * 250) + (GV.fail * 700));
        }
        else if (GV.diff == 2) //��
        {
            leftlv = "Lv2"; rightlv = "Lv3";
            GV.score = 6500 - ((GV.Hintcnt * 500) + (GV.fail * 1300));
        }
        else if (GV.diff == 3) //��
        {
            leftlv = "Lv3"; rightlv = "Max";
            GV.score = 10000 - ((GV.Hintcnt * 750) + (GV.fail * 2000));
        }

        //GV.score �� �������
        if(GV.score < 0)
        {
            GV.score = 0;
        }
    }

    //���̵� ���
    private void CalculateLevel()
    {
        if (GV.score >= 3000)
        {
            GV.diff = 2;
        }
        else if(GV.score >= 6000)
        {
            GV.diff = 3;
        }
    }

    private bool gageSetting = false;
    private float percent = 0f;
    float width, y;

    // ������ ����
    public void setGageBar()
    {
        width = emptyGage.GetComponent<RectTransform>().rect.width; // ����ִ� ���������� ����
        y = emptyGage.GetComponent<RectTransform>().anchoredPosition.y; // ����ִ� ���������� y��

        percent = 0f;
        if (GV.diff == 1) // ���̵� �� �϶�
        {
            percent = GV.score / 3000.0f;
        }
        else if (GV.diff == 2) // ���̵� �� �϶�
        {
            percent = GV.score / 6000.0f;
        }
        else if (GV.diff == 3) // ���̵� �� �϶�
        {
            percent = GV.score / 10000.0f;
        }

        if (percent > 1f)
        {
            percent = 1f;
        }

        gageSetting = true;
    }

    float temp = 0f;
    // Update is called once per frame
    void Update()
    {
        // ������ �� �ִϸ��̼� ȿ��
        if (gageSetting) // �� ������ �Ϸ�Ǿ��ٸ�
        {
            if (temp >= percent) // ������ �ۼ�Ʈ������ ũ�ų� ������ �ִϸ��̼� ����
            {
                gageSetting = false;
            }
            fillGage.GetComponent<RectTransform>().anchoredPosition = new Vector3(-(width / 2) + width * temp / 2, y, 0); // ���� ���������� ���� ����
            fillGage.transform.localScale = new Vector3(temp, 1, 0); // ���� ���������� ��ġ ����
            temp += 0.005f;
        }
    }
}
