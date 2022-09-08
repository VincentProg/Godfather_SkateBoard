using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerCol : MonoBehaviour
{
    PlayerInput myInputs;
    private Vector2 movementInput;

    private HoverController controller;
    private CharStats controllerStats;
    Rigidbody rb;

    Vector3 _dirToP1 = Vector3.zero;

    public GameObject OtherPlayer;
    public GameObject OtherHurtBox;
    Rigidbody otherRb;

    [Range(0f, 200f)] public float Force_To_OP = 100;
    [Range(0f, 200f)] public float Force_WallBounce = 100;
    [Range(.3f, 1.5f)] public float min_Speed_To_Bounce;
    bool dontbounce = false;
    bool hitIt = false;

    Vector3 _Velgo;
    private void Start()
    {
        myInputs = GetComponent<PlayerInput>();

        rb = GetComponent<Rigidbody>();
        otherRb = OtherPlayer.GetComponent<Rigidbody>();
        controller = transform.parent.GetComponent<HoverController>();
        controllerStats = transform.parent.GetComponent<CharStats>();

    }
    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Wall"))
        {
            dontbounce = true;
            print(dontbounce);
        }
        
        if (col.gameObject == OtherHurtBox)
        {
            EjectOP();
        }

    }
    private void OnTriggerExit(Collider col)
    {
        dontbounce = false;
        print(dontbounce);
    }
    private void OnCollisionEnter(Collision col)
    {
        print(rb.velocity.magnitude);
        
        if (col.gameObject.CompareTag("Wall") && !dontbounce && rb.velocity.magnitude >= min_Speed_To_Bounce && hitIt)
        {
            Vector3 Newdir = Vector3.Reflect(_Velgo, col.contacts[0].normal);
            Reflect(Newdir);

            /*Vector3 Newdir = Vector3.Reflect(_Velgo, col.contacts[0].normal);
            rb.velocity = Vector3.zero;
            rb.AddForce(Newdir * (Force_WallBounce / 100), ForceMode.VelocityChange);
            //rb.AddTorque(180 * Time.fixedDeltaTime * transform.up, ForceMode.Impulse);
            transform.rotation = Quaternion.LookRotation(Newdir, Vector3.up);
            //transform.eulerAngles += new Vector3(0, -90, 0);*/
        }
    }

    void Reflect(Vector3 dir)
    {
        rb.velocity = Vector3.zero;
        rb.AddForce(dir * (Force_WallBounce / 100), ForceMode.VelocityChange);
        transform.rotation = Quaternion.LookRotation(dir, Vector3.up);
    }
    void EjectOP()
    {
        //controllerStats.DOSOMETHINGS;
        otherRb.AddForce(rb.velocity * (Force_To_OP / 100), ForceMode.VelocityChange);
        rb.velocity = (rb.velocity - rb.velocity*.7f);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            StartCoroutine(Cd_to_bounce());
        }
        _Velgo = rb.velocity;//NE PAS ENLEVER A GARDER !!!
        Debug.DrawRay(transform.position, rb.velocity);
    }
    
    
    IEnumerator Cd_to_bounce()
    {
        hitIt = true;
        yield return new WaitForSeconds(1f);
        hitIt = false;
    }
}
