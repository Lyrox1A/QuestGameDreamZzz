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
    
    private void OnTriggerEnter(Collider other)
    {
        if (filterByTag && !other.CompareTag(reactOn))
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

        
        onTriggerExit.Invoke();
    }
}
