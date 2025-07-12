using System.Collections;
using UnityEngine;

public class TransitionManager : MonoBehaviour
{
    public static TransitionManager Instance;

    [Header("Dependencies")]
    [SerializeField] private Animator transitionAnimator;
    [SerializeField] private float transitionTime = 1.5f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }

    public void StartTransitionToPosition(Collider2D playerCollider, Rigidbody2D rb, Vector3 targetPosition)
    {
        StartCoroutine(TransitionCoroutine(playerCollider, rb, targetPosition));
    }

    private IEnumerator TransitionCoroutine(Collider2D playerCollider, Rigidbody2D rb, Vector3 targetPosition)
    {
        if (playerCollider != null)
            playerCollider.enabled = false;

        // Fade to black
        transitionAnimator.SetTrigger("ToBlack");
        yield return new WaitForSeconds(transitionTime);

        // Move player
        rb.position = targetPosition;

        // Fade back to white
        transitionAnimator.SetTrigger("ToWhite");
        //yield return new WaitForSeconds(transitionTime);

        if (playerCollider != null)
            playerCollider.enabled = true;
    }
}
