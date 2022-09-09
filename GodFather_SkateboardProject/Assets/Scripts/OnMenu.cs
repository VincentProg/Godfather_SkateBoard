using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnMenu : MonoBehaviour
{
    Animator animator;
    public GameObject Ring;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    // Update is called once per frame

    public void ClickStart()
    {
        animator.SetTrigger("StartButton");
    }

    public void OnEvent()
    {
        Ring.gameObject.SetActive(true);
    }
}
