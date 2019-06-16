using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    public Vector2 rotationLimit;

    [Range(1,10)]
    public float sensitivity;
    public float inputScroll;
    /*
    private float InputX;
    private float InputZ;
    
    public Vector3 desiredMoveDirection;
    public float speed;
    public Camera cam;
    public CharacterController controller;
    */

    private BowScript bowScript;

    [Header("UI arrows")]
    public Image[] selectArrow;
    public int currentArrowSlot;
    private int previousArrowSlot;

    [SerializeField] private Material[] newMaterial;
    [SerializeField] private GameObject[] arrows;
    private MeshRenderer normalColor;
    private Material explosion;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        //cam = Camera.main;
        //controller = this.GetComponent<CharacterController>();
        bowScript = this.GetComponent<BowScript>();

        selectArrow[currentArrowSlot].color = Color.green;
        
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
        ArrowTypeInput();
    }

    void Rotate()
    {
        //rotating
        float mousey = Input.GetAxis("Mouse Y");

        transform.eulerAngles += new Vector3(-mousey * sensitivity, 0, 0);

        transform.eulerAngles = new Vector3(ClampAngle(transform.eulerAngles.x, rotationLimit.x, rotationLimit.y),
        transform.eulerAngles.y, transform.eulerAngles.z);
    }

    void ArrowTypeInput()
    {
        if(bowScript.isAiming != true)
        {
            selectArrow[previousArrowSlot].color = Color.white;

            if (Input.GetKeyDown(KeyCode.Alpha1)) { currentArrowSlot = 0; }
            if (Input.GetKeyDown(KeyCode.Alpha2)) { currentArrowSlot = 1; }
            if (Input.GetKeyDown(KeyCode.Alpha3)) { currentArrowSlot = 2; }
            if (Input.GetKeyDown(KeyCode.Alpha4)) { currentArrowSlot = 3; }

            inputScroll = Input.GetAxis("Mouse ScrollWheel");

            if (inputScroll > 0.1f)
            {
                if (currentArrowSlot >= 1)
                {
                    currentArrowSlot -= 1;
                }
                else { currentArrowSlot = 3; }
            }
            else if (inputScroll < -0.1f)
            {
                if (currentArrowSlot < selectArrow.Length - 1)
                {
                    currentArrowSlot += 1;
                }
                else { currentArrowSlot = 0; }
            }

            previousArrowSlot = currentArrowSlot;
            selectArrow[currentArrowSlot].color = Color.green;

            for (int i = 0; i < arrows.Length; i++)
            {
                normalColor = arrows[i].GetComponent<MeshRenderer>();
                normalColor.material = newMaterial[currentArrowSlot];
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
