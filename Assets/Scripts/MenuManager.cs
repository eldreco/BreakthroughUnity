using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
	private MainManager mainManager;
	
	public void EnterPlayScene(){
		SceneManager.LoadScene("Main");
	}
	
	protected void Update()
	{
		mainManager = GameObject.Find("MainManager").GetComponent<MainManager>();
	}
	
	public void SetRed(){
		mainManager.SetColor(new Color(0.9f, 0.5f, 0.5f));
		GameObject.Find("Main Camera").GetComponent<Camera>().backgroundColor = mainManager.GetColor();
		mainManager.SaveColor();
	}
	
	public void SetGreen(){
		mainManager.SetColor( new Color(0.5f, 0.9f, 0.5f));
		GameObject.Find("Main Camera").GetComponent<Camera>().backgroundColor = mainManager.GetColor();
		mainManager.SaveColor();
	}
	
	public void SetBlue(){
		mainManager.SetColor(new Color(0.5f, 0.9f, 0.9f));
		GameObject.Find("Main Camera").GetComponent<Camera>().backgroundColor = mainManager.GetColor();
		mainManager.SaveColor();
	}
	
	public void SetYellow(){
		mainManager.SetColor(new Color(1f, 1f, 0.5f));
		GameObject.Find("Main Camera").GetComponent<Camera>().backgroundColor = mainManager.GetColor();
		mainManager.SaveColor();
	}
	
	public void SetOrange(){
		mainManager.SetColor(new Color(1f, 0.7f, 0.3f));
		GameObject.Find("Main Camera").GetComponent<Camera>().backgroundColor = mainManager.GetColor();
		mainManager.SaveColor();
	}
	
	public void SetPurple(){
		mainManager.SetColor(new Color(0.7f, 0.3f, 1f));
		GameObject.Find("Main Camera").GetComponent<Camera>().backgroundColor = mainManager.GetColor();
		mainManager.SaveColor();
	}
	
	public void SetLGrey(){
		mainManager.SetColor(new Color(0.9f, 0.9f, 0.9f));
		GameObject.Find("Main Camera").GetComponent<Camera>().backgroundColor = mainManager.GetColor();
		mainManager.SaveColor();
	}
	
	public void SetGrey(){
		mainManager.SetColor(new Color(0.6f, 0.6f, 0.6f));
		GameObject.Find("Main Camera").GetComponent<Camera>().backgroundColor = mainManager.GetColor();
		mainManager.SaveColor();
	}
	
	public void SetDGrey(){
		mainManager.SetColor(new Color(0.4f, 0.4f, 0.4f));
		GameObject.Find("Main Camera").GetComponent<Camera>().backgroundColor = mainManager.GetColor();
		mainManager.SaveColor();
	}
}
