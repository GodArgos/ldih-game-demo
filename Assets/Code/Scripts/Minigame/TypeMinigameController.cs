using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class TypeMinigameController : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private GameObject minigameCanvas;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private GameObject ResultCanvas;
    [SerializeField] private GameObject failedImage;
    [SerializeField] private GameObject successImage;
    [SerializeField] private Animator typingAnimator;
    [SerializeField] private TMP_Text timerText;

    [Header("Parameters")]
    [SerializeField] private int maxLines = 3;
    [SerializeField] private float maxTime = 20f;
    [SerializeField] private float stopTypingDelay = 1.0f;

    private bool gameStarted = false;
    private bool gameEnded = false;
    private float timer = 0f;

    // Para animación de escritura
    private float lastTypedTime = 0f;
    private int lastCharCount = 0;

    private void Start()
    {
        minigameCanvas.SetActive(false);
        ResultCanvas.SetActive(false);
        timer = maxTime;
        StartMinigame();
    }

    private void Update()
    {
        if (!gameStarted || gameEnded) return;

        timer -= Time.deltaTime;

        UpdateTimerDisplay();

        // Detectar si ganó
        if (!string.IsNullOrEmpty(inputField.text))
        {
            int currentLineCount = inputField.textComponent.textInfo.lineCount;

            //  Gana solo cuando ya no cabe más texto (overflow)
            if (currentLineCount >= maxLines && inputField.textComponent.isTextOverflowing)
            {
                EndGame(true);
            }
        }

        // Detectar si perdió
        if (timer <= 0f)
        {
            EndGame(false);
        }

        DetectTypingAnimation();
    }


    public void StartMinigame()
    {
        minigameCanvas.SetActive(true);
        StartCoroutine(StartInput());
    }

    private IEnumerator StartInput()
    {
        yield return null;

        EventSystem.current.SetSelectedGameObject(null);
        inputField.text = ""; // Limpiar
        inputField.Select();
        inputField.ActivateInputField();

        timer = maxTime;
        gameStarted = true;
        gameEnded = false;
        ResultCanvas.SetActive(false);
        lastTypedTime = Time.time;
        lastCharCount = 0;
        typingAnimator.SetBool("isTyping", false);
        typingAnimator.speed = 1f;
    }

    private void EndGame(bool won)
    {
        gameEnded = true;
        gameStarted = false;

        ResultCanvas.SetActive(true);

        if (won)
        {
            failedImage.SetActive(false);
            successImage.SetActive(true);
        }
        else
        {
            successImage.SetActive(false);
            failedImage.SetActive(true);
        }

        //  Detener animación inmediatamente
        typingAnimator.SetBool("isTyping", false);
        typingAnimator.speed = 1f;
    }

    public void CloseMinigame()
    {
        ResultCanvas.SetActive(false);
        minigameCanvas.SetActive(false);
    }

    private void DetectTypingAnimation()
    {
        if (!gameStarted || gameEnded) return; //  Evita actualizar animación si terminó

        int currentCharCount = inputField.text.Length;

        if (currentCharCount != lastCharCount)
        {
            float deltaChars = Mathf.Abs(currentCharCount - lastCharCount);
            float deltaTime = Time.time - lastTypedTime;
            float typingSpeed = deltaTime > 0 ? deltaChars / deltaTime : 1f;

            typingAnimator.SetBool("isTyping", true);
            typingAnimator.speed = Mathf.Clamp(typingSpeed, 0.5f, 5f);

            lastTypedTime = Time.time;
            lastCharCount = currentCharCount;
        }

        if (Time.time - lastTypedTime > stopTypingDelay)
        {
            typingAnimator.SetBool("isTyping", false);
            typingAnimator.speed = 1f;
        }
    }

    private void UpdateTimerDisplay()
    {
        float remainingTime = Mathf.Max(0f, timer);

        int minutes = Mathf.FloorToInt(remainingTime / 60f);
        int seconds = Mathf.FloorToInt(remainingTime % 60f);
        int hundredths = Mathf.FloorToInt((remainingTime * 100f) % 100);

        timerText.text = $"{minutes:00}:{seconds:00}:{hundredths:00}";
    }
}
