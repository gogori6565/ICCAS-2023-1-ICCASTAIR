using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
    public class PlaySceneTimer : MonoBehaviour
    {
        public Text TimerText;
        public static float setTime;
        // Start is called before the first frame update
        void Start()
        {
            setTime = 60;
        }

        // Update is called once per frame
        void Update()
        {
            setTime -= Time.deltaTime;
            if (setTime <= 0)
            {
                setTime = 0;
                SceneManager.LoadScene("Result_Symmetry");
            }
            if (setTime >= 10)
            {
                TimerText.text = "00:" + (int)setTime;
            }
            else if (setTime < 10)
            {
                TimerText.text = "00:0" + (int)setTime;
            }

        }
    }