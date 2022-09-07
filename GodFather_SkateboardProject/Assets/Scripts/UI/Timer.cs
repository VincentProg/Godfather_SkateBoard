using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{

    [SerializeField]
    private float initialTime;
    private float time;

    [SerializeField]
    private TextMeshProUGUI timerText;

    // Start is called before the first frame update
    void Start()
    {
        time = initialTime;   
    }

    // Update is called once per frame
    void Update()
    {
        if(time >0)
        time -= Time.deltaTime;
        else
        {
            EndGame();
        }

        timerText.text = ((int)time).ToString();
    }

    private void EndGame()
    {

    }
}
