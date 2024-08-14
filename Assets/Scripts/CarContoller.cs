using UnityEngine;
using System.Collections;

public class CarController : MonoBehaviour
{
    public KeyCode changeLaneKey;
    private int currentLane = 0;
    private float screenWidth;
    public float laneChangeDuration = 0.3f;
    public float turnAngle = 45f;

    private Vector3 initialPosition;
    private Coroutine laneChangeCoroutine;

    private void Awake()
    {
        screenWidth = Camera.main.orthographicSize * Camera.main.aspect * 2f;
    }

    private void Start()
    {
        initialPosition = transform.position;
        SetInitialPosition();
    }

    public void StartLaneChange()
    {
        if (!IsChangingLane)
        {
            laneChangeCoroutine = StartCoroutine(ChangeLane());
        }
    }

    private bool IsChangingLane { get; set; } = false;

    private IEnumerator ChangeLane()
    {
        if (IsMoveAllowed())
        {
            IsChangingLane = true;
            currentLane = 1 - currentLane;
            float[] currentLanes = (transform.position.x < 0) ? GetLeftCarLanes() : GetRightCarLanes();
            Vector3 startPosition = transform.position;
            Vector3 endPosition = startPosition;
            endPosition.x = currentLanes[currentLane];

            Quaternion startRotation = transform.rotation;
            Quaternion middleRotation = Quaternion.Euler(0, 0, turnAngle * (currentLane == 0 ? 1 : -1));
            Quaternion endRotation = Quaternion.Euler(0, 0, 0);

            float elapsedTime = 0;

            while (elapsedTime < laneChangeDuration)
            {
                float indexTime = elapsedTime / laneChangeDuration;
                transform.position = Vector3.Lerp(startPosition, endPosition, indexTime);
                if (indexTime < 0.5f)
                {
                    transform.rotation = Quaternion.Lerp(startRotation, middleRotation, indexTime * 2);
                }
                else
                {
                    transform.rotation = Quaternion.Lerp(middleRotation, endRotation, (indexTime - 0.5f) * 2);
                }

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            transform.position = endPosition;
            transform.rotation = endRotation;
            IsChangingLane = false;
        }
    }

    private bool IsMoveAllowed()
    {
        return true;
    }

    private void SetInitialPosition()
    {
        float[] initialLanes = (initialPosition.x < 0) ? GetLeftCarLanes() : GetRightCarLanes();
        currentLane = (initialPosition.x < 0) ? 1 : 0;
        Vector3 newPosition = initialPosition;
        newPosition.x = initialLanes[currentLane];
        transform.position = newPosition;
        transform.rotation = Quaternion.identity;

        UpdatePosition();
    }

    private void UpdatePosition()
    {
        float[] currentLanes = (transform.position.x < 0) ? GetLeftCarLanes() : GetRightCarLanes();
        Vector3 newPosition = transform.position;
        newPosition.x = currentLanes[currentLane];
        transform.position = newPosition;
    }

    public void ResetCar()
    {
        if (laneChangeCoroutine != null)
        {
            StopCoroutine(laneChangeCoroutine);
        }

        IsChangingLane = false;
        SetInitialPosition();
        UpdatePosition();
    }

    private float[] GetLeftCarLanes()
    {
        float laneWidth = screenWidth / 4f;
        float leftLaneCenter = -screenWidth / 4f;
        return new float[] { leftLaneCenter - laneWidth / 2f, leftLaneCenter + laneWidth / 2f };
    }

    private float[] GetRightCarLanes()
    {
        float laneWidth = screenWidth / 4f;
        float rightLaneCenter = screenWidth / 4f;
        return new float[] { rightLaneCenter - laneWidth / 2f, rightLaneCenter + laneWidth / 2f };
    }
}