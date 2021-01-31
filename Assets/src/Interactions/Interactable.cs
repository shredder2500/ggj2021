using System;
using UnityEngine;
using UnityEngine.Events;

namespace src.Interactions
{
  public class Interactable : MonoBehaviour
  {
    public UnityEvent onPlayerEnter;
    public UnityEvent onPlayerExit;
    public UnityEvent onInteract;

    [SerializeField] private ControlsAsset controls;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
      if (other.CompareTag("Player"))
      {
        onPlayerEnter?.Invoke();
        if(!controls) return;
        
        controls.Interact += onInteract.Invoke;
      }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
      if (other.CompareTag("Player"))
      {
        onPlayerExit?.Invoke();
        if (!controls) return;
        
        controls.Interact -= onInteract.Invoke;
      }
    }
  }
}