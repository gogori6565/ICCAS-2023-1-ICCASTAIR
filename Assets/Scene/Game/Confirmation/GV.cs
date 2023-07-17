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
    public static int Qnumber, suc, fail; //suc(맞음 개수), fail(틀림 개수) - Firebase DB
    public static int Hintcnt; //힌트 사용 개수 - Firebase DB
    public static int score; //점수
    public static int diff; //난이도
    public static int PreScore, PreUsedHint, PreWrongAnswer; //이전 판 점수, 힌트 사용 개수, 틀린 개수

    public GameObject cf; //ConfirmationFirebase 객체

    public static float startTime; //sliding puzzle 게임 시작 시간
    public static float elapsedTime; //경과 시간

    public static float HintstartTime; //힌트 시작 시간
    public static float HintelapsedTime; //힌트 경과 시간
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

        cf.GetComponent<ConfirmationFirebase>().DiffReadDB(); //유저의 확인 강박 게임 '난이도' 가져오기
        cf.GetComponent<ConfirmationFirebase>().DeductionReadDB(LoginController.myPlayData.ConfirmationPlay); //이전 판 게임 정보 가져오기
        
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

    public static List<int> randomNumbers = new List<int>(); // ToDoList 질문 - 중복되지 않는 랜덤한 숫자 저장
    public static int ListNum; //난이도 별로 상이 (하-5, 중-7, 상-10)

    public static List<int> QuestionNum = new List<int>(); // Question - 중복되지 않는 랜덤한 숫자 저장

    public void GameStart()
    {
        if (diff == 1) //하
        {
            ListNum = 1;
        } 
        else if (diff == 2) //중
        {
            ListNum = 7;
        }
        else if (diff == 3) //상
        {
            ListNum = 10;
        }

        // 랜덤한 숫자를 저장할 리스트
        List<int> numbers = new List<int>();

        for (int i = 0; i <= 20; i++)
        {
            numbers.Add(i);
        }

        randomNumbers.Clear(); // 이전에 생성된 랜덤 숫자들을 초기화

        // 중복되지 않는 랜덤한 숫자를 선택
        for (int i = 0; i < ListNum; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, numbers.Count); // 리스트에서 랜덤한 인덱스 선택
            int randomNumber = numbers[randomIndex]; // 선택된 인덱스에 해당하는 숫자 얻음
            randomNumbers.Add(randomNumber); // 중복되지 않는 랜덤한 숫자를 전역 변수 리스트에 추가
            numbers.RemoveAt(randomIndex); // 이미 선택된 숫자는 리스트에서 제거하여 중복 선택 방지
        }

        /*
        // 결과 출력
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
        QuestionNum.Clear(); // 이전에 생성된 질문 숫자들을 초기화

        while (numberList.Count > 0 && QuestionNum.Count < 5) // 뽑을 숫자 개수를 5개로 제한
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
