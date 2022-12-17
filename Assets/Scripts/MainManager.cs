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
	public bool _passedEasy{get; private set;}
	public bool _passedMedium{get; private set;}	
	public int _record{get; private set;}

	[SerializeField] private AudioSource _bgMusic;
	[SerializeField] private AudioSource _selectSound;
	public AudioSource selectSound{get;set;}
	
	private void Awake()
	{
		//Make this object a SINGLETON
		if (Instance != null)
		{
			Destroy(gameObject);
			return;
		}
		
		Instance = this;
		selectSound = _selectSound;
		DontDestroyOnLoad(gameObject);
		LoadAll();
	}
	
	private void Start()
	{
		Debug.Log(Application.persistentDataPath);
		Color black = new Color(0,0,0,0);
		if (MainManager.Instance != null)
		{
			if(_bgColor == black)
				SetBaseColor();
			else
				GameObject.Find("Main Camera").GetComponent<Camera>().backgroundColor = _bgColor;
			_bgMusic.mute = _mute;
			_selectSound.mute = _mute;
			MenuUIManager.Instance._pEasy = _passedEasy;
			MenuUIManager.Instance._pMedium = _passedMedium;

		}
	}
	
	// Update is called every frame, if the MonoBehaviour is enabled.
	protected void Update()
	{
		GameObject.Find("Main Camera").GetComponent<Camera>().backgroundColor = _bgColor;
	}

	public void MuteUnmuteBGMusic(){
		_bgMusic.mute = !_bgMusic.mute;
		_selectSound.mute = !_selectSound.mute;
	}	

	public void SetRecord(int record){
		_record = record;
		SaveRecord();
	}
	public void SetBaseColor()
	{
		_bgColor = new Color(0.9f, 0.9f, 0.9f);
		GameObject.Find("Main Camera").GetComponent<Camera>().backgroundColor = _bgColor;
	}
	
	public void SetColor(Color color)
	{
		_bgColor = color;
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

	public void SetProgress(bool passedEasy, bool PassedMedium){
		_passedEasy = passedEasy;
		_passedMedium = PassedMedium;
		SaveProgress();
	}
	
	[System.Serializable]
	public class SaveData
	{
		public Color bgColor;
		public string mode;
		public bool mute;
		public bool passedEasy;
		public bool passedMedium;
		public int record;
	}

	public void SaveAll(){
		SaveColor();
		SaveMode();
		SaveMute();
		SaveProgress();
		SaveRecord();
	}

	public void LoadAll(){
		LoadColor();
		LoadMode();
		LoadMute();
		LoadProgress();
		LoadRecord();
	}

	private SaveData SaveRest(){
		SaveData d = new SaveData();
		d.passedEasy = _passedEasy;
		d.passedMedium = _passedMedium;
		d.bgColor = _bgColor;
		d.mute = _mute;
		d.record = _record;
		return d;
	}

	public void SaveRecord(){
		SaveData data = SaveRest();
		data.record = _record;

		string json = JsonUtility.ToJson(data);

		File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
	}

	public void LoadRecord(){
		string path = Application.persistentDataPath + "/savefile.json";
		if (File.Exists(path))
		{
			string json = File.ReadAllText(path);
			SaveData data = JsonUtility.FromJson<SaveData>(json);

			_record = data.record;
		}
	}
	public void SaveProgress(){
		SaveData data = SaveRest();
		data.passedEasy = _passedEasy;
		data.passedMedium = _passedMedium;

		string json = JsonUtility.ToJson(data);

		File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
	}

	public void LoadProgress(){
		string path = Application.persistentDataPath + "/savefile.json";
		if (File.Exists(path))
		{
			string json = File.ReadAllText(path);
			SaveData data = JsonUtility.FromJson<SaveData>(json);

			_passedEasy = data.passedEasy;
			_passedMedium = data.passedMedium;
		}
	}

	public void SaveColor(){
		SaveData data = SaveRest();
		data.bgColor = _bgColor;

		string json = JsonUtility.ToJson(data);

		File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
	}

	public void LoadColor(){
		string path = Application.persistentDataPath + "/savefile.json";
		if (File.Exists(path))
		{
			string json = File.ReadAllText(path);
			SaveData data = JsonUtility.FromJson<SaveData>(json);

			_bgColor = data.bgColor;
		}
	}

	public void SaveMode(){
		SaveData data = new SaveData();
		data.mode = _mode;

		string json = JsonUtility.ToJson(data);

		File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
	}

	public void LoadMode(){
		string path = Application.persistentDataPath + "/savefile.json";
		if (File.Exists(path))
		{
			string json = File.ReadAllText(path);
			SaveData data = JsonUtility.FromJson<SaveData>(json);

			_mode = data.mode;
		}
	}

	public void SaveMute(){
		SaveData data = SaveRest();
		data.mute = _mute;

		string json = JsonUtility.ToJson(data);

		File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
	}

	public void LoadMute(){
		string path = Application.persistentDataPath + "/savefile.json";
		if (File.Exists(path))
		{
			string json = File.ReadAllText(path);
			SaveData data = JsonUtility.FromJson<SaveData>(json);

			_mute = data.mute;
		}
	}
}


