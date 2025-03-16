using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class CaveTeleporting : MonoBehaviour
{

    public GameObject Entry;
    public GameObject Exit;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("player detected!");
            other.transform.position = Exit.transform.position;
        }
    }
}
