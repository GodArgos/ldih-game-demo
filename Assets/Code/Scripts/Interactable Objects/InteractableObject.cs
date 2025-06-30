using UnityEngine;
using System;
using System.Collections;
using Unity.VisualScripting;

[RequireComponent(typeof(InteractObjectUICanvas))]
public class InteractableObject : MonoBehaviour
{
    #region Variables
    [Tooltip("State of object interactability")]
    public bool IsInteractable = true;
    [Tooltip("If 'TRUE', allows player to interact with the object indefinitely. If 'FALSE', the object will only be interactable once.")]
    [SerializeField] private bool allowPostInteraction = true;
    public virtual bool AllowPostInteraction
    {
        get { return allowPostInteraction; }
        set { allowPostInteraction = value; }
    }

    public event Action OnInteractionFinished;
    #endregion

    #region Unity Methods
    public virtual void Start()
    {
        OnInteractionFinished += HandlePostinteractability;
    }
    #endregion

    #region Methods
    public virtual void Interact()
    {
        if (!IsInteractable) return;

        Debug.Log($"Interacted with: {gameObject.name}");

        // Simulaci�n de un di�logo o proceso de interacci�n
        StartCoroutine(SimulateInteractionProcess());    
    }

    private IEnumerator SimulateInteractionProcess()
    {
        IsInteractable = false; // Bloquea la interacci�n mientras el proceso est� en curso
        yield return new WaitForSeconds(3f); // Simulaci�n de un tiempo de di�logo

        IsInteractable = true; // Vuelve a permitir la interacci�n
        OnInteractionFinished?.Invoke(); // Llama al evento cuando el di�logo termine
    }

    private void HandlePostinteractability()
    {
        IsInteractable = allowPostInteraction ? true : false;
    }
    #endregion
}
