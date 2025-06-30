using UnityEngine;
using UnityEngine.Events;

public class InteractableEventObject : InteractableObject
{
    #region Variables
    [Space(5)]
    [Tooltip("Actions done when interacting has ended.")]
    public UnityEvent OnPostInteractionActions;
    #endregion

    #region Methods
    public override void Interact()
    {
        base.Interact();
        OnPostInteractionActions?.Invoke();
    }

    public void SimulateEndedAction()
    {
        Debug.Log("Ending action has been done.");
    }
    #endregion
}
