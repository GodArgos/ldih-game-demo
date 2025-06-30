using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<PlayerController>().Rigidbody;
    }

    public void Move(Vector2 movementInput, float speed)
    {
        if (movementInput != Vector2.zero)
        {
            rb.linearVelocity = movementInput.normalized * speed;
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }
}
