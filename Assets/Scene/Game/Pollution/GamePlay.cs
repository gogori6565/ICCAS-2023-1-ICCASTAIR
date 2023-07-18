using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlay : MonoBehaviour
{
    public GameObject dirtyObject; // Dirty ������Ʈ�� Inspector���� �Ҵ�
    private List<Transform> elements = new List<Transform>(); // Dirty ������Ʈ�� �ڽ� ��ҵ��� ������ ����Ʈ

    private int clickCount = 0; // Ŭ���� ����� ī��Ʈ ����

    private int numElementsToShow;

    private bool isClickable = true; // Ŭ�� ���� ���θ� ��Ÿ���� ����
    public static GamePlay Instance; // �ν��Ͻ� ������ ���� ���� ����



    private void Start()
    {
        Instance = this;

        //���ӿ��
        GetElements();
        ShowRandomElements();
    }

    private void GetElements()
    {
        int childCount = dirtyObject.transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            Transform element = dirtyObject.transform.GetChild(i);
            elements.Add(element);

            // Collider ������Ʈ �߰�
            Collider collider = element.gameObject.AddComponent<BoxCollider>();
            collider.isTrigger = true; // Ŭ�� �̺�Ʈ�� �����ϱ� ���� Ʈ���ŷ� ����

            // ElementClickHandler ������Ʈ �߰� �� ���� ����
            ElementClickHandler clickHandler = element.gameObject.AddComponent<ElementClickHandler>();
            clickHandler.gamePlay = this;
        }
    }

    private void ShowRandomElements()
    {
        int numElements = elements.Count;
        ShuffleList(elements);


        if (LoginController.myDiffData.PollutionGameDifficulty == 1)
        {
            numElementsToShow = 10; // ȭ�鿡 ������ ����� ����(��)
            ResultScene.subtractPoints = new int[] { 0, 700, 500, 300, 0 };
        }
        if (LoginController.myDiffData.PollutionGameDifficulty == 2)
        {
            numElementsToShow = 15; // ȭ�鿡 ������ ����� ����(��)

            ResultScene.subtractPoints = new int[] { 0, 850, 650, 550, 450, 350, 250, 150 };
            
        }

        if (LoginController.myDiffData.PollutionGameDifficulty == 3)
        {
            numElementsToShow = 20; // ȭ�鿡 ������ ����� ����(��)
            
            ResultScene.subtractPoints = new int[] { 0, 1000, 800, 600, 500, 400, 300, 200 }; ;
           
        }
        

        for (int i = 0; i < numElementsToShow; i++)
        {
            if (i < numElements)
            {
                elements[i].gameObject.SetActive(true);
            }
        }

        for (int i = numElementsToShow; i < numElements; i++)
        {
            elements[i].gameObject.SetActive(false);
        }
    }

    private void ShuffleList(List<Transform> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            int k = UnityEngine.Random.Range(0, n);
            n--;
            Transform temp = list[n];
            list[n] = list[k];
            list[k] = temp;
        }
    }

    public void OnMouseClick(Transform clickedElement)
    {
        if (!isClickable)
        {
            return; // Ŭ�� �Ұ����� ������ ���� Ŭ�� �̺�Ʈ�� �����ϰ� ����
        }

        clickCount++; // Ŭ�� ī��Ʈ ����
        Debug.Log("Click Count: " + clickCount); // Ŭ�� ī��Ʈ�� �ܼ�â�� ǥ��

        clickedElement.gameObject.SetActive(false); // Ŭ���� ��Ҹ� ��Ȱ��ȭ ó��

        CursorChanger cursorChanger = FindObjectOfType<CursorChanger>();
        if (cursorChanger != null)
        {
            
            cursorChanger.MarkCursorChange();
            cursorChanger.CursorIndexCount(clickCount);
        }

        ClearText clearText = FindObjectOfType<ClearText>();
        clearText.CheckClear(clickCount, numElementsToShow);

        ChangeScene6 click = FindObjectOfType<ChangeScene6>();
        click.ClickCnt(clickCount);
    }


    public void SetClickable(bool clickable)
    {
        isClickable = clickable; // Ŭ�� ���� ���� ����
    }

    public int GetClickCount()
    {
        return clickCount; // Ŭ�� ī��Ʈ ��ȯ
    }


}

public class ElementClickHandler : MonoBehaviour
{
    public GamePlay gamePlay;

    private void OnMouseDown()
    {
        Transform clickedElement = transform;
        gamePlay.OnMouseClick(clickedElement);
    }
}
