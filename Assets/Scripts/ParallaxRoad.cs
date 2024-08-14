using UnityEngine;

public class ParallaxRoad : MonoBehaviour
{
    public GameObject[] roadParts;
    public float speed = 5f;
    private float partHeight;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        AdjustRoadSize();

        partHeight = roadParts[0].GetComponent<SpriteRenderer>().bounds.size.y;

        SortRoadParts();
    }

    void Update()
    {
        MoveRoad();
        CheckAndRepositionRoad();
    }

    void AdjustRoadSize()
    {
        float screenWidth = mainCamera.orthographicSize * 2f * mainCamera.aspect;

        foreach (GameObject roadPart in roadParts)
        {
            SpriteRenderer sr = roadPart.GetComponent<SpriteRenderer>();
            float roadWidth = sr.bounds.size.x;
            float scaleX = screenWidth / roadWidth;

            roadPart.transform.localScale = new Vector3(scaleX, roadPart.transform.localScale.y, roadPart.transform.localScale.z);
        }
    }

    void MoveRoad()
    {
        foreach (GameObject roadPart in roadParts)
        {
            roadPart.transform.Translate(Vector2.down * speed * Time.deltaTime);
        }
    }

    void CheckAndRepositionRoad()
    {
        SortRoadParts();

        GameObject lowerPart = roadParts[2];
        float lowerPartTopEdge = lowerPart.transform.position.y + partHeight / 2;

        if (lowerPartTopEdge < mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y)
        {
            GameObject upperPart = roadParts[0];
            lowerPart.transform.position = new Vector3(upperPart.transform.position.x, upperPart.transform.position.y + partHeight, upperPart.transform.position.z);
        }
    }

    void SortRoadParts()
    {
        System.Array.Sort(roadParts, (a, b) => b.transform.position.y.CompareTo(a.transform.position.y));
    }
}