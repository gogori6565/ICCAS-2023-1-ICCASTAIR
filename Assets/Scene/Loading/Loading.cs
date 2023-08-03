using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    public GameObject fillGage, emptyGage;
    float width, y;
    void Start()
    {
        width = emptyGage.GetComponent<RectTransform>().rect.width; // ����ִ� ���������� ����
        y = emptyGage.GetComponent<RectTransform>().anchoredPosition.y; // ����ִ� ���������� y��
        //StartCoroutine(LoadStatisticSceneWithDelay());
    }

    IEnumerator LoadStatisticSceneWithDelay()
    {
        // Wait for 0.5 seconds
        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene("Statistic");

    }
    float temp = 0f;
    private void Update()
    {
        if (temp >= 1f) // ������ �ۼ�Ʈ������ ũ�ų� ������ �ִϸ��̼� ����
        {
            SceneManager.LoadScene("Statistic");
        }
        fillGage.GetComponent<RectTransform>().anchoredPosition = new Vector3(-(width / 2) + width * temp / 2, y, 0); // ���� ���������� ���� ����
        fillGage.transform.localScale = new Vector3(temp, 1, 0); // ���� ���������� ��ġ ����
        temp += 0.005f;
    }
}