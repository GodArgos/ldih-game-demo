using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteAlways]
public class InteractObjectUICreator : MonoBehaviour
{
    [Header("Interaction UI")]
    [Tooltip("UI Prefab that will be instantiated on top of the object when the player is in range of interaction.")]
    [SerializeField] private GameObject uiPrefab;
    [Tooltip("Axis offset for the UI Prefab to be instantiated. EX: [0,0,0] would be on the center of the interactable object.")]
    [SerializeField] private Vector3 axisOffset = Vector3.zero;

    private Transform uiHolder;
    [HideInInspector] public GameObject uiCanvas;

    private void Awake()
    {
        if (!Application.isPlaying)
        {
            CreateUIHolder();
        }
    }

    private void Reset()
    {
        if (!Application.isPlaying)
        {
            CreateUIHolder();
        }
    }

    private void OnValidate()
    {
        if (!Application.isPlaying)
        {
            CreateUIHolder();
        }
    }

    private void CreateUIHolder()
    {
        if (uiHolder == null) // Buscar si ya existe
        {
            foreach (Transform child in transform)
            {
                if (child.name == "UI_Holder")
                {
                    uiHolder = child;
                    break;
                }
            }
        }

        if (uiHolder == null) // Si no existe, crearlo
        {
            GameObject newHolder = new GameObject("UI_Holder");
            newHolder.transform.SetParent(this.transform);
            newHolder.transform.localPosition = axisOffset; // Aplicar el offset aquí
            uiHolder = newHolder.transform;

#if UNITY_EDITOR
            Undo.RegisterCreatedObjectUndo(newHolder, "Created UI_Holder");
            EditorUtility.SetDirty(this);
#endif
        }
        else
        {
            // Si ya existe, asegurar que tenga el offset correcto
            uiHolder.localPosition = axisOffset;
        }

        EnsureUICanvasExists();
    }

    private void EnsureUICanvasExists()
    {
        if (uiPrefab != null && uiCanvas == null && uiHolder.childCount == 0)
        {
            uiCanvas = Instantiate(uiPrefab, uiHolder);
            uiCanvas.transform.localPosition = Vector3.zero; // No aplicar offset al Canvas
            uiCanvas.SetActive(false);

#if UNITY_EDITOR
            Undo.RegisterCreatedObjectUndo(uiCanvas, "Instantiated UI Prefab");
            EditorUtility.SetDirty(this);
#endif
        }
        else if (uiHolder.childCount > 0)
        {
            uiCanvas = uiHolder.GetChild(0).gameObject;
        }
    }

    public Transform GetUIHolder()
    {
        return uiHolder;
    }
}
