using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SurveyScore : MonoBehaviour
{
    public string DBurl = "https://pattern-breaker-1cbe6-default-rtdb.firebaseio.com/";
    DatabaseReference reference;

    // 30���� "Question" ���� ������Ʈ�� ������ �迭
    private GameObject[] questions = new GameObject[31];

    public static int Surveyscore;

    private void Start()
    {
        FirebaseApp.DefaultInstance.Options.DatabaseUrl = new Uri(DBurl);
        reference = FirebaseDatabase.DefaultInstance.GetReference("UserData");

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

        Surveyscore = yesCount + 30;

        UnityEngine.Debug.Log(Surveyscore);

        SubmitEvent(Surveyscore);
    }

    public void SubmitEvent(int score)
    {
        UnityEngine.Debug.Log("����");
        int diff = 1;
        if (score <= 43) // ����
        {
            diff = 3;
        }
        else if (score <= 48)
        {
            diff = 2;
        }
        else if (score <= 60)
        {
            diff = 1;
        }

        string jsondata2 = JsonUtility.ToJson(diff);

        reference.Child(SignUpSceneController.myID).Child("ConfirmationGameDifficulty").SetValueAsync(diff);
        reference.Child(SignUpSceneController.myID).Child("PollutionGameDifficulty").SetValueAsync(diff);
        reference.Child(SignUpSceneController.myID).Child("SymmetryGameDifficulty").SetValueAsync(diff);

        SignUpSceneController.myID = ""; // id �ʱ�ȭ

        SceneManager.LoadScene("SurveyResult");
    }
}