using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{

    [Header("Start Game")]
    [SerializeField] private GameObject pressText;
    [SerializeField] private GameObject spawner;
    [SerializeField] private Animator anim;
    private bool isColliding = false;


    private void Start()
    {
        pressText.SetActive(false);
        spawner.SetActive(false);
    }

    private void Update()
    {
        if (isColliding)
        {
            pressText.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                anim.enabled = true;
                spawner.SetActive(true);
                Destroy(pressText);
                Destroy(gameObject);
            }
        }
        else if (!isColliding)
        {
            pressText.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            isColliding = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isColliding = false;
        }
    }
}
