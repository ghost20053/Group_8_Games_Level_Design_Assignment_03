using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using TMPro;

public class ProximityChecker : MonoBehaviour
{
    public GameObject _targetObject;
    public float _alertDistance = 10.0f;
    public float _triggerDistance = 6.0f;
    public float _resetDistance = 8.0f;
    public float _resetTime = 2.5f;
    public float _effectTime = 2.0f;
    public bool _resetModeTimer = false;
    public bool _useDebugText = false;

    private Animator _anim;
    private bool _alert = false;
    private bool _triggered = false;
    private bool _resetting = false;
    private bool _effectsOn = false;
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

        // ## ALERT
        // if distance is less than alert distance, but greater than trigger distance, and we are not alerted, go into alert state.
        if (_dist < _alertDistance && _dist > _triggerDistance && !_alert)
        {
            _alert = true;
            _anim.SetBool("Alert", true);
        }
        // if we are already alert, and distance is greater than alert distance, go out of alert state.
        else if (_dist > _alertDistance && _alert)
        {
            _alert = false;
            _anim.SetBool("Alert", false);
        }

        //## TRIGGER
        // if distance is less than trigger distance and we are ready to trigger, set triggered to true, and start effects
        if (_dist < _triggerDistance && !_triggered)
        {
            _triggered = true;
            _effectsOn = true;
            _anim.SetBool("Triggered", true);
            _anim.SetBool("Armed", false);
            Invoke("WaitForEffect", _effectTime);
        }

        //## RESET
        // if triggered is true, and any effects have played
        // and we are using a resetModeTimer
        // and we are not in the middle of resetting, then start timer
        if (_triggered && !_effectsOn && _resetModeTimer && !_resetting)
        {
            Invoke("ResetCheckTimer", _resetTime);
            _resetting = true;
            _anim.SetBool("TimerResetting", true);
        }

        // if triggered is true, and any effects have played
        // and we are NOT using a resetModeTimer
        // if distance is more than reset distance, then re-arm the distance checker.
        if (_triggered && !_resetModeTimer && !_effectsOn)
        {
            if (_dist > _resetDistance)
            {
                ResetTrigger();
            }
        }
    }

    // after resetTime, set triggered to false again. This arms the distance checker.
    private void ResetCheckTimer()
    {
        ResetTrigger();
    }

    // in case effects need to play before we reset and arm the distance checker again.
    private void WaitForEffect()
    {
        _effectsOn = false;
    }

    // re-arms the distance checker
    private void ResetTrigger()
    {
        _triggered = false;
        _resetting = false;
        _anim.SetBool("Armed", true);
        _anim.SetBool("Triggered", false);
        _anim.SetBool("TimerResetting", false);
    }
}
