using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplayerManager : MonoBehaviour
{
    public static MultiplayerManager instance;
    private PlayerControllerV[] players = new PlayerControllerV[2];

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
        if (players.Length >= 2) return;

        if(players.Length == 0)
        {
            players[0] = player;
        } else
        {
            players[1] = player;
        }
    }

    public void PlayerDeath(PlayerControllerV player)
    {

    }
}
