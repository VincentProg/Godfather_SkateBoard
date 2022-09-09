using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButtons : MonoBehaviour
{

    private Vector3 initialPos;
    [SerializeField]
    private Vector3 initialScale, offset;
    Animator animator;

    private RectTransform t;

    [SerializeField] 
    private Text text;
    [SerializeField]
    private Color SelectedColor;

    public GameObject[] arrows;

    private void Start()
    {
        t = GetComponent<RectTransform>();
        initialScale = t.localScale;
        initialPos = t.position;
        t.localScale = initialScale;
        animator = GetComponent<Animator>();
        OnUnselect();
    }

    public void OnSelect()
    {
        t.position = initialPos + offset;
        t.localScale = Vector3.one;
        text.color = SelectedColor;
        animator.SetBool("Active", true);
    }

    public void OnUnselect()
    {
        t.position = initialPos;
        t.localScale = initialScale;
        text.color = Color.white;
        animator.SetBool("Active", false);
    }
}
