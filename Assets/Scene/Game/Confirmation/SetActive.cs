using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetActive : MonoBehaviour
{
    public GameObject objectToHide; // 숨기고자 하는 오브젝트를 가리키는 변수

    private Button button; // 버튼 컴포넌트를 참조하기 위한 변수

    private void Start()
    {
        // 버튼 컴포넌트를 가져옴
        button = GetComponent<Button>();

        // 버튼에 클릭 이벤트 리스너를 추가
        button.onClick.AddListener(HideObject);
    }

    private void HideObject()
    {
        // 오브젝트를 비활성화하여 숨김
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
