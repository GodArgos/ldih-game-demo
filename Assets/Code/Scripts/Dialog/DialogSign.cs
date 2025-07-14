using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Coffee.UIEffects;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[System.Serializable]
public class DialogContent
{
    public enum ContentType { Text, Image }

    public ContentType Type = ContentType.Text;

    [TextArea(3, 5)]
    public string DialogText;

    public GameObject DialogImage;

    public bool AllowEvents = false;

    public UnityEvent OnDialogNext;

    public string speakerName;
}

public class DialogSign : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private TMP_Text textBox;
    [SerializeField] private TMP_Text speakerBox;
    private UIEffect textEffect;
    [SerializeField] private TypeWriterEffect typewriter;

    [Header("Settings")]
    [SerializeField] private float disolveFactor = 1.0f;

    [Header("Dialog System")]
    [SerializeField] private List<DialogContent> dialogs = new List<DialogContent>();

    private int currentDialogIndex = 0;
    private DefaultInputSystem inputSystem;
    private bool allowNext = false;

    private void Awake()
    {
        inputSystem = new DefaultInputSystem();
    }

    private void Start()
    {
        GameManager.Instance.OnInteraction = true;
        textEffect = textBox.GetComponent<UIEffect>();
        InitializeDialog();
        HideNextButton();
    }

    private void InitializeDialog()
    {
        if (dialogs.Count == 0) return;

        var currentContent = dialogs[currentDialogIndex];
        DisplayDialog(currentContent);
    }

    public void OnNextButtonClicked(InputAction.CallbackContext context)
    {
        if (!allowNext) return;
        
        currentDialogIndex++;
        if (currentDialogIndex >= dialogs.Count) return;

        StartCoroutine(DisolveText());
    }

    private void ShowNextButton()
    {
        if (currentDialogIndex >= dialogs.Count - 1)
            return;

        allowNext = true;
        //nextButton.SetActive(true);
    }

    private void HideNextButton()
    {
        allowNext = false;
        //nextButton.SetActive(false);
    }

    private void OnEnable()
    {
        TypeWriterEffect.CompleteTextRevealed += ShowNextButton;
        inputSystem.Enable();
        inputSystem.Dialog.Next.performed += OnNextButtonClicked;
    }

    private void OnDisable()
    {
        TypeWriterEffect.CompleteTextRevealed -= ShowNextButton;
        inputSystem.Disable();
        inputSystem.Dialog.Next.performed -= OnNextButtonClicked;
        GameManager.Instance.OnInteraction = false;
    }

    private IEnumerator DisolveText()
    {
        HideNextButton();

        if (dialogs[currentDialogIndex - 1].Type == DialogContent.ContentType.Text)
        {
            while (textEffect.transitionRate < 1)
            {
                textEffect.transitionRate += 0.05f * disolveFactor;
                yield return new WaitForSeconds(0.05f * disolveFactor);
            }
        }

        InitializeDialog();
        textEffect.transitionRate = 0;
    }

    private void DisplayDialog(DialogContent content)
    {
        // Ocultar todos
        textBox.gameObject.SetActive(false);
        speakerBox.gameObject.SetActive(false);

        foreach (var dialog in dialogs)
        {
            if (dialog.DialogImage != null)
                dialog.DialogImage.SetActive(false);
        }

        if (content.Type == DialogContent.ContentType.Text)
        {
            textBox.text = content.DialogText;
            textBox.maxVisibleCharacters = 0;
            textBox.gameObject.SetActive(true);

            //  Mostrar emisor si hay
            if (!string.IsNullOrEmpty(content.speakerName))
            {
                speakerBox.text = content.speakerName;
                speakerBox.gameObject.SetActive(true);
            }
        }
        else if (content.Type == DialogContent.ContentType.Image && content.DialogImage != null)
        {
            content.DialogImage.SetActive(true);
            ShowNextButton();
        }

        if (content.AllowEvents)
        {
            content.OnDialogNext?.Invoke();
        }
    }
}
