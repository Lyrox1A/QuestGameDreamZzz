using UnityEngine;
using FMODUnity;
public class Footsteps : MonoBehaviour
{
    [SerializeField] private string footstepruinParameterName = "Ruin";
    
    //[SerializeField] private string footstepforestParameterName = "ForestGrass";
    
    private StudioEventEmitter footstepsEmitter;

    private void Awake()
    {
        footstepsEmitter = GetComponent<StudioEventEmitter>();
    }

    public void SetRuin(bool ruin)
    {
        footstepsEmitter.SetParameter(footstepruinParameterName, ruin ? 1 : 0);
    }
    
    // public void SetForest( bool forest)
    // {
    //     footstepsEmitter.SetParameter(footstepforestParameterName, forest ? 1 : 0);
    // }
    
}
