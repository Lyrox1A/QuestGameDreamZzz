using System;
using System.Collections;
using System.Collections.Generic;

using DG.Tweening;

using UnityEngine;

public class Prism : MonoBehaviour
{
    [SerializeField] private Transform platform;

    [SerializeField] private Vector3 retractedPosition;
    
    [SerializeField] private Vector3 extendedPosition;

    [SerializeField] private bool startExtended;

    [Min(0)]
    [SerializeField] private float moveDuration = 1f;
    
    [SerializeField] private Ease ease = DOTween.defaultEaseType;

    private bool extended;

    private void Awake()
    {
        extended = startExtended;
        platform.transform.localPosition = startExtended ? extendedPosition : retractedPosition; 
    }

    public void Toggle()
    {
        if (extended)
        {
            Retract();
        }
        else
        {
            Extend();
        }
    }
    
    public void Extend()
    {
        MovePlatform(true);
    }

    public void Retract()
    {
        MovePlatform(false);
    }

    private void MovePlatform(bool extend)
    {
        extended = extend;

        float speed = (retractedPosition - extendedPosition).magnitude / moveDuration; 
        
        Vector3 targetPosition = extend ? extendedPosition : retractedPosition;

        platform.DOKill();

        platform.DOLocalMove(targetPosition, speed)
                .SetEase(ease)
                .SetSpeedBased()
                .OnComplete( () => { Debug.Log("Salam"); } );
    }
}
