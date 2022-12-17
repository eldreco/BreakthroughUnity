using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
	private MainManager _mainManager;
	private MenuUIManager _menuUIManager;
	
	private void Awake() {
		_menuUIManager = gameObject.GetComponent<MenuUIManager>();
	}

	public void PressPlayButton(){
		_menuUIManager.PressPlayButton();
	}

	public void LoadMainEasy(){
		_mainManager.SetMode("Easy");
		SceneManager.LoadScene("Main");
	}

	public void LoadMainMedium(){
		if(MainManager.Instance._passedEasy){
			_mainManager.SetMode("Medium");
			SceneManager.LoadScene("Main");
		}
	}
	
	public void LoadMainHard(){
		if(MainManager.Instance._passedMedium){
			_mainManager.SetMode("Hard");
			SceneManager.LoadScene("Main");
		}
	}

	public void LoadMainTwoPlayers(){
		_mainManager.SetMode("TwoPlayers");
		SceneManager.LoadScene("Main");
	}
	
	protected void Update()
	{
		_mainManager = GameObject.Find("MainManager").GetComponent<MainManager>();
	}
	
	public void SetRed(){
		_mainManager.SetColor(new Color(0.9f, 0.5f, 0.5f));
		GameObject.Find("Main Camera").GetComponent<Camera>().backgroundColor = _mainManager.GetColor();
		_mainManager.SaveColor();
	}
	
	public void SetGreen(){
		_mainManager.SetColor( new Color(0.5f, 0.9f, 0.5f));
		GameObject.Find("Main Camera").GetComponent<Camera>().backgroundColor = _mainManager.GetColor();
		_mainManager.SaveColor();
	}
	
	public void SetBlue(){
		_mainManager.SetColor(new Color(0.5f, 0.9f, 0.9f));
		GameObject.Find("Main Camera").GetComponent<Camera>().backgroundColor = _mainManager.GetColor();
		_mainManager.SaveColor();
	}
	
	public void SetYellow(){
		_mainManager.SetColor(new Color(1f, 1f, 0.5f));
		GameObject.Find("Main Camera").GetComponent<Camera>().backgroundColor = _mainManager.GetColor();
		_mainManager.SaveColor();
	}
	
	public void SetOrange(){
		_mainManager.SetColor(new Color(1f, 0.7f, 0.3f));
		GameObject.Find("Main Camera").GetComponent<Camera>().backgroundColor = _mainManager.GetColor();
		_mainManager.SaveColor();
	}
	
	public void SetPurple(){
		_mainManager.SetColor(new Color(0.7f, 0.3f, 1f));
		GameObject.Find("Main Camera").GetComponent<Camera>().backgroundColor = _mainManager.GetColor();
		_mainManager.SaveColor();
	}
	
	public void SetLGrey(){
		_mainManager.SetColor(new Color(0.9f, 0.9f, 0.9f));
		GameObject.Find("Main Camera").GetComponent<Camera>().backgroundColor = _mainManager.GetColor();
		_mainManager.SaveColor();
	}
	
	public void SetGrey(){
		_mainManager.SetColor(new Color(0.6f, 0.6f, 0.6f));
		GameObject.Find("Main Camera").GetComponent<Camera>().backgroundColor = _mainManager.GetColor();
		_mainManager.SaveColor();
	}
	
	public void SetDGrey(){
		_mainManager.SetColor(new Color(0.4f, 0.4f, 0.4f));
		GameObject.Find("Main Camera").GetComponent<Camera>().backgroundColor = _mainManager.GetColor();
		_mainManager.SaveColor();
	}
}
