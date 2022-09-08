using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MultiplayerManager : MonoBehaviour
{
    public static MultiplayerManager instance;
    private HoverController[] players = new HoverController[2];

    [SerializeField]
    private LayerMask[] masks = new LayerMask[2];

    [SerializeField]
    private GameObject canvasSplitScreen;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else Destroy(gameObject);

    }
    public void AddPlayer(HoverController player)
    {
        if (players[1] != null) return;

        if(players[0] == null)
        {
            players[0] = player;
        } else
        {
            players[1] = player;
            players[0].transform.parent.GetComponentInChildren<Camera>().rect = new Rect(0, 0, 1, 0.5f);
            players[1].transform.parent.GetComponentInChildren<Camera>().rect = new Rect(0, 0.5f, 1, 0.5f);

            canvasSplitScreen.SetActive(true);
        }

        PlayerSpawn(player);
    }

    private void PlayerSpawn(HoverController player)
    {
        player.gameObject.layer = (players[1] != null) ? 8 : 7;
        player.transform.Find("Vcam").gameObject.layer = (players[1] != null) ? 8 : 7;
        player.transform.parent.GetComponentInChildren<Camera>().cullingMask = (players[1] != null) ? masks[1] : masks[0];
        
    }

    public void PlayerDeath(HoverController player)
    {
        //Spawner (Respawn)
    }

    public void SetPause( bool activatePause)
    {       
        foreach(HoverController player in players)
        {
            if(player != null)
            player.SwitchInputs(activatePause);
        }
    }
}
