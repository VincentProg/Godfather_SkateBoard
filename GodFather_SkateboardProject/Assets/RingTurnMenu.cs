using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingTurnMenu : MonoBehaviour
{
    Rigidbody rb;
    public float turnTorque =0.5f;
    // Start is called before the first frame update
    void Start()
    {
       rb = GetComponent<Rigidbody>(); 
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddTorque(turnTorque * Time.fixedDeltaTime * transform.up);
    }
}
