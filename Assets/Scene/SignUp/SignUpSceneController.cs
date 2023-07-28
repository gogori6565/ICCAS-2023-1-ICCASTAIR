using System.Collections;
using UnityEngine;
using UnityEngine.UI;
// firebase
using Firebase; 
using Firebase.Database;
using System;
using UnityEngine.SceneManagement;

public class SignUpSceneController : MonoBehaviour
{
    public string DBurl = "https://pattern-breaker-1cbe6-default-rtdb.firebaseio.com/";
    DatabaseReference reference, reference2;

    private bool checkID = false;
    private bool checkPW = false;
    private bool checkDay = false;
    private bool checkMonth = false;
    private bool checkYear = false;
    private bool checkGender = false;

    public InputField idText;
    public InputField pwText;
    public InputField confirmPwText;
    public InputField dayText;
    public InputField monthText;
    public InputField yearText;

    public Text idErrorText;
    public Text pwErrorText;
    public Text dayErrorText;
    public Text monthErrorText;
    public Text yearErrorText;
    public Text genderErrorText;

    public static string myID = "";
    private string myGender = "";

    // Use this for initialization
    void Start()
    {
        FirebaseApp.DefaultInstance.Options.DatabaseUrl = new Uri(DBurl);
        reference = FirebaseDatabase.DefaultInstance.GetReference("UserData");
        reference2 = FirebaseDatabase.DefaultInstance.GetReference("GameData");
    }

    public static bool flag = false;
    public void EnterID() // Enter 버튼 이벤트 : id 체크
    {
        checkID = false;
        // id 길이 4 ~ 10
        if (idText.text.Length < 4)
        {
            idErrorText.color = Color.red;
            idErrorText.text = "You can only use between 4~10";
            return;
        }

        if (flag)
        {
            idErrorText.color = Color.blue;
            idErrorText.text = "This username is available.";
            checkID = true;
        }
        else
        {
            idErrorText.color = Color.red;
            idErrorText.text = "This username is already in use.";
        }
    }

    public void IdChange() // id value가 바뀔때 이벤트
    {
        checkID = false;
        idErrorText.text = "";

        // id firebase에서 중복 체크
        DatabaseReference re = reference.Child(idText.text).Child("PassWord");
        re.GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                if (snapshot.Value == null)
                {
                    flag = true;
                }
                else
                {
                    flag = false;
                }
            }
        });
    }

    public void PwChange() // pw value가 바뀔때 이벤트
    {
        checkPW = false;
        if (pwText.text == "" || confirmPwText.text == "")
        {
            pwErrorText.text = "";
            return;
        }

        // password 길이 4 ~ 12
        if (pwText.text.Length < 4)
        {
            pwErrorText.color = Color.red;
            pwErrorText.text = "You can only use between 4~12";
            return;
        }

        // password 확인
        if (pwText.text != confirmPwText.text)
        {
            pwErrorText.color = Color.red;
            pwErrorText.text = "Passwords do not match";
            return;
        }

        pwErrorText.color = Color.blue;
        pwErrorText.text = "password matches";
        checkPW = true;
    }


    public void DayChange() // Day value가 바뀔때 이벤트
    {
        checkDay = false;
        if (dayText.text == "")
        {
            dayErrorText.text = "1 ~ 31";
            return;
        }
        if (Convert.ToInt32(dayText.text) == 0 || Convert.ToInt32(dayText.text) > 31)
        {
            dayErrorText.text = "1 ~ 31";
            return;
        }
        dayErrorText.text = "";
        checkDay = true;
    }

    public void MonthChange() // Month value가 바뀔때 이벤트
    {
        checkMonth = false;
        if (monthText.text == "")
        {
            monthErrorText.text = "1 ~ 12";
            return;
        }
        if (Convert.ToInt32(monthText.text) == 0 || Convert.ToInt32(monthText.text) > 12)
        {
            monthErrorText.text = "1 ~ 12";
            return;
        }
        monthErrorText.text = "";
        checkMonth = true;
    }

    public void YearChange() // Year value가 바뀔때 이벤트
    {
        checkYear = false;
        if (yearText.text == "")
        {
            yearErrorText.text = "1901 ~ 2023";
            return;
        }
        if (Convert.ToInt32(yearText.text) < 1900 || Convert.ToInt32(yearText.text) > 2023)
        {
            yearErrorText.text = "1901 ~ 2023";
            return;
        }
        yearErrorText.text = "";
        checkYear = true;
    }

    public void SignUpEvent() // Sign Up 버튼 이벤트
    {
        // 모든 체크가 완료되면 회원가입 성공
        if (checkID && checkPW && checkDay && checkMonth && checkYear && checkGender)
        {
            myID = idText.text;
            // 회원 정보 저장
            SignUpData myData = new SignUpData(pwText.text,
                yearText.text + "/" + monthText.text + "/" + dayText.text, myGender);

            string jsondata = JsonUtility.ToJson(myData);

            reference.Child(idText.text).SetRawJsonValueAsync(jsondata);

            // 게임 정보 저장
            SignUpData2 myData2 = new SignUpData2();

            string jsondata2 = JsonUtility.ToJson(myData2);

            reference2.Child("Confirmation").Child(idText.text).SetRawJsonValueAsync(jsondata2);
            reference2.Child("Pollution").Child(idText.text).SetRawJsonValueAsync(jsondata2);
            reference2.Child("Symmetry").Child(idText.text).SetRawJsonValueAsync(jsondata2);

            Debug.Log("회원가입 성공");

            SceneManager.LoadScene("LogIn");
        }
        else
        {
            Debug.Log("회원가입 실패");
        }
    }

    public void MaleEvent() // Male 버튼 이벤트
    {
        checkGender = true;
        genderErrorText.color = Color.blue;
        genderErrorText.text = "Male";
        myGender = "Male";
    }

    public void FemaleEvent() // Female 버튼 이벤트
    {
        checkGender = true;
        genderErrorText.color = Color.blue;
        genderErrorText.text = "Female";
        myGender = "Female";
    }

    // Update is called once per frame
    void Update()
    {

    }

    class SignUpData
    {
        public string PassWord;
        public string Birth;
        public string Gender;
        public int SymmetryGameDifficulty;
        public int ConfirmationGameDifficulty;
        public int PollutionGameDifficulty;

        public SignUpData(string PassWord, string Birth, string Gender)
        {
            this.PassWord = PassWord;
            this.Birth = Birth;
            this.Gender = Gender;
            SymmetryGameDifficulty = 1;
            ConfirmationGameDifficulty = 1;
            PollutionGameDifficulty = 1;
        }
    }

    class SignUpData2
    {
        public int play;
        public SignUpData2()
        {
            this.play = 0;
        }
    }
}