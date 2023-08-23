using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    [SerializeField] private Car car;
    [SerializeField] [Range(0.0f, 1.0f)] private float normalizeSpeedShake;
    [SerializeField] private float shakeAmount;

    
    private void Update()
    {
        if(car.NormalizeLinearVelocity >= normalizeSpeedShake)
        transform.localPosition += Random.insideUnitSphere * shakeAmount * Time.deltaTime;
    }
}
