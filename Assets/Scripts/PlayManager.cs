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

	
	//Constant values to move pieces
	private const float moveXValue = 0.535f;
	private const float moveYValue = 0.675f;
	
	protected void Start()
	{
		boardManager = GameObject.Find("Board").GetComponent<BoardManager>();
		board = boardManager.GetBoard();
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

	}
	
	public void PlayWhites(GameObject nextTile, List<GameObject> moves){
		GameObject tileFrom = boardManager.GetSelectedTile();
		for(int i = 0; i < moves.Count; i++){
			if(moves[i] == nextTile){
				MoveWhitePiece(piece,tileFrom ,nextTile);
			}
		}
		boardManager.SetSelectedTile(null);
		

	}
	
	private void MoveWhitePiece(GameObject pieceToMove, GameObject tileToMoveFrom ,GameObject tileToMoveTo){
		
		//Update Board
		boardManager.UpdateBoard(pieceToMove, tileToMoveTo);
		
		//Check which position to move to
		if(tileToMoveFrom.name[0] == tileToMoveTo.name[0])
			MoveFront(pieceToMove);
		else if(tileToMoveFrom.name[0] + 1 == tileToMoveTo.name[0])
			MoveFrontRight(pieceToMove);
		else if(tileToMoveFrom.name[0] - 1 == tileToMoveTo.name[0])
			MoveFrontLeft(pieceToMove);
			
		//Reset colors
		boardManager.SetAllBaseColor();
		pieceToMove.GetComponent<SpriteRenderer>().color = new Color(1,1,1,1);
		
		gameManager.LockPlay();
	}
	
	public void MoveBlackPiece(GameObject pieceToMove, GameObject tileToMoveFrom ,GameObject tileToMoveTo){
		
		//Update Board
		boardManager.UpdateBoard(pieceToMove, tileToMoveTo);
		
		//Check which position to move to
		if(tileToMoveFrom.name[0] == tileToMoveTo.name[0])
			MoveDown(pieceToMove);
		else if(tileToMoveFrom.name[0] + 1 == tileToMoveTo.name[0])
			MoveDownRight(pieceToMove);
		else if(tileToMoveFrom.name[0] - 1 == tileToMoveTo.name[0])
			MoveDownLeft(pieceToMove);
	}
	
	private void MoveDown(GameObject pieceToMove){
		Vector3 pos = new Vector3(pieceToMove.transform.position.x,
			pieceToMove.transform.position.y - moveYValue,
			pieceToMove.transform.position.z);
		pieceToMove.transform.position = pos;
	}
	
	private void MoveDownLeft(GameObject pieceToMove){
		Vector3 pos = new Vector3(pieceToMove.transform.position.x - moveXValue,
			pieceToMove.transform.position.y - moveYValue,
			pieceToMove.transform.position.z);
		pieceToMove.transform.position = pos;
	}
	
	private void MoveDownRight(GameObject pieceToMove){

		Vector3 pos = new Vector3(pieceToMove.transform.position.x + moveXValue,
			pieceToMove.transform.position.y - moveYValue,
			pieceToMove.transform.position.z);
		pieceToMove.transform.position = pos;
	}
	
	private void MoveFront(GameObject pieceToMove){
		Vector3 pos = new Vector3(pieceToMove.transform.position.x,
								  pieceToMove.transform.position.y + moveYValue,
								  pieceToMove.transform.position.z);
		pieceToMove.transform.position = pos;
	}
	
	private void MoveFrontLeft(GameObject pieceToMove){
		Vector3 pos = new Vector3(pieceToMove.transform.position.x - moveXValue,
								  pieceToMove.transform.position.y + moveYValue,
								  pieceToMove.transform.position.z);
		pieceToMove.transform.position = pos;
	}
	
	private void MoveFrontRight(GameObject pieceToMove){

		Vector3 pos = new Vector3(pieceToMove.transform.position.x + moveXValue,
								  pieceToMove.transform.position.y + moveYValue,
								  pieceToMove.transform.position.z);
		pieceToMove.transform.position = pos;
	}
	
	public void SetPlayPiece(GameObject playPiece){
		piece = playPiece;
	}
}
