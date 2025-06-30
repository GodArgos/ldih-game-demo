using UnityEngine;

[RequireComponent (typeof(PlayerController))]
public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    private Vector2 lastDirection = Vector2.zero;

    private void Awake()
    {
        animator = GetComponent<PlayerController>().Animator;
    }

    public void UpdateAnimation(Vector2 movementInput)
    {
        if (movementInput != Vector2.zero)
        {
            lastDirection = movementInput.normalized;
        }

        // Actualiza los parámetros del Animator
        animator.SetFloat("Horizontal", movementInput != Vector2.zero ? movementInput.x : lastDirection.x);
        animator.SetFloat("Vertical", movementInput != Vector2.zero ? movementInput.y : lastDirection.y);
        animator.SetFloat("Speed", movementInput != Vector2.zero ? movementInput.sqrMagnitude : 0f);
    }
}
