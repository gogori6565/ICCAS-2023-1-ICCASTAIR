using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
    public class PlaySceneTimer : MonoBehaviour
    {
        public Text TimerText;
        public static float setTime = 60;
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
                Debug.Log("Game End");
                setTime = 0;
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