using UnityEngine;
using System.Collections;

public class ThirdPersonCamera : MonoBehaviour {

    public Transform target;
    public float distance = 4.0f;

    public float bufferup = 0.1f;
    public float bufferright = 0.75f;

    public float xSensitivity = 250.0f;
    public float ySensitivity = 120.0f;

    public float yMinLimit = -25f;
    public float yMaxLimit = 70f;

    public static float xRotation = 0.0f;

    private float x = 0.0f;
    private float y = 0.0f;

    void Start () {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Vector3 angles = transform.eulerAngles;
        xRotation = angles.y;
        y = angles.x;
    }
	

	void LateUpdate () {
        if (target)
        {
            //Mouse look controls
            xRotation += Input.GetAxis("Mouse X") * xSensitivity * 0.02f;
            y -= Input.GetAxis("Mouse Y") * ySensitivity * 0.02f;

            //Controller look controls
			if(Input.GetAxis("JoyLookHori") > 0.4 || Input.GetAxis("JoyLookHori") < -0.4)
				xRotation += Input.GetAxis("JoyLookHori") * xSensitivity * 0.02f;
			if(Input.GetAxis("JoyLookVert") > 0.4 || Input.GetAxis("JoyLookVert") < -0.4)
				y += Input.GetAxis("JoyLookVert") * ySensitivity * 0.02f;

            y = ClampAngle(y, yMinLimit, yMaxLimit);

            Quaternion rotation = Quaternion.Euler(y, xRotation, 0);
            Vector3 position = rotation * new Vector3(bufferright, 0.0f, -distance) + target.position + new Vector3(0.0f, bufferup, 0.0f);

            transform.rotation = rotation;
            transform.position = position;
        }
    }

    //Restricts angle between +-360 degrees
    float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }
}
