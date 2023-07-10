using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class GV : MonoBehaviour
{
    //on&off objects - value : 0(unclicked) or 1(clicked)
    public static int Light_LivingRoom, tv, PowerStrip_LivingRoom, Fan_LivingRoom, Window_LivingRoom,
                        Light_Kitchen, GasRange, GasValve, faucet, Window_Kitchen,
                        Light_Room, computer, PowerStrip_Room, Fan_Room, Window_Room;

    //bag Objects
    public static int Carkey, HouseKey, FirstAidKit, Tissue, Phone, Wallet;

    //To Do List Sentences Array
    public static string[] ListSentences;

    static GV()
    {
        InitializeSentences();
    }

    // Start is called before the first frame update
    void Start()
    {
        Light_LivingRoom = 0; tv = 0; PowerStrip_LivingRoom = 0; Fan_LivingRoom = 0; Window_LivingRoom = 0;
        Light_Kitchen = 0; GasRange = 0; GasValve=0; faucet = 0; Window_Kitchen = 0;
        Light_Room = 0; computer = 0; PowerStrip_Room = 0; Fan_Room = 0; Window_Room = 0;

        Carkey = 0; HouseKey = 0; FirstAidKit = 0; Tissue = 0; Phone = 0; Wallet = 0;
    }

    //To Do List Sentences Array reset
    static void InitializeSentences()
    {
        ListSentences = new string[]
        {
            "Turn off living room lights",
            "Turn off kitchen lights",
            "Turn off room lights",
            "Turn off TV",
            "Turn off fan in the room",
            "Turn off fan in the living room",
            "Turn off power strip in the room",
            "Turn off power strip in the living room",
            "Turn off gas stove",
            "Turn off gas valve",
            "Turn off faucet",
            "Turn off computer",
            "Close living room window",
            "Close kitchen window",
            "Close bedroom window",
            "Take the wallet",
            "Take the phone",
            "Take the car keys",
            "Take the house keys",
            "Take tissues",
            "Take emergency medicine"
        };
    }

    public static List<int> randomNumbers = new List<int>(); // 중복되지 않는 랜덤한 숫자를 저장할 전역 변수 리스트
    public static int ListNum = 5; //난이도 별로 상이 (하-5, 중-7, 상-10)

    public void GameStart()
    {
        // 랜덤한 숫자를 저장할 리스트
        List<int> numbers = new List<int>();

        for (int i = 0; i <= 20; i++)
        {
            numbers.Add(i);
        }

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
    }
}
