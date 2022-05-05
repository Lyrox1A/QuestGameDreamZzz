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
        EnterPlayMode();
    }

    private void OnDisable()
    {
        DialogUIController.DialogClosed -= EndDialog;
    }

    private void EnterPlayMode()
    {
        Cursor.lockState = CursorLockMode.Locked; 
        player.EnableInput();
        
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
