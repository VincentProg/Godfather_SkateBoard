using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharStats : MonoBehaviour
{
    public bool Debugmod;

    public float Health = 50f;
    public float base_Damage = 10;

    GameObject _parentGO;
    Rigidbody _parentRB;

    Rigidbody _parentRbP2;

    public GameObject _hitboxP2;
    public GameObject _hurtboxP2;
    CharStats Enemy_stats;

    Vector3 _dirToP2;
    void Start()
    {
        Enemy_stats = _hitboxP2.GetComponent<CharStats>();
        _parentGO = transform.parent.gameObject;
        _parentRB = _parentGO.GetComponent<Rigidbody>();

        _parentRbP2 = _hitboxP2.transform.parent.gameObject.GetComponent<Rigidbody>();

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "HurtBox")
        {
            DamageP2(base_Damage);
            EjectP2();
        }
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "HitBox")
        {
            EjectP2();
        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            print(collision.contacts[0].normal);
            EjectMe();
        }
    }
    void DamageP2(float damage)
    {
        Enemy_stats.Health -= damage;
        print("Lost " + damage + " HP now : " + Enemy_stats.Health);
    }
    void EjectP2()
    {
        _parentRbP2.AddForce(_dirToP2 * -2f, ForceMode.Impulse);
        print("Bump");
    }
    void EjectMe()
    {
        //_parentRB.AddForce();
        print("BumpMe");
    }

    // Update is called once per frame
    void Update()
    {
        _dirToP2 = transform.position - _hurtboxP2.transform.position;

        if (Debugmod)
        {
            Debug.DrawLine(transform.position, _hurtboxP2.transform.position);
            /*print(_dirToP2);*/
        }
    }
}
