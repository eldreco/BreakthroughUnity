using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuUIManager : MonoBehaviour
{
    private GameObject _backgrounds;
    private GameObject _playButton;
    private GameObject _modesUI;

    private bool _playPressed;

    private void Awake() {
        _backgrounds =      GameObject.Find("Backgrounds");
        _playButton =       GameObject.Find("PlayButton");
        _modesUI = GameObject.Find("ModesUI");
    }

    private void Start() {
        BackButtonPressed();
        _playPressed = false;
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
    }

    private void BackButtonPressed(){
        _playButton.GetComponentInChildren<TMPro.TMP_Text>().text = "PLAY";
        _backgrounds.SetActive(true);
        _modesUI.SetActive(false);
    }

}
