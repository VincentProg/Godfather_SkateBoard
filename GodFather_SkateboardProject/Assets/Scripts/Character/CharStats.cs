using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharStats : MonoBehaviour
{
    //public bool Debugmod;

    public float Health = 100f;
    public float base_Damage = 10;

    public Image _myLifeBar;
    private Animator anim;
    HoverController player;

    void Start()
    {
        player = GetComponent<HoverController>();
    }
    public void TakeDamage(float damage, float Vel)
    {
        float Totdamage = damage * Vel;
        Health -= Totdamage;
        print("Hit : " + _myLifeBar.transform.parent.name + ", Taken : " + Totdamage + ", Now at : " + Health + "%");
        player.playDamagesAnim(Health);


        
    }
    public void AddHealth(float heatlh)
    {
        Health += heatlh;
        if(Health >= 100) { Health = 100; }
        print("Hit : " + _myLifeBar.transform.parent.name + ", Add : " + heatlh + ", Now at : " + Health + "%");

    }
    public void SetHealth(float heatlh)
    {
        if (heatlh >= 100) { heatlh = 100; }
        Health = heatlh;
        print("Set : " + _myLifeBar.transform.parent.name + ", Now at : " + Health + "%");

    }
    private void Update()
    {
        _myLifeBar.fillAmount = Health/100f;
    }
}
