using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySceneController : MonoBehaviour
{
    GameObject Room1;
    GameObject Room2;
    GameObject[] room1AllStuff;
    GameObject[] room2AllStuff;
    int[] selectStuffIndex; // 선택된 모든 물건들 index
    int[] notSelectStuffIndex; // 선택되지 않은 물건들 index
    int[] selectSymmetryStuff; // 대칭 물건 index
    int[] selectNotSymmetryStuff; // 비대칭 물건 index
    ArrayList stuffList;
    int diff = 2; // 선택된 난이도

    // Start is called before the first frame update
    void Start()
    {
        Room1 = GameObject.Find("Canvas").transform.Find("Room").gameObject;
        Room2 = GameObject.Find("Canvas").transform.Find("Room2").gameObject;

        // ShowSceneController의 사물정보 가져오기
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

    public void startSetting(int difficulty, int size) // size : 총 물건 개수  
    {
        room1AllStuff = new GameObject[20];
        for (int i = 0; i < 20; i++) // Room1의 모든 물체 오브젝트 불러오기
        {
            room1AllStuff[i] = Room1.transform.GetChild(i).gameObject;
        }

        for (int i = 0; i < notSelectStuffIndex.Length; i++) // Room1의 선택된 오브젝트만 배치
        {
            room1AllStuff[notSelectStuffIndex[i]].SetActive(false);
        }


        room2AllStuff = new GameObject[20];
        for (int i = 0; i < 20; i++) // Room2의 모든 물체 오브젝트 불러오기
        {
            room2AllStuff[i] = Room2.transform.GetChild(i).gameObject;
        }

        for (int i = 0; i < notSelectStuffIndex.Length; i++) // Room2의 선택된 오브젝트만 배치
        {
            room2AllStuff[notSelectStuffIndex[i]].SetActive(false);
        }
    }

    public void gameSetting(int difficulty, int size) // (하,중,상 = 1,2,3)
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
