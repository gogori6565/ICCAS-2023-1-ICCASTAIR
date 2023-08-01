using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SurveyMusic : MonoBehaviour
{
    GameObject BackgroundMusic;
    AudioSource backmusic;

    void Awake()
    {
        BackgroundMusic = GameObject.Find("SurveyMusic");
        backmusic = BackgroundMusic.GetComponent<AudioSource>();
        DontDestroyOnLoad(BackgroundMusic);

        backmusic.Play();
    }

    public void OnDestroy()
    {
        backmusic.Stop();
    }

    public void MusicStop()
    {
        backmusic.Stop();
    }
}