using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;

public class SoundPlayer : MonoBehaviour
{
    [SerializeField] private UnityEvent onStart;
    private AudioSource audioSource;
    private AudioSource bgmSource;
    private AudioSource ambienceSource; 

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        bgmSource = gameObject.AddComponent<AudioSource>();
        ambienceSource = gameObject.AddComponent<AudioSource>();

        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 1f;
        audioSource.rolloffMode = AudioRolloffMode.Linear;
        audioSource.minDistance = 0f;
        audioSource.maxDistance = 50f;

        bgmSource.playOnAwake = false;
        bgmSource.loop = true;

        ambienceSource.playOnAwake = false;
        ambienceSource.loop = true;

        onStart.Invoke();
    }

    public void PlaySound(SoundEvent soundEvent)
    {
        if (soundEvent == null || soundEvent.clip == null) return;

        AudioSource source = soundEvent.isBGM ? bgmSource : (soundEvent.isAmbience ? ambienceSource : audioSource);
        if (source != null)
        {
            source.clip = soundEvent.clip;
            source.outputAudioMixerGroup = soundEvent.audioMixerGroup;
            source.volume = soundEvent.volume;
            source.Play();
        }
    }

    public void StopBGM()
    {
        if (bgmSource.isPlaying)
        {
            bgmSource.Stop();
        }
    }

    public void StopAmbience() // New method to stop ambience
    {
        if (ambienceSource.isPlaying)
        {
            ambienceSource.Stop();
        }
    }
}