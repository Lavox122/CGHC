using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCheckpointSpawn : MonoBehaviour
{
    public GameObject spawnPoint;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SoundManager.Instance.PlaySound(AudioLibrary.Instance.CheckPointClip);
            spawnPoint.transform.position = transform.position;
            Debug.Log("changed");
        }
    }
}
