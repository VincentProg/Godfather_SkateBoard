using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeBars : MonoBehaviour
{
    [SerializeField]
    Image imgP1, imgP2;

    CharStats P1, P2;

    private void Start()
    {
        P1 = MultiplayerManager.instance.players[0].GetComponent<CharStats>();
        P2 = MultiplayerManager.instance.players[1].GetComponent<CharStats>();
    }

    // Update is called once per frame
    void Update()
    {
        imgP1.fillAmount = P1.Health / 100f;
        imgP2.fillAmount = P2.Health / 100f;
        imgP1.color = new Color(imgP1.color.r, imgP1.fillAmount, imgP1.color.b);
        imgP2.color = new Color(imgP2.color.r, imgP2.fillAmount, imgP2.color.b);
    }
}
