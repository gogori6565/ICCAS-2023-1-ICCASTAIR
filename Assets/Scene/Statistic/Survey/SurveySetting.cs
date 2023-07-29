using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SurveySetting : MonoBehaviour
{
    public GameObject contentObj;
    public GameObject[] allQuestion = new GameObject[30];
    public GameObject[] allQText = new GameObject[30];
    private string[] Qstr = { "I avoid using public telephones because of possible contamination. ",
    "I frequently get nasty thoughts and have difficulty in getting rid of them.",
    "1 am more concerned than most people about honesty. ",
    "1 am often late because I can¡¯t seem to get through everything on time. ",
    "I don¡¯t worry unduly about contamination if I touch an animal ",
    "1 frequently have to check things (e.g. gas or water taps, doors, etc.) several times. ",
    "1 have a very strict conscience.",
    "I find that almost every day I am upset by unpleasant thoughts that come into my mind against my will.",
    "1 do not worry unduly if I accidently bump into somebody ",
    "I usually have serious doubts about the simple everyday things I do ",
    "Neither of my parents was very strict during my childhood ",
    "I tend to get behind in my work because I repeat things over and over again. ",
    "I use only an average amount of soap. ",
    "Some numbers are extremely unlucky. ",
    "I do not check letters over and over again before posting them. ",
    "I do not take a long time to dress in a morning. ",
    "I am not excessively concerned about cleanliness. ",
    "One of my major problems is that I pay too much attention to detail. ",
    "I can use well-kept toilets without any hesitation. ",
    "My major problem is repeated checking. ",
    "I am not unduly concerned about germs and diseases. ",
    "I do not tend to check things more than once. ",
    "I do not stick to a very strict routine when doing ordinary things. ",
    "My hands do not feel dirty after touching money. ",
    "1 do not usually count when doing a routine task. ",
   "I take rather a long time to complete my washing in the morning. ",
    "I do not use a great deal of antiseptics. ",
    "I spend a lot of time every day checking things over and over again. ",
    "Hanging and folding my clothes at night does not. take up a lot of time. ",
    "Even when I do something very carefully I often feel that it is not quite right. "};
    
    void Start()
    {
        contentObj = GameObject.Find("Canvas").transform.Find("Scroll View").transform.Find("Viewport").transform.Find("Content").gameObject;
        for(int i=0; i<allQuestion.Length; i++)
        {
            allQuestion[i] = contentObj.transform.GetChild(i).gameObject;
            allQText[i] = allQuestion[i].transform.GetChild(0).gameObject;
        }

        for(int i=0; i<allQText.Length; i++)
        {
            allQText[i].GetComponent<Text>().text = "Q." + (i + 1) + "   " + Qstr[i];
        }
    }
}
