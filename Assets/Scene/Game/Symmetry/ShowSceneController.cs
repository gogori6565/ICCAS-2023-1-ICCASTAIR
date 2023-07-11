using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowSceneController : MonoBehaviour
{
    GameObject Room;
    public static int[] selectStuffIndex; // ���õ� ��� ���ǵ� index
    public static int[] notSelectStuffIndex; // ���õ��� ���� ���ǵ� index
    public static int[] selectSymmetryStuff; // ��Ī ���� index
    public static int[] selectNotSymmetryStuff; // ���Ī ���� index
    public static ArrayList stuffList;
    public static int diff = 2; // ���õ� ���̵�
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

    public void selectStuff(int difficulty, int size) // difficulty (��,��,�� = 1,2,3) , size (�� ���ǰ���)
    {
        selectStuffIndex = new int[size + 4];

        int flag = 1;
        int random;

        for (int i = 0; i < size; i++) // size���� ������ �������� �����Ѵ�.
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

        for (int i = 0; i < 4; i++) // ������ ���� 4������ �����Ѵ�.  < ��ġ����Default : å��, å��, �� ����, �� ���� (1) >
        {
            selectStuffIndex[i + size] = 16 + i;
        }

        int notSelectStuffSize = 20 - size - 4;
        notSelectStuffIndex = new int[notSelectStuffSize];
        int count = 0;
        for (int i = 0; i < 16; i++) // ���õ��� ���� ���� ����
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
        for (int i = 0; i < 20; i++) // Room�� ��� ��ü ������Ʈ �ҷ�����
        {
            allStuff[i] = Room.transform.GetChild(i).gameObject;
        }

        for (int i = 0; i < notSelectStuffSize; i++) // Room�� ���õ� ������Ʈ�� ��ġ
        {
            allStuff[notSelectStuffIndex[i]].SetActive(false);
        }
    }


    public void stuffStateChange(int difficulty, int size)
    {
        GameObject[] Stuffs = new GameObject[size];

        for (int i = 0; i < Stuffs.Length; i++) // startSetting() ���� ������ ������Ʈ�� ��������
        {
            Stuffs[i] = Room.transform.GetChild(selectStuffIndex[i]).gameObject;
        }

        int notSymmetrySize = 0; // ���Ī�� ���� ����

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

        selectNotSymmetryStuff = new int[notSymmetrySize]; // ���Ī ���� index �����ֱ�
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
        if (difficulty == 1) // ���̵��� ���� ��ġ���� ���� ������ �������� ���� ���� �����ֱ�
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
        for (int i = 0; i < moveChange; i++) // ��ġ����    0.3 or 0.4 or 0.5
        {
            for (int j = 0; j < 20; j++)
            {
                if (Stuffs[selectNotSymmetryStuff[i]] == Room.transform.GetChild(j).gameObject)
                {
                    index = j;
                }
            }
            random = Random.Range(0, 2);
            if (random == 0) // x�� ��ġ ����
            {
                random = Random.Range(0, 2);
                if (random == 0) // + ����
                {
                    random = Random.Range(3, 6);
                    value = (float)random / 10;
                    stuffList.Add(new stuffState(index, 1, 0, value));
                    Stuffs[selectNotSymmetryStuff[i]].transform.Translate(new Vector3(value, 0, 0));
                }
                else if (random == 1) // - ����
                {
                    random = Random.Range(-3, -6);
                    value = (float)random / 10;
                    stuffList.Add(new stuffState(index, 1, 0, value));
                    Stuffs[selectNotSymmetryStuff[i]].transform.Translate(new Vector3(value, 0, 0));
                }
            }
            else if (random == 1) // y�� ��ġ ����
            {
                random = Random.Range(0, 2);
                if (random == 0) // + ����
                {
                    random = Random.Range(3, 6);
                    value = (float)random / 10;
                    stuffList.Add(new stuffState(index, 1, 1, value));
                    Stuffs[selectNotSymmetryStuff[i]].transform.Translate(new Vector3(0, value, 0));
                }
                else if (random == 1) // - ����
                {
                    random = Random.Range(-3, -6);
                    value = (float)random / 10;
                    stuffList.Add(new stuffState(index, 1, 1, value));
                    Stuffs[selectNotSymmetryStuff[i]].transform.Translate(new Vector3(0, value, 0));
                }
            }
        }

        for (int i = 0; i < angleChange; i++) // ��������
        {
            for (int j = 0; j < 20; j++)
            {
                if (Stuffs[selectNotSymmetryStuff[i + moveChange]] == Room.transform.GetChild(j).gameObject)
                {
                    index = j;
                }
            }
            random = Random.Range(0, 2);
            if (random == 0) // + ����
            {
                stuffList.Add(new stuffState(index, 1, 2, 20));
                Stuffs[selectNotSymmetryStuff[i + moveChange]].transform.Rotate(0, 0, 20);
            }
            else if (random == 1) // - ����
            {
                stuffList.Add(new stuffState(index, 1, 2, -20));
                Stuffs[selectNotSymmetryStuff[i + moveChange]].transform.Rotate(0, 0, -20);
            }
        }

        selectSymmetryStuff = new int[size - notSymmetrySize]; // ��Ī ���� index ã��
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

        for (int i = 0; i < selectSymmetryStuff.Length; i++) // ��Ī ���ǵ� ���Ī���� �����
        {
            for (int j = 0; j < 20; j++)
            {
                if (Stuffs[selectSymmetryStuff[i]] == Room.transform.GetChild(j).gameObject)
                {
                    index = j;
                }
            }

            random = Random.Range(0, 3);
            if (random == 0) // x�� ��ġ ����
            {
                random = Random.Range(0, 2);
                if (random == 0) // + ����
                {
                    random = Random.Range(3, 6);
                    value = (float)random / 10;
                    stuffList.Add(new stuffState(index, 0, 0, value)); 
                    Stuffs[selectSymmetryStuff[i]].transform.Translate(new Vector3(value, 0, 0));
                }
                else if (random == 1) // - ����
                {
                    random = Random.Range(-3, -6);
                    value = (float)random / 10;
                    stuffList.Add(new stuffState(index, 0, 0, value));
                    Stuffs[selectSymmetryStuff[i]].transform.Translate(new Vector3(value, 0, 0));
                }
            }
            else if (random == 1) // y�� ��ġ ����
            {
                random = Random.Range(0, 2);
                if (random == 0) // + ����
                {
                    random = Random.Range(3, 6);
                    value = (float)random / 10;
                    stuffList.Add(new stuffState(index, 0, 1, value));
                    Stuffs[selectSymmetryStuff[i]].transform.Translate(new Vector3(0, value, 0));
                }
                else if (random == 1) // - ����
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
                if (random == 0) // + ����
                {
                    stuffList.Add(new stuffState(index, 0, 2, 20));
                    Stuffs[selectSymmetryStuff[i]].transform.Rotate(0, 0, 20);
                }
                else if (random == 1) // - ����
                {
                    stuffList.Add(new stuffState(index, 0, 2, -20));
                    Stuffs[selectSymmetryStuff[i]].transform.Rotate(0, 0, -20);
                }
            }
        }
    }
    public class stuffState // ���õ� ���ǵ��� ���������� ��� �ڷᱸ��
    {
        public int index;
        public int changed = 0; // (����x, ���� = 0, 1)
        public int state = 0; // (��ġ����x, ��ġ����y , �������� = 0, 1, 2)
        public float degree = 0; // �ٲ�����
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
