using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharStats : MonoBehaviour
{
    public float Health = 50f;
    public float base_Damage = 10;
    public GameObject other_Player;

    CharStats scrpt;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "HurtBox")
        {
            scrpt.Health -= base_Damage;
            print("Lost 10 HP now : " + scrpt.Health);
        }
        else if (other.name == "HitBox")
        {
            print("Bump");
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        scrpt = other_Player.GetComponent<CharStats>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
