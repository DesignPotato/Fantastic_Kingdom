using UnityEngine;
using System.Collections;
public class CharacterMouseLook : MonoBehaviour {

    public float xSpeed = 250.0f;
    private float x = 0.0f;

    private bool locked = true;

    void Start () {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
    }

	void LateUpdate () {
        if (Input.GetKeyDown(KeyCode.C))
        {
            locked = false;
        }
        if (Input.GetKeyUp(KeyCode.C))
        {
            locked = true;
        }
        if (locked)
        {
            //x = ThirdPersonCamera.xRotation;
            Quaternion rotation = Quaternion.Euler(0, x, 0);
            transform.rotation = rotation;
        }
        
    }
}
