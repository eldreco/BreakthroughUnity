using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
	
	public static AudioPlayer Instance;
	
	[SerializeField] private AudioSource _audioMoveWhite;
	[SerializeField] private AudioSource _audioMoveBlack;
	[SerializeField] private AudioSource _audioSelectPiece;
	[SerializeField] private AudioSource _audioWin;

	protected void Awake()
	{
		Instance = this;
	}
	
    public void PlayAudioWhiteMove(){
        if(MainManager.Instance._mute == false)
            _audioMoveWhite.Play();
    }

    public void PlayAudioBlackMove(){
        if(MainManager.Instance._mute == false)
            _audioMoveBlack.Play();
    }
    
	public void PlayAudioSelectPiece(){
		if(MainManager.Instance._mute == false)
			_audioSelectPiece.Play();
	}
	
	public void PlayAudioWin(){
		if(MainManager.Instance._mute == false)
			_audioWin.Play();
	}
}
