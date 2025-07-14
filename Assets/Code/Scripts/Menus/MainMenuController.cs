using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class MainMenuController : MonoBehaviour
{
    private AsyncOperation asyncOperation;
    private bool gameReady = false;
    private DefaultInputSystem inputSystem;
    public bool withButton = true;

    private void Awake()
    {
        // Start loading the game scene in the background
        StartCoroutine(LoadGameAsync());
    }

    private void OnEnable()
    {
        if (!withButton) return;
        inputSystem = new DefaultInputSystem();
        inputSystem.Enable();
        inputSystem.Menu.Start.performed += StartGame;
    }

    private void OnDisable()
    {
        inputSystem.Disable();
    }

    private IEnumerator LoadGameAsync()
    {
        yield return new WaitForSeconds(1f);

        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        asyncOperation = SceneManager.LoadSceneAsync(nextSceneIndex);
        asyncOperation.allowSceneActivation = false;

        while (!asyncOperation.isDone)
        {
            if (asyncOperation.progress >= 0.9f)
            {
                gameReady = true;
                yield break;
            }

            yield return null;
        }
    }

    private void StartGame(InputAction.CallbackContext context)
    {
        if (asyncOperation != null && !asyncOperation.allowSceneActivation)
        {
            asyncOperation.allowSceneActivation = true;
        }
    }

    public void StartGamePublic()
    {
        if (asyncOperation != null && !asyncOperation.allowSceneActivation)
        {
            asyncOperation.allowSceneActivation = true;
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
