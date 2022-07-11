using System;

using DG.Tweening;

using UnityEngine;
using UnityEngine.EventSystems;

public class DialogUIController : MonoBehaviour
{
    public static event Action<Dialog> DialogOpened;  
    
    public static event Action<Dialog> DialogClosed;

    [SerializeField] private DialogBox dialogBox; 
    
    private Dialog currentDialog;
    private int currentIndex;

    private void OnEnable()
    {
        DialogBox.DialogContinued += AdvanceDialogContinue;
    }

    private void Start()
    {
        dialogBox.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        DialogBox.DialogContinued -= AdvanceDialogContinue;
    }

    public void StartDialog(Dialog dialog)
    {
        bool alreadyOpen = currentDialog != null;

        if (alreadyOpen)
        {
            currentDialog.onDialogEnd.Invoke();
            
        }
        
        currentDialog = dialog;
        if (!alreadyOpen)
        {
            OpenDialog();
        }
        
        DisplayDialogEntry(0);
    }

    private void OpenDialog()
    {
        if (DialogOpened != null)
        {
            DialogOpened(currentDialog);
        }
        dialogBox.gameObject.SetActive(true);
        dialogBox.DOShow();
        FindObjectOfType<PlayerController>().GetComponent<PlayerController>().enabled = false;
    }

    private void CloseDialog()
    {
        Dialog finishedDialog = currentDialog;

        currentDialog = null;
        currentIndex = 0;

        EventSystem.current.SetSelectedGameObject(null);
        
        dialogBox.DOHide()
                 .OnComplete(() =>
                 {
                     dialogBox.gameObject.SetActive(true);
                 });

        if (DialogClosed != null)
        {
            DialogClosed(finishedDialog);
        }
        
        finishedDialog.onDialogEnd.Invoke();
        FindObjectOfType<PlayerController>().GetComponent<PlayerController>().enabled = true;
    }

        public void DisplayDialogEntry(int index)
    {
        if (index >= currentDialog.entries.Count)
        {
            CloseDialog();
            return;
        }
        
        currentIndex = index;
        DialogEntry entry = currentDialog.entries[currentIndex];
        dialogBox.DisplayDialogEntry(entry);
    }

    private void AdvanceDialogContinue(DialogBox _)
    {
        currentIndex++;
        DisplayDialogEntry(currentIndex);
    }
}

