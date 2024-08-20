using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFX : MonoBehaviour
{
    private AudioSource SFXsource;

    [Space]
    [Header("Footsteps")]
    public ParticleSystem footstepVFX;
    public List<AudioClip> footstepSFX;

    [Space]
    [Header("Jumping")]
    public ParticleSystem jumpVFX;
    public List<AudioClip> landingSFX;
    public List<AudioClip> jumpSFX;




    // Start is called before the first frame update
    void Awake()
    {
        SFXsource = GetComponent<AudioSource>();
    }
    public void PlayJump()
    {
        jumpVFX.Play();

        AudioClip clip;
        clip = jumpSFX[Random.Range(0, jumpSFX.Count)];

        SFXsource.clip = clip;
        SFXsource.volume = Random.Range(0.02f, 0.05f);
        SFXsource.pitch = Random.Range(0.8f, 1.2f);
        SFXsource.Play();
    }
    public void PlayLanding()
    {
        jumpVFX.Play();

        AudioClip clip;
        clip = landingSFX[Random.Range(0, landingSFX.Count)];

        SFXsource.clip = clip;
        SFXsource.volume = Random.Range(0.02f, 0.05f);
        SFXsource.pitch = Random.Range(0.8f, 1.2f);
        SFXsource.Play();
    }

    public void PlayFootstep()
    {
        footstepVFX.Play();

        AudioClip clip;
        clip = footstepSFX[Random.Range(0, footstepSFX.Count)];

        SFXsource.clip = clip;
        SFXsource.volume = Random.Range(0.02f, 0.05f);
        SFXsource.pitch = Random.Range(0.8f, 1.2f);
        SFXsource.Play();
    }
    
    public void PlayDeath()
    {
        footstepVFX.Play();

        AudioClip clip;
        clip = footstepSFX[Random.Range(0, footstepSFX.Count)];

        SFXsource.clip = clip;
        SFXsource.volume = Random.Range(0.08f, 0.010f);
        SFXsource.pitch = Random.Range(1.0f, 1.4f);
        SFXsource.Play();
    }

}
