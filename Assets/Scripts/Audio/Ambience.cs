using System;

using FMODUnity;

using UnityEngine;

[RequireComponent(typeof(StudioEventEmitter))]
public class Ambience : MonoBehaviour
{
    [SerializeField] private string indoorParameterName = "inside";
    
    private StudioEventEmitter ambienceEmitter;

    private void Awake()
    {
        ambienceEmitter = GetComponent<StudioEventEmitter>();
    }

    public void SetIndoor(bool indoor)
    {
        ambienceEmitter.SetParameter(indoorParameterName, indoor ? 1 : 0);
    }
}
 