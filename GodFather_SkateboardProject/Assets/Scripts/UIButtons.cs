using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButtons : MonoBehaviour
{

    private Vector3 initialPos;
    [SerializeField]
    private Vector3 initialScale, offset;

    private RectTransform t;

    [SerializeField] 
    private Text text;
    [SerializeField]
    private Color SelectedColor;

    private void Start()
    {
        t = GetComponent<RectTransform>();
        initialPos = t.position;
        t.localScale = initialScale;
        OnUnselect();
    }

    public void OnSelect()
    {
        t.position = initialPos + offset;
        t.localScale = Vector3.one;
        text.color = SelectedColor;
    }

    public void OnUnselect()
    {
        t.position = initialPos;
        t.localScale = initialScale;
        text.color = Color.white;
    }
}
