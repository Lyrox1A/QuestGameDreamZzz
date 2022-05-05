using System;
using System.Collections;
using System.Collections.Generic;

using UnityEditor;

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
            return;
        }
        
        FindObjectOfType<GameController>().StartDialog(this);
    }
}

[Serializable]
public class DialogEntry
{
    [TextArea(3, 4)]
    public string text;
    
    public string speaker;

    public List<Selection> selections;
}


[Serializable]

public class Selection
{
    public string selectionText;
    public UnityEvent onSelected;
    public Dialog nextDialog;
}