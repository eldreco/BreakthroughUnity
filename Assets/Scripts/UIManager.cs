using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    [SerializeField] private GameObject _top;
    [SerializeField] private GameObject _bottom;
    [SerializeField] private GameObject _canvas;

    private Animator _anim;

    private void Awake() {
        _canvas.SetActive(true);
        _anim = _canvas.GetComponent<Animator>();
    }

    public void GoAnim(){
        _anim.SetTrigger("GoAnim");
    }

    public IEnumerator SetYourGoAnim(){
        yield return new WaitForSeconds(1);
        _anim.SetInteger("PlayerGo", 1);
        yield return new WaitForSeconds(1);
        _canvas.SetActive(false);
    }

    public IEnumerator SetOppGoAnim(){
        yield return new WaitForSeconds(1);
        _anim.SetInteger("PlayerGo", -1);
        yield return new WaitForSeconds(1);
        _canvas.SetActive(false);
    }
}
