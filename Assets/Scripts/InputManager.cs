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
	
	private void Awake()
	{
		_boardManager = GameObject.Find("Board").GetComponent<BoardManager>();
		_playManager = gameObject.GetComponent<PlayManager>();
		_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		if(GameObject.Find("MainManager") != null)
			_mainManager = GameObject.Find("MainManager").GetComponent<MainManager>();
        if (_mainManager != null)
            _gameMode = _mainManager._mode;
        else
            _gameMode = "TwoPlayers";
		//Input Actions
		PlayerInputActions playerInput = new PlayerInputActions();
		playerInput.Play.Enable();
		playerInput.Play.SelectPiece.performed += _ => SelectPiece();
		playerInput.Play.SelectTile.performed += _ => SelectTile();
	}

	public void SelectPiece(){
		Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position); //Input.touches[0].position for mobile and Input.mousePosition for PC
        RaycastHit hit;

		if (Physics.Raycast(ray, out hit)){

			bool shouldSelectWhitePiece = _gameManager.GetActiveState() == GameManager.GameState.PLAYWHITES && 
										  hit.transform.gameObject.name == "White";
			bool shouldSelectBlackPiece = _gameManager.GetActiveState() == GameManager.GameState.PLAYBLACKS &&
									      _gameMode == "TwoPlayers" && hit.transform.gameObject.name[0] == 'B' && 
										  hit.transform.gameObject.name[1] == 'l';
			if(shouldSelectWhitePiece)
				SelectWhitePiece(hit);
			else if(shouldSelectBlackPiece)
				SelectBlackPiece(hit);
		}
	}
	
	private void SelectWhitePiece(RaycastHit hit){
        _piece = hit.transform.gameObject;
        _pieceSprite = _piece.GetComponent<SpriteRenderer>();

        //Highlight selected piece
        _pieceSprite.color = new Color(1, 1, 0, 1);

        //Change color of previously selected pieces back to normal color
        if (_previousPiece != null && _previousPiece.name[0] == 'W' && _piece != _previousPiece)
            _previousPieceSprite.color = _baseColor;
        _previousPiece = _piece;
        _previousPieceSprite = _pieceSprite;
        _boardManager.ChoosePiece(_piece);
		_playManager.SetPlayPiece(_piece);
		AudioPlayer.Instance.PlayAudioSelectPiece();
	}

	private void SelectBlackPiece(RaycastHit hit){
        _piece = hit.transform.gameObject;
        _pieceSprite = _piece.GetComponent<SpriteRenderer>();

        //Highlight selected piece
        _pieceSprite.sprite = _blackPieceSelectedSprite;

        //Change color of previously selected pieces back to normal color
        if (_previousPiece != null && _previousPiece.name[0] == 'B' && _piece != _previousPiece)
            _previousPieceSprite.sprite = _blackPieceSprite;
        _previousPiece = _piece;
        _previousPieceSprite = _pieceSprite;
        _boardManager.ChoosePiece(_piece);
		_playManager.SetPlayPiece(_piece);
		AudioPlayer.Instance.PlayAudioSelectPiece();
        
	}

    public void SelectTile(){
        GameObject tile = null;
        //Get object clicked
	    Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position); //Input.touches[0].position for mobile and Input.mousePosition for PC
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
