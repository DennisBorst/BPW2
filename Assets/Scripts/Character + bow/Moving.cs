using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Moving : MonoBehaviour
{

    public Vector2 rotationLimit;

    [Range(1, 10)]
    public float sensitivity;

    private float InputX;
    private float InputZ;
    public Vector3 desiredMoveDirection;
    public float speed;
    private float minSpeed;
    private float maxSpeed;
    public Camera cam;
    public CharacterController controller;
    private BowScript bowScript;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        controller = this.GetComponent<CharacterController>();

        minSpeed = speed * 0.75f;
        maxSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Rotate()
    {
        //rotating
        float mousex = Input.GetAxis("Mouse X");
        float mousey = Input.GetAxis("Mouse Y");

        transform.eulerAngles += new Vector3(0, mousex * sensitivity, 0);

        transform.eulerAngles = new Vector3(transform.eulerAngles.x,
        transform.eulerAngles.y, transform.eulerAngles.z);
    }

    void Move()
    {
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

        if(InputZ > 0.5f && InputX > 0.5f || InputZ < -0.5f && InputX < -0.5f || InputZ < -0.5f && InputX > 0.5f || InputZ > 0.5f && InputX < -0.5f)
        {
            speed = minSpeed;
        }
        else
        {
            speed = maxSpeed;
        }
        desiredMoveDirection = forward * InputZ + right * InputX;
        controller.Move(desiredMoveDirection * Time.deltaTime * speed);
    }
}
