using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnimationSounds : MonoBehaviour
{
    public AudioSource audioSource;
    [SerializeField] BoolValue vitLanguage;
    [SerializeField] AudioClip[] engAudioClips;
    [SerializeField] AudioClip[] VitAudioClips;
    public void PlayAudio(AudioClip audio)
    {

        if (vitLanguage.Value)
        {
            for (int i = 0; i < engAudioClips.Length; i++)
            {
                if (audio.name == engAudioClips[i].name) audioSource.PlayOneShot(VitAudioClips[i]); ;
            }
        }
        else
            audioSource.PlayOneShot(audio);
    }
}