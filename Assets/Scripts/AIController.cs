using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AIController : MonoBehaviour
{	
	//References to other scripts
	private BoardManager _boardManager;
	private GameManager _gameManager;
	private PlayManager _playManager;
	private MainManager _mainManager;

	private bool _firstPlay;
	
	private IDictionary<GameObject, GameObject> _blacks = new Dictionary<GameObject, GameObject>();

    void Awake()
    {
	    Setup();
    }
    
	private void Setup(){
		_boardManager = GameObject.Find("Board").GetComponent<BoardManager>();
		_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		_playManager = GameObject.Find("GameManager").GetComponent<PlayManager>();
		if(GameObject.Find("MainManager") != null)
			_mainManager = GameObject.Find("MainManager").GetComponent<MainManager>();
		_firstPlay = true;
	}
	
	
	public IEnumerator Play(){
		if(_gameManager.GetActiveState() == GameManager.GameState.PLAYBLACKS){
			
			if(_firstPlay && _gameManager._player == 1){
				yield return new WaitForSeconds(2);
				_firstPlay = false;

			}else
			//Wait one second to play for better UX
				yield return new WaitForSeconds(0.5f);
			

			ChooseMove();
			_gameManager.LockPlay();
		}
	}
	
	private void ChooseMove(){ //Change when different ais implemented
		if(_mainManager != null){
			if(_mainManager._mode == "Easy")
				ChooseRandomMove();
			else if(_mainManager._mode == "Medium" || _mainManager._mode == "Hard")
				AdvancedPlayerMove();
		}
		else{
			AdvancedPlayerMove();
		}
	}
	
	private void AdvancedPlayerMove(){
		IDictionary<GameObject, GameObject> board = _boardManager.GetBoard();
		FillBlacks();
		GameObject pieceToMove = null;
		GameObject tileToMoveFrom = null;
		GameObject tileToMoveTo = null;

		List<GameState> states = new List<GameState>();
		GameState highestValueState = null;
		List<GameState> highestStates = new List<GameState>();
		GameState chosenState = null;

		(GameObject, GameObject) wins = CanWin();
		tileToMoveFrom = wins.Item1;
		tileToMoveTo = wins.Item2;
		
		if(tileToMoveFrom != null){
			pieceToMove = _blacks[tileToMoveFrom];
		}else{
			foreach (KeyValuePair<GameObject, GameObject> kvp in _blacks){
				List<GameObject> moves = _boardManager.PossibleMovesBlacks(kvp.Key);
				if(moves.Count > 0){
					foreach (GameObject move in moves){
						tileToMoveFrom = kvp.Key;
						tileToMoveTo = move;
						pieceToMove = _blacks[tileToMoveFrom];		
						GameState state = new GameState(board, pieceToMove, tileToMoveFrom, tileToMoveTo);
						states.Add(state);
					}
				}
			}
		
			foreach (GameState state in states){
				if(highestValueState == null)
					highestValueState = state;
				else if(_mainManager._mode == "Medium"){
					if(state._valueMedium >= highestValueState._valueMedium){
						highestValueState = state;
						highestStates.Add(state);
					}
				}else if(_mainManager._mode == "Hard"){
					if(state._valueHard >= highestValueState._valueHard){
						highestValueState = state;
						highestStates.Add(state);
					}
				}
			}
			
			foreach(GameState s in highestStates){
				if(chosenState == null)
					chosenState = s;
				else if(s._valueHard > chosenState._valueHard)
					chosenState = s;
				
			}

			if(chosenState == null && highestStates.Count>0)
				chosenState = highestStates.ElementAt(Random.Range(0, highestStates.Count));
			if(chosenState == null)
				chosenState = states.ElementAt(Random.Range(0, states.Count));
			pieceToMove = chosenState._pieceMoved;
			tileToMoveFrom = chosenState._tileFrom;
			tileToMoveTo = chosenState._tileTo;			
		}
		UpdateBlacks(pieceToMove, tileToMoveFrom, tileToMoveTo);
		_playManager.MoveBlackPiece(pieceToMove, tileToMoveFrom, tileToMoveTo);
	}

	private void ChooseRandomMove(){
		IDictionary<GameObject, GameObject> board = _boardManager.GetBoard();
		FillBlacks();
		GameObject pieceToMove = null;
		GameObject tileToMoveFrom = null;
		GameObject tileToMoveTo = null;
		List<GameObject> moves = new List<GameObject>();

		(GameObject, GameObject) eats = CanEat();
		(GameObject, GameObject) wins = CanWin();

		tileToMoveFrom = wins.Item1;
		tileToMoveTo = wins.Item2;
		if(tileToMoveFrom == null){
			tileToMoveFrom = eats.Item1;
			tileToMoveTo = eats.Item2;
		}

		if(tileToMoveFrom != null){
			pieceToMove = _blacks[tileToMoveFrom];
		}else{
			while(moves.Count == 0){
				GameObject key = _blacks.ElementAt(Random.Range(0, _blacks.Count)).Key;
				if(_blacks[key] != null){
					tileToMoveFrom = key;
					pieceToMove = board[tileToMoveFrom];
					moves = _boardManager.PossibleMovesBlacks(tileToMoveFrom);
				}
			}
			tileToMoveTo = moves[Random.Range(0,moves.Count)];
		}

		UpdateBlacks(pieceToMove, tileToMoveFrom, tileToMoveTo);
		_playManager.MoveBlackPiece(pieceToMove, tileToMoveFrom, tileToMoveTo);
	}
    
	private (GameObject, GameObject) CanEat(){
		GameObject tileToMoveFrom = null;
		GameObject tileToMoveTo = null;
		List<GameObject> moves = new List<GameObject>();
		
		foreach (KeyValuePair<GameObject, GameObject> kvp in _blacks){
			moves = _boardManager.PossibleMovesBlacks(kvp.Key);

			foreach(GameObject pos in moves){
				if(_boardManager.GetBoard()[pos] != null){
					if(_boardManager.GetBoard()[pos].name[0] == 'W'){
						tileToMoveFrom = kvp.Key;
						tileToMoveTo = pos;
					}
				}
			}
		}
		return (tileToMoveFrom, tileToMoveTo);
	}
	
	private (GameObject, GameObject) CanWin(){
		GameObject tileToMoveFrom = null;
		GameObject tileToMoveTo = null;
		List<GameObject> moves = new List<GameObject>();
		
		foreach (KeyValuePair<GameObject, GameObject> kvp in _blacks){
			moves = _boardManager.PossibleMovesBlacks(kvp.Key);

			foreach(GameObject pos in moves){
				if(pos.name[1] == '1'){
					tileToMoveFrom = kvp.Key;
					tileToMoveTo = pos;
				}
			}
		}
		
		return (tileToMoveFrom, tileToMoveTo);
	}
    
	private void UpdateBlacks(GameObject pieceMoved,GameObject tileToMoveFrom, GameObject tileToMoveTo){
		//Setting previous position to null
		_blacks[tileToMoveFrom] = null;

		//Setting new position for piece
		Destroy(_boardManager.GetBoard()[tileToMoveTo]);
		_blacks[tileToMoveTo] = pieceMoved;
	}
    
	private void FillBlacks(){
		foreach (KeyValuePair<GameObject, GameObject> kvp in _boardManager.GetBoard()){
			if(kvp.Value != null && kvp.Value.name[0] == 'B')
				_blacks[kvp.Key] = kvp.Value;
		}
	}

}
