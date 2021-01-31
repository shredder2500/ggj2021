using System;
using UnityEngine;
using UnityEngine.Events;

namespace src.Interactions
{
  public class Trigger : MonoBehaviour
  {
    public UnityEvent onEnter;

    private void OnTriggerEnter2D(Collider2D other)
    {
      if (other.CompareTag("Player"))
      {
        onEnter.Invoke();
      }
    }
  }
}