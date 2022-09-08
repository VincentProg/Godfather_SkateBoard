using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Btn_Selector : MonoBehaviour
{

    [SerializeField]
    List<UIButtons> buttons = new List<UIButtons>();

    private UIButtons btnSelected;

    private void Start()
    {
        for(int i = 0; i < buttons.Count; i++)
        {
            buttons[i].OnUnselect();
        }
    }

    public void PressLeft()
    {
        buttons[0].OnSelect();
        buttons[1].OnUnselect();
        btnSelected = buttons[0];
    }

    public void PressRight()
    {
        buttons[1].OnSelect();
        buttons[0].OnUnselect();
        btnSelected = buttons[1];
    }

    public void LaunchBtn()
    {
        btnSelected.GetComponent<Button>().onClick.Invoke();
    }
}
