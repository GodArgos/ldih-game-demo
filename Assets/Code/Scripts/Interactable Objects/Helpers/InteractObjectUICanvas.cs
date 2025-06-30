using UnityEngine;

[RequireComponent (typeof(InteractObjectUICreator))]
public class InteractObjectUICanvas : MonoBehaviour
{
    private GameObject uiCanvas;

    private void Start()
    {
        uiCanvas = GetComponent<InteractObjectUICreator>().uiCanvas;
    }

    public void ShowUI()
    {
        if (uiCanvas != null) uiCanvas.SetActive(true);
    }

    public void HideUI()
    {
        if (uiCanvas != null) uiCanvas.SetActive(false);
    }
}
