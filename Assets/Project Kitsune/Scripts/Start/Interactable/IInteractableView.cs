using System;

public interface IInteractableView
{
    public event Action OnTriggerEnter;
    public event Action OnTriggerExit;
}