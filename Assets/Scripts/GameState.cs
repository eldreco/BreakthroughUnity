using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState
{
    public int _valueMedium {get; private set;}
	public int _valueHard {get; private set;}
    public GameObject _pieceMoved {get; private set;}
    public GameObject _tileFrom {get; private set;}
    public GameObject _tileTo {get; private set;}

    private IDictionary<GameObject, GameObject> _board;

    public GameState(IDictionary<GameObject, GameObject> board, GameObject pieceMoved, GameObject tileFrom, GameObject tileTo){
        _board = board;
        _pieceMoved = pieceMoved;
        _tileFrom = tileFrom;
        _tileTo = tileTo;
	    CalculateValueMediumPlayer();
        CalculateValueHardPlayer();
    }

    private void CalculateValueMediumPlayer(){
        foreach (KeyValuePair<GameObject, GameObject> kvp in _board){
            if (kvp.Value != null){
                if (kvp.Value.name[0] == 'B')
                    _valueMedium += 9 - int.Parse(char.ToString(kvp.Key.name[1]));
            }
        } 
    }

	private void CalculateValueHardPlayer(){
		
		char nextPosCol1 = (char)((int)_tileTo.name[0] - 1);
		char nextPosCol2 = (char)((int)_tileTo.name[0] + 1);
		int nextPosRow = int.Parse(char.ToString(_tileTo.name[1])) - 1;
		string nextPos1 = nextPosCol1 + nextPosRow.ToString();
		string nextPos2 = nextPosCol2 + nextPosRow.ToString();
		string nextPos3 = _tileTo.name[0] + nextPosRow.ToString();
		string nextPos4 = _tileTo.name[0] + (nextPosRow + 1).ToString();
		string nextPos5 = _tileTo.name[0] + (nextPosRow + 2).ToString();
		
		if(_board[_tileTo] != null){
			_valueHard += 3000;
			Debug.Log("HEREEEEE" + _valueHard);
		}
		
		foreach (KeyValuePair<GameObject, GameObject> kvp in _board){
            if(kvp.Value != null){
	            if(kvp.Value.name[0] == 'B'){
					if(kvp.Key.name[0] == 'B' || kvp.Key.name[0] == 'D' || kvp.Key.name[0] == 'E' || kvp.Key.name[0] == 'G')
						_valueHard += 100;
		            _valueHard += (9 - int.Parse(char.ToString(kvp.Key.name[1]))) * 100;
		            if(GameObject.Find(nextPos3) != null &&  _board[GameObject.Find(nextPos3)] != null){
		            	if(_board[GameObject.Find(nextPos3)].name[0] == 'B')
		            		_valueHard += 100;
		            	if(GameObject.Find(nextPos4) != null &&  _board[GameObject.Find(nextPos4)] != null){
			            	if(_board[GameObject.Find(nextPos4)].name[0] == 'B')
				            	_valueHard += 1000;	
				            if(GameObject.Find(nextPos5) != null &&  _board[GameObject.Find(nextPos5)] != null){
					            if(_board[GameObject.Find(nextPos5)].name[0] == 'B')
						            _valueHard += 10000;	
				            }
		            	}
		            }
		            	
	            }
            }
		}
		
		
		bool couldBeEaten = false;
		bool eatenPosNotNulls1 = GameObject.Find(nextPos1) != null && _board[GameObject.Find(nextPos1)] != null;
		bool eatenPosNotNulls2 = GameObject.Find(nextPos2) != null && _board[GameObject.Find(nextPos2)] != null;
		if(eatenPosNotNulls1)
			couldBeEaten = _board[GameObject.Find(nextPos1)].name[0] == 'W';
		else if(eatenPosNotNulls2)
			couldBeEaten = _board[GameObject.Find(nextPos2)].name[0] == 'W';
			
		if(couldBeEaten)
			_valueHard -= 2000;
		
		Debug.Log(_valueHard);
    }
}
