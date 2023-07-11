using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

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
    int diff; // 선택된 난이도
    public static gameData myGameData;

    // Start is called before the first frame update
    void Start()
    {
        myGameData = new gameData();
        myGameData.setEmpty();

        Room1 = GameObject.Find("Canvas").transform.Find("Room").gameObject;
        Room2 = GameObject.Find("Canvas").transform.Find("Room2").gameObject;

        // ShowSceneController의 사물정보 가져오기
        selectStuffIndex = ShowSceneController.selectStuffIndex; 
        notSelectStuffIndex = ShowSceneController.notSelectStuffIndex;
        selectSymmetryStuff = ShowSceneController.selectSymmetryStuff;
        selectNotSymmetryStuff = ShowSceneController.selectNotSymmetryStuff;
        stuffList = ShowSceneController.stuffList;
        diff = ShowSceneController.diff;
        myGameData.diff = diff;

        startSetting(diff, diff * 5);
        gameSetting(diff, diff * 5);
    }

    int touchSymmetryCount = 0;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left click
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition); // 좌표 가져오기
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0f); // 해당 좌표 오브젝트 찾기

            if(hit.collider != null)
            {
                GameObject click_obj = hit.transform.gameObject;
                foreach (ShowSceneController.stuffState i in stuffList)
                {
                    if(room2AllStuff[i.index] == click_obj)
                    {
                        if(i.changed == 0) // 건드린 물건이 대칭일때
                        {
                            if(i.touch == 0) // 처음 건드리는 물건이면
                            {
                                if (i.state == 0)
                                {
                                    click_obj.transform.Translate(new Vector3(i.degree, 0, 0));
                                }
                                else if (i.state == 1)
                                {
                                    click_obj.transform.Translate(new Vector3(0, i.degree, 0));
                                }
                                else if (i.state == 2)
                                {
                                    click_obj.transform.Rotate(0, 0, i.degree);
                                }
                                myGameData.symmetryTouchCount++;
                                i.touch = 1;
                                //Debug.Log("대칭물건 : " + click_obj.name + " 건드림");
                                if (myGameData.symmetryTouchCount == getSymmetryCount(diff)) // 대칭물건을 모두 비대칭으로 만들면
                                {
                                    myGameData.timeRemain = (int)PlaySceneTimer.setTime;
                                    SceneManager.LoadScene("SymmetryResult");
                                }
                            }
                            else if(i.touch == 1) // 비대칭으로 만든 물건을 다시 건드리면
                            {
                                myGameData.symmetryMoreTouchCount++;
                                //Debug.Log("비대칭으로 만든물건 : " + click_obj.name + " 건드림");
                            }
                        }else if(i.changed == 1) // 건드린 물건이 비대칭일때
                        {
                            myGameData.asymmetryTouchCount++;
                            //Debug.Log("비대칭물건 : " + click_obj.name + " 건드림");
                        }
                    }
                }
            }
        }
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

    public int getSymmetryCount(int diff)
    {
        if(diff == 1)
        {
            return 3;
        }
        else if(diff == 2)
        {
            return 5;
        }
        else
        {
            return 8;
        }
    }

    public class gameData
    {
        public int diff; // 난이도
        public int symmetryTouchCount; // 대칭물건 터치횟수
        public int symmetryMoreTouchCount; // 비대칭으로 만든 물건 터치횟수
        public int asymmetryTouchCount; // 비대칭물건 터치횟수
        public int timeRemain; // 남은 시간(초)

        public void setEmpty()
        {
            diff = 0;
            symmetryTouchCount = 0;
            symmetryMoreTouchCount = 0;
            asymmetryTouchCount = 0;
            timeRemain = 0;
        }
    }
}
