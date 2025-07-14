using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerController))]
public class PlayerInteractor : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private float interactionRange = 5f;
    [SerializeField] private LayerMask interactableLayer;

    private Transform sourceTransform;
    private Vector2 lastDirection = Vector2.down;
    private InteractObjectUICanvas currentUI;
    private bool isInteracting = false;
    private InteractableObject currentInteractableObject;

    private void Awake()
    {
        sourceTransform = GetComponent<PlayerController>().InteractionPoint;
    }

    private void Update()
    {
        if (!isInteracting) // Solo muestra la UI si no está interactuando
        {
            ShowInteractionUI();
        }
    }

    public void HandleInteraction(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            InteractWithObject();
        }
    }

    private void ShowInteractionUI()
    {
        if (isInteracting) return; // No mostrar UI si está interactuando

        RaycastHit2D hit = Physics2D.Raycast(sourceTransform.position, lastDirection, interactionRange, interactableLayer);

        if (hit.collider != null)
        {
            
            InteractableObject interactableObject = hit.collider.GetComponent<InteractableObject>();
            if (interactableObject != null && interactableObject.IsInteractable)
            {
                InteractObjectUICanvas uiCanvas = hit.collider.GetComponent<InteractObjectUICanvas>();
                if (uiCanvas != null)
                {
                    if (currentUI != uiCanvas)
                    {
                        if (currentUI != null) currentUI.HideUI();
                        uiCanvas.ShowUI();
                        currentUI = uiCanvas;
                    }
                    return;
                }
            }
        }

        if (currentUI != null)
        {
            currentUI.HideUI();
            currentUI = null;
        }
    }

    private void InteractWithObject()
    {
        if (lastDirection == Vector2.zero || isInteracting) return;

        RaycastHit2D hit = Physics2D.Raycast(sourceTransform.position, lastDirection, interactionRange, interactableLayer);

        if (hit.collider != null)
        {
            InteractableObject interactableObject = hit.collider.GetComponent<InteractableObject>();

            if (interactableObject != null && interactableObject.IsInteractable)
            {
                if (currentUI != null)
                {
                    currentUI.HideUI();
                    currentUI = null;
                }

                isInteracting = true;
                currentInteractableObject = interactableObject;
                currentInteractableObject.OnInteractionFinished += ResetInteraction; // Suscribir evento

                interactableObject.Interact();
            }
        }
    }

    private void ResetInteraction()
    {
        if (currentInteractableObject != null)
        {
            currentInteractableObject.OnInteractionFinished -= ResetInteraction; // Desuscribir evento
        }

        isInteracting = false;
    }

    public void SetDirection(Vector2 direction)
    {
        if (direction == Vector2.zero) return;

        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            lastDirection = direction.x > 0 ? Vector2.right : Vector2.left;
        }
        else
        {
            lastDirection = direction.y > 0 ? Vector2.up : Vector2.down;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (sourceTransform != null)
        {
            Gizmos.DrawRay(sourceTransform.position, lastDirection * interactionRange);
        }
    }
}
