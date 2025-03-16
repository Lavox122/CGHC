using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunMagazine : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyObject", 5f);
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}
