using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorChanger : MonoBehaviour
{
    public Texture2D customCursor;

    private void Start()
    {
        customCursor = Resources.Load<Texture2D>("��");
        if (customCursor != null)
        {
            Cursor.SetCursor(customCursor, Vector2.zero, CursorMode.Auto);
        }
        else
        {
            Debug.LogError("�� �̹��� ������ �ε��� �� �����ϴ�.");
        }
    }

    // Update is called once per frame
    void Update()
        {
            
    }
}