using System;
using UnityEngine;

public class DestroyByBoundary : MonoBehaviour
{
    public Boundary boundary;
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && other.gameObject.GetComponent<Spaceship>())
        {
            //EventManager.Instance.TriggerEvent("EnemyKilled", other.gameObject);
            //EventManagerNew.Instance.Fire(new EnemyKilledEvent(other.gameObject,0));
            Services.EventManagerNew.Fire(new EnemyKilledEvent(other.gameObject,0));
        }
        else
            Destroy(other.gameObject);
    }
}

[Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}
