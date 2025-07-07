using UnityEngine;
using System.Collections;

public class RoomTransferController : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private Transform targetPoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Rigidbody2D rb = collision.transform.parent.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                TransitionManager.Instance.StartTransitionToPosition(collision, rb, targetPoint.position);
            }
        }
    }
}
