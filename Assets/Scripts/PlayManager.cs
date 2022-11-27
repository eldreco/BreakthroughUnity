using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayManager : MonoBehaviour
{
	private GameObject piece;
	
	//References to other classes or objects
	private GameManager gameManager;
	private BoardManager boardManager;
	private IDictionary<GameObject, GameObject> board;
	
	protected void Start()
	{
		boardManager = GameObject.Find("Board").GetComponent<BoardManager>();
		board = boardManager.GetBoard();
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}
	
	public void PlayWhites(GameObject nextTile){
		GameObject tileFrom = boardManager.GetSelectedTile();
		List<GameObject> moves = boardManager.PossibleMovesWhites(tileFrom);
		for(int i = 0; i < moves.Count; i++){
			if(moves[i] == nextTile){
				MoveWhitePiece(piece,tileFrom ,nextTile);
				boardManager.SetSelectedTile(null);
			}
		}
	}
	
	private void MoveWhitePiece(GameObject pieceToMove, GameObject tileToMoveFrom ,GameObject tileToMoveTo){
		
		//Update Board
		boardManager.UpdateBoard(pieceToMove, tileToMoveTo);
		
		//Move piece
		Vector3 pos = new Vector3(tileToMoveTo.transform.position.x,
			tileToMoveTo.transform.position.y,
			pieceToMove.transform.position.z);
		pieceToMove.transform.position = pos;
			
		//Reset colors
		boardManager.SetAllBaseColor();
		pieceToMove.GetComponent<SpriteRenderer>().color = new Color(1,1,1,1);
		
		gameManager.LockPlay();
	}
	
	public void MoveBlackPiece(GameObject pieceToMove, GameObject tileToMoveFrom ,GameObject tileToMoveTo){
		
		//Update Board
		boardManager.UpdateBoard(pieceToMove, tileToMoveTo);
		
		//Move piece 
		Vector3 pos = new Vector3(tileToMoveTo.transform.position.x,
			tileToMoveTo.transform.position.y,
			pieceToMove.transform.position.z);
		pieceToMove.transform.position = pos;
	}
	
	public void SetPlayPiece(GameObject playPiece){
		piece = playPiece;
	}
}
