using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
	private GameObject piece;
	private GameObject previousPiece;
	private SpriteRenderer pieceSprite;
	private SpriteRenderer previousPieceSprite;
	private Color baseColor = new Color(1,1,1,1); //Change
	
	//References to other scripts
	private BoardManager boardManager; 
	private PlayManager playManager;
	private GameManager gameManager;
	
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
		boardManager = GameObject.Find("Board").GetComponent<BoardManager>();
		playManager = gameObject.GetComponent<PlayManager>();
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}
	
	public void SelectPiece(){
		if(gameManager.GetActiveState() == GameManager.GameState.PLAYWHITES){
			//Get object clicked
			Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit)){
				if(hit.transform.gameObject.name == "White"){ //Check if it's a white piece
					piece = hit.transform.gameObject;
					pieceSprite = piece.GetComponent<SpriteRenderer>();
					
					//Highlight selected piece
					pieceSprite.color = new Color (1,1,0,1);
					
					//Change color of previously selected pieces back to normal color
					if(previousPiece != null && piece != previousPiece)
						previousPieceSprite.color = baseColor;
					previousPiece = piece;
					previousPieceSprite = pieceSprite;
					
					//Call boardManager to display possible moves
					boardManager.ChoosePiece(piece);
					
					//Call playManager to set playPiece
					playManager.SetPlayPiece(piece);
				}
			}
		}
	}
	
	public void SelectTile(){
		if(gameManager.GetActiveState() == GameManager.GameState.PLAYWHITES){
			GameObject tile = null;
			
			//Get object clicked
			Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit)){
				if(hit.transform.gameObject.tag == "Tile"){//Check if it's a tile		
					tile = hit.transform.gameObject;
					
					//Call playManager to move the piece to the new selected tile
					playManager.PlayWhites(tile);
				}
			}
		}
	}
}
