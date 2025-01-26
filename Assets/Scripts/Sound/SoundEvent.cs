using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "New Sound Event", menuName = "Sound Event", order = 51)]
public class SoundEvent : ScriptableObject
{
    public AudioClip clip;
    public AudioMixerGroup audioMixerGroup;
    public bool isBGM;
    public bool isAmbience; 
    public float volume = 1f;
}