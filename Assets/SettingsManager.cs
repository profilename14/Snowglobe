using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsManager : MonoBehaviour
{

//Start is called before the first frame update
void Start()
{
	audioMixer.SetFloat("volume", 0);
}
	public AudioMixer audioMixer;
    
//Accepts a volume between 20 and -80 to give to the volume mixer
	public void SetVolume (float volume)
	{
        	audioMixer.SetFloat("volume", volume);
	}
//Toggles fullscreen mode
	public void SetFullscreen (bool isFullscreen)
	{
		Screen.fullScreen = isFullscreen;
	}
	
}
