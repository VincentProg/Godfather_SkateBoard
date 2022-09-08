using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Btn_Selector : MonoBehaviour
{
    public static Btn_Selector instance;
    [SerializeField]
    List<UIButtons> buttons = new List<UIButtons>();

    private UIButtons btnSelected;

    private void Awake()
    {
        instance = this;
    }

    public void SelectButton0()
    {
        buttons[0].OnSelect();
        buttons[1].OnUnselect();
        btnSelected = buttons[0];
    }

    public void SelectButton1()
    {
        buttons[1].OnSelect();
        buttons[0].OnUnselect();
        btnSelected = buttons[1];
    }

    public void LaunchBtn()
    {
        if (btnSelected != null)
        {
            print(btnSelected.gameObject.name);
            btnSelected.GetComponent<Button>().onClick.Invoke();
        }


    }
}
