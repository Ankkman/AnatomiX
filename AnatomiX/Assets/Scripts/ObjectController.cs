using UnityEngine;

public class ObjectController : MonoBehaviour
{
    public float rotationSpeed = 100.0f;
    public float zoomSpeed = 10.0f;
    public float minZoom = 5.0f;
    public float maxZoom = 20.0f;

    private float currentZoom = 10.0f;

    private void OnEnable()
    {
        CustomVirtualJoystick.OnJoystickMoved += OnJoystickMoved;
    }

    private void OnDisable()
    {
        CustomVirtualJoystick.OnJoystickMoved -= OnJoystickMoved;
    }

    private void OnJoystickMoved(Vector2 input)
    {
        // Rotate the object around the Y axis (horizontal joystick movement)
        transform.Rotate(Vector3.up, input.x * rotationSpeed * Time.deltaTime, Space.World);

        // Zoom the object (vertical joystick movement)
        currentZoom -= input.y * zoomSpeed * Time.deltaTime;
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);
        Camera.main.transform.position = transform.position - Camera.main.transform.forward * currentZoom;
    }
}
