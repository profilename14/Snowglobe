using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ComicCutsceneManager : MonoBehaviour
{
    [Tooltip("What gamepad/keyboard button action ID should trigger the next card?")]
    public string advanceButton = "Jump";

    [Tooltip("Which image should be in front, starting from zero?")]
    public int cardToShow = 0;

    [Tooltip("How fast to flip cards (in cards per second)")]
    public float flipSpeed = 5f;

    [Tooltip("How many degrees should each card behind the top card rotate?")]
    public float rotationIncrement = -5f;

    [Tooltip("How far should the next card be offset from the top card position?")]
    public Vector2 fanIncrement = new Vector2(15, -15);

    [Tooltip("Where should the top card fly to when we skip past it?")]
    public Vector2 flipAwayOffset = new Vector2(-100, 0);

    [Tooltip("What should happen when we're left with an empty stack?")]
    public UnityEvent OnFinishedStack;

    [Tooltip("Array of audio clips to be played for each panel (optional)")]
    public AudioClip[] voiceLines;

    [Tooltip("Background music to play during the cutscene")]
    public AudioClip backgroundMusic;

    [Tooltip("Fade duration for music at the end of the cutscene")]
    public float musicFadeDuration = 2f;

    private Image[] _images;
    private Vector2 _centerPosition;
    private float _currentCard;
    private bool _hasFinished;
    private AudioSource _audioSource;
    private AudioSource _musicSource;

    void Start()
    {
        _audioSource = gameObject.AddComponent<AudioSource>();
        _musicSource = gameObject.AddComponent<AudioSource>();
        _musicSource.loop = true;

        _images = GetComponentsInChildren<Image>();
        System.Array.Reverse(_images);

        _centerPosition = _images[0].rectTransform.anchoredPosition;

        if (backgroundMusic != null)
        {
            _musicSource.clip = backgroundMusic;
            _musicSource.Play();
        }

        Layout();
    }

    void Update()
    {
        if (Input.GetButtonDown(advanceButton))
        {
            TryAdvance();
        }

        if (Mathf.Approximately(cardToShow, _currentCard))
        {
            if (cardToShow == _images.Length && !_hasFinished)
            {
                _hasFinished = true;
                StartCoroutine(FadeOutMusic());
            }
            return;
        }

        _currentCard = Mathf.MoveTowards(_currentCard, cardToShow, flipSpeed * Time.deltaTime);
        Layout();
    }

    public bool TryAdvance()
    {
        if (cardToShow >= _images.Length)
            return false;

        PlayVoiceLine(cardToShow);
        cardToShow++;
        return true;
    }

    void Layout()
    {
        for (int i = 0; i < _images.Length; i++)
        {
            var image = _images[i];

            float t = i - _currentCard;

            var color = image.color;
            color.a = Mathf.Clamp01(t + 1f);
            image.color = color;

            var trans = image.rectTransform;
            trans.localRotation = Quaternion.Euler(0, 0, rotationIncrement * t);

            trans.anchoredPosition = _centerPosition + (t < 0f ?
                Vector2.Lerp(flipAwayOffset, Vector2.zero, t + 1f)
                : Mathf.Pow(t, 0.75f) * fanIncrement);
        }
    }

    private void PlayVoiceLine(int index)
    {
        if (voiceLines != null && index < voiceLines.Length && voiceLines[index] != null)
        {
            _audioSource.clip = voiceLines[index];
            _audioSource.Play();
        }
    }

    private System.Collections.IEnumerator FadeOutMusic()
    {
        float startVolume = _musicSource.volume;

        for (float t = 0; t < musicFadeDuration; t += Time.deltaTime)
        {
            _musicSource.volume = Mathf.Lerp(startVolume, 0, t / musicFadeDuration);
            yield return null;
        }


        _musicSource.Stop();
        
        if (OnFinishedStack != null)
        {
            OnFinishedStack.Invoke();
        }

    }
}
