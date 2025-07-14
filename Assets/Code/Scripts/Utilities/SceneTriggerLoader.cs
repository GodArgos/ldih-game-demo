using UnityEngine;

public class SceneTriggerLoader : MonoBehaviour
{
    [Tooltip("Referencia al MainMenuController que ya está cargando la escena.")]
    public MainMenuController loader;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            loader?.StartGamePublic();
        }
    }
}
