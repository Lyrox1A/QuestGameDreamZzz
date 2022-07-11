using System;
using System.Collections;
using System.Collections.Generic;

using UnityEditor;

using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private DialogUIController dialogUIController;
    
    private PlayerController player;

    private void OnEnable()
    {
        DialogUIController.DialogClosed += EndDialog;
    }

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();

        if (player == null)
        {
            Debug.LogError("No player found in scene.", this);
        }
        
        EnterPlayMode();
    }

    private void OnDisable()
    {
        DialogUIController.DialogClosed -= EndDialog;
    }

    public void EnterPlayMode()
    {
        Cursor.lockState = CursorLockMode.Locked; 
        player.EnableInput();
        
    }

    public void EnterCutsceneMode()
    {
        Cursor.lockState = CursorLockMode.Locked;
        player.DisableInput();
    }

    public void StartDialog(Dialog dialog)
    {
        Cursor.lockState = CursorLockMode.None;
        player.DisableInput();
        dialogUIController.StartDialog(dialog);
    }

    private void EndDialog(Dialog _)
    {
        EnterPlayMode();
    }

}
