using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class LevelManager : MonoBehaviour
{
    public string sceneName;
    public AudioClip mainMenuMusic;

    public void changeScene()
    {
	    SceneManager.LoadScene(sceneName);
        AudioManager.instance.FadeOutMusic(2f);
    }
}
