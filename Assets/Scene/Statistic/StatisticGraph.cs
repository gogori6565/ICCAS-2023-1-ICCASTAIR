using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatisticGraph : MonoBehaviour
{
    public GameObject dot;
    private int[] score; //= { 1000, 5000, 3000, 5000, 2000, 7000, 10000 };
    private int[] score2; //= { 2000, 3000, 5000, 7000, 6000, 8000, 9000 };
    private int[] score3; //= { 500, 3500, 4000, 5500, 3000, 7000, 7500 };
    private float[] score4 = new float[3]; //= { 5500, 3000, 7000 };
    private float fieldX, fieldY; 
    private const float x_spacing = 0.8f; // 점간의 x간격

    float cA, pA, sA;

    public GameObject lineObj;
    private LineRenderer line;
    List<Vector2> points = new List<Vector2>();

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

        setFieldPosition(-800, 0);
        setDot(dot, score, Color.blue);
        DrawLine(score, Color.blue);

        setFieldPosition(100, 0);
        setDot(dot, score2, Color.green);
        DrawLine(score2, Color.green);

        setFieldPosition(-800, -400);
        setDot(dot, score3, Color.black);
        DrawLine(score3, Color.black);

        setFieldPosition(200, -400);
        fsetDot(dot, score4, Color.yellow);
        fDrawLine(score4, Color.yellow);
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

    public void setDot(GameObject startDot, int[] score, Color dotColor)
    {
        Vector3 v = new Vector3(fieldX, fieldY);
        for(int i = 0; i < score.Length; i++)
        {
            v = new Vector3(v.x + x_spacing, fieldY + (float)score[i]/3300);
            points.Add(new Vector3(v.x + x_spacing - lineObj.transform.position.x - 0.8f, fieldY + (float)score[i] / 3300));
            GameObject newDot = Instantiate(dot);
            newDot.GetComponent<Renderer>().material.color = dotColor;
            newDot.transform.position = v;
        }
    }

    public void DrawLine(int[] score, Color lineColor)
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
    public void fsetDot(GameObject startDot, float[] score, Color dotColor)
    {
        Vector3 v = new Vector3(fieldX, fieldY);
        for(int i = 0; i < score.Length; i++)
        {
            v = new Vector3(v.x + x_spacing, fieldY + (float)score[i]/3300);
            points.Add(new Vector3(v.x + x_spacing - lineObj.transform.position.x - 0.8f, fieldY + (float)score[i] / 3300));
            GameObject newDot = Instantiate(dot);
            newDot.GetComponent<Renderer>().material.color = dotColor;
            newDot.transform.position = v;
        }
    }

    public void fDrawLine(float[] score, Color lineColor)
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

    
    void Update()
    {
        
    }
}
