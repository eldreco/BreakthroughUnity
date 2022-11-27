using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
	//Setup the gamestates
	public enum GameState{START, WHITESTURN, BLACKSTURN, PLAYWHITES, PLAYBLACKS, WONWHITE, WONBLACK}
	public GameState activeState;

	public GameObject endText;
	private bool lockedPlay = false;
	
	//References to other classes
	private BoardManager boardManager;
	private AIController aiController;
	
	private void Start(){

		SetupGame();
	}
	
	private void Update(){
		CheckPlayed();
	}
	
	private void CheckPlayed(){
		if(CheckWinState() == "Whites")
			WinWhites();
		else if(CheckWinState() == "Blacks")
			WinBlacks();
		else{
			if(activeState == GameState.PLAYWHITES && lockedPlay){
				BlacksTurn();
			}else if(activeState == GameState.PLAYBLACKS && lockedPlay){
				WhitesTurn();
			}
		}
	}
	
	private void SetupGame(){
		activeState = GameState.START;
		boardManager = GameObject.Find("Board").GetComponent<BoardManager>();
		boardManager.SetupBoard();
		aiController = GameObject.Find("AIController").GetComponent<AIController>();
		endText.GetComponent<TMP_Text>().text = "";
		WhitesTurn();
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
	}
	
	private void PlayBlacks(){
		SetActiveState(GameState.PLAYBLACKS);
		StartCoroutine(aiController.Play());
	}
	
	private void WinWhites(){
		SetActiveState(GameState.WONWHITE);
		endText.GetComponent<TMP_Text>().text = "YOU WIN!";
	}
	
	private void WinBlacks(){
		SetActiveState(GameState.WONBLACK);
		endText.GetComponent<TMP_Text>().text = "YOU LOSE";
	}
	private string CheckWinState(){
		bool noWhites = true;
		bool noBlacks = true;
		foreach (KeyValuePair<GameObject, GameObject> kvp in boardManager.GetBoard()){
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
		activeState = state;
	}
	
	public GameState GetActiveState(){
		return activeState;
	}
	
	public void LockPlay(){
		lockedPlay = true;
	}
	
	public void UnlockPlay(){
		lockedPlay = false;
	}
	
	public void LoadMenu(){
		SceneManager.LoadScene("Menu");
	}
	
	public void Restart(){
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}
