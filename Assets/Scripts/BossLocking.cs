using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLocking : MonoBehaviour
{

    public GameObject Gate;
    public GameObject Boss;
    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Gate.SetActive(true);
            Boss.SetActive(true);
            this.enabled = false;
        }
    }
}
