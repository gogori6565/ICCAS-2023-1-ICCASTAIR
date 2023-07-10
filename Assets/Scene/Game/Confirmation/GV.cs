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

    public static List<int> randomNumbers = new List<int>(); // �ߺ����� �ʴ� ������ ���ڸ� ������ ���� ���� ����Ʈ
    public static int ListNum = 5; //���̵� ���� ���� (��-5, ��-7, ��-10)

    public void GameStart()
    {
        // ������ ���ڸ� ������ ����Ʈ
        List<int> numbers = new List<int>();

        for (int i = 0; i <= 20; i++)
        {
            numbers.Add(i);
        }

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
    }
}
