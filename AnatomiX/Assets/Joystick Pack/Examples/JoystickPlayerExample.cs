using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickPlayerExample : MonoBehaviour
{
    private float speed = 40.0f;
    private VariableJoystick variableJoystick;

    public void SetJoystick(VariableJoystick joystick)
        {
            variableJoystick = joystick;
        }

    public void Update() //was FixedUpdate before
    {
        /**Vector3 direction = Vector3.forward * variableJoystick.Vertical + Vector3.right * variableJoystick.Horizontal;
        rb.AddForce(direction * speed * Time.fixedDeltaTime, ForceMode.VelocityChange);**/

         // Get the joystick input
        float horizontalInput = variableJoystick.Horizontal;
        float verticalInput = variableJoystick.Vertical;

        // Calculate the rotation based on joystick input
        Vector3 rotation = new Vector3(-verticalInput, horizontalInput, 0) * speed * Time.fixedDeltaTime;

        // Apply the rotation to the object
        transform.Rotate(rotation);
    }
}