using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectAndMouseHover : MonoBehaviour, IPointerEnterHandler, IDeselectHandler
{
    private Selectable selectable;

    private void Awake()
    {
        selectable = GetComponent<Selectable>();
        if (selectable == null)
        {
            Debug.LogWarning("No Selectable found on GameObject", this);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!selectable.interactable)
        {
            return;
        }

        if (EventSystem.current.alreadySelecting)
        {
            return;
        }
        
        EventSystem.current.SetSelectedGameObject(gameObject);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        if (!selectable.interactable)
        {
            return;
        }
        
        selectable.OnPointerExit(null);
    }
}
