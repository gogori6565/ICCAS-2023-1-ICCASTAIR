using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class CursorChanger : MonoBehaviour
{

    public List<string> cursorImagePaths = new List<string> {
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
        LoadCursorImageAsync(cursorImagePaths[cursorIndex]);
    }

    private void OnDisable()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    private void Update()
    {
        if (washChange)
        {
            if (ResultScene.preScore < 6000) //���̵� ��, �� ������ 
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

            LoadCursorImageAsync(cursorImagePaths[0]);
        }
        if (shouldChangeCursor)
        {
            shouldChangeCursor = false;

            if (ResultScene.preScore >= 6000) //���̵� ��
            {
                cursorIndex++;
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

            LoadCursorImageAsync(cursorImagePaths[cursorIndex]);
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

    private async void LoadCursorImageAsync(string imagePath)
    {
        var handle = Addressables.LoadAssetAsync<Texture2D>(imagePath);
        await handle.Task;

        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            customCursor = handle.Result;

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
        else
        {
            Debug.LogError("Ŀ�� �̹��� ������ �ε��ϴ� ���� ������ �߻��߽��ϴ�.");
        }
    }
}
