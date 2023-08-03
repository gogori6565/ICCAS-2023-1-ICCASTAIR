using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    public GameObject fillGage, emptyGage;
    float width, y;
    void Start()
    {
        width = emptyGage.GetComponent<RectTransform>().rect.width; // 비어있는 게이지바의 길이
        y = emptyGage.GetComponent<RectTransform>().anchoredPosition.y; // 비어있는 게이지바의 y값
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
        if (temp >= 1f) // 설정된 퍼센트값보다 크거나 같으면 애니메이션 종료
        {
            SceneManager.LoadScene("Statistic");
        }
        fillGage.GetComponent<RectTransform>().anchoredPosition = new Vector3(-(width / 2) + width * temp / 2, y, 0); // 색깔 게이지바의 길이 설정
        fillGage.transform.localScale = new Vector3(temp, 1, 0); // 색깔 게이지바의 위치 설정
        temp += 0.005f;
    }
}