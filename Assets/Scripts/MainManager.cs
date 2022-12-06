using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
	public static MainManager Instance;
	private Color _bgColor;
	public string _difficulty{get; private set;}
	
	private void Awake()
	{
		//Make this object a SINGLETON
		if (Instance != null)
		{
			Destroy(gameObject);
			return;
		}
		
		Instance = this;
		DontDestroyOnLoad(gameObject);
		LoadColor();
		LoadDifficulty();
	}
	
	private void Start()
	{

		Color black = new Color(0,0,0,0);
		if (MainManager.Instance != null)
		{
			if(_bgColor == black)
				SetBaseColor();
			else
				SetColor(_bgColor);
		}
		Debug.Log(_bgColor);
	}
	
	// Update is called every frame, if the MonoBehaviour is enabled.
	protected void Update()
	{
		GameObject.Find("Main Camera").GetComponent<Camera>().backgroundColor = _bgColor;
	}
	
	public void SetBaseColor()
	{
		MainManager.Instance._bgColor = new Color(0.9f, 0.9f, 0.9f);
		GameObject.Find("Main Camera").GetComponent<Camera>().backgroundColor = _bgColor;
	}
	
	public void SetColor(Color color)
	{
		MainManager.Instance._bgColor = color;
		GameObject.Find("Main Camera").GetComponent<Camera>().backgroundColor = _bgColor;
		SaveColor();
	}
	
	public Color GetColor(){
		return _bgColor;
	}

	public void SetDifficulty(string difficulty){
		_difficulty = difficulty;
		SaveDifficulty();
	}
	
	[System.Serializable]
	public class SaveData
	{
		public Color bgColor;
		public string difficulty;
	}

	public void SaveColor()
	{
		SaveData data = new SaveData();
		data.bgColor = _bgColor;

		string json = JsonUtility.ToJson(data);

		File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
	}

	public void LoadColor()
	{
		string path = Application.persistentDataPath + "/savefile.json";
		if (File.Exists(path))
		{
			string json = File.ReadAllText(path);
			SaveData data = JsonUtility.FromJson<SaveData>(json);

			_bgColor = data.bgColor;
		}
	}

	public void SaveDifficulty()
	{
		SaveData data = new SaveData();
		data.difficulty = _difficulty;

		string json = JsonUtility.ToJson(data);

		File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
	}

	public void LoadDifficulty()
	{
		string path = Application.persistentDataPath + "/savefile.json";
		if (File.Exists(path))
		{
			string json = File.ReadAllText(path);
			SaveData data = JsonUtility.FromJson<SaveData>(json);

			_difficulty = data.difficulty;
		}
	}
	
}


