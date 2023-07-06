using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShowSceneTimer : MonoBehaviour
{
    public Text TimerText;
    float setTime = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        setTime -= Time.deltaTime;
        if (setTime <= 0)
        {
            SceneManager.LoadScene("Play");
        }
        TimerText.text = "00:0" + (int)setTime;
    }
}
