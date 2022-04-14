using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Dialog : MonoBehaviour
{
    [SerializeField] public List<DialogEntry> entries;

    public UnityEvent onDialogEnd;

    public void StartDialog()
    {
        if (entries.Count == 0)
        {
            Debug.LogWarning("Dialog has no entries!", this);
        }
    }
}

[Serializable]
public class DialogEntry
{
    public string speaker;

    public string text;
    
}