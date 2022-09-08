using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerControllerV : MonoBehaviour
{
    [SerializeField]
    private float playerMaxSpeed;
    
    private Vector3 playerVelocity;
    private bool groundedPlayer;

    private Vector2 movementInput;

    private Rigidbody rb;
    private bool canImpulse;
    [SerializeField]
    private float strengthImpulse;
    [SerializeField]
    private float decelerationSpeed;
    [SerializeField]
    private float turnSpeed;

    [SerializeField]
    TextMeshProUGUI textSpeed;


    private void Awake()
    {
        //MultiplayerManager.instance.AddPlayer(this);
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        canImpulse = true;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    public void OnImpulse()
    {
        //print("impulse");
        if (canImpulse)
            StartCoroutine(Impulse()) ;
    }

    void FixedUpdate()
    {
        rb.velocity = rb.velocity.normalized * (rb.velocity.magnitude - decelerationSpeed);
        float yAngle = transform.rotation.eulerAngles.y + movementInput.x;
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y + movementInput.x, 0);
        rb.velocity = transform.forward * rb.velocity.magnitude;

        textSpeed.text = ((int)(rb.velocity.magnitude * 3)).ToString();
    }

    IEnumerator Impulse()
    {
        canImpulse = false;
        rb.velocity += transform.forward * strengthImpulse;
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, playerMaxSpeed);
        yield return new WaitForSeconds(1);
        canImpulse = true;
    }
    public void Pause_btnUp()
    {
        Btn_Selector.instance.SelectButton0();
    }
    public void Pause_btnDown()
    {
        Btn_Selector.instance.SelectButton1();
    }
}

