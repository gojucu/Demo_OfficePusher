using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area : MonoBehaviour
{
    BoxCollider boxCollider;

    Vector3 extents;
    Vector3 point;
    [SerializeField]GameObject area;
    void Start()
    {
        boxCollider = area.GetComponent<BoxCollider>();
        extents = boxCollider.size / 2f;
    }
    public BoxCollider GetBoxCollider()
    {
        return boxCollider;
    }
    public Vector3 GetRandomPointInsideCollider()//Get random point for enemies to walk inside area.
    {
        boxCollider = area.GetComponent<BoxCollider>();
        extents = boxCollider.size / 2f;
        point = new Vector3(
            Random.Range(-extents.x+1, extents.x-1),
            Random.Range(-extents.y+1, extents.y-1),
            Random.Range(-extents.z+1, extents.z-1)
        );

        return boxCollider.transform.TransformPoint(point);
    }
}
