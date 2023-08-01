using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cMusic : MonoBehaviour
{
    GameObject BackgroundMusic;
    AudioSource backmusic;

    void Awake()
    {
        BackgroundMusic = GameObject.Find("cMusic");
        backmusic = BackgroundMusic.GetComponent<AudioSource>();
        DontDestroyOnLoad(BackgroundMusic);
    }

    public void MusicStart()
    {
        backmusic.Play();
    }

    public void MusicStop()
    {
        backmusic.Stop();
    }
}
