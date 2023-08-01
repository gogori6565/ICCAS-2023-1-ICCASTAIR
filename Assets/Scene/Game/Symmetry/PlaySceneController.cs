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
    private AudioSource clickSound, failClickSound, successClickSound;

    GameObject BackgroundMusic;
    AudioSource backmusic;

    // Start is called before the first frame update
    void Start()
    {
        setAudioSetting();

        BackgroundMusic = GameObject.Find("sMusic");
        backmusic = BackgroundMusic.GetComponent<AudioSource>();

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

    bool isNotTouchd = true;
    int touchSymmetryCount = 0;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left click
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition); // 좌표 가져오기
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0f); // 해당 좌표 오브젝트 찾기
            isNotTouchd = true;

            if (hit.collider != null)
            {
                GameObject click_obj = hit.transform.gameObject;
                foreach (ShowSceneController.stuffState i in stuffList)
                {
                    isNotTouchd = false;
                    if (room2AllStuff[i.index] == click_obj)
                    {
                        if(i.changed == 0) // 건드린 물건이 대칭일때
                        {
                            if(i.touch % 2 == 0) // 대칭 물건이면
                            {
                                successClickSound.Play();
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
                                if(i.touch == 0)
                                {
                                    Debug.Log("대칭물건 건드림 (+)");
                                    myGameData.symmetryTouchCount++;
                                }
                                else
                                {
                                    Debug.Log("대칭물건 건드림 (x)");
                                }
                                i.touch++;
                                if (myGameData.symmetryTouchCount == getSymmetryCount(diff) && isSymmetryTouch()) // 대칭물건을 모두 비대칭으로 만들면
                                {
                                    myGameData.timeRemain = (int)PlaySceneTimer.setTime;
                                    SceneManager.LoadScene("Result_Symmetry");
                                }
                            }
                            else if(i.touch % 2 == 1) // 비대칭으로 만든 물건을 다시 건드리면
                            {
                                failClickSound.Play();
                                Debug.Log("비대칭물건 건드림 (-)");
                                if (i.state == 0)
                                {
                                    click_obj.transform.Translate(new Vector3(-i.degree, 0, 0));
                                }
                                else if (i.state == 1)
                                {
                                    click_obj.transform.Translate(new Vector3(0, -i.degree, 0));
                                }
                                else if (i.state == 2)
                                {
                                    click_obj.transform.Rotate(0, 0, -i.degree);
                                }
                                myGameData.symmetryMoreTouchCount++;
                                i.touch++;
                            }
                        }
                        else if(i.changed == 1) // 건드린 물건이 비대칭일때
                        {
                            if(i.touch % 2 == 0)
                            {
                                failClickSound.Play();
                                Debug.Log("비대칭물건 건드림 (--)");
                                if (i.state == 0)
                                {
                                    click_obj.transform.Translate(new Vector3(-i.degree, 0, 0));
                                }
                                else if (i.state == 1)
                                {
                                    click_obj.transform.Translate(new Vector3(0, -i.degree, 0));
                                }
                                else if (i.state == 2)
                                {
                                    click_obj.transform.Rotate(0, 0, -i.degree);
                                }
                                i.touch++;
                                myGameData.asymmetryTouchCount++;
                            }
                            else if (i.touch % 2 == 1)
                            {
                                successClickSound.Play();
                                Debug.Log("대칭물건 건드림 (x)");
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
                                i.touch++;
                                if (myGameData.symmetryTouchCount == getSymmetryCount(diff) && isSymmetryTouch()) // 대칭물건을 모두 비대칭으로 만들면
                                {
                                    myGameData.timeRemain = (int)PlaySceneTimer.setTime;
                                    SceneManager.LoadScene("Result_Symmetry");
                                }
                            }
                        }
                    }
                }
            }
            if (isNotTouchd) // 물건을 터치하지 않았을때
            {
                clickSound.Play();
            }
        }
    }

    public bool isSymmetryTouch() // 대칭물건이 있으면 false 없으면 true
    {
        bool b = true;
        foreach (ShowSceneController.stuffState i in stuffList)
        {
            if(i.changed == 0)
            {
                if(i.touch % 2 != 1)
                {
                    b = false;
                }
            }
            else if (i.changed == 1)
            {
                if (i.touch % 2 != 0)
                {
                    b = false;
                }
            }
        }
        return b;
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

    public void OnDestroy()
    {
        backmusic.Stop();
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

    // 사운드 소스 불러오기
    public void setAudioSetting()
    {
        GameObject obj = GameObject.Find("ClickAudio");
        clickSound = obj.GetComponent<AudioSource>();
        obj = GameObject.Find("FailClickAudio");
        failClickSound = obj.GetComponent<AudioSource>();
        obj = GameObject.Find("SuccessClickAudio");
        successClickSound = obj.GetComponent<AudioSource>();
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
