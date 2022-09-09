using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionCol : MonoBehaviour
{
    private HoverController controller;
    private CharStats controllerStats;

    Vector3 _dirToP1 = Vector3.zero;

    [Range(0f, 200f)] public int _impulseStrenght = 100;
    private void Start()
    {
        controller = transform.parent.GetComponent<HoverController>();
        controllerStats = transform.parent.GetComponent<CharStats>();
    }
    private void Update()
    {
        transform.rotation = controller.transform.rotation;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "HurtBox")
        {
            DamageP2(controllerStats.base_Damage);
            EjectP2();
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "HitBox" && collision.gameObject != gameObject)
        {
            Debug.Log(collision.gameObject.name + " && " + gameObject.name);
            //EjectP2();
        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            print(collision.contacts[0].normal);
            Vector3 Newdir = Vector3.Reflect(controller.rb.velocity, collision.contacts[0].normal);
            controller.rb.velocity = Vector3.zero;
            controller.rb.AddForce(Newdir * (_impulseStrenght / 100f), ForceMode.Impulse);

            controller.rb.AddTorque(180 * Time.fixedDeltaTime * transform.up, ForceMode.Impulse);
            //transform.rotation = Quaternion.LookRotation(Newdir, Vector3.up);
            //controller.transform.eulerAngles += new Vector3(0, -90, 0);
            print(controller.rb);
        }
    }

    void DamageP2(float damage)
    {
        controller.player2.gameObject.GetComponent<CharStats>().Health -= damage;
        print("Lost " + damage + " HP now : " + controller.player2.gameObject.GetComponent<CharStats>().Health);
    }
    void EjectP2()
    {
        controller.rb.velocity = Vector3.zero;
        _dirToP1 = transform.position - controller.player2.player2Collisions.transform.position + new Vector3(0, -0f, 0);

        controller.player2.rb.AddForce(-_dirToP1 * 20f, ForceMode.Impulse);
        print("Bump to " + -_dirToP1);

    }
}
