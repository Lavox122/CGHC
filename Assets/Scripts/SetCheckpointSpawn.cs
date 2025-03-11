using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCheckpointSpawn : MonoBehaviour
{
    public Transform spawnPoint;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            spawnPoint.position = transform.position;
            Debug.Log("changed");
        }
    }
}
