using System;
using System.Collections.Generic;

using UnityEngine;

public class StateUpdater : MonoBehaviour
{
    [SerializeField] private List<State> stateUpdates;

    private void OnValidate()
    {
        if (stateUpdates == null)
        {
            return;
        }
        foreach (State state in stateUpdates)
        {
            if (string.IsNullOrWhiteSpace(state.id) && state.amount == 0)
            {
                state.id = "<ID>";
                state.amount = 1;
            }
        }
    }

    public void UpdateState()
    {
        FindObjectOfType<GameState>().Add(stateUpdates);
    }
}
