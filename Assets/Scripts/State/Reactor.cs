using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

public class Reactor : MonoBehaviour
{
    [SerializeField] private List<State> conditions;

    [SerializeField] private UnityEvent onFulfilled;

    [SerializeField] private UnityEvent onUnfulfilled;

    [SerializeField] private QuestEntry questEntry;


    private bool fulfilled = false;
    
    private void OnEnable()
    {
        if (questEntry != null)
        {
            questEntry.gameObject.SetActive(true);
        }
        OnStateChanged(FindObjectOfType<GameState>());
        GameState.StateChanged += OnStateChanged;
    }

    private void OnDisable()
    {
        if (questEntry != null)
        {
            questEntry.gameObject.SetActive(false);
        }
        
        GameState.StateChanged -= OnStateChanged;
    }

    private void OnStateChanged(GameState gameState)
    {
        bool newFulfilled = gameState.CheckConditions(conditions);

        if (newFulfilled && !fulfilled)
        {
            if (questEntry != null)
            {
                questEntry.SetQuestStatus(true);
            }
            
            onFulfilled.Invoke();
        }
        
        else if (!newFulfilled && fulfilled)
        {
            onUnfulfilled.Invoke();
        }

        fulfilled = newFulfilled;
    }
    
}
