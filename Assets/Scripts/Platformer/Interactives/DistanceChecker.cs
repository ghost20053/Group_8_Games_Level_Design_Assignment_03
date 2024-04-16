using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using TMPro;

public class DistanceChecker : MonoBehaviour
{
    public GameObject _targetObject;
    //public float _alertDistance = 10.0f;
    public float _maxActiveDistance = 12.0f;
    public bool _useDebugText = false;

    private Animator _anim;
    private bool _active = false;
    private Vector2 _thisVect;
    private Vector2 _targetVect;
    private float _dist;

    //private TextMeshProUGUI _textAsset;
    private Text _textAsset;

    // Start is called before the first frame update
    void Start()
    {
        _anim = this.GetComponent<Animator>();
        //_textAsset = this.GetComponentInChildren<TextMeshProUGUI>();
        _textAsset = this.GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_useDebugText)
        {
            _textAsset.text = "";
        }
        
        // check distance from this transform pos, to target transform pos
        _thisVect = new Vector2(this.transform.position.x, this.transform.position.y);
        _targetVect = new Vector2(_targetObject.transform.position.x, _targetObject.transform.position.y);
        _dist = Vector2.Distance(_thisVect, _targetVect);

        if (_useDebugText)
        {
            _textAsset.text = (Mathf.Floor(_dist * 100) / 100).ToString();
        }

        // ## ON OR OFF
        // if distance is less than active distance, and we are not active, go into active state.
        if (!_active && _dist < _maxActiveDistance)
        {
            _active = true;
            _anim.SetBool("Active", true);

        }
        if (_active && _dist < _maxActiveDistance)
        {
            // DISTANCE EFFECT
            _anim.SetFloat("Distance", _dist);
        }
        // if distance is more than active distance, and we are active, go out of active state.
        else if (_active && _dist > _maxActiveDistance)
        {
            _active = false;
            _anim.SetBool("Active", false);

        }

    }

}
