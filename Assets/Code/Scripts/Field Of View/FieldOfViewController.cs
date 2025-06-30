using System;
using UnityEngine;

public class FieldOfViewController : MonoBehaviour
{
    [Header("Paramaeters")]
    [SerializeField] private float fov = 360f;
    [SerializeField] private int edgeNumber = 360;
    [SerializeField] private float initialAngle = 0f;
    [SerializeField] private float viewDistance = 8f;
    [SerializeField] private LayerMask layers;
    private Camera currentCamera;
    private Mesh mesh;
    private Vector3 origin;

    private void Start()
    {
        currentCamera = Camera.main;
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        origin = Vector3.zero;
    }

    private void LateUpdate()
    {
        GenerateMesh();
    }

    private void GenerateMesh()
    {
        float currentAngle = initialAngle;
        float angleIncrement = fov / edgeNumber;

        Vector3[] vertices = new Vector3[edgeNumber + 1];
        int[] triangles = new int[edgeNumber * 3];
        Color[] colors = new Color[vertices.Length];

        vertices[0] = transform.localPosition;
        colors[0] = new Color(0, 0, 0, 0); // Centro del cono, sin fade

        int verticesIndex = 1;
        int triangleIndex = 0;

        for (int i = 0; i < edgeNumber; i++)
        {
            Vector3 currentVertice;
            float fadeValue;

            RaycastHit2D raycastHit2D = Physics2D.Raycast(origin, GetVectorFromAngle(currentAngle), viewDistance, layers);

            if (raycastHit2D.collider == null)
            {
                // No hit  exterior libre  aplicar fade
                currentVertice = transform.localPosition + GetVectorFromAngle(currentAngle) * viewDistance;
                fadeValue = 1f;
            }
            else
            {
                // Hit  colisionó con algo  sin fade
                currentVertice = currentCamera.transform.InverseTransformPoint(raycastHit2D.point);
                fadeValue = 0f;
            }

            vertices[verticesIndex] = currentVertice;
            colors[verticesIndex] = new Color(0, 0, 0, fadeValue);

            if (i > 0)
            {
                triangles[triangleIndex + 0] = 0;
                triangles[triangleIndex + 1] = verticesIndex - 1;
                triangles[triangleIndex + 2] = verticesIndex;
                triangleIndex += 3;
            }

            verticesIndex++;
            currentAngle -= angleIncrement;
        }

        // Último triángulo para cerrar el mesh
        triangles[triangleIndex + 0] = 0;
        triangles[triangleIndex + 1] = verticesIndex - 1;
        triangles[triangleIndex + 2] = 1;

        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.colors = colors;
    }


    Vector3 GetVectorFromAngle(float angle)
    {
        float angleRad = angle * (Mathf.PI / 180f);
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }

    public void SetOrigin(Vector3 pos)
    {
        origin = pos;
    }
}
