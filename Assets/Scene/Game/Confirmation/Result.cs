using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Result : MonoBehaviour
{
    public Text Score, LeftLV, RightLV, UsedHints, WrongAnswer, ChangeScore;
    public GameObject emptyGage;
    public GameObject fillGage;

    public GameObject cf;

    private string leftlv, rightlv;
    private int hintdiff, faildiff, scorediff; //이전과 차이 (힌트, 오답, 점수)

    private AudioSource levelupSound;

    void Start()
    {
        setAudioSetting();

        Score = GameObject.Find("Score").GetComponent<UnityEngine.UI.Text>();
        LeftLV = GameObject.Find("LeftLV").GetComponent<UnityEngine.UI.Text>();
        RightLV = GameObject.Find("RightLV").GetComponent<UnityEngine.UI.Text>();
        UsedHints = GameObject.Find("UsedHints").GetComponent<UnityEngine.UI.Text>();
        WrongAnswer = GameObject.Find("WrongAnswer").GetComponent<UnityEngine.UI.Text>();

        GV.score = 0; //점수

        CalculateScore();

        //유저 play 횟수 가져오기 -> (+1) 횟수 저장 -> playdata 저장
        cf.GetComponent<ConfirmationFirebase>().PlayCntReadDB();

        PrintText();

        setGageBar();

        CalculateLevel(); //난이도 계산 - 마지막

        //유저의 확인 강박 게임 난이도 갱신
        cf.GetComponent<ConfirmationFirebase>().DiffWriteDB(); 

    }

    private void PrintText()
    {
        hintdiff = GV.Hintcnt - GV.PreUsedHint;
        faildiff = GV.fail - GV.PreWrongAnswer;
        scorediff = GV.score - GV.PreScore;

        LeftLV.text = leftlv;
        RightLV.text = rightlv;

        UnityEngine.Debug.Log("GV.PreUsedHint" + GV.PreUsedHint);
        UnityEngine.Debug.Log("GV.PreWrongAnswer" + GV.PreWrongAnswer);
        UnityEngine.Debug.Log("GV.PreScore" + GV.PreScore);


        if(GV.PreUsedHint == 0 && GV.PreWrongAnswer == 0 && GV.PreScore == 0) //이전 데이터가 없다면
        {
            Score.text = GV.score.ToString() + " (-)";
            UsedHints.text = "Used Hints : " + GV.Hintcnt + " Times (-)";
            WrongAnswer.text = "Wrong Answer : " + GV.fail + " Times (-)";
        }
        else
        {
            if(scorediff > 0)
            {
                Score.text = GV.score.ToString() + " (+" + scorediff + ")";
            }
            else if(scorediff < 0)
            {
                Score.text = GV.score.ToString() + " (" + scorediff + ")";
            }
            else
            {
                Score.text = GV.score.ToString() + " (-)";
            }

            if (hintdiff > 0) //양수이면
            {
                UsedHints.text = "Used Hints : " + GV.Hintcnt + " Times (+" + hintdiff + ")";
            }
            else if (hintdiff < 0) //음수이면
            {
                UsedHints.text = "Used Hints : " + GV.Hintcnt + " Times (" + hintdiff + ")";
            }
            else
            {
                UsedHints.text = "Used Hints : " + GV.Hintcnt + " Times (-)";
            }

            if (faildiff > 0)
            {
                WrongAnswer.text = "Wrong Answer : " + GV.fail + " Times (+" + faildiff + ")";
            }
            else if (faildiff < 0)
            {
                WrongAnswer.text = "Wrong Answer : " + GV.fail + " Times (" + faildiff + ")";
            }
            else
            {
                WrongAnswer.text = "Wrong Answer : " + GV.fail + " Times (-)";
            }
        }
    }

    //점수 계산 & 레벨 text
    private void CalculateScore()
    {
        if (GV.diff == 1) //하
        {
            leftlv = "Lv1"; rightlv = "Lv2";
            GV.score = 3500 - ((GV.Hintcnt * 250) + (GV.fail * 700));
        }
        else if (GV.diff == 2) //중
        {
            leftlv = "Lv2"; rightlv = "Lv3";
            GV.score = 6500 - ((GV.Hintcnt * 500) + (GV.fail * 1300));
        }
        else if (GV.diff == 3) //상
        {
            leftlv = "Lv3"; rightlv = "Max";
            GV.score = 10000 - ((GV.Hintcnt * 750) + (GV.fail * 2000));
        }

        //GV.score 가 음수라면
        if(GV.score < 0)
        {
            GV.score = 0;
        }
    }

    //난이도 계산
    private void CalculateLevel()
    {
        if (GV.score >= 6000)
        {
            GV.diff = 3;
        }
        else if(GV.score >= 3000)
        {
            GV.diff = 2;
        }
        else
        {
            GV.diff = 1;
        }
    }

    private bool gageSetting = false;
    private float percent = 0f;
    float width, y;

    // 점수바 설정
    public void setGageBar()
    {
        width = emptyGage.GetComponent<RectTransform>().rect.width; // 비어있는 게이지바의 길이
        y = emptyGage.GetComponent<RectTransform>().anchoredPosition.y; // 비어있는 게이지바의 y값

        percent = 0f;
        if (GV.diff == 1) // 난이도 하 일때
        {
            percent = GV.score / 3000.0f;
        }
        else if (GV.diff == 2) // 난이도 중 일때
        {
            percent = GV.score / 6000.0f;
        }
        else if (GV.diff == 3) // 난이도 상 일때
        {
            percent = GV.score / 10000.0f;
        }

        if (percent > 1f)
        {
            percent = 1f;
        }

        gageSetting = true;
    }

    float temp = 0f;
    // Update is called once per frame
    void Update()
    {
        // 게이지 바 애니메이션 효과
        if (gageSetting) // 바 설정이 완료되었다면
        {
            if (temp >= percent) // 설정된 퍼센트값보다 크거나 같으면 애니메이션 종료
            {
                gageSetting = false;
            }
            fillGage.GetComponent<RectTransform>().anchoredPosition = new Vector3(-(width / 2) + width * temp / 2, y, 0); // 색깔 게이지바의 길이 설정
            fillGage.transform.localScale = new Vector3(temp, 1, 0); // 색깔 게이지바의 위치 설정
            temp += 0.005f;
            if (temp >= 1f)
            {
                levelupSound.Play();
            }
        }
    }

    // 사운드 소스 불러오기
    public void setAudioSetting()
    {
        GameObject obj = GameObject.Find("LevelupSound");
        levelupSound = obj.GetComponent<AudioSource>();
    }
}
