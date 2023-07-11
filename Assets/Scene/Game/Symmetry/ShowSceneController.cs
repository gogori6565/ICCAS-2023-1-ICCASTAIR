using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowSceneController : MonoBehaviour
{
    GameObject Room;
    public static int[] selectStuffIndex; // 선택된 모든 물건들 index
    public static int[] notSelectStuffIndex; // 선택되지 않은 물건들 index
    public static int[] selectSymmetryStuff; // 대칭 물건 index
    public static int[] selectNotSymmetryStuff; // 비대칭 물건 index
    public static ArrayList stuffList;
    public static int diff = 2; // 선택된 난이도
    // Start is called before the first frame update
    void Start()
    {
        Room = GameObject.Find("Canvas").transform.Find("Room").gameObject;
        stuffList = new ArrayList();
        selectStuff(diff, diff * 5);
        stuffStateChange(diff, diff * 5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void selectStuff(int difficulty, int size) // difficulty (하,중,상 = 1,2,3) , size (총 물건개수)
    {
        selectStuffIndex = new int[size + 4];

        int flag = 1;
        int random;

        for (int i = 0; i < size; i++) // size개의 물건을 랜덤으로 선택한다.
        {
            while (true)
            {
                flag = 1;
                random = Random.Range(0, 16);
                for (int j = 0; j < size; j++)
                {
                    if (random == selectStuffIndex[j])
                    {
                        flag = 0;
                        break;
                    }
                }
                if (flag == 1)
                    break;
            }
            selectStuffIndex[i] = random;
        }

        for (int i = 0; i < 4; i++) // 고정된 물건 4가지를 선택한다.  < 위치각도Default : 책상, 책장, 벽 선반, 벽 선반 (1) >
        {
            selectStuffIndex[i + size] = 16 + i;
        }

        int notSelectStuffSize = 20 - size - 4;
        notSelectStuffIndex = new int[notSelectStuffSize];
        int count = 0;
        for (int i = 0; i < 16; i++) // 선택되지 않은 물건 고르기
        {
            flag = 1;
            for (int j = 0; j < size; j++)
            {
                if (selectStuffIndex[j] == i)
                {
                    flag = 0;
                }
            }
            if (flag == 1)
            {
                notSelectStuffIndex[count] = i;
                count++;
            }
        }

        GameObject[] allStuff = new GameObject[20];
        for (int i = 0; i < 20; i++) // Room의 모든 물체 오브젝트 불러오기
        {
            allStuff[i] = Room.transform.GetChild(i).gameObject;
        }

        for (int i = 0; i < notSelectStuffSize; i++) // Room의 선택된 오브젝트만 배치
        {
            allStuff[notSelectStuffIndex[i]].SetActive(false);
        }
    }


    public void stuffStateChange(int difficulty, int size)
    {
        GameObject[] Stuffs = new GameObject[size];

        for (int i = 0; i < Stuffs.Length; i++) // startSetting() 에서 설정된 오브젝트들 가져오기
        {
            Stuffs[i] = Room.transform.GetChild(selectStuffIndex[i]).gameObject;
        }

        int notSymmetrySize = 0; // 비대칭인 물건 개수

        if (difficulty == 1)
        {
            notSymmetrySize = 2;
        }
        else if (difficulty == 2)
        {
            notSymmetrySize = 5;
        }
        else if (difficulty == 3)
        {
            notSymmetrySize = 7;
        }

        selectNotSymmetryStuff = new int[notSymmetrySize]; // 비대칭 물건 index 정해주기
        int random;
        int count = 0;
        int flag = 1;
        while (true)
        {
            flag = 1;
            if (count == notSymmetrySize) break;

            random = Random.Range(0, size);
            for (int i = 0; i < selectNotSymmetryStuff.Length; i++)
            {
                if (selectNotSymmetryStuff[i] == random)
                    flag = 0;
            }
            if (flag == 1)
            {
                selectNotSymmetryStuff[count] = random;
                count++;
            }
        }

        int moveChange = 0;
        int angleChange = 0;
        if (difficulty == 1) // 난이도에 따른 위치변경 물건 개수와 각도변경 물건 개수 정해주기
        {
            moveChange = 2;
            angleChange = 0;
        }
        else if (difficulty == 2)
        {
            moveChange = 3;
            angleChange = 2;
        }
        else if (difficulty == 3)
        {
            moveChange = 3;
            angleChange = 4;
        }

        float value = 0;
        int index = -1;
        for (int i = 0; i < moveChange; i++) // 위치변경    0.3 or 0.4 or 0.5
        {
            for (int j = 0; j < 20; j++)
            {
                if (Stuffs[selectNotSymmetryStuff[i]] == Room.transform.GetChild(j).gameObject)
                {
                    index = j;
                }
            }
            random = Random.Range(0, 2);
            if (random == 0) // x축 위치 변경
            {
                random = Random.Range(0, 2);
                if (random == 0) // + 변경
                {
                    random = Random.Range(3, 6);
                    value = (float)random / 10;
                    stuffList.Add(new stuffState(index, 1, 0, value));
                    Stuffs[selectNotSymmetryStuff[i]].transform.Translate(new Vector3(value, 0, 0));
                }
                else if (random == 1) // - 변경
                {
                    random = Random.Range(-3, -6);
                    value = (float)random / 10;
                    stuffList.Add(new stuffState(index, 1, 0, value));
                    Stuffs[selectNotSymmetryStuff[i]].transform.Translate(new Vector3(value, 0, 0));
                }
            }
            else if (random == 1) // y축 위치 변경
            {
                random = Random.Range(0, 2);
                if (random == 0) // + 변경
                {
                    random = Random.Range(3, 6);
                    value = (float)random / 10;
                    stuffList.Add(new stuffState(index, 1, 1, value));
                    Stuffs[selectNotSymmetryStuff[i]].transform.Translate(new Vector3(0, value, 0));
                }
                else if (random == 1) // - 변경
                {
                    random = Random.Range(-3, -6);
                    value = (float)random / 10;
                    stuffList.Add(new stuffState(index, 1, 1, value));
                    Stuffs[selectNotSymmetryStuff[i]].transform.Translate(new Vector3(0, value, 0));
                }
            }
        }

        for (int i = 0; i < angleChange; i++) // 각도변경
        {
            for (int j = 0; j < 20; j++)
            {
                if (Stuffs[selectNotSymmetryStuff[i + moveChange]] == Room.transform.GetChild(j).gameObject)
                {
                    index = j;
                }
            }
            random = Random.Range(0, 2);
            if (random == 0) // + 변경
            {
                stuffList.Add(new stuffState(index, 1, 2, 20));
                Stuffs[selectNotSymmetryStuff[i + moveChange]].transform.Rotate(0, 0, 20);
            }
            else if (random == 1) // - 변경
            {
                stuffList.Add(new stuffState(index, 1, 2, -20));
                Stuffs[selectNotSymmetryStuff[i + moveChange]].transform.Rotate(0, 0, -20);
            }
        }

        selectSymmetryStuff = new int[size - notSymmetrySize]; // 대칭 물건 index 찾기
        count = 0;
        for (int i = 0; i < size; i++)
        {
            flag = 1;
            for (int j = 0; j < selectNotSymmetryStuff.Length; j++)
            {
                if (selectNotSymmetryStuff[j] == i)
                {
                    flag = 0;
                }
            }
            if (flag == 1)
            {
                selectSymmetryStuff[count++] = i;
            }
        }

        for (int i = 0; i < selectSymmetryStuff.Length; i++) // 대칭 물건들 비대칭으로 만들기
        {
            for (int j = 0; j < 20; j++)
            {
                if (Stuffs[selectSymmetryStuff[i]] == Room.transform.GetChild(j).gameObject)
                {
                    index = j;
                }
            }

            random = Random.Range(0, 3);
            if (random == 0) // x축 위치 변경
            {
                random = Random.Range(0, 2);
                if (random == 0) // + 변경
                {
                    random = Random.Range(3, 6);
                    value = (float)random / 10;
                    stuffList.Add(new stuffState(index, 0, 0, value)); 
                    Stuffs[selectSymmetryStuff[i]].transform.Translate(new Vector3(value, 0, 0));
                }
                else if (random == 1) // - 변경
                {
                    random = Random.Range(-3, -6);
                    value = (float)random / 10;
                    stuffList.Add(new stuffState(index, 0, 0, value));
                    Stuffs[selectSymmetryStuff[i]].transform.Translate(new Vector3(value, 0, 0));
                }
            }
            else if (random == 1) // y축 위치 변경
            {
                random = Random.Range(0, 2);
                if (random == 0) // + 변경
                {
                    random = Random.Range(3, 6);
                    value = (float)random / 10;
                    stuffList.Add(new stuffState(index, 0, 1, value));
                    Stuffs[selectSymmetryStuff[i]].transform.Translate(new Vector3(0, value, 0));
                }
                else if (random == 1) // - 변경
                {
                    random = Random.Range(-3, -6);
                    value = (float)random / 10;
                    stuffList.Add(new stuffState(index, 0, 1, value));
                    Stuffs[selectSymmetryStuff[i]].transform.Translate(new Vector3(0, value, 0));
                }
            }
            else if(random == 2)
            {
                random = Random.Range(0, 2);
                if (random == 0) // + 변경
                {
                    stuffList.Add(new stuffState(index, 0, 2, 20));
                    Stuffs[selectSymmetryStuff[i]].transform.Rotate(0, 0, 20);
                }
                else if (random == 1) // - 변경
                {
                    stuffList.Add(new stuffState(index, 0, 2, -20));
                    Stuffs[selectSymmetryStuff[i]].transform.Rotate(0, 0, -20);
                }
            }
        }
    }
    public class stuffState // 선택된 물건들의 상태정보를 담는 자료구조
    {
        public int index;
        public int changed = 0; // (변경x, 변경 = 0, 1)
        public int state = 0; // (위치변경x, 위치변경y , 각도변경 = 0, 1, 2)
        public float degree = 0; // 바뀐정도
        public int touch = 0;

        public stuffState(int index, int changed, int state, float degree)
        {
            this.index = index;
            this.changed = changed;
            this.state = state;
            this.degree = degree;
        }
    }
}
