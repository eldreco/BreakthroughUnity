using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState
{
    public int _valueAdvanced {get; private set;}
    public GameObject _pieceMoved {get; private set;}
    public GameObject _tileFrom {get; private set;}
    public GameObject _tileTo {get; private set;}

    private IDictionary<GameObject, GameObject> _board;

    public GameState(IDictionary<GameObject, GameObject> board, GameObject pieceMoved, GameObject tileFrom, GameObject tileTo){
        _board = board;
        _pieceMoved = pieceMoved;
        _tileFrom = tileFrom;
        _tileTo = tileTo;
        CaluculateValueAdvancedPlayer();
    }

    private void CaluculateValueAdvancedPlayer(){
		foreach (KeyValuePair<GameObject, GameObject> kvp in _board){
            if(kvp.Value != null){
                if(kvp.Value.name[0] == 'B'){
                    _valueAdvanced += 9 - int.Parse(char.ToString(kvp.Key.name[1]));
                }
            }
        }
    }
}
