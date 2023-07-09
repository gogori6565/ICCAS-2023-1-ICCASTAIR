using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //����Ƽ���� ���� ���õ� ��� ���ֱ� ���� �߰�

public class ChangeScene6 : MonoBehaviour
{
    string[] scenes = { "Kitchen_p", "Restroom", "Room_p" };

    public void ChangeSceneBtn()
    {
        switch (this.gameObject.name)
        {
            case "MainMenu_Button":
                SceneManager.LoadScene("PollutionRule");
                break;

         //�� ��ȯ(����)
            case "GameStart_Button":
                int randomIndex = Random.Range(0, scenes.Length);
                string randomScene = scenes[randomIndex]; // ���� �ε����� �ش��ϴ� Scene ��������
                SceneManager.LoadScene(randomScene); // Scene ��ȯ
                break;
        }
    }
 
}
