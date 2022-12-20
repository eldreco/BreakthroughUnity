using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
	//Setup the gamestates
	public enum GameState{START, WHITESTURN, BLACKSTURN, PLAYWHITES, PLAYBLACKS, WONWHITE, WONBLACK, STOP}
	[SerializeField] private GameState _activeState;

	[SerializeField] private GameObject _endText;
	private bool _lockedPlay = false;
	
	//References to other classes
	private BoardManager _boardManager;
	private AIController _aiController;
	private UIManager 	 _uiManager;
	private MainManager  _mainManager;

	[SerializeField] private string _gameMode;

	private int _nMoves;

	public int _player{get;private set;}
	
	private void Awake() {
		SetupGame();
	}

	private void SetupGame(){

		_nMoves = 0;
		_boardManager = GameObject.Find("Board").GetComponent<BoardManager>();
		_aiController = GameObject.Find("AIController").GetComponent<AIController>();
		_endText.GetComponent<TMP_Text>().text = "";
		_uiManager = gameObject.GetComponent<UIManager>();
		if(GameObject.Find("MainManager") != null)
			_mainManager = GameObject.Find("MainManager").GetComponent<MainManager>();
		if(_mainManager != null)
			_gameMode = _mainManager._mode;
		else
			_gameMode = "TwoPlayers";
	}

	private void Start(){
		StartManager();	
	}
	
	private void StartManager(){
		_endText.GetComponent<TMP_Text>().fontSize = 130f;
		_activeState = GameState.START;
		_uiManager.GoAnim();
		int player = Random.Range(0,2);
		_player = player;
		if (player == 0){
			StartCoroutine(_uiManager.SetYourGoAnim());
			StartCoroutine(SetTextGo(player));
			WhitesTurn();
		}  else {
			StartCoroutine(_uiManager.SetOppGoAnim());
			StartCoroutine(SetTextGo(player));
			BlacksTurn();
		}

			
	}

	private IEnumerator SetTextGo(int player){
		yield return new WaitForSeconds(1);
		if (player == 0){
			if(_gameMode == "TwoPlayers")
				_endText.GetComponent<TMP_Text>().text = "Whites to move";
			else
				_endText.GetComponent<TMP_Text>().text = "Your go";
		}else {
			if(_gameMode == "TwoPlayers")
				_endText.GetComponent<TMP_Text>().text = "Blacks to move";
			else
				_endText.GetComponent<TMP_Text>().text = "Your opponent's go";
		}
		yield return new WaitForSeconds(2);
		_endText.GetComponent<TMP_Text>().fontSize = 100f;
		if(MainManager.Instance._record != 0 &&  _gameMode == "Hard")
			_endText.GetComponent<TMP_Text>().text = "YOUR RECORD: " + MainManager.Instance._record + " moves";
		else if(_gameMode == "Easy"){
			_endText.GetComponent<TMP_Text>().fontSize = 80f;
			_endText.GetComponent<TMP_Text>().text = "Reach the last row! If the opponent reaches the first row you lose.";
		}else
			_endText.GetComponent<TMP_Text>().text = "";
	}

	private void Update(){
		CheckPlayed();
	}
	
	private void CheckPlayed(){
		if(CheckWinState() == "Whites" && _activeState != GameState.STOP)
			WinWhites();
		else if(CheckWinState() == "Blacks")
			WinBlacks();
		else{
			if(_activeState == GameState.PLAYWHITES && _lockedPlay){
				BlacksTurn();
			}else if(_activeState == GameState.PLAYBLACKS && _lockedPlay){
				WhitesTurn();
			}
		}
	}
	
	private void WhitesTurn(){
		SetActiveState(GameState.WHITESTURN);
		//Make white able to play
		UnlockPlay();
		PlayWhites();
	}
	
	private void BlacksTurn(){
		SetActiveState(GameState.BLACKSTURN);
		//Make black able to play
		UnlockPlay();
		PlayBlacks();
	}
	
	private void PlayWhites(){
		SetActiveState(GameState.PLAYWHITES);
		_nMoves++;
	}
	
	private void PlayBlacks(){
		SetActiveState(GameState.PLAYBLACKS);
		if(_gameMode != "TwoPlayers")
			StartCoroutine(_aiController.Play());
	}
	
	private void WinWhites(){
		SetActiveState(GameState.WONWHITE);
		_endText.GetComponent<TMP_Text>().fontSize = 130f;
		if(_gameMode != "TwoPlayers"){
			_endText.GetComponent<TMP_Text>().text = "YOU WIN!";
			if(_gameMode == "Easy")
				MainManager.Instance.SetProgress(true, MainManager.Instance._passedMedium);
			else if(_gameMode == "Medium")
				MainManager.Instance.SetProgress(true, true);

			if(_gameMode == "Hard"){
				if(_nMoves < MainManager.Instance._record || MainManager.Instance._record == 0){
					MainManager.Instance.SetRecord(_nMoves);
					StartCoroutine(ShowRecord(_nMoves, true));
				}else
					StartCoroutine(ShowRecord(MainManager.Instance._record, false));
			}
		}else
			_endText.GetComponent<TMP_Text>().text = "WHITES WIN!";
		SetActiveState(GameState.STOP);
		SetStop();

	}
	
	private IEnumerator ShowRecord(int record, bool isNew){	
		yield return new WaitForSeconds(1f);
		_endText.GetComponent<TMP_Text>().fontSize = 100f;
		if(isNew){
			_endText.GetComponent<TMP_Text>().text = "CONGRATULATIONS!";
			yield return new WaitForSeconds(1);
			_endText.GetComponent<TMP_Text>().text = "NEW RECORD!: " + record + " moves";
		}else{
			_endText.GetComponent<TMP_Text>().text = "BUT STILL SAME RECORD";
			yield return new WaitForSeconds(1);
			_endText.GetComponent<TMP_Text>().text = "YOUR RECORD: " + record + " moves";
		}
	}

	private void WinBlacks(){
		SetActiveState(GameState.WONBLACK);
		if(_gameMode != "TwoPlayers")
			_endText.GetComponent<TMP_Text>().text = "YOU LOSE!";
		else
			_endText.GetComponent<TMP_Text>().text = "BLACKS WIN!";
	}
	
	private void SetStop(){
		AudioPlayer.Instance.PlayAudioWin();
	}

	private string CheckWinState(){
		bool noWhites = true;
		bool noBlacks = true;
		foreach (KeyValuePair<GameObject, GameObject> kvp in _boardManager.GetBoard()){
			if(kvp.Value != null){
				if(kvp.Key.name[1] == '8' && kvp.Value.name[0] == 'W')//Whites win by getting to top
					return "Whites";
				if(kvp.Key.name[1] == '1' && kvp.Value.name[0] == 'B')//Blacks win by getting to bottom
					return "Blacks";
				if(noWhites && kvp.Value.name[0] == 'W')//Check if there is a White piece
					noWhites = false;
				if(noBlacks && kvp.Value.name[0] == 'B')//Check if there is a Black piece
					noBlacks = false;
			}
		}
		//If no player wins by getting to the end, check if a player wins by eating all pieces
		if(noWhites)
			return "Blacks";
		else if(noBlacks)
			return "Whites";
		return "";
	}
	
	public void SetActiveState(GameState state){
		_activeState = state;
	}
	
	public GameState GetActiveState(){
		return _activeState;
	}
	
	public void LockPlay(){
		_lockedPlay = true;
	}
	
	public void UnlockPlay(){
		_lockedPlay = false;
	}
	
	public void LoadMenu(){//Changed for WebGL
		MainManager.Instance.selectSound.Play();
		//SceneManager.LoadScene("Menu");
		SceneManager.LoadScene("MenuWebGL");
	}
	
	public void Restart(){
		MainManager.Instance.selectSound.Play();
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}
