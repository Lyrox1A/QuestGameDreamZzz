using UnityEngine;
using UnityEngine.Events;

public class Collectable : MonoBehaviour
{
    [SerializeField] private State state;

    [SerializeField] private UnityEvent onCollected;

    public void Collect()
    {
        onCollected.Invoke();
        FindObjectOfType<GameState>().Add(state);
        Destroy(gameObject);
    }
}
