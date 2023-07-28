using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SurveyScore : MonoBehaviour
{
    // 30개의 "Question" 게임 오브젝트를 저장할 배열
    private GameObject[] questions = new GameObject[31];

    public static int Surveyscore;

    private void Start()
    {
        // "Question" 게임 오브젝트들을 코드로 찾아와서 배열에 할당합니다.
        for (int i = 1; i <= 30; i++)
        {
            questions[i] = GameObject.Find("Question" + i);
        }

        // 버튼을 찾아서 onclick 리스너를 등록합니다.
        Button submitButton = GameObject.Find("SurveySummit_Button").GetComponent<Button>();
        submitButton.onClick.AddListener(OnSubmitButtonClicked);

        UnityEngine.Debug.Log(submitButton);
    }

    // Submit 버튼을 클릭할 때 호출되는 함수
    private void OnSubmitButtonClicked()
    {
        int yesCount = 0; // Yes 토글이 선택된 개수를 저장할 변수

        for (int i = 1; i <= 30; i++)
        {
            // "Question" 오브젝트가 null인지 확인
            if (questions[i] == null)
            {
                UnityEngine.Debug.LogError("Question 오브젝트를 찾을 수 없습니다.");
                continue;
            }

            // "Toggle_Group" 오브젝트를 찾습니다.
            Transform toggleGroup = questions[i].transform.Find("Toggle_Group");

            // "Yes"와 "No" 토글을 찾아서 값을 가져옵니다.
            Toggle[] toggles = toggleGroup.GetComponentsInChildren<Toggle>();
            bool isYesSelected = toggles[0].isOn;
            bool isNoSelected = toggles[1].isOn;

            // 가져온 값을 이용하여 원하는 동작을 수행합니다.

            if (isYesSelected)
            {
                yesCount++;
            }
        }

        // Yes 토글이 선택된 개수 출력
        UnityEngine.Debug.Log("토글 개수 yes : "+ yesCount);

        Surveyscore = yesCount + 30;

        UnityEngine.Debug.Log(Surveyscore);
    }
}
