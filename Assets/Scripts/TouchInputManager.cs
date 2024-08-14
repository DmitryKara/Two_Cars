using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class TouchInputManager : MonoBehaviour
{
    public CarController leftCarController;
    public CarController rightCarController;

    public Pause pauseManager;

    private CarControllerManager controls;

    private void Awake()
    {
        controls = new CarControllerManager();
    }

    private void OnEnable()
    {
        controls.Gameplay.TouchPress.performed += OnTouch;
        controls.Gameplay.Enable();
    }

    private void OnDisable()
    {
        controls.Gameplay.TouchPress.performed -= OnTouch;
    }

    private void OnTouch(InputAction.CallbackContext context)
    {
        if (Countdown.isCountdownActive || IsPointerOverUIElement() || pauseManager.IsPaused)
        {
            return;
        }

        Vector2 touchPosition = controls.Gameplay.TouchPosition.ReadValue<Vector2>();
        float screenWidth = Screen.width;

        if (touchPosition.x < screenWidth / 2)
        {
            leftCarController.StartLaneChange();
        }
        else
        {
            rightCarController.StartLaneChange();
        }
    }

    private bool IsPointerOverUIElement()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current)
        {
            position = InputSystem.GetDevice<Pointer>().position.ReadValue()
        };

        var results = new System.Collections.Generic.List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        return results.Count > 0;
    }
}

