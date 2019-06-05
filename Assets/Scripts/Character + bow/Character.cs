using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{

    public Vector2 rotationLimit;

    [Range(1,10)]
    public float sensitivity;

    private float InputX;
    private float InputZ;
    public float inputScroll;
    public Vector3 desiredMoveDirection;
    public float speed;
    public Camera cam;
    public CharacterController controller;

    [Header("UI arrows")]
    public Image[] selectArrow;
    public int currentArrowSlot;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        cam = Camera.main;
        controller = this.GetComponent<CharacterController>();

        selectArrow[currentArrowSlot].color = Color.green;
    }

    // Update is called once per frame
    void Update()
    {
        MoveAndRotate();
        ScrollWheel();
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

    void ScrollWheel()
    {
        inputScroll = Input.GetAxis("Mouse ScrollWheel");

        if(inputScroll > 0.1f)
        {
            if (currentArrowSlot >= 1)
            {
                currentArrowSlot -= 1;
                selectArrow[currentArrowSlot].color = Color.green;
                selectArrow[currentArrowSlot + 1].color = Color.white;
            }
            else
            {
                currentArrowSlot = 3;
                selectArrow[currentArrowSlot].color = Color.green;
                selectArrow[currentArrowSlot - (selectArrow.Length - 1)].color = Color.white;
            }
        }
        else if(inputScroll < -0.1f)
        {
            if (currentArrowSlot < selectArrow.Length - 1)
            {
                currentArrowSlot += 1;
                selectArrow[currentArrowSlot].color = Color.green;
                selectArrow[currentArrowSlot - 1].color = Color.white;
            }
            else
            {
                currentArrowSlot = 0;
                selectArrow[currentArrowSlot].color = Color.green;
                selectArrow[currentArrowSlot + (selectArrow.Length - 1)].color = Color.white;
            }
        }
    }

    float ClampAngle(float angle, float from, float to)
    {
        // accepts e.g. -80, 80
        if (angle < 0f) angle = 360 + angle;
        if (angle > 180f) return Mathf.Max(angle, 360 + from);
        return Mathf.Min(angle, to);
    }

    #region Singleton
    private static Character instance;


    private void Awake()
    {
        instance = this;
    }

    public static Character Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new Character();
            }

            return instance;
        }
    }
    #endregion
}
