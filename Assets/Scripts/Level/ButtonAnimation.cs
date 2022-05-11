using System;
using System.Collections;
using System.Collections.Generic;

using DG.Tweening;

using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class ButtonAnimation : MonoBehaviour
{
    [SerializeField] private float yMovement = -0.5f;
    [SerializeField] private Color pressColor = Color.red;
    
    [Min(0)]
    [SerializeField] private float downDuration = 0.5f;
    
    [Header("In")]

    [SerializeField] private Ease easeIn = DOTween.defaultEaseType;
    [Min(0)]
    [SerializeField] private float durationIn = 0.5f;
    
    [Header("Out")]

    [SerializeField] private Ease easeOut = DOTween.defaultEaseType;
    [Min(0)]
    [SerializeField] private float durationOut = 0.5f;

    private MeshRenderer meshRenderer;
    private Color originalColor;

    private Sequence buttonSequence;
    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        originalColor = meshRenderer.material.color;
    }

    public void PlayAnimation()
    {
        buttonSequence.Complete(true);

        Sequence newSequence = DOTween.Sequence();

        newSequence.Append(transform.DOLocalMoveY(yMovement, durationIn)
                                    .SetRelative()
                                    .SetEase(easeIn))
                   .Join(meshRenderer.material.DOColor(pressColor, durationIn).SetEase(Ease.Linear))
                   .AppendInterval(downDuration)
                   .Append(transform.DOLocalMoveY(-yMovement, durationOut)
                                    .SetRelative()
                                    .SetEase(easeOut))
                   .Join(meshRenderer.material.DOColor(originalColor, durationOut).SetEase(Ease.Linear));

        newSequence.Play();

        buttonSequence = newSequence;
    }
}
