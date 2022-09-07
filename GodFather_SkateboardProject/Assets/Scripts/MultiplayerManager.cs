using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplayerManager : MonoBehaviour
{
    public static MultiplayerManager instance;
    private PlayerControllerV[] players = new PlayerControllerV[2];

    [SerializeField]
    private LayerMask[] masks = new LayerMask[2];

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else Destroy(gameObject);
    }
    public void AddPlayer(PlayerControllerV player)
    {
        if (players[1] != null) return;

        if(players[0] == null)
        {
            players[0] = player;
        } else
        {
            players[1] = player;
        }

        PlayerSpawn(player);
    }

    private void PlayerSpawn(PlayerControllerV player)
    {
        player.gameObject.layer = (players[1] != null) ? 8 : 7;
        player.transform.parent.Find("Vcam").gameObject.layer = (players[1] != null) ? 8 : 7;
        player.transform.parent.GetComponentInChildren<Camera>().cullingMask = (players[1] != null) ? masks[1] : masks[0];
        
    }

    public void PlayerDeath(PlayerControllerV player)
    {

    }
}
