using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLocking : MonoBehaviour
{

    public GameObject Gate;
    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Gate.SetActive(true);
        }
    }
}
