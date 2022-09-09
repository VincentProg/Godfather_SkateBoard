using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class torqueScrpti : MonoBehaviour
{
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
            rb.AddRelativeTorque(50 * Time.fixedDeltaTime * transform.forward, ForceMode.Impulse);
        if(Input.GetKeyDown(KeyCode.V))
            rb.AddTorque(50 * Time.fixedDeltaTime * transform.up, ForceMode.Impulse);
        if(Input.GetKeyDown(KeyCode.B))
            rb.AddTorque(50 * Time.fixedDeltaTime * transform.right, ForceMode.Impulse);
    }
}
