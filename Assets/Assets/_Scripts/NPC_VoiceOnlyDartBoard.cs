using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_VoiceOnlyDartBoard : MonoBehaviour
{
    AudioSource myAudioSource;
    [SerializeField]
    AudioClip bravoAudioClip;
    [SerializeField]
    AudioClip youWonAudioClip;
    void Start()
    {
        if (myAudioSource == null) myAudioSource = this.GetComponent<AudioSource>();
    }

  public void SayBravo()
    {
        myAudioSource.clip = bravoAudioClip;
        myAudioSource.Play();
    }
  public  void SayYouWon()
    {
        myAudioSource.clip = youWonAudioClip;
        myAudioSource.Play();
    } 
}
