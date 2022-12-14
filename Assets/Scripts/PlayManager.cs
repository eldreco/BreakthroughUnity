using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayManager : MonoBehaviour
{
	private GameObject _piece;
	
	[SerializeField] private Sprite _blackPieceSprite;

	//References to other classes or objects
	private GameManager _gameManager;
	private BoardManager _boardManager;
	private InputManager _inputManager;
	private AudioPlayer _audioPlayer;
	private IDictionary<GameObject, GameObject> _board;
	
	private void Awake() {
        _boardManager = GameObject.Find("Board").GetComponent<BoardManager>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		_inputManager = gameObject.GetComponent<InputManager>();
		_audioPlayer = gameObject.GetComponent<AudioPlayer>();
    }

    protected void Start()
    {
		_board = _boardManager.GetBoard();
	}
	
	public void PlayWhites(GameObject nextTile){
		GameObject tileFrom = _boardManager.GetSelectedTile();
		List<GameObject> moves = _boardManager.PossibleMovesWhites(tileFrom);
		for(int i = 0; i < moves.Count; i++){
			if(moves[i] == nextTile){
				MoveWhitePiece(_piece,tileFrom ,nextTile);
				_boardManager.SetSelectedTile(null);
			}
		}
	}
	
	public void PlayBlacks(GameObject nextTile){
		GameObject tileFrom = _boardManager.GetSelectedTile();
		List<GameObject> moves = _boardManager.PossibleMovesBlacks(tileFrom);
		for(int i = 0; i < moves.Count; i++){
			if(moves[i] == nextTile){
				MoveBlackPiece(_piece,tileFrom ,nextTile);
				_boardManager.SetSelectedTile(null);
			}
		}
	}

	private void MoveWhitePiece(GameObject pieceToMove, GameObject tileToMoveFrom ,GameObject tileToMoveTo){
		
		//Update Board
		_boardManager.UpdateBoard(pieceToMove, tileToMoveTo);
		
		//Move piece
		Vector3 pos = new Vector3(tileToMoveTo.transform.position.x,
			tileToMoveTo.transform.position.y,
			pieceToMove.transform.position.z);
		pieceToMove.transform.position = pos;
			
		//Reset colors
		_boardManager.SetAllBaseColor();
		pieceToMove.GetComponent<SpriteRenderer>().color = new Color(1,1,1,1);
		
		_boardManager.SendBlacksFront();
		_gameManager.LockPlay();

		_audioPlayer.PlayAudioWhiteMove();
	}
	
	public void MoveBlackPiece(GameObject pieceToMove, GameObject tileToMoveFrom ,GameObject tileToMoveTo){
		
		//Update Board
		_boardManager.UpdateBoard(pieceToMove, tileToMoveTo);
		
		//Move piece 
		Vector3 pos = new Vector3(tileToMoveTo.transform.position.x,
			tileToMoveTo.transform.position.y,
			pieceToMove.transform.position.z);
		pieceToMove.transform.position = pos;

		_boardManager.SetAllBaseColor();
		pieceToMove.GetComponent<SpriteRenderer>().sprite = _blackPieceSprite;

		_boardManager.SendWhitesFront();
		_gameManager.LockPlay();

		_audioPlayer.PlayAudioBlackMove();
	}
	
	public void SetPlayPiece(GameObject playPiece){
		_piece = playPiece;
	}
}
