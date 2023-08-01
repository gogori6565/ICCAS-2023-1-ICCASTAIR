using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sMusic : MonoBehaviour
{
    GameObject BackgroundMusic;
    AudioSource backmusic;

    void Awake()
    {
        BackgroundMusic = GameObject.Find("sMusic");
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