using UnityEngine;
using System.Collections;
//SHOULD MOVE THIS INTO MAIN CAMERA SCRIPT
public class CharacterMouseLook : MonoBehaviour {

    public float xSpeed = 250.0f;
    private float x = 0.0f;

    void Start () {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
    }

	void LateUpdate () {
        x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
        Quaternion rotation = Quaternion.Euler(0, x, 0);
        transform.rotation = rotation;
    }
}
