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

        // Simulación de un diálogo o proceso de interacción
        StartCoroutine(SimulateInteractionProcess());    
    }

    private IEnumerator SimulateInteractionProcess()
    {
        IsInteractable = false; // Bloquea la interacción mientras el proceso está en curso
        yield return new WaitForSeconds(3f); // Simulación de un tiempo de diálogo

        IsInteractable = true; // Vuelve a permitir la interacción
        OnInteractionFinished?.Invoke(); // Llama al evento cuando el diálogo termine
    }

    private void HandlePostinteractability()
    {
        IsInteractable = allowPostInteraction ? true : false;
    }
    #endregion
}
