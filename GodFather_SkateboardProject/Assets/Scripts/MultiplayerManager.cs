using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MultiplayerManager : MonoBehaviour
{
    public static MultiplayerManager instance;
    public HoverController[] players = new HoverController[2];

    [SerializeField]
    private LayerMask[] masks = new LayerMask[2];

    [SerializeField]
    private GameObject canvasSplitScreen;

    [SerializeField]
    GameObject initialCam;

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
            initialCam.SetActive(false);
        } else
        {
            players[1] = player;
            players[0].transform.parent.GetComponentInChildren<Camera>().rect = new Rect(0, 0, 1, 0.5f);
            players[1].transform.parent.GetComponentInChildren<Camera>().rect = new Rect(0, 0.5f, 1, 0.5f);

            players[0].GetComponent<PlayerCol>().GetOtherPlayer(players[1]);
            players[1].GetComponent<PlayerCol>().GetOtherPlayer(players[0]);

            canvasSplitScreen.SetActive(true);
        }

        PlayerSpawn(player);
    }
    private void PlayerSpawn(HoverController player)
    {
        player.gameObject.layer = (players[1] != null) ? 8 : 7;
        player.camRef.layer = (players[1] != null) ? 8 : 7;
        player.transform.parent.GetComponentInChildren<Camera>().cullingMask = (players[1] != null) ? masks[1] : masks[0];
        if (players[1] != null) player.PlayerNumber = 2;
        else player.PlayerNumber = 1;
        
    }

    public HoverController GetPlayer(int number)
    {
        return players[number];
    }

    public void PlayerDeath(HoverController player)
    {
        //Spawner (Respawn)
    }

    public void SetPause( bool activatePause)
    {       
        foreach(HoverController player in players)
        {

            if (player != null)
            {
                print("multimanager");
                player.SwitchInputs(activatePause);
            }
        }
    }
}
