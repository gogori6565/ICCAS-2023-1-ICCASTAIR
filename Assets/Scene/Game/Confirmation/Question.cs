using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//using static System.Net.Mime.MediaTypeNames;

public class Question : MonoBehaviour
{
    int suc, fail; // 문제번호, 정답, 오답 - Firebase DB
    public Button btn; // Yes/No
    public Text Qtext;

    void Start()
    {
        Qtext = GameObject.Find("Qtext").GetComponent<UnityEngine.UI.Text>();

        if (GV.Qnumber == 1)
        {
            Qtext.text = "Q" + GV.Qnumber + ". " + GV.questions[GV.QuestionNum[GV.Qnumber - 1]];
            GV.Qnumber++;
        }
    }

    // Yes/No button clicked
    public void NextQuestion()
    {
        if (GV.Qnumber <= 5)
        {
            Qtext.text = "Q" + GV.Qnumber + ". " + GV.questions[GV.QuestionNum[GV.Qnumber - 1]];
            GV.Qnumber++;
        }
        else
        {
            SceneManager.LoadScene("ResultPage_Confirmation");
        }
    }
}
