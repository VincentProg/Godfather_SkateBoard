using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharStats : MonoBehaviour
{
    public bool Debugmod;

    [Range(0f,200f)]public int gaindspeed = 100;
    public float Health = 50f;
    public float base_Damage = 10;

    GameObject _parentGO;
    Rigidbody _parentRB;

    Rigidbody _parentRbP2;

    public GameObject _hitboxP2;
    public GameObject _hurtboxP2;
    CharStats Enemy_stats;

    Vector3 _dirToP1;
    Vector3 _velGo;
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
            Vector3 Newdir = Vector3.Reflect(_velGo, collision.contacts[0].normal);
            _parentRB.velocity = Vector3.zero;
            _parentRB.AddForce(Newdir * (gaindspeed / 100f), ForceMode.Impulse);

            _parentGO.transform.rotation = Quaternion.LookRotation(Newdir, Vector3.up);
            _parentGO.transform.eulerAngles += new Vector3(0, -90, 0);
            print(_parentRB);
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
        _parentRB.velocity = Vector3.zero;
        _parentRbP2.AddForce(-_dirToP1 * 20f, ForceMode.Impulse);
        print("Bump to " + -_dirToP1);
        
    } 
    void EjectMe()
    {
        //_parentRB.AddForce();
        print("BumpMe");
    }

    // Update is called once per frame
    void Update()
    {
        

        Debug.DrawRay(transform.position, _velGo);
        _dirToP1 = transform.position - _hurtboxP2.transform.position + new Vector3(0, -0f,0);
        _velGo = _parentRB.velocity;

        if (Debugmod)
        {
            Debug.DrawLine(transform.position, _hurtboxP2.transform.position);
            /*print(_dirToP1);*/
        }
    }

}
