using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorChanger : MonoBehaviour
{
    public Texture2D customCursor;

    private void Start()
    {
        customCursor = Resources.Load<Texture2D>("Hand/손");
        if (customCursor != null)
        {
            Cursor.SetCursor(customCursor, Vector2.zero, CursorMode.Auto);
        }
        else
        {
            Debug.LogError("손 이미지 파일을 로드할 수 없습니다.");
        }
    }
    
    private void OnDisable()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    // Update is called once per frame
    void Update()
        {

    }
}
