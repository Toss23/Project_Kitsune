using System;
using UnityEngine;

public class InteractableView : MonoBehaviour, IInteractableView
{
    public event Action OnTriggerEnter;
    public event Action OnTriggerExit;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Character"))
        {
            OnTriggerEnter?.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Character"))
        {
            OnTriggerExit?.Invoke();
        }
    }
}
