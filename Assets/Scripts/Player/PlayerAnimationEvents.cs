using System.Collections;
using System.Collections.Generic;

using FMODUnity;

using UnityEngine;
using UnityEngine.Events;

public class PlayerAnimationEvents : MonoBehaviour
{
    [SerializeField] private StudioEventEmitter stepSound;
    
    //[SerializeField] private StudioEventEmitter landsound

    [SerializeField] private string stepSoundParameterName = "surface";
    [SerializeField] private PhysicMaterial defaultPhysicMaterial;
    
    [Header("Raycast")] 
    
    [SerializeField] private LayerMask layerMask;
    
    [Header("Unity Events")]
    
    
    
    [SerializeField] private UnityEvent onStep;
    
    
    public void Step(AnimationEvent animationEvent)
    {
        if (animationEvent.animatorClipInfo.weight < 0.5)
        {
            return;
        }
        
        stepSound.Play();
        ChangeStepSound();
        onStep.Invoke();
    }

    public void Land()
    {
        
    }

    private void ChangeStepSound()
    {
        bool hit = Physics.Raycast(transform.position + Vector3.up * 0.5f, 
                                   Vector3.down, 
                                   out RaycastHit hitInfo ,
                                   5f, 
                                   layerMask, 
                                   QueryTriggerInteraction.Ignore);

        if (!hit)
        {
            Debug.LogWarning("no ground found");
            return;
        }

        PhysicMaterial groundPhysicMaterial = hitInfo.collider.sharedMaterial;

        int stepSoundParameterValue = GetStepSoundParameterValue(groundPhysicMaterial);
        
        stepSound.SetParameter(stepSoundParameterName, stepSoundParameterValue);
    }

    private int GetStepSoundParameterValue(PhysicMaterial groundPhysicsMaterial)
    {
        if (groundPhysicsMaterial == null)
        {
            groundPhysicsMaterial = defaultPhysicMaterial;
        }

        switch (groundPhysicsMaterial.name)
        {
            case "Grass":
                return 0;
            case "Wood" :
                return 1;
            default:
                return 0;
            
            
        }
    }
}
