using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
	public static MainManager Instance;
	private Color bgColor;
	
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
	}
	
	private void Start()
	{
		Color black = new Color(0,0,0,0);
		if (MainManager.Instance != null)
		{
			if(bgColor == black)
				SetBaseColor();
			else
				SetColor(bgColor);
		}
		Debug.Log(bgColor);
	}
	
	// Update is called every frame, if the MonoBehaviour is enabled.
	protected void Update()
	{
		GameObject.Find("Main Camera").GetComponent<Camera>().backgroundColor = bgColor;
	}
	
	public void SetBaseColor()
	{
		MainManager.Instance.bgColor = new Color(0.9f, 0.9f, 0.9f);
		GameObject.Find("Main Camera").GetComponent<Camera>().backgroundColor = bgColor;
	}
	
	public void SetColor(Color color)
	{
		MainManager.Instance.bgColor = color;
		GameObject.Find("Main Camera").GetComponent<Camera>().backgroundColor = bgColor;
		SaveColor();
	}
	
	public Color GetColor(){
		return bgColor;
	}
	
	[System.Serializable]
	public class SaveData
	{
		public Color bgColor;
	}
	public void SaveColor()
	{
		SaveData data = new SaveData();
		data.bgColor = bgColor;

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

			bgColor = data.bgColor;
		}
	}
	
}


