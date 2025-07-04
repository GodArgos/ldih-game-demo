using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private Button StartButton;
    [SerializeField] private Button ExitButton;

    private AsyncOperation asyncOperation;

    private void Awake()
    {
        if (StartButton != null)
        {
            StartButton.interactable = false;
            StartButton.onClick.AddListener(StartGame);
        }

        StartCoroutine(LoadGameAsync());
    }

    private void OnEnable()
    {
        if (ExitButton != null)
        {
            ExitButton.onClick.AddListener(ExitGame);
        }
    }

    private void OnDisable()
    {
        if (ExitButton != null)
        {
            ExitButton.onClick.RemoveListener(ExitGame);
        }
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
                if (StartButton != null)
                    StartButton.interactable = true;
                yield break;
            }

            yield return null;
        }
    }

    private void StartGame()
    {
        if (asyncOperation != null && !asyncOperation.allowSceneActivation)
        {
            asyncOperation.allowSceneActivation = true;
        }
    }

    private void ExitGame()
    {
        Application.Quit();
    }
}
