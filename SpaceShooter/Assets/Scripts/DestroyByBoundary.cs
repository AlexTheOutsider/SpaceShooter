using System;
using UnityEngine;

public class DestroyByBoundary : MonoBehaviour
{
    public Boundary boundary;
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && other.gameObject.GetComponent<Spaceship>())
            EventManager.Instance.TriggerEvent("EnemyKilled", other.gameObject);
        else
            Destroy(other.gameObject);
    }
}

[Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}
