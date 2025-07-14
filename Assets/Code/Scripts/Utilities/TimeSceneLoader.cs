using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeSceneLoader : MonoBehaviour
{
    [Tooltip("Tiempo en segundos antes de cambiar de escena.")]
    public float delayInSeconds = 5f;

    private AsyncOperation asyncOperation;

    private void Start()
    {
        StartCoroutine(LoadFirstSceneAfterDelay());
    }

    private IEnumerator LoadFirstSceneAfterDelay()
    {
        // Start loading the first scene (index 0)
        asyncOperation = SceneManager.LoadSceneAsync(0);
        asyncOperation.allowSceneActivation = false;

        // Wait until loading reaches 90%
        while (asyncOperation.progress < 0.9f)
        {
            yield return null;
        }

        // Wait X seconds before completing the activation
        yield return new WaitForSeconds(delayInSeconds);

        asyncOperation.allowSceneActivation = true;
    }
}
