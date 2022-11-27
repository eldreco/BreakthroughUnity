using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardManager : MonoBehaviour
{
	public List<GameObject> tiles;
	private IDictionary<GameObject, GameObject> board = new Dictionary<GameObject, GameObject>();
	public List<GameObject> whitePieces;
	public List<GameObject> blackPieces;

	
	private GameObject selectedTile;
	
	
	public void SetupBoard(){
		int counter = 0;
		foreach(GameObject i in tiles){
			if(counter < 16)
				board.Add(i, whitePieces[counter]);
			else if(counter > 47)
				board.Add(i, blackPieces[counter-48]);
			else{
				board.Add(i, null);
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
	
			foreach (KeyValuePair<GameObject, GameObject> kvp in board)
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
		if(board[pos].name[0] == 'B'){
			//Setup of the possible next positions
			char nextPosCol1 = (char)((int)pos.name[0] - 1);
			char nextPosCol2 = (char)((int)pos.name[0] + 1);
			int nextPosRow = int.Parse(char.ToString(pos.name[1])) - 1;
			string nextPos1 = pos.name[0] + nextPosRow.ToString();
			string nextPos2 = nextPosCol1 + nextPosRow.ToString();
			string nextPos3 = nextPosCol2 + nextPosRow.ToString();
		
			foreach (KeyValuePair<GameObject, GameObject> kvp in board)
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
		foreach (KeyValuePair<GameObject, GameObject> kvp in board)
		{
			if(piece == kvp.Value){
				selectedPos = kvp.Key;
			}
		}
		
		//Set the selected tile to later move the piece from that tile
		SetSelectedTile(selectedPos);
		
		//Light up the tiles of next possible moves
		List<GameObject> nextMoves = PossibleMovesWhites(selectedPos);
		foreach(GameObject obj in nextMoves)
			LightUpTile(obj);
	}
	
	public void UpdateBoard(GameObject pieceMoved, GameObject tileToMoveTo){
		GameObject tile = null;
		foreach (KeyValuePair<GameObject, GameObject> kvp in board){
			if(kvp.Value == pieceMoved){
				tile = kvp.Key;
			}
		}
		//Setting previous position to null
		board[tile] = null;

		//Setting new position for piece
		Destroy(board[tileToMoveTo]);
		board[tileToMoveTo] = pieceMoved;
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
		foreach (KeyValuePair<GameObject, GameObject> kvp in board)
		{
			SetBaseColorTile(kvp.Key);
		}
	}
	
	public IDictionary<GameObject, GameObject> GetBoard(){
		return board;
	}
	
	public void SetSelectedTile(GameObject tile){
		selectedTile = tile;
	}
	
	public GameObject GetSelectedTile(){
		return selectedTile;
	}

}

