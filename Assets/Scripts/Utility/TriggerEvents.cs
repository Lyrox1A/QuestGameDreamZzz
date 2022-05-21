using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEvents : MonoBehaviour
{
    [SerializeField] private UnityEvent onTriggerEnter;

    [SerializeField] private UnityEvent onTriggerExit;

    [SerializeField] private bool filterByTag = true;

    [SerializeField] private string reactOn = "Player";

    [SerializeField] private bool combineTrigger = true;

    private int triggerCount = 0; 
    
    private void OnTriggerEnter(Collider other)
    {
        if (filterByTag && !other.CompareTag(reactOn))
        {
            return;
        }

        triggerCount++;
        
        if (triggerCount < 1)
        {
            triggerCount = 1;
        }

        if (combineTrigger && triggerCount != 1)
        {
            return;
        }
        
        onTriggerEnter.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        if (filterByTag && !other.CompareTag(reactOn))
        {
            return;
        }

        triggerCount--;

        if (combineTrigger && triggerCount != 0)
        {
            return;
        }

        
        onTriggerExit.Invoke();
    }
}
