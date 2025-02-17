using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UIElements;

public class AutoRotate : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Vector3 rotateAxis = Vector3.up;
    [SerializeField] private float speed = 4f;
    [SerializeField] private bool rotate180 = false;

    private float angle = 0f;
    private bool reverse = false;
    private void Update()
    {
        if (rotate180)
        {
            Rotate180();
        }
        else
        {
            Rotate360();
        }
    }

    private void Rotate360()
    {
        transform.Rotate(rotateAxis * speed * Time.deltaTime);
    }

    private void Rotate180()
    {
        float deltaAngle = speed * Time.deltaTime;
        if (reverse)
        {
            deltaAngle = -deltaAngle;
        }

        angle += deltaAngle;
        transform.Rotate(rotateAxis * deltaAngle);

        if (angle >= 180f || angle <= 0f)
        {
            reverse = !reverse;
        }
    }
}
