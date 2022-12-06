using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
	private GameObject _piece;
	private GameObject _previousPiece;
	private SpriteRenderer _pieceSprite;
	private SpriteRenderer _previousPieceSprite;
	private Color _baseColor = new Color(1,1,1,1); //Change
	
	//References to other scripts
	private BoardManager _boardManager; 
	private PlayManager _playManager;
	private GameManager _gameManager;
	
	protected void Awake()
	{
		//Input Actions
		PlayerInputActions playerInput = new PlayerInputActions();
		playerInput.Play.Enable();
		playerInput.Play.SelectPiece.performed += _ => SelectPiece();
		playerInput.Play.SelectTile.performed += _ => SelectTile();
		

	}
	
	// Start is called on the frame when a script is enabled just before any of the Update methods is called the first time.
	protected void Start()
	{
		//Other scripts
		_boardManager = GameObject.Find("Board").GetComponent<BoardManager>();
		_playManager = gameObject.GetComponent<PlayManager>();
		_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}
	
	public void SelectPiece(){
		if(_gameManager.GetActiveState() == GameManager.GameState.PLAYWHITES){
			//Get object clicked
			Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit)){
				if(hit.transform.gameObject.name == "White"){ //Check if it's a white piece
					_piece = hit.transform.gameObject;
					_pieceSprite = _piece.GetComponent<SpriteRenderer>();
					
					//Highlight selected piece
					_pieceSprite.color = new Color (1,1,0,1);
					
					//Change color of previously selected pieces back to normal color
					if(_previousPiece != null && _piece != _previousPiece)
						_previousPieceSprite.color = _baseColor;
					_previousPiece = _piece;
					_previousPieceSprite = _pieceSprite;
					
					//Call boardManager to display possible moves
					_boardManager.ChoosePiece(_piece);
					
					//Call playManager to set playPiece
					_playManager.SetPlayPiece(_piece);
				}
			}
		}
	}
	
	public void SelectTile(){
		if(_gameManager.GetActiveState() == GameManager.GameState.PLAYWHITES){
			GameObject tile = null;
			
			//Get object clicked
			Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit)){
				if(hit.transform.gameObject.tag == "Tile"){//Check if it's a tile		
					tile = hit.transform.gameObject;
					
					//Call playManager to move the piece to the new selected tile
					_playManager.PlayWhites(tile);
				}
			}
		}
	}
}
