using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Question : MonoBehaviour
{
    string answer, btnText; //Á¤´ä
    public Button btn; // Yes/No
    public Text Qtext;

    void Start()
    {
        Qtext = GameObject.Find("Qtext").GetComponent<UnityEngine.UI.Text>();

        if (GV.Qnumber == 1)
        {
            Qtext.text = "Q" + GV.Qnumber + ". " + GV.questions[GV.QuestionNum[GV.Qnumber - 1]];
        }
    }

    // Yes/No button clicked
    public void NextQuestion()
    {
        Answer();

        UnityEngine.Debug.Log(answer);

        btnText = btn.GetComponentInChildren<UnityEngine.UI.Text>().text;

        if (string.Equals(answer, btnText))
        {
            GV.suc++;
        }
        else
        {
            GV.fail++;
        }

        UnityEngine.Debug.Log("suc: "+ GV.suc);
        UnityEngine.Debug.Log("fail: "+ GV.fail);

        GV.Qnumber++;
        if (GV.Qnumber <= 5)
        {
            Qtext.text = "Q" + GV.Qnumber + ". " + GV.questions[GV.QuestionNum[GV.Qnumber - 1]];
        }
        else
        {
            SceneManager.LoadScene("ResultPage_Confirmation");
        }
    }

    public void Answer()
    {
        UnityEngine.Debug.Log(GV.QuestionNum[GV.Qnumber - 1]);
        UnityEngine.Debug.Log(GV.Light_LivingRoom);
        UnityEngine.Debug.Log(GV.Light_Kitchen);
        UnityEngine.Debug.Log(GV.Light_Room);

        switch (GV.QuestionNum[GV.Qnumber - 1])
        {
            case 0:
                if (GV.Light_LivingRoom == 1)
                {
                    answer = "Yes";
                }
                else
                {
                    answer = "No";
                }
                break;
            case 1:
                if (GV.Light_Kitchen == 1)
                {
                    answer = "Yes";
                }
                else
                {
                    answer = "No";
                }
                break;
            case 2:
                if (GV.Light_Room == 1)
                {
                    answer = "Yes";
                }
                else
                {
                    answer = "No";
                }
                break;
            case 3:
                if (GV.tv == 1)
                {
                    answer = "Yes";
                }
                else
                {
                    answer = "No";
                }
                break;
            case 4:
                if (GV.Fan_Room == 1)
                {
                    answer = "Yes";
                }
                else
                {
                    answer = "No";
                }
                break;
            case 5:
                if (GV.Fan_LivingRoom == 1)
                {
                    answer = "Yes";
                }
                else
                {
                    answer = "No";
                }
                break;
            case 6:
                if (GV.PowerStrip_Room == 1)
                {
                    answer = "Yes";
                }
                else
                {
                    answer = "No";
                }
                break;
            case 7:
                if (GV.PowerStrip_LivingRoom == 1)
                {
                    answer = "Yes";
                }
                else
                {
                    answer = "No";
                }
                break;
            case 8:
                if (GV.GasRange == 1)
                {
                    answer = "Yes";
                }
                else
                {
                    answer = "No";
                }
                break;
            case 9:
                if (GV.GasValve == 1)
                {
                    answer = "Yes";
                }
                else
                {
                    answer = "No";
                }
                break;
            case 10:
                if (GV.faucet == 1)
                {
                    answer = "Yes";
                }
                else
                {
                    answer = "No";
                }
                break;
            case 11:
                if (GV.computer == 1)
                {
                    answer = "Yes";
                }
                else
                {
                    answer = "No";
                }
                break;
            case 12:
                if (GV.Window_LivingRoom == 1)
                {
                    answer = "Yes";
                }
                else
                {
                    answer = "No";
                }
                break;
            case 13:
                if (GV.Window_Kitchen == 1)
                {
                    answer = "Yes";
                }
                else
                {
                    answer = "No";
                }
                break;
            case 14:
                if (GV.Window_Room == 1)
                {
                    answer = "Yes";
                }
                else
                {
                    answer = "No";
                }
                break;
            case 15:
                if (GV.Wallet == 1)
                {
                    answer = "Yes";
                }
                else
                {
                    answer = "No";
                }
                break;
            case 16:
                if (GV.Phone == 1)
                {
                    answer = "Yes";
                }
                else
                {
                    answer = "No";
                }
                break;
            case 17:
                if (GV.Carkey == 1)
                {
                    answer = "Yes";
                }
                else
                {
                    answer = "No";
                }
                break;
            case 18:
                if (GV.Housekey == 1)
                {
                    answer = "Yes";
                }
                else
                {
                    answer = "No";
                }
                break;
            case 19:
                if (GV.Tissue == 1)
                {
                    answer = "Yes";
                }
                else
                {
                    answer = "No";
                }
                break;
            case 20:
                if (GV.FirstAidKit == 1)
                {
                    answer = "Yes";
                }
                else
                {
                    answer = "No";
                }
                break;
        }
    }
}
