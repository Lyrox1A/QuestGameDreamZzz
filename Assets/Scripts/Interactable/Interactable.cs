using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
   [SerializeField] private UnityEvent onInteract;

   [SerializeField] private UnityEvent onSelect;

   [SerializeField] private UnityEvent onDeselect;
   
   

   private void Start()
   {
      List<Interaction> interactions = GetComponentsInChildren<Interaction>(true).ToList();

      if (interactions.Count > 0)
      {
         interactions[0].gameObject.SetActive(true);
      }
      
         
   }

   public void Interact()
   {
      onInteract.Invoke();

      Interaction interaction = FindActiveInteraction();
      if (interaction != null && GetComponent<Collectable>() == false)
      {
         interaction.Excute();
         FindObjectOfType<PlayerController>().GetComponentInChildren<Animator>().SetBool("OnInteract", true);
         
      }
      StartCoroutine(ResetInteraction());
   }

   IEnumerator ResetInteraction()
   {
      yield return new WaitForSeconds(4f);
      FindObjectOfType<PlayerController>().GetComponentInChildren<Animator>().SetBool("OnInteract", false);
   }

   public void Select()
   {
      onSelect.Invoke();
   }
   
   public void Deselect()
   {
      onDeselect.Invoke();
   }

   private Interaction FindActiveInteraction()
   {
      return GetComponentInChildren<Interaction>(false);
      
   }
   
}
