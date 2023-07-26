using System.Collections.Generic;
using UnityEngine;

public class CursorChanger : MonoBehaviour
{
    public List<string> cursorImagePaths = new List<string>
    {
        "Hand/손",
        "Hand/손1",
        "Hand/손2",
        "Hand/손3",
        "Hand/손4",
        "Hand/손5",
        "Hand/손6",
        "Hand/손7",
    }; // 커서 이미지 경로 리스트

    public static int cursorIndex = 0; // 현재 커서 이미지 인덱스
    private Texture2D customCursor; // 커서 이미지
    private int clickCount;
    private bool shouldChangeCursor = false; // 커서 이미지 변경 여부를 나타내는 변수
    private bool washChange = false; // 커서 이미지 변경 여부를 나타내는 변수
    private bool washflag = false;

    private void Start()
    {

        LoadCursorImage(cursorImagePaths[cursorIndex]);
        Cursor.SetCursor(customCursor, Vector2.zero, CursorMode.Auto);
    }

    private void OnDisable()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    private void Update()
    {
        if (washChange)
        {
            if (LoginController.myDiffData.PollutionGameDifficulty != 3) //난이도 중, 하 에서만 
            {
                if (clickCount % 2 != 0) //wash버튼 누를 때 홀,짝 구분
                {
                    washflag = true;
                }
                else
                {
                    washflag = false;
                }
            }

            washChange = false;

            cursorIndex = 0;

            LoadCursorImage(cursorImagePaths[0]);
        }
        if (shouldChangeCursor)
        {
            shouldChangeCursor = false;

            if (LoginController.myDiffData.PollutionGameDifficulty == 3) //난이도 상
            {
                if (cursorIndex < cursorImagePaths.Count)
                {
                    cursorIndex++;
                }
            }
            else  //난이도 하, 중
            {

                if (washflag) //홀수에서 wash하면 
                {
                    if (clickCount % 2 != 0)
                    {
                        cursorIndex++;
                    }
                }
                else //짝수에서 wash하면
                {

                    if (clickCount % 2 == 0)
                    {
                        cursorIndex++;
                    }
                }
            }

            LoadCursorImage(cursorImagePaths[cursorIndex]);
        }
    }

    public void MarkCursorChange()
    {
        shouldChangeCursor = true;
    }

    public void WashCursorChange()
    {
        washChange = true;
    }

    public void CursorIndexCount(int cnt)
    {
        clickCount = cnt;
    }

    private void LoadCursorImage(string imagePath)
    {
        customCursor = Resources.Load<Texture2D>(imagePath);

        if (customCursor != null)
        {
            // 커서 이미지 로딩이 완료되면 변경
            Cursor.SetCursor(customCursor, Vector2.zero, CursorMode.Auto);
        }
        else
        {
            Debug.LogError("커서 이미지 파일을 로드할 수 없습니다.");
        }
    }
}
