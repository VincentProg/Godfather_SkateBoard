using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

[SelectionBase]
[RequireComponent(typeof(Rigidbody))]
public class HoverController : MonoBehaviour
{
    PlayerInput myInputs;
    Rigidbody rb;

    [Header("Suspensions")]
    [Range(0.0f, 10f)]
    [SerializeField] private float multiplier = 2.5f;
    [SerializeField] private float maxHeight = 5f;
    [SerializeField] private Transform[] anchors = new Transform[4];
    RaycastHit[] hits = new RaycastHit[4];

    [Header("----------------------------------------")]
    [Header("Forward")]
    [SerializeField] private float moveForce = 1000f;
    [SerializeField] private float strengthImpulse = 40;

    [Space(10)]
    [Header("Turn")]
    [SerializeField] private float turnTorque = 40f;
    [SerializeField] private float _sharpTurn = 100;
    [SerializeField] private float _wideBend = 15;
    private float magnitude;
    public float Magnitude { get { return magnitude; } }

    [Header("Speed")]
    [Tooltip("magnitude => [4 - 40]")]
    [SerializeField] private float playerMaxSpeed = 20;

    [SerializeField] private bool grounded;

    private Vector2 movementInput;

    private Quaternion _startRotation;

    private bool canImpulse;

    private bool canReset;

    private Vector2 Axis;

    private void Awake()
    {
        MultiplayerManager.instance.AddPlayer(this);
    }

    void Start()
    {
        myInputs = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody>();
        canImpulse = true;
        _startRotation = new Quaternion(0,0,0,1);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    private void Update()
    {
        LimitRotation();
    }

    void FixedUpdate()
    {
        if (grounded)
            Axis = movementInput;
        else Axis = Vector2.zero;

        for (int i = 0; i < 4; i++)
        {
            ApplyForce(anchors[i], hits[i]);
        }

        TorqueSetting();

        rb.AddForce(Axis.y * moveForce * transform.forward);
        rb.AddTorque(Axis.x * turnTorque * transform.up);

        LimitMaxSpeed();
    }

    private void TorqueSetting()
    {
        magnitude = Mathf.Clamp(rb.velocity.magnitude, 0.4f, 4);
        turnTorque = Mathf.Clamp(turnTorque, _sharpTurn, _wideBend);
        turnTorque /= magnitude;
    }

    public void OnImpulse()
    {
        if (canImpulse)
            StartCoroutine(Impulse());
    }

    private void LimitMaxSpeed()
    {
        if (rb.velocity.magnitude > playerMaxSpeed)
            rb.velocity = rb.velocity.normalized * playerMaxSpeed;
    }
    private void LimitRotation()
    {
        //Si le skate se retourne
        if (Mathf.Abs(transform.position.z) >= 90f || Mathf.Abs(transform.position.x) >= 110f) { WaitToReset(); }
        if (Input.GetKeyDown(KeyCode.R)) ResetRotation();
    }
    private void WaitToReset()
    {
        canReset = true;
        //display "Press R to Reset" after 2Secs
    }

    private void ResetRotation()
    {
        canReset = false;
        transform.rotation = _startRotation;
        //ou appeler le spawner
    }

    private void ApplyForce(Transform anchor, RaycastHit hit)
    {
        if (Physics.Raycast(anchor.position, -anchor.up, out hit, maxHeight))
        {
            Debug.DrawRay(anchor.position, -anchor.up * maxHeight, Color.green);
            grounded = true;
            float force = 0;
            force = Mathf.Abs(1 / (hit.point.y - anchor.position.y));
            rb.AddForceAtPosition(transform.up * force * multiplier, anchor.position, ForceMode.Acceleration);
        }else grounded = false;
    }

    IEnumerator Impulse()
    {
        canImpulse = false;
        rb.AddForce(moveForce * transform.forward * strengthImpulse);
        yield return new WaitForSeconds(1);
        canImpulse = true;
    }

    public void SetPause(InputAction.CallbackContext context)
    {        
        if(context.started)
             MenuUtility.instance.SetPause();
    }
    public void SwitchInputs(bool switchToPause)
    {
        if (switchToPause)
        {
            print(myInputs);
            myInputs.SwitchCurrentActionMap("PauseMenu");
        } else
        {
            myInputs.SwitchCurrentActionMap("Gameplay");
        }
    }

    public void Pause_BtnUp(InputAction.CallbackContext context)
    {
        if(context.started)
        Btn_Selector.instance.SelectButton0();
    }

    public void Pause_BtnDown(InputAction.CallbackContext context)
    {
        if(context.started)
        Btn_Selector.instance.SelectButton1();
    }

    public void Pause_Click(InputAction.CallbackContext context)
    {
        if(context.started)
        Btn_Selector.instance.LaunchBtn();
    }
}
