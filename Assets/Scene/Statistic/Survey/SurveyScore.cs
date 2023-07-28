using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SurveyScore : MonoBehaviour
{
    // 30���� "Question" ���� ������Ʈ�� ������ �迭
    private GameObject[] questions = new GameObject[31];

    public static int Surveyscore;

    private void Start()
    {
        // "Question" ���� ������Ʈ���� �ڵ�� ã�ƿͼ� �迭�� �Ҵ��մϴ�.
        for (int i = 1; i <= 30; i++)
        {
            questions[i] = GameObject.Find("Question" + i);
        }

        // ��ư�� ã�Ƽ� onclick �����ʸ� ����մϴ�.
        Button submitButton = GameObject.Find("SurveySummit_Button").GetComponent<Button>();
        submitButton.onClick.AddListener(OnSubmitButtonClicked);

        UnityEngine.Debug.Log(submitButton);
    }

    // Submit ��ư�� Ŭ���� �� ȣ��Ǵ� �Լ�
    private void OnSubmitButtonClicked()
    {
        int yesCount = 0; // Yes ����� ���õ� ������ ������ ����

        for (int i = 1; i <= 30; i++)
        {
            // "Question" ������Ʈ�� null���� Ȯ��
            if (questions[i] == null)
            {
                UnityEngine.Debug.LogError("Question ������Ʈ�� ã�� �� �����ϴ�.");
                continue;
            }

            // "Toggle_Group" ������Ʈ�� ã���ϴ�.
            Transform toggleGroup = questions[i].transform.Find("Toggle_Group");

            // "Yes"�� "No" ����� ã�Ƽ� ���� �����ɴϴ�.
            Toggle[] toggles = toggleGroup.GetComponentsInChildren<Toggle>();
            bool isYesSelected = toggles[0].isOn;
            bool isNoSelected = toggles[1].isOn;

            // ������ ���� �̿��Ͽ� ���ϴ� ������ �����մϴ�.

            if (isYesSelected)
            {
                yesCount++;
            }
        }

        // Yes ����� ���õ� ���� ���
        UnityEngine.Debug.Log("��� ���� yes : "+ yesCount);

        Surveyscore = yesCount + 30;

        UnityEngine.Debug.Log(Surveyscore);
    }
}
