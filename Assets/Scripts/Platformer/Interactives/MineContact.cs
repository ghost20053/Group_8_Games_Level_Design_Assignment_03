using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineContact : MonoBehaviour
{
    public GameObject _health;
    public float _detonateTimer = 0.3f;
    public GameObject _explodeFX;

    private Animator _anim;
    private bool _triggered = false;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
    }


    void Triggered()
    {
        if (!_triggered)
        {
            _triggered = true;
            Invoke("Detonate", _detonateTimer);
            _anim.SetBool("Triggered", true);
        }
    }
    void Detonate()
    {
        _health.SendMessage("SubtractMineDamage");
        Instantiate(_explodeFX, transform.position, transform.rotation);
        //Debug.Log("BOOM!");
        Destroy(this.gameObject);
    }
}
