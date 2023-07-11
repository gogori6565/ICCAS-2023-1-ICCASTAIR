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
    int[] selectStuffIndex; // ���õ� ��� ���ǵ� index
    int[] notSelectStuffIndex; // ���õ��� ���� ���ǵ� index
    int[] selectSymmetryStuff; // ��Ī ���� index
    int[] selectNotSymmetryStuff; // ���Ī ���� index
    ArrayList stuffList;
    int diff; // ���õ� ���̵�
    public static gameData myGameData;

    // Start is called before the first frame update
    void Start()
    {
        myGameData = new gameData();
        myGameData.setEmpty();

        Room1 = GameObject.Find("Canvas").transform.Find("Room").gameObject;
        Room2 = GameObject.Find("Canvas").transform.Find("Room2").gameObject;

        // ShowSceneController�� �繰���� ��������
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
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition); // ��ǥ ��������
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0f); // �ش� ��ǥ ������Ʈ ã��

            if(hit.collider != null)
            {
                GameObject click_obj = hit.transform.gameObject;
                foreach (ShowSceneController.stuffState i in stuffList)
                {
                    if(room2AllStuff[i.index] == click_obj)
                    {
                        if(i.changed == 0) // �ǵ帰 ������ ��Ī�϶�
                        {
                            if(i.touch == 0) // ó�� �ǵ帮�� �����̸�
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
                                //Debug.Log("��Ī���� : " + click_obj.name + " �ǵ帲");
                                if (myGameData.symmetryTouchCount == getSymmetryCount(diff)) // ��Ī������ ��� ���Ī���� �����
                                {
                                    myGameData.timeRemain = (int)PlaySceneTimer.setTime;
                                    SceneManager.LoadScene("SymmetryResult");
                                }
                            }
                            else if(i.touch == 1) // ���Ī���� ���� ������ �ٽ� �ǵ帮��
                            {
                                myGameData.symmetryMoreTouchCount++;
                                //Debug.Log("���Ī���� ���繰�� : " + click_obj.name + " �ǵ帲");
                            }
                        }else if(i.changed == 1) // �ǵ帰 ������ ���Ī�϶�
                        {
                            myGameData.asymmetryTouchCount++;
                            //Debug.Log("���Ī���� : " + click_obj.name + " �ǵ帲");
                        }
                    }
                }
            }
        }
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
        public int diff; // ���̵�
        public int symmetryTouchCount; // ��Ī���� ��ġȽ��
        public int symmetryMoreTouchCount; // ���Ī���� ���� ���� ��ġȽ��
        public int asymmetryTouchCount; // ���Ī���� ��ġȽ��
        public int timeRemain; // ���� �ð�(��)

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
