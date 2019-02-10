using System;
using UnityEngine;

public class DestroyByBoundary : MonoBehaviour
{
    public Boundary boundary;
    private void OnTriggerExit(Collider other)
    {
        Destroy(other.gameObject);
    }
}

[Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}
