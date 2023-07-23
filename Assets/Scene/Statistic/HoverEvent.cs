using UnityEngine;
using UnityEngine.UI;

public class HoverEvent : MonoBehaviour
{
    public GameObject textObject; // 띄울 Text를 가진 오브젝트를 Inspector에서 연결해주어야 합니다.
    public GameObject imageObject;
    private bool isHovering = false;

    private void Start()
    {
        imageObject.SetActive(false);
        textObject.SetActive(false);
    }

    private void OnMouseEnter()
    {
        isHovering = true;
        textObject.SetActive(true); // 마우스 hover 시 Text 오브젝트를 활성화
        imageObject.SetActive(true);
        UpdateTextPosition(); // Text 위치 업데이트

        for(int i=0; i< 24; i++)
        {
            if(this.gameObject == StatisticGraph.newDotArray[i])
            {
                textObject.GetComponent<Text>().text = StatisticGraph.dotStr[i];
            }
        }
    }

    private void OnMouseExit()
    {
        isHovering = false;
        textObject.SetActive(false); // 마우스가 오브젝트를 벗어날 때 Text 오브젝트를 비활성화
        imageObject.SetActive(false);
    }

    void Update()
    {
        if (isHovering)
        {
            UpdateTextPosition(); // Text 위치 업데이트
        }
    }

    // Text 위치 업데이트 함수
    private void UpdateTextPosition()
    {
        Vector3 objectPosition = transform.position;

        float yOffset = 1.0f; // Text와 오브젝트 사이의 Y축 간격
        Vector3 textPosition = objectPosition + Vector3.up * yOffset;

        textObject.transform.position = textPosition;
        imageObject.transform.position = textPosition;
    }

}

