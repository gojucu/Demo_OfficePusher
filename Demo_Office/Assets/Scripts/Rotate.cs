using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{

    [SerializeField] float RotationsPerMinute = 1f;
    public void RotateAround()
    {
        float yDegreesPerFrame = Time.deltaTime / 60 * 360 * RotationsPerMinute; ;
        transform.RotateAround(transform.position, transform.up, yDegreesPerFrame);
    }
}
