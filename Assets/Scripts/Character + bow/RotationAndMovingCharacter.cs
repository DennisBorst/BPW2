using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationAndMovingCharacter : MonoBehaviour
{

    public Vector2 rotationLimit;

    [Range(1,10)]
    public float sensitivity;

    public float InputX;
    public float InputZ;
    public Vector3 desiredMoveDirection;
    public float speed;
    public Camera cam;
    public CharacterController controller;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        cam = Camera.main;
        controller = this.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveAndRotate();
    }

    void MoveAndRotate()
    {
        //rotating
        float mousex = Input.GetAxis("Mouse X");
        float mousey = Input.GetAxis("Mouse Y");

        transform.eulerAngles += new Vector3(-mousey * sensitivity, mousex * sensitivity, 0);

        transform.eulerAngles = new Vector3(ClampAngle(transform.eulerAngles.x, rotationLimit.x, rotationLimit.y),
        transform.eulerAngles.y, transform.eulerAngles.z);

        //moving
        InputX = Input.GetAxis("Horizontal");
        InputZ = Input.GetAxis("Vertical");

        var camera = Camera.main;
        var forward = cam.transform.forward;
        var right = cam.transform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        desiredMoveDirection = forward * InputZ + right * InputX;
        controller.Move(desiredMoveDirection * Time.deltaTime * speed);
    }

    float ClampAngle(float angle, float from, float to)
    {
        // accepts e.g. -80, 80
        if (angle < 0f) angle = 360 + angle;
        if (angle > 180f) return Mathf.Max(angle, 360 + from);
        return Mathf.Min(angle, to);
    }
}
