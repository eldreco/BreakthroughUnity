﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuUIManager : MonoBehaviour
{
    private GameObject _backgrounds;
    private GameObject _playButton;
    private GameObject _modesUI;

    private bool _playPressed;

    [SerializeField] private GameObject _muteBtn;
    [SerializeField] private Image _muteImage;
    [SerializeField] private Sprite _muteSprite;
    [SerializeField] private Sprite _unmuteSprite;
    [SerializeField] private TMPro.TMP_Text _muteTxt;



    private void Awake() {
        _backgrounds = GameObject.Find("Backgrounds");
        _playButton =  GameObject.Find("PlayButton");
        _modesUI =     GameObject.Find("ModesUI");
    }

    private void Start() {
        BackButtonPressed();
        _playPressed = false;
        if(MainManager.Instance._mute == false){
            _muteTxt.text = "MUTE";
            _muteImage.sprite = _muteSprite;
        }else{
            _muteTxt.text = "UNMUTE";
            _muteImage.sprite = _unmuteSprite;
        }
    }

    public void PressMuteButton(){
        if(MainManager.Instance._mute == false){
            _muteTxt.text = "UNMUTE";
            _muteImage.sprite = _unmuteSprite;
            MainManager.Instance.SetMute(true);
        }else{
            _muteTxt.text = "MUTE";
            _muteImage.sprite = _muteSprite;
            MainManager.Instance.SetMute(false);
        }
    }

    public void PressPlayButton(){
        if(_playPressed){
            BackButtonPressed();
            _playPressed = false;
        } else{
            PlayButtonPressed();
            _playPressed = true;
        }
    }

    private void PlayButtonPressed(){
        _playButton.GetComponentInChildren<TMPro.TMP_Text>().text = "BACK";
        _backgrounds.SetActive(false);
        _modesUI.SetActive(true);
        _muteBtn.SetActive(false);
    }

    private void BackButtonPressed(){
        _playButton.GetComponentInChildren<TMPro.TMP_Text>().text = "PLAY";
        _backgrounds.SetActive(true);
        _modesUI.SetActive(false);
        _muteBtn.SetActive(true);
    }

}
