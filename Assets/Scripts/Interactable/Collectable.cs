using System.Collections;

using UnityEngine;
using UnityEngine.Events;

public class Collectable : MonoBehaviour
{
    [SerializeField] private State state;

    [SerializeField] private UnityEvent onCollected;

    public void Collect()
    {
        onCollected.Invoke(); 
        FindObjectOfType<PlayerController>().GetComponentInChildren<Animator>().SetBool("PickingUp", true);
        FindObjectOfType<GameState>().Add(state);
        Destroy(gameObject);
        StartCoroutine(ResetPickUpInteraction());
    }

    IEnumerator ResetPickUpInteraction()
    {
        yield return new WaitForSeconds(3f);
        FindObjectOfType<PlayerController>().GetComponentInChildren<Animator>().SetBool("PickingUp", false);
    }
    

}
