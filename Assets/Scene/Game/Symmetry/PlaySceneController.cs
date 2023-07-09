using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySceneController : MonoBehaviour
{
    GameObject Room1;
    GameObject Room2;
    GameObject[] room1AllStuff;
    GameObject[] room2AllStuff;
    int[] selectStuffIndex; // ���õ� ��� ���ǵ� index
    int[] notSelectStuffIndex; // ���õ��� ���� ���ǵ� index
    int[] selectSymmetryStuff; // ��Ī ���� index
    int[] selectNotSymmetryStuff; // ���Ī ���� index
    ArrayList stuffList;
    int diff = 2; // ���õ� ���̵�

    // Start is called before the first frame update
    void Start()
    {
        Room1 = GameObject.Find("Canvas").transform.Find("Room").gameObject;
        Room2 = GameObject.Find("Canvas").transform.Find("Room2").gameObject;

        // ShowSceneController�� �繰���� ��������
        selectStuffIndex = ShowSceneController.selectStuffIndex; 
        notSelectStuffIndex = ShowSceneController.notSelectStuffIndex;
        selectSymmetryStuff = ShowSceneController.selectSymmetryStuff;
        selectNotSymmetryStuff = ShowSceneController.selectNotSymmetryStuff;
        stuffList = ShowSceneController.stuffList;
        diff = ShowSceneController.diff;

        startSetting(diff, diff * 5);
        gameSetting(diff, diff * 5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startSetting(int difficulty, int size) // size : �� ���� ����  
    {
        room1AllStuff = new GameObject[20];
        for (int i = 0; i < 20; i++) // Room1�� ��� ��ü ������Ʈ �ҷ�����
        {
            room1AllStuff[i] = Room1.transform.GetChild(i).gameObject;
        }

        for (int i = 0; i < notSelectStuffIndex.Length; i++) // Room1�� ���õ� ������Ʈ�� ��ġ
        {
            room1AllStuff[notSelectStuffIndex[i]].SetActive(false);
        }


        room2AllStuff = new GameObject[20];
        for (int i = 0; i < 20; i++) // Room2�� ��� ��ü ������Ʈ �ҷ�����
        {
            room2AllStuff[i] = Room2.transform.GetChild(i).gameObject;
        }

        for (int i = 0; i < notSelectStuffIndex.Length; i++) // Room2�� ���õ� ������Ʈ�� ��ġ
        {
            room2AllStuff[notSelectStuffIndex[i]].SetActive(false);
        }
    }

    public void gameSetting(int difficulty, int size) // (��,��,�� = 1,2,3)
    {
        foreach (ShowSceneController.stuffState i in stuffList)
        {
            if (i.changed == 1)
            {
                if (i.state == 0)
                {
                    room2AllStuff[i.index].transform.Translate(new Vector3(i.degree, 0, 0));
                }
                else if (i.state == 1)
                {
                    room2AllStuff[i.index].transform.Translate(new Vector3(0, i.degree, 0));
                }
                else if (i.state == 2)
                {
                    room2AllStuff[i.index].transform.Rotate(0, 0, i.degree);
                }
            }
        }
    }
}
