using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
	public static MenuManager Instance;

	private MainManager _mainManager;
	private MenuUIManager _menuUIManager;
	private AudioSource _selectSound;
	

	private void Awake() {
		if (Instance != null){
			Destroy(gameObject);
			return;
		}
		Instance = this;
		_menuUIManager = gameObject.GetComponent<MenuUIManager>();
	}

	protected void Start(){
		_selectSound = MainManager.Instance.selectSound;
	}

	public void PressPlayButton(){
		_menuUIManager.PressPlayButton();
		_selectSound.Play();
	}

	public void LoadMainEasy(){
		_selectSound.Play();
		_mainManager.SetMode("Easy");
		SceneManager.LoadScene("Main");
	}

	public void LoadMainMedium(){
		if(MainManager.Instance._passedEasy){
			_selectSound.Play();
			_mainManager.SetMode("Medium");
			SceneManager.LoadScene("Main");
		}
	}
	
	public void LoadMainHard(){
		if(MainManager.Instance._passedMedium){
			_selectSound.Play();
			_mainManager.SetMode("Hard");
			SceneManager.LoadScene("Main");
		}
	}

	public void LoadMainTwoPlayers(){
		_selectSound.Play();
		_mainManager.SetMode("TwoPlayers");
		SceneManager.LoadScene("Main");
	}

	public void OpenTutorial(){
		SceneManager.LoadScene("HowToPlay");
	}
	
	protected void Update()
	{
		_mainManager = GameObject.Find("MainManager").GetComponent<MainManager>();
	}

	public void SetRed(){
		_selectSound.Play();
		_mainManager.SetColor(new Color(0.9f, 0.5f, 0.5f));
		GameObject.Find("Main Camera").GetComponent<Camera>().backgroundColor = _mainManager.GetColor();
		_mainManager.SaveColor();
	}
	
	public void SetGreen(){
		_selectSound.Play();
		_mainManager.SetColor( new Color(0.5f, 0.9f, 0.5f));
		GameObject.Find("Main Camera").GetComponent<Camera>().backgroundColor = _mainManager.GetColor();
		_mainManager.SaveColor();
	}
	
	public void SetBlue(){
		_selectSound.Play();
		_mainManager.SetColor(new Color(0.5f, 0.9f, 0.9f));
		GameObject.Find("Main Camera").GetComponent<Camera>().backgroundColor = _mainManager.GetColor();
		_mainManager.SaveColor();
	}
	
	public void SetYellow(){
		_selectSound.Play();
		_mainManager.SetColor(new Color(1f, 1f, 0.5f));
		GameObject.Find("Main Camera").GetComponent<Camera>().backgroundColor = _mainManager.GetColor();
		_mainManager.SaveColor();
	}
	
	public void SetOrange(){
		_selectSound.Play();
		_mainManager.SetColor(new Color(1f, 0.7f, 0.3f));
		GameObject.Find("Main Camera").GetComponent<Camera>().backgroundColor = _mainManager.GetColor();
		_mainManager.SaveColor();
	}
	
	public void SetPurple(){
		_selectSound.Play();
		_mainManager.SetColor(new Color(0.7f, 0.3f, 1f));
		GameObject.Find("Main Camera").GetComponent<Camera>().backgroundColor = _mainManager.GetColor();
		_mainManager.SaveColor();
	}
	
	public void SetLGrey(){
		_selectSound.Play();
		_mainManager.SetColor(new Color(0.9f, 0.9f, 0.9f));
		GameObject.Find("Main Camera").GetComponent<Camera>().backgroundColor = _mainManager.GetColor();
		_mainManager.SaveColor();
	}
	
	public void SetGrey(){
		_selectSound.Play();
		_mainManager.SetColor(new Color(0.6f, 0.6f, 0.6f));
		GameObject.Find("Main Camera").GetComponent<Camera>().backgroundColor = _mainManager.GetColor();
		_mainManager.SaveColor();
	}
	
	public void SetDGrey(){
		_selectSound.Play();
		_mainManager.SetColor(new Color(0.4f, 0.4f, 0.4f));
		GameObject.Find("Main Camera").GetComponent<Camera>().backgroundColor = _mainManager.GetColor();
		_mainManager.SaveColor();
	}
}
