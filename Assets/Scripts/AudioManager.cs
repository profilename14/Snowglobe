using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource musicSource;
    public AudioSource atmosphereSource;
    public AudioLowPassFilter lowPassFilter;

    private float normalCutoffFrequency = 22000f;  // Normal cutoff frequency for unmuted audio
    private float muffledCutoffFrequency = 1000f;  // Muffled cutoff frequency for muted audio

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Play background music
    public void PlayMusic(AudioClip clip, float volume = 1f)
    {
        musicSource.clip = clip;
        musicSource.volume = volume;
        musicSource.Play();
    }

    // Change background music
    public void ChangeMusic(AudioClip newClip, float fadeDuration = 1f, float volume = 1f)
    {
        StartCoroutine(FadeMusic(newClip, fadeDuration, volume));
    }

    // Play atmospheric sounds
    public void PlayAtmosphere(AudioClip clip, float volume = 1f)
    {
        atmosphereSource.clip = clip;
        atmosphereSource.volume = volume;
        atmosphereSource.Play();
    }

    // Muffle the audio
    public void MuffleAudio(bool muffle, float duration = 0.05f)
    {
        StartCoroutine(MuffleRoutine(muffle ? muffledCutoffFrequency : normalCutoffFrequency, duration));
    }

    // Coroutine to fade music in and out when changing tracks
    private IEnumerator FadeMusic(AudioClip newClip, float duration, float targetVolume)
    {
        float startVolume = musicSource.volume;

        // Fade out
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            musicSource.volume = Mathf.Lerp(startVolume, 0, t / duration);
            yield return null;
        }

        // Change music clip
        musicSource.clip = newClip;
        musicSource.Play();

        // Fade in
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            musicSource.volume = Mathf.Lerp(0, targetVolume, t / duration);
            yield return null;
        }
    }

    // Coroutine to muffle/unmuffle audio
    private IEnumerator MuffleRoutine(float targetCutoff, float duration)
    {
        float startCutoff = lowPassFilter.cutoffFrequency;

        for (float t = 0; t < duration; t += Time.unscaledDeltaTime)
        {
            lowPassFilter.cutoffFrequency = Mathf.Lerp(startCutoff, targetCutoff, t / duration);
            yield return null;
        }
    }
}
