using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

[SelectionBase]
[RequireComponent(typeof(Rigidbody))]
public class HoverController : MonoBehaviour
{
    //tesst
    [HideInInspector] public Rigidbody rb;
    private Animator anim;

    PlayerInput myInputs;

    [Header("Suspensions")]
    [Range(0.0f, 10f)]
    [SerializeField] private float multiplier = 2.5f;
    [SerializeField] private float maxHeight = 5f;
    [SerializeField] private float minHeight = 1f;
    [SerializeField] private Transform[] anchors = new Transform[4];
    RaycastHit[] hits = new RaycastHit[4];

    [Header("----------------------------------------")]
    [Header("Forward")]
    [SerializeField] private float moveForce = 1000f;
    [SerializeField] private float strengthImpulse = 50;

    [Space(10)]
    [Header("Turn")]
    [SerializeField] private float turnTorque = 100f;
    [Range(0.1f, 2f)]
    [SerializeField] private float _sharpTurn = 0.5f;
    [Range(0.1f, 2f)]
    [SerializeField] private float _wideBend = 0.5f;
    private float magnitude;
    float currentTorque;

    [Header("Speed")]
    [Tooltip("magnitude => [4 - 40]")]
    [SerializeField] private float playerMaxSpeed = 20;

    [SerializeField] private bool grounded;
    public int PlayerNumber;

    private Vector2 movementInput;

    private Quaternion _startRotation;

    private bool canImpulse;

    private bool canReset;
    private bool resetDeccel;

    private Vector2 Axis;
    private Vector3 downDir;
    [HideInInspector] public Vector3 DirectionCOl;

    [HideInInspector] public int m_Wins;
    [HideInInspector] public Color m_ColoredPlayerText;

    public enum HealthState { Alive, Dead, Respawn}
    public HealthState _healthState;

    [HideInInspector] public HoverController player2;
    [HideInInspector] public BoxCollider[] player2Collisions;

    public GameObject camRef;
    private void Awake()
    {
        //MultiplayerManager.instance.AddPlayer(this);
    }
    //private void OnEnable()
    //{
    //    try
    //    {
    //        if (PlayerNumber == 0)
    //            MultiplayerManager.instance.GetPlayer(1).AddRefToPlayer();
    //        else
    //            MultiplayerManager.instance.GetPlayer(0).AddRefToPlayer();
    //    }
    //    catch { }
    //}
    //public void AddRefToPlayer()
    //{
    //    if (PlayerNumber == 0)
    //    {
    //        player2 = MultiplayerManager.instance.GetPlayer(1);
    //        player2.AddRefToPlayer();
    //    }
    //    else
    //        player2 = MultiplayerManager.instance.GetPlayer(0);

    //}

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        canImpulse = true;
        _startRotation = new Quaternion(0, 0, 0, 1);
        currentTorque = turnTorque;
        downDir = -transform.parent.transform.up;
        myInputs = GetComponent<PlayerInput>();
        MultiplayerManager.instance.AddPlayer(this);
        Debug.Log(downDir);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
        PlayRightLeftAnim(movementInput.x);
    }
    
    void FixedUpdate()
    {
        Axis = movementInput;
        rb.AddForce(Axis.y * moveForce * Time.fixedDeltaTime * transform.forward, ForceMode.Impulse);

        for (int i = 0; i < 4; i++)
        {
            ApplyForce(anchors[i], hits[i]);
        }

        TorqueSetting();

        rb.AddTorque(Axis.x * turnTorque * Time.fixedDeltaTime * transform.up, ForceMode.Impulse);

        LimitMaxSpeed();
    }
    private void TorqueSetting()
    {
        magnitude = Mathf.Clamp(rb.velocity.sqrMagnitude, _sharpTurn, _wideBend);
        turnTorque = Mathf.Lerp(turnTorque, currentTorque / magnitude, 2);
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

    private void ApplyForce(Transform anchor, RaycastHit hit)
    {
        if (Physics.Raycast(anchor.position, -anchor.up, out hit, maxHeight))
        {
            if (hit.collider.tag == "ground")
            {
                float force = 0;
                force = Mathf.Abs(1 / (hit.point.y - anchor.position.y));
                rb.AddForceAtPosition(transform.up * force * multiplier, anchor.position, ForceMode.Acceleration);

            }
            else FlipSkate();

        }
        else FlipSkate();
    }

    public void FlipSkate()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, new Quaternion(transform.rotation.x, transform.rotation.y, 0, 1), 1);
    }
    IEnumerator Impulse()
    {
        canImpulse = false;
        PlayPushAnim();
        rb.AddForce(moveForce * Time.fixedDeltaTime * transform.forward * strengthImpulse, ForceMode.Impulse);
        yield return new WaitForSeconds(1);
        canImpulse = true;
    }

    public void EnableControl()
    {

    }
    public void DisableControl()
    {

    }
    
    public void SetPause(InputAction.CallbackContext context)
    {
        if (context.started)
            MenuUtility.instance.SetPause();
    }
    public void SwitchInputs(bool switchToPause)
    {
        if (switchToPause)
        {
            print(myInputs);
            myInputs.SwitchCurrentActionMap("PauseMenu");
        }
        else
        {
            myInputs.SwitchCurrentActionMap("Gameplay");
        }
    }

    public void Pause_BtnUp(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            print("button up");
            Btn_Selector.instance.SelectButton0();
        }
    }

    public void Pause_BtnDown(InputAction.CallbackContext context)
    {
        if (context.started)
            Btn_Selector.instance.SelectButton1();
    }

    public void Pause_Click(InputAction.CallbackContext context)
    {
        if (context.started)
            Btn_Selector.instance.LaunchBtn();
    }

    public void PlayRightLeftAnim(float x)
    {
        if (x > 0)
            anim.SetBool("GoRight", true);
        else anim.SetBool("GoLeft", true);
    }

    public void playDamagesAnim(float health)
    {
        if (health > 0)
            anim.SetTrigger("Hit");
        else anim.SetTrigger("Death");
    }

    public void PlayBumpAnim()
    {
        anim.SetTrigger("Bump");
    }

    public void PlayPushAnim()
    {
        anim.SetTrigger("Push");
    }
}

