using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] AudioSource _audioMoveWhite;
    [SerializeField] AudioSource _audioMoveBlack;

    public void PlayAudioWhiteMove(){
        _audioMoveWhite.Play();
    }

    public void PlayAudioBlackMove(){
        _audioMoveBlack.Play();
    }
}
