using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnerOnFall : MonoBehaviour
{
    private new BoxCollider collider;
    [SerializeField] private CarRespawner respawner;

    private void Start()
    {
        collider = GetComponent<BoxCollider>(); 
    }

    private void OnTriggerEnter(Collider other)
    {
         if (other.transform.root.GetComponent<Car>() == null) return;

         respawner.Respawn();
    }
}

