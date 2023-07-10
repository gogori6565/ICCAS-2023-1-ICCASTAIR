using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetActive : MonoBehaviour
{
    public GameObject objectToHide; // ������� �ϴ� ������Ʈ�� ����Ű�� ����

    private Button button; // ��ư ������Ʈ�� �����ϱ� ���� ����

    private void Start()
    {
        // ��ư ������Ʈ�� ������
        button = GetComponent<Button>();

        // ��ư�� Ŭ�� �̺�Ʈ �����ʸ� �߰�
        button.onClick.AddListener(HideObject);
    }

    private void HideObject()
    {
        // ������Ʈ�� ��Ȱ��ȭ�Ͽ� ����
        objectToHide.SetActive(false);

        switch (this.gameObject.name)
        {
            case "Carkey":

                break;
            case "Housekey":
                break;
        }
    }
}
