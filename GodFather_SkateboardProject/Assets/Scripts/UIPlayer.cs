using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIPlayer : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private TextMeshProUGUI text;
    [SerializeField]
    private float multiplier;

    private void Update()
    {
        text.text = ((int)(rb.velocity.magnitude * multiplier)).ToString();
    }
}
