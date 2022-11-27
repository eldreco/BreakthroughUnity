using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AIController : MonoBehaviour
{
	
	//References to other scripts
	private BoardManager boardManager;
	private GameManager gameManager;
	private PlayManager playManager;
	
	private IDictionary<GameObject, GameObject> blacks = new Dictionary<GameObject, GameObject>();
    // Start is called before the first frame update
    void Start()
    {
	    Setup();
    }
    
	private void Setup(){
		boardManager = GameObject.Find("Board").GetComponent<BoardManager>();
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		playManager = GameObject.Find("GameManager").GetComponent<PlayManager>();
	}
	
	
	public void Play(){
		if(gameManager.GetActiveState() == GameManager.GameState.PLAYBLACKS){
			ChooseMove();
			gameManager.LockPlay();
		}
	}
	
	private void ChooseMove(){
		ChooseRandomMove();
	}
	
	private void ChooseRandomMove(){
		IDictionary<GameObject, GameObject> board = boardManager.GetBoard();
		FillBlacks();
		GameObject pieceToMove = null;
		GameObject tileToMoveFrom = null;
		GameObject tileToMoveTo = null;
		List<GameObject> moves = new List<GameObject>();

		while(moves.Count == 0){
			GameObject key = blacks.ElementAt(Random.Range(0, blacks.Count)).Key;
			if(blacks[key] != null){
				tileToMoveFrom = key;
				pieceToMove = board[tileToMoveFrom];
				moves = boardManager.PossibleMovesBlacks(tileToMoveFrom);
			}
		}

		tileToMoveTo = moves[Random.Range(0,moves.Count)];

		UpdateBlacks(pieceToMove, tileToMoveFrom, tileToMoveTo);
		playManager.MoveBlackPiece(pieceToMove, tileToMoveFrom, tileToMoveTo);
	}
    
	private void UpdateBlacks(GameObject pieceMoved,GameObject tileToMoveFrom, GameObject tileToMoveTo){
		//Setting previous position to null
		blacks[tileToMoveFrom] = null;

		//Setting new position for piece
		Destroy(boardManager.GetBoard()[tileToMoveTo]);
		blacks[tileToMoveTo] = pieceMoved;
	}
    
	private void FillBlacks(){
		foreach (KeyValuePair<GameObject, GameObject> kvp in boardManager.GetBoard()){
			if(kvp.Value != null && kvp.Value.name[0] == 'B')
				blacks[kvp.Key] = kvp.Value;
		}
	}

}
