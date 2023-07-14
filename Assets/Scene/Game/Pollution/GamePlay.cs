using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlay : MonoBehaviour
{
    public GameObject dirtyObject; // Dirty 오브젝트를 Inspector에서 할당합니다.
    public int numElementsToShow = 13; // 화면에 보여질 요소의 개수를 설정합니다.

    private List<Transform> elements = new List<Transform>(); // Dirty 오브젝트의 자식 요소들을 저장할 리스트

    private int clickCount = 0; // 클릭된 요소의 카운트 변수
  
    private bool isClickable = true; // 클릭 가능 여부를 나타내는 변수
    public static GamePlay Instance; // 인스턴스 참조를 위한 정적 변수



    private void Start()
    {
        Instance = this;
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

            // Collider 컴포넌트 추가
            Collider collider = element.gameObject.AddComponent<BoxCollider>();
            collider.isTrigger = true; // 클릭 이벤트를 감지하기 위해 트리거로 설정

            // ElementClickHandler 컴포넌트 추가 및 참조 설정
            ElementClickHandler clickHandler = element.gameObject.AddComponent<ElementClickHandler>();
            clickHandler.gamePlay = this;
        }
    }

    private void ShowRandomElements()
    {
        int numElements = elements.Count;
        ShuffleList(elements);

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
            return; // 클릭 불가능한 상태일 때는 클릭 이벤트를 무시하고 종료
        }

        clickCount++; // 클릭 카운트 증가
        Debug.Log("Click Count: " + clickCount); // 클릭 카운트를 콘솔창에 표시

        clickedElement.gameObject.SetActive(false); // 클릭된 요소를 비활성화 처리

        CursorChanger cursorChanger = FindObjectOfType<CursorChanger>();
        if (cursorChanger != null)
        {
            
            cursorChanger.MarkCursorChange();

            cursorChanger.CursorIndexCount(clickCount);
        }
    }


    public void SetClickable(bool clickable)
    {
        isClickable = clickable; // 클릭 가능 여부 설정
    }

    public int GetClickCount()
    {
        return clickCount; // 클릭 카운트 반환
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

