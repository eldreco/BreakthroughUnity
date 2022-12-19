using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ColorSelector : MonoBehaviour
{
    private void Start() {
        gameObject.GetComponent<SpriteRenderer>().color = MainManager.Instance.GetColor();
    }

    public void OpenMenu(){
        SceneManager.LoadScene("Menu");
    }
}
