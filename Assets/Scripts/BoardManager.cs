﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardManager : MonoBehaviour
{
	[SerializeField] private List<GameObject> _tiles;
	[SerializeField] private List<GameObject> _whitePieces;
	[SerializeField] private List<GameObject> _blackPieces;
	private IDictionary<GameObject, GameObject> _board = new Dictionary<GameObject, GameObject>();
	private GameObject _pieces;

	private GameObject _selectedTile;
	
	private void Start() {
		_pieces = GameObject.Find("Pieces");
	}

	public void SetupBoard(){
		int counter = 0;
		foreach(GameObject i in _tiles){
			if(counter < 16)
				_board.Add(i, _whitePieces[counter]);
			else if(counter > 47)
				_board.Add(i, _blackPieces[counter-48]);
			else{
				_board.Add(i, null);
			}
			counter++;
		}
	}
	
	
	public List<GameObject> PossibleMovesWhites(GameObject pos){
		List<GameObject> moves = new List<GameObject>();
		if(GetSelectedTile() != null){
			//Setup of the possible next positions
			char nextPosCol1 = (char)((int)pos.name[0] + 1);
			char nextPosCol2 = (char)((int)pos.name[0] - 1);
			int nextPosRow = int.Parse(char.ToString(pos.name[1])) + 1;
			string nextPos1 = pos.name[0] + nextPosRow.ToString();
			string nextPos2 = nextPosCol1 + nextPosRow.ToString();
			string nextPos3 = nextPosCol2 + nextPosRow.ToString();
	
			foreach (KeyValuePair<GameObject, GameObject> kvp in _board)
			{
				//Check for each of the three possible positions
				if(kvp.Key.name == nextPos1 || kvp.Key.name == nextPos2 || kvp.Key.name == nextPos3){
					//Check if there is either no piece or an enemy piece in diagonals
					if(kvp.Value == null)
						moves.Add(kvp.Key);
					else if(kvp.Value.name[0] != 'W' && kvp.Key.name != nextPos1)
						moves.Add(kvp.Key);				
				}
			}
		}
		return moves;
	}
	
	public List<GameObject> PossibleMovesBlacks(GameObject pos){
		List<GameObject> moves = new List<GameObject>();
		if(_board[pos] == null)
			return moves;
		if(_board[pos].name[0] == 'B'){
			//Setup of the possible next positions
			char nextPosCol1 = (char)((int)pos.name[0] - 1);
			char nextPosCol2 = (char)((int)pos.name[0] + 1);
			int nextPosRow = int.Parse(char.ToString(pos.name[1])) - 1;
			string nextPos1 = pos.name[0] + nextPosRow.ToString();
			string nextPos2 = nextPosCol1 + nextPosRow.ToString();
			string nextPos3 = nextPosCol2 + nextPosRow.ToString();
		
			foreach (KeyValuePair<GameObject, GameObject> kvp in _board)
			{
				//Check for each of the three possible positions
				if(kvp.Key.name == nextPos1 || kvp.Key.name == nextPos2 || kvp.Key.name == nextPos3){
					//Check if there is either no piece or an enemy piece in diagonals
					if(kvp.Value == null)
						moves.Add(kvp.Key);
					else if(kvp.Value.name[0] != 'B' && kvp.Key.name != nextPos1)
						moves.Add(kvp.Key);				
				}
			}
		}
		
		return moves;
	}
	
	public void ChoosePiece(GameObject piece){
		GameObject selectedPos = null;
		SetAllBaseColor();
		foreach (KeyValuePair<GameObject, GameObject> kvp in _board)
		{
			if(piece == kvp.Value){
				selectedPos = kvp.Key;
			}
		}
		
		//Set the selected tile to later move the piece from that tile
		SetSelectedTile(selectedPos);

		List<GameObject> nextMoves = new List<GameObject>();
		//Light up the tiles of next possible moves
		if(piece.name[0] == 'W')
			nextMoves = PossibleMovesWhites(selectedPos);
		else if(piece.name[0] == 'B')
			nextMoves = PossibleMovesBlacks(selectedPos);
		foreach(GameObject obj in nextMoves)
			LightUpTile(obj);
		SendPiecesBack();
	}
	
	public void UpdateBoard(GameObject pieceMoved, GameObject tileToMoveTo){
		GameObject tile = null;
		foreach (KeyValuePair<GameObject, GameObject> kvp in _board){
			if(kvp.Value == pieceMoved){
				tile = kvp.Key;
			}
		}
		//Setting previous position to null
		_board[tile] = null;

		//Setting new position for piece
		Destroy(_board[tileToMoveTo]);
		_board[tileToMoveTo] = pieceMoved;
	}
	
	private void LightUpTile(GameObject tile){
		Image sprite = tile.GetComponent<Image>();
		Color lightUpColor = new Color(sprite.color.r, sprite.color.g, 0.5f, sprite.color.a);
		sprite.color = lightUpColor;
	}
	
	private void SetBaseColorTile(GameObject tile){
		if(tile != null){
			Image sprite = tile.GetComponent<Image>();
			Color baseColor = sprite.color.r == 1 ? new Color(sprite.color.r, sprite.color.g, 1, sprite.color.a) : new Color(sprite.color.r, sprite.color.g, 0.8f, sprite.color.a);
			sprite.color = baseColor;
		}
	}
	
	public void SetAllBaseColor(){
		foreach (KeyValuePair<GameObject, GameObject> kvp in _board)
		{
			SetBaseColorTile(kvp.Key);
		}
	}
	
	public void SendPiecesFront(){
		_pieces.transform.position = new Vector3(_pieces.transform.position.x,
												 _pieces.transform.position.y,
												 0);
	}

	public void SendPiecesBack(){
		_pieces.transform.position = new Vector3(_pieces.transform.position.x,
												 _pieces.transform.position.y,
												 100f);
	}

	public IDictionary<GameObject, GameObject> GetBoard(){
		return _board;
	}
	
	public void SetSelectedTile(GameObject tile){
		_selectedTile = tile;
	}
	
	public GameObject GetSelectedTile(){
		return _selectedTile;
	}

}

