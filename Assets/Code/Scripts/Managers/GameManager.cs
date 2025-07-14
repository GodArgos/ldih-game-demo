using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }

    [SerializeField] private PlayerController playerController;
    [SerializeField] private GameObject HUD;

    private bool onInteraction = false;
    public bool OnInteraction
    {
        get
        {
            return onInteraction;
        }
        set
        {
            if (value && playerController != null)
            {
                playerController.ResetAnimator();
                playerController.enabled = false;
            }
            else if (!value && playerController != null)
            {
                playerController.enabled = true;
            }

            if (HUD != null)
            {
                HUD.SetActive(!value);
            }

            onInteraction = value;
        }
    }

    private bool hasMap = false;
    public bool HasMap
    {
        get
        {
            return hasMap;
        }
        set
        {
            HUD.SetActive(value);

            hasMap = value;
        }
    }
}
