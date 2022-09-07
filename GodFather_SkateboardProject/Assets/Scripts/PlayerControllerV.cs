using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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

    private void Awake()
    {
        MultiplayerManager.instance.AddPlayer(this);
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
    }

    IEnumerator Impulse()
    {
        canImpulse = false;

        rb.velocity += Vector3.ClampMagnitude(transform.forward * strengthImpulse, playerMaxSpeed);
        yield return new WaitForSeconds(1);
        canImpulse = true;
    }
}

