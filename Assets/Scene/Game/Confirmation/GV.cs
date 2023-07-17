using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;
using System.Threading;
using System.CodeDom.Compiler;

public class GV : MonoBehaviour
{
    //on&off objects - value : 0(unclicked) or 1(clicked)
    public static int Light_LivingRoom, tv, PowerStrip_LivingRoom, Fan_LivingRoom, Window_LivingRoom,
                        Light_Kitchen, GasRange, GasValve, faucet, Window_Kitchen,
                        Light_Room, computer, PowerStrip_Room, Fan_Room, Window_Room;

    //bag Objects
    public static int Carkey, Housekey, FirstAidKit, Tissue, Phone, Wallet;

    public static string[] ListSentences;
    public static string[] questions;

    public static int outside;
    public static int Qnumber, suc, fail; //suc(���� ����), fail(Ʋ�� ����) - Firebase DB
    public static int Hintcnt; //��Ʈ ��� ���� - Firebase DB
    public static int score; //����
    public static int diff; //���̵�
    public static int PreScore, PreUsedHint, PreWrongAnswer; //���� �� ����, ��Ʈ ��� ����, Ʋ�� ����

    public GameObject cf; //ConfirmationFirebase ��ü

    public static float startTime; //sliding puzzle ���� ���� �ð�
    public static float elapsedTime; //��� �ð�

    public static float HintstartTime; //��Ʈ ���� �ð�
    public static float HintelapsedTime; //��Ʈ ��� �ð�
    public static Boolean ActiveHint;

    static GV()
    {
        InitializeList();
        InitializeQuestion();
    }

    // Start is called before the first frame update
    void Start()
    {
        Light_LivingRoom = 0; tv = 0; PowerStrip_LivingRoom = 0; Fan_LivingRoom = 0; Window_LivingRoom = 0;
        Light_Kitchen = 0; GasRange = 0; GasValve=0; faucet = 0; Window_Kitchen = 0;
        Light_Room = 0; computer = 0; PowerStrip_Room = 0; Fan_Room = 0; Window_Room = 0;

        Carkey = 0; Housekey = 0; FirstAidKit = 0; Tissue = 0; Phone = 0; Wallet = 0;

        outside = 0;
        Qnumber = 1; suc = 0; fail = 0;
        Hintcnt = 0;
        score = 0;

        ActiveHint = false;

        cf.GetComponent<ConfirmationFirebase>().DiffReadDB(); //������ Ȯ�� ���� ���� '���̵�' ��������
        cf.GetComponent<ConfirmationFirebase>().DeductionReadDB(LoginController.myPlayData.ConfirmationPlay); //���� �� ���� ���� ��������
        
        UnityEngine.Debug.Log("restart");
        UnityEngine.Debug.Log(LoginController.myID);
        UnityEngine.Debug.Log("level: " + diff);
    }

    //To Do List Sentences Array reset
    static void InitializeList()
    {
        ListSentences = new string[]
        {
            "Turn off living room light",
            "Turn off kitchen light",
            "Turn off room light",
            "Turn off TV",
            "Turn off fan in the room",
            "Turn off fan in the living room",
            "Turn off power strip in the room",
            "Turn off power strip in the living room",
            "Turn off gas range",
            "Turn off gas valve",
            "Turn off faucet",
            "Turn off computer",
            "Close living room window",
            "Close kitchen window",
            "Close room window",
            "Take the wallet",
            "Take the phone",
            "Take the car key",
            "Take the house key",
            "Take tissue",
            "Take emergency medicine"
        };
    }

    static void InitializeQuestion()
    {
        questions = new string[]
        {
            "Did you turn off the living room light before leaving?",
            "Did you turn off the kitchen light before leaving?",
            "Did you turn off the room light before leaving?",
            "Did you turn off the TV before leaving?",
            "Did you turn off the fan in the room before leaving?",
            "Did you turn off the fan in the living room before leaving?",
            "Did you unplug the socket in the room before leaving?",
            "Did you unplug the socket in the living room before leaving?",
            "Did you turn off the gas range before leaving?",
            "Did you turn off the gas valve before leaving?",
            "Did you turn off the faucet before leaving?",
            "Did you turn off the computer before leaving?",
            "Did you close the window in the living room before leaving?",
            "Did you close the window in the room before leaving?",
            "Did you close the window in the kitchen before leaving?",
            "Did you put your wallet in the bag before leaving?",
            "Did you put your phone in the bag before leaving?",
            "Did you put the car key in the bag before leaving?",
            "Did you put the house key in the bag before leaving?",
            "Did you put tissue in the bag before leaving?",
            "Did you put first aid kit in the bag before leaving?"
        };
    }

    public static List<int> randomNumbers = new List<int>(); // ToDoList ���� - �ߺ����� �ʴ� ������ ���� ����
    public static int ListNum; //���̵� ���� ���� (��-5, ��-7, ��-10)

    public static List<int> QuestionNum = new List<int>(); // Question - �ߺ����� �ʴ� ������ ���� ����

    public void GameStart()
    {
        if (diff == 1) //��
        {
            ListNum = 1;
        } 
        else if (diff == 2) //��
        {
            ListNum = 7;
        }
        else if (diff == 3) //��
        {
            ListNum = 10;
        }

        // ������ ���ڸ� ������ ����Ʈ
        List<int> numbers = new List<int>();

        for (int i = 0; i <= 20; i++)
        {
            numbers.Add(i);
        }

        randomNumbers.Clear(); // ������ ������ ���� ���ڵ��� �ʱ�ȭ

        // �ߺ����� �ʴ� ������ ���ڸ� ����
        for (int i = 0; i < ListNum; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, numbers.Count); // ����Ʈ���� ������ �ε��� ����
            int randomNumber = numbers[randomIndex]; // ���õ� �ε����� �ش��ϴ� ���� ����
            randomNumbers.Add(randomNumber); // �ߺ����� �ʴ� ������ ���ڸ� ���� ���� ����Ʈ�� �߰�
            numbers.RemoveAt(randomIndex); // �̹� ���õ� ���ڴ� ����Ʈ���� �����Ͽ� �ߺ� ���� ����
        }

        /*
        // ��� ���
        foreach (int number in GV.randomNumbers)
        {
            UnityEngine.Debug.Log(number);
        }
        */

        // Generate a list of numbers from 0 to 20
        List<int> numberList = new List<int>();
        for (int i = 0; i <= 20; i++)
        {
            numberList.Add(i);
        }

        // Shuffle the number list
        System.Random random = new System.Random();
        QuestionNum.Clear(); // ������ ������ ���� ���ڵ��� �ʱ�ȭ

        while (numberList.Count > 0 && QuestionNum.Count < 5) // ���� ���� ������ 5���� ����
        {
            int index = random.Next(numberList.Count);
            int randomNumber = numberList[index];
            numberList.RemoveAt(index);
            QuestionNum.Add(randomNumber);
        }

        /* Print the assigned numbers to the console
        for (int i = 0; i < QuestionNum.Count; i++)
        {
            UnityEngine.Debug.Log("Question " + (i + 1) + ": " + QuestionNum[i]);
        }*/
    }
}
