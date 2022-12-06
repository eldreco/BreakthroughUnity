using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
	private MainManager _mainManager;
	
	public void EnterPlayScene(){
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
