using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
	public static MainManager Instance;
	private Color _bgColor;
	public string _mode{get; private set;}
	public bool _mute{get; private set;}
	
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
		LoadMode();
		LoadMute();
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
			
			SetMute(false);
		}
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

	public void SetMode(string difficulty){
		_mode = difficulty;
		SaveMode();
	}

	public void SetMute(bool mute){
		_mute = mute;
		SaveMute();
	}
	
	[System.Serializable]
	public class SaveData
	{
		public Color bgColor;
		public string mode;
		public bool mute;
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

	public void SaveMode()
	{
		SaveData data = new SaveData();
		data.mode = _mode;

		string json = JsonUtility.ToJson(data);

		File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
	}

	public void LoadMode()
	{
		string path = Application.persistentDataPath + "/savefile.json";
		if (File.Exists(path))
		{
			string json = File.ReadAllText(path);
			SaveData data = JsonUtility.FromJson<SaveData>(json);

			_mode = data.mode;
		}
	}

	public void SaveMute()
	{
		SaveData data = new SaveData();
		data.mute = _mute;

		string json = JsonUtility.ToJson(data);

		File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
	}

	public void LoadMute()
	{
		string path = Application.persistentDataPath + "/savefile.json";
		if (File.Exists(path))
		{
			string json = File.ReadAllText(path);
			SaveData data = JsonUtility.FromJson<SaveData>(json);

			_mute = data.mute;
		}
	}
}


