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
	
	private IDictionary<GameObject, GameObject> blacks = new Dictionary<GameObject, GameObject>();
    // Start is called before the first frame update
    void Start()
    {
	    Setup();
    }
    
	private void Setup(){
		_boardManager = GameObject.Find("Board").GetComponent<BoardManager>();
		_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		_playManager = GameObject.Find("GameManager").GetComponent<PlayManager>();
	}
	
	
	public IEnumerator Play(){
		if(_gameManager.GetActiveState() == GameManager.GameState.PLAYBLACKS){
			
			//Wait one second to play for better UX
			yield return new WaitForSeconds(1);

			ChooseMove();
			_gameManager.LockPlay();
		}
	}
	
	private void ChooseMove(){ //Change when different ais implemented
		ChooseRandomMove();
	}
	
	private void ChooseRandomMove(){
		IDictionary<GameObject, GameObject> board = _boardManager.GetBoard();
		FillBlacks();
		GameObject pieceToMove = null;
		GameObject tileToMoveFrom = null;
		GameObject tileToMoveTo = null;
		List<GameObject> moves = new List<GameObject>();

		(GameObject, GameObject) eats = CanEat();
		tileToMoveFrom = eats.Item1;
		tileToMoveTo = eats.Item2;
		
		if(tileToMoveFrom != null){
			pieceToMove = blacks[tileToMoveFrom];
		}else{
			while(moves.Count == 0){
				GameObject key = blacks.ElementAt(Random.Range(0, blacks.Count)).Key;
				if(blacks[key] != null){
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
		
		foreach (KeyValuePair<GameObject, GameObject> kvp in blacks){
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
    
	private void UpdateBlacks(GameObject pieceMoved,GameObject tileToMoveFrom, GameObject tileToMoveTo){
		//Setting previous position to null
		blacks[tileToMoveFrom] = null;

		//Setting new position for piece
		Destroy(_boardManager.GetBoard()[tileToMoveTo]);
		blacks[tileToMoveTo] = pieceMoved;
	}
    
	private void FillBlacks(){
		foreach (KeyValuePair<GameObject, GameObject> kvp in _boardManager.GetBoard()){
			if(kvp.Value != null && kvp.Value.name[0] == 'B')
				blacks[kvp.Key] = kvp.Value;
		}
	}

}
