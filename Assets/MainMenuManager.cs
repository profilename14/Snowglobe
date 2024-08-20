using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public AudioClip mainMenuMusic;

    void Start()
    {
        if (mainMenuMusic != null)
        {
            AudioManager.instance.PlayMusic(mainMenuMusic);
        }
    }
}
