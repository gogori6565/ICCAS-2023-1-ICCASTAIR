using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneStatistic : MonoBehaviour
{
    public void ToStatistic()
    {
        SceneManager.LoadScene("Loading");
    }
}
