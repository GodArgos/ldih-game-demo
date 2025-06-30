using UnityEngine;

public class LightController : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private float visionDistance = 30;
    [SerializeField] private LayerMask layers;
    [SerializeField] private string wallTag = "Wall";
    
    private GameObject[] terrainWalls;
    private SpriteRenderer[] terrainMeshes;

    private void Start()
    {
        GetAllWalls();

        for (int i = 0; i < terrainWalls.Length; i++)
        {
            Vector2[] vertices = terrainMeshes[i].sprite.vertices;
            for (int j = 0; j < vertices.Length; j++)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawSphere(vertices[i], 3);
            }
        }
    }

    void Update()
    {
        if (terrainWalls == null) return;

        DrawEdgeWallRay();
    }

    private void DrawAngleRay(Vector2 point)
    {
        //Vector2 direction = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));

        RaycastHit2D hit = Physics2D.Raycast(transform.position, point, visionDistance, layers);

        if (hit.collider != null)
        {
            Debug.DrawRay(transform.position, point * hit.distance, Color.green);
            Debug.Log("DID HID");
        }
        else
        {
            Debug.DrawRay(transform.position, point * visionDistance, Color.red);
            Debug.Log("no HID");
        }
    }

    private void GetAllWalls()
    {
        terrainWalls = GameObject.FindGameObjectsWithTag(wallTag);
        terrainMeshes = new SpriteRenderer[terrainWalls.Length];

        for (int i = 0; i < terrainWalls.Length; i++)
        {
            terrainMeshes[i] = terrainWalls[i].GetComponent<SpriteRenderer>();
        }
    }

    private void DrawEdgeWallRay()
    {
        for (int i = 0; i < terrainWalls.Length; i++)
        {
            Vector2[] vertices = terrainMeshes[i].sprite.vertices;
            for (int j = 0; j < vertices.Length; j++)
            {
                DrawAngleRay(vertices[j]);
            }
        }
    }


}
