using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatisticGraph : MonoBehaviour
{
    public GameObject dot;
    private int[] score; //= { 1000, 5000, 3000, 5000, 2000, 7000, 10000 };
    private int[] score2; //= { 2000, 3000, 5000, 7000, 6000, 8000, 9000 };
    private int[] score3; //= { 500, 3500, 4000, 5500, 3000, 7000, 7500 };
    private float[] score4 = new float[3]; //= { 5500, 3000, 7000 };
    private float fieldX, fieldY; 
    private float x_spacing = 0.8f; // 점간의 x간격

    float cA, pA, sA;

    public GameObject lineObj;
    private LineRenderer line;
    List<Vector2> points = new List<Vector2>();

    public static GameObject[] newDotArray = new GameObject[24];
    public static string[] dotStr = new string[24];
    private int count = 0;

    public Text cText, pText, sText;

    public void Graph()
    {
        Array.Reverse(StatisticFirebase.cUsedHint);
        Array.Reverse(StatisticFirebase.cWrongAnswer);
        Array.Reverse(StatisticFirebase.pFoundDirtyThings);
        Array.Reverse(StatisticFirebase.pNumberOfWashings);
        Array.Reverse(StatisticFirebase.pRemainingTime);
        Array.Reverse(StatisticFirebase.sAsymmetryTouch);
        Array.Reverse(StatisticFirebase.sRemainTime);
        Array.Reverse(StatisticFirebase.sSymmetryTouch);

        score = StatisticFirebase.cScore;
        score2 = StatisticFirebase.pScore;
        score3 = StatisticFirebase.sScore;
        score4[0] = StatisticFirebase.cAverage;
        score4[1] = StatisticFirebase.pAverage;
        score4[2] = StatisticFirebase.sAverage;

        Array.Reverse(score);
        Array.Reverse(score2);
        Array.Reverse(score3);

        setLine();

        setFieldPosition(-763, 30);
        setDot(dot, score, new Color32(57, 31, 87,255));// Color.blue);
        DrawLine(score, new Color32(57, 31, 87,255));// Color.blue);

        setFieldPosition(96, 30);
        setDot(dot, score2, new Color32(5, 72, 113, 255));// Color.green);
        DrawLine(score2, new Color32(5, 72, 113, 255));// Color.green);

        setFieldPosition(-763, -360);
        setDot(dot, score3, new Color32(128, 60, 35, 255));// Color.black);
        DrawLine(score3, new Color32(128, 60, 35, 255));// Color.black);

        setFieldPosition(40, -360);
        fsetDot(dot, score4, new Color32(23, 67, 11, 255));// Color.yellow);
        fDrawLine(score4, new Color32(23, 67, 11, 255));// Color.yellow);

        setPlayText();
    }

    public void setFieldPosition(float x, float y)
    {
        fieldX = x/100;
        fieldY = y/100;
    }

    public void setLine()
    {
        line = lineObj.GetComponent<LineRenderer>();
        line.positionCount = 2;
    }

    public void setDot(GameObject startDot, int[] score, Color32 dotColor)
    {
        Vector3 v = new Vector3(fieldX, fieldY);
        for(int i = 0; i < score.Length; i++)
        {
            v = new Vector3(v.x + x_spacing, fieldY + (float)score[i]/4000);
            points.Add(new Vector3(v.x + x_spacing - lineObj.transform.position.x - 0.8f, fieldY + (float)score[i] / 4000));
            newDotArray[count] = Instantiate(dot);
            newDotArray[count].GetComponent<Renderer>().material.color = dotColor;
            newDotArray[count].transform.position = v;
            if (count < 7)
            {
                dotStr[count] = score[i] + "\nUsedHint : " + StatisticFirebase.cUsedHint[i] +
                    "\nWrongAnswer : " + StatisticFirebase.cWrongAnswer[i];
            }
            else if (count < 14)
            {
                dotStr[count] = score[i] + "\nFoundDirtyThings : " + StatisticFirebase.pFoundDirtyThings[i] +
                    "\nNumberOfWashings : " + StatisticFirebase.pNumberOfWashings[i] + "\nRemainingTime : " +
                    StatisticFirebase.pRemainingTime[i];
            }
            else if(count < 21)
            {
                dotStr[count] = score[i] + "\nAsymmetryTouch : " + StatisticFirebase.sAsymmetryTouch[i] +
                    "\nRemainTime : " + StatisticFirebase.sRemainTime[i] + "\nSymmetryTouch : " + StatisticFirebase.sSymmetryTouch[i];
            }
            
            count++;
        }
    }

    public void DrawLine(int[] score, Color32 lineColor)
    {
        for (int i=0; i<score.Length-1; i++)
        {
            LineRenderer newLine = Instantiate(line);
            newLine.SetColors(lineColor, lineColor);
            newLine.SetPosition(0, points[i]);
            newLine.SetPosition(1, points[i+1]);
        }
        points.Clear();
    }

    //float 배열
    public void fsetDot(GameObject startDot, float[] score, Color32 dotColor)
    {
        x_spacing = 2f;
        Vector3 v = new Vector3(fieldX, fieldY);
        for(int i = 0; i < score.Length; i++)
        {
            v = new Vector3(v.x + x_spacing, fieldY + (float)score[i]/4000);
            points.Add(new Vector3(v.x - lineObj.transform.position.x, fieldY + (float)score[i] / 4000));
            newDotArray[count] = Instantiate(dot);
            newDotArray[count].GetComponent<Renderer>().material.color = dotColor;
            newDotArray[count].transform.position = v;
            if(i == 0)
            {
                dotStr[count] = "Checking Average\n" + score[i];
            }
            else if(i == 1)
            {
                dotStr[count] = "Contamination Average\n" + score[i];
            }
            else if(i == 2)
            {
                dotStr[count] = "Symmetry Average\n" + score[i];
            }

            count++;
        }
    }

    public void fDrawLine(float[] score, Color32 lineColor)
    {
        for (int i=0; i<score.Length-1; i++)
        {
            LineRenderer newLine = Instantiate(line);
            newLine.SetColors(lineColor, lineColor);
            newLine.SetPosition(0, points[i]);
            newLine.SetPosition(1, points[i+1]);
        }
        points.Clear();
    }

    public void setPlayText()
    {
        int cPlay = LoginController.myPlayData.ConfirmationPlay;
        int pPlay = LoginController.myPlayData.PollutionPlay;
        int sPlay = LoginController.myPlayData.SymmetryPlay;

        string tempStr = "";

        for(int i = cPlay-6; i<=cPlay; i++)
        {
            if(i <= 0)
            {
                tempStr += "-            ";
            }
            else
            {
                if(i > 9)
                {
                    tempStr += i + "          ";
                }
                else
                {
                    tempStr += i + "            ";
                }
            }
        }
        cText.text = tempStr;
        tempStr = "";

        for (int i = pPlay - 6; i <= pPlay; i++)
        {
            if (i <= 0)
            {
                tempStr += "-            ";
            }
            else
            {
                if (i > 9)
                {
                    tempStr += i + "          ";
                }
                else
                {
                    tempStr += i + "            ";
                }
            }
        }
        pText.text = tempStr;
        tempStr = "";

        for (int i = sPlay - 6; i <= sPlay; i++)
        {
            if (i <= 0)
            {
                tempStr += "-            ";
            }
            else
            {
                if (i > 9)
                {
                    tempStr += i + "          ";
                }
                else
                {
                    tempStr += i + "            ";
                }
            }
        }
        sText.text = tempStr;
        tempStr = "";
    }

    void Update()
    {
        
    }
}
