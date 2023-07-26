using System.Collections.Generic;
using UnityEngine;

public class CursorChanger : MonoBehaviour
{
    public List<string> cursorImagePaths = new List<string>
    {
        "Hand/��",
        "Hand/��1",
        "Hand/��2",
        "Hand/��3",
        "Hand/��4",
        "Hand/��5",
        "Hand/��6",
        "Hand/��7",
    }; // Ŀ�� �̹��� ��� ����Ʈ

    public static int cursorIndex = 0; // ���� Ŀ�� �̹��� �ε���
    private Texture2D customCursor; // Ŀ�� �̹���
    private int clickCount;
    private bool shouldChangeCursor = false; // Ŀ�� �̹��� ���� ���θ� ��Ÿ���� ����
    private bool washChange = false; // Ŀ�� �̹��� ���� ���θ� ��Ÿ���� ����
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
            if (LoginController.myDiffData.PollutionGameDifficulty != 3) //���̵� ��, �� ������ 
            {
                if (clickCount % 2 != 0) //wash��ư ���� �� Ȧ,¦ ����
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

            if (LoginController.myDiffData.PollutionGameDifficulty == 3) //���̵� ��
            {
                if (cursorIndex < cursorImagePaths.Count)
                {
                    cursorIndex++;
                }
            }
            else  //���̵� ��, ��
            {

                if (washflag) //Ȧ������ wash�ϸ� 
                {
                    if (clickCount % 2 != 0)
                    {
                        cursorIndex++;
                    }
                }
                else //¦������ wash�ϸ�
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
            // Ŀ�� �̹��� �ε��� �Ϸ�Ǹ� ����
            Cursor.SetCursor(customCursor, Vector2.zero, CursorMode.Auto);
        }
        else
        {
            Debug.LogError("Ŀ�� �̹��� ������ �ε��� �� �����ϴ�.");
        }
    }
}
