using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Spawner : MonoBehaviour
{
    private BoxCollider zone;
    public void SpawnAtPosition(Vector3 pos, HoverController player)
    {
        Bounds boundsBox = zone.bounds;
       
        if (pos == Vector3.zero)
        {
            Vector3 newPos = new Vector3(Random.Range(boundsBox.min.x, boundsBox.max.x), Random.Range(boundsBox.min.y, boundsBox.max.y), Random.Range(boundsBox.min.z, boundsBox.max.z));
            player.transform.position = newPos;
        }
        else
        {
            player.transform.position = pos;
        }
    }
}
