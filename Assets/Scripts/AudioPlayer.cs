using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] AudioSource _audioMoveWhite;
    [SerializeField] AudioSource _audioMoveBlack;

    public void PlayAudioWhiteMove(){
        if(MainManager.Instance._mute == false)
            _audioMoveWhite.Play();
    }

    public void PlayAudioBlackMove(){
        if(MainManager.Instance._mute == false)
            _audioMoveBlack.Play();
    }
}
