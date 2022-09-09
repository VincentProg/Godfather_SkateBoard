using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerCol : MonoBehaviour
{
    //PlayerInput myInputs;
    //private Vector2 movementInput;

    private HoverController controller;
    private CharStats controllerStats;
    private CharStats charStats;
    private Rigidbody rb;

    Vector3 _dirToP1 = Vector3.zero;

    public GameObject OtherPlayer;
    public GameObject OtherHurtBox;
    Rigidbody otherRb;

    [Range(0f, 200f)] public float Force_To_OP = 100;
    [Range(0f, 200f)] public float Wallbounce_OnHit = 100;
    [Range(0f, 200f)] public float Wallbounce_unpressed = 100;
    public float Bounce_React = 5f;

    [Range(.1f, 1.5f)] public float min_Speed_To_Bounce;
    [Range(.01f, 2f)] public float Max_Time_To_React = .5f;
    public float Resting_Time = 3f;

    bool dontbounce = false;
    bool hitIt = false;

    bool Isresting = false;
    bool cdrest = false;

    Vector3 _Velgo;
    private void Start()
    {
        //myInputs = GetComponent<PlayerInput>();
        charStats = GetComponent<CharStats>();
        rb = GetComponent<Rigidbody>();        
        controller = GetComponent<HoverController>();
        controllerStats = GetComponent<CharStats>();
    }

    public void GetOtherPlayer(GameObject other)
    {
        OtherPlayer = other;
        otherRb = OtherPlayer.GetComponent<Rigidbody>();
    }
    //public void OnMove(InputAction.CallbackContext context)
    //{
    //    movementInput = context.ReadValue<Vector2>();
    //}
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Wall"))
        {
            dontbounce = true;
        }
        
        if (col.gameObject == OtherHurtBox)
        {
            if (!Isresting) { 
                charStats.TakeDamage(charStats.base_Damage, rb.velocity.magnitude / 5f);

            }
            Isresting = true;
            EjectOP();
        }

    }
    private void OnTriggerExit(Collider col)
    {
        dontbounce = false;
    }
    private void OnCollisionEnter(Collision col)
    {
        print(rb.velocity.magnitude);
        
        if (col.gameObject.CompareTag("Wall") && !dontbounce && rb.velocity.magnitude >= min_Speed_To_Bounce)
        {
            Vector3 Newdir = Vector3.Reflect(_Velgo, col.contacts[0].normal);
            if (hitIt)
            {
                Reflect(Newdir, Wallbounce_OnHit);
            }else
            {
                Reflect(Newdir, Wallbounce_unpressed);

            }
        }
    }

    void Reflect(Vector3 dir, float _percent)
    {
        if (hitIt)
        {
            Debug.Log("HIIIIIIIIIIIIIIIIT");
        }
        controller.PlayBumpAnim();
        rb.velocity = Vector3.zero;
        rb.AddForce(dir * (_percent/100), ForceMode.VelocityChange);
        transform.rotation = Quaternion.LookRotation(dir, Vector3.up);
    }
    void EjectOP()
    {
        //controllerStats.DOSOMETHINGS;
        controller.PlayBumpAnim();
        otherRb.AddForce(rb.velocity * (Force_To_OP / 100), ForceMode.VelocityChange);
        rb.velocity = (rb.velocity - rb.velocity*.7f);
    }

    void Update()
    {
        if (Isresting) 
        {
            StartCoroutine(Resting());
            if (!cdrest)
            {
                StartCoroutine(CDrest());
            }

        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            StopCoroutine(Cd_to_bounce());
            StartCoroutine(Cd_to_bounce());
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            charStats.AddHealth(100);
        }
        _Velgo = rb.velocity;//NE PAS ENLEVER A GARDER !!!
        Debug.DrawRay(transform.position, rb.velocity);
    }

    IEnumerator CDrest()
    {
        cdrest = true;
        OtherPlayer?.transform.GetChild(0).gameObject.SetActive(false);
        yield return new WaitForSeconds(.5f);
        OtherPlayer?.transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(.5f);
        cdrest = false;
    }
    IEnumerator Resting()
    {
        yield return new WaitForSeconds(Resting_Time);
        Isresting = false;
    }

    IEnumerator Cd_to_bounce()
    {
        hitIt = true;
        yield return new WaitForSeconds(Max_Time_To_React);
        hitIt = false;
    }
}
