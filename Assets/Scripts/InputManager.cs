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
	private MainManager _mainManager;

	[SerializeField] private string _gameMode;
	[SerializeField] private Sprite _blackPieceSprite;
	[SerializeField] private Sprite _blackPieceSelectedSprite;
	
	protected void Awake()
	{
		_boardManager = GameObject.Find("Board").GetComponent<BoardManager>();
		_playManager = gameObject.GetComponent<PlayManager>();
		_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		if(GameObject.Find("MainManager") != null)
			_mainManager = GameObject.Find("MainManager").GetComponent<MainManager>();
		//Input Actions
		PlayerInputActions playerInput = new PlayerInputActions();
		playerInput.Play.Enable();
		playerInput.Play.SelectPiece.performed += _ => SelectPiece();
		playerInput.Play.SelectTile.performed += _ => SelectTile();
	}
	
	private void Start() {
		if(_mainManager != null)
			_gameMode = _mainManager._mode;
		else
			_gameMode = "TwoPlayers";
	}

	public void SelectPiece(){
		if(_gameManager.GetActiveState() == GameManager.GameState.PLAYWHITES)
			SelectWhitePiece();
		else if(_gameManager.GetActiveState() == GameManager.GameState.PLAYBLACKS && _gameMode == "TwoPlayers")
			SelectBlackPiece();
	}
	
	public void SelectWhitePiece(){
        //Get object clicked
        Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)){
            if (hit.transform.gameObject.name == "White"){ //Check if it's a white piece
                _piece = hit.transform.gameObject;
                _pieceSprite = _piece.GetComponent<SpriteRenderer>();

                //Highlight selected piece
                _pieceSprite.color = new Color(1, 1, 0, 1);

                //Change color of previously selected pieces back to normal color
                if (_previousPiece != null && _previousPiece.name[0] == 'W' && _piece != _previousPiece)
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

	private void SelectBlackPiece(){
        Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)){
            if (hit.transform.gameObject.name[0] == 'B' && hit.transform.gameObject.name[1] == 'l'){ //Check if it's a black piece
                _piece = hit.transform.gameObject;
                _pieceSprite = _piece.GetComponent<SpriteRenderer>();

                //Highlight selected piece
                _pieceSprite.sprite = _blackPieceSelectedSprite;


                //Change color of previously selected pieces back to normal color
                if (_previousPiece != null && _previousPiece.name[0] == 'B' && _piece != _previousPiece)
                    _previousPieceSprite.sprite = _blackPieceSprite;
                _previousPiece = _piece;
                _previousPieceSprite = _pieceSprite;

                //Call boardManager to display possible moves
                _boardManager.ChoosePiece(_piece);

                //Call playManager to set playPiece
                _playManager.SetPlayPiece(_piece);
            }
        }
	}

    public void SelectTile(){
        GameObject tile = null;

        //Get object clicked
        Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)){
            if (hit.transform.gameObject.tag == "Tile"){//Check if it's a tile		
                tile = hit.transform.gameObject;

                if (_gameManager.GetActiveState() == GameManager.GameState.PLAYWHITES)
                    _playManager.PlayWhites(tile);
                else if (_gameManager.GetActiveState() == GameManager.GameState.PLAYBLACKS && _gameMode == "TwoPlayers")
                    _playManager.PlayBlacks(tile);
            }
        }

    }
}
