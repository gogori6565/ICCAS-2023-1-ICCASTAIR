using UnityEngine;
using UnityEngine.UI;

public class HoverEvent : MonoBehaviour
{
    public GameObject textObject; // ��� Text�� ���� ������Ʈ�� Inspector���� �������־�� �մϴ�.
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
        textObject.SetActive(true); // ���콺 hover �� Text ������Ʈ�� Ȱ��ȭ
        imageObject.SetActive(true);
        UpdateTextPosition(); // Text ��ġ ������Ʈ

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
        textObject.SetActive(false); // ���콺�� ������Ʈ�� ��� �� Text ������Ʈ�� ��Ȱ��ȭ
        imageObject.SetActive(false);
    }

    void Update()
    {
        if (isHovering)
        {
            UpdateTextPosition(); // Text ��ġ ������Ʈ
        }
    }

    // Text ��ġ ������Ʈ �Լ�
    private void UpdateTextPosition()
    {
        Vector3 objectPosition = transform.position;

        float yOffset = 1.0f; // Text�� ������Ʈ ������ Y�� ����
        Vector3 textPosition = objectPosition + Vector3.up * yOffset;

        textObject.transform.position = textPosition;
        imageObject.transform.position = textPosition;
    }

}

