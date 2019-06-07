using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{

    [Header("Start Game")]
    [SerializeField] private GameObject pressText;
    [SerializeField] private GameObject spawner;
    [SerializeField] private Animator anim;


    private void Start()
    {
        pressText.SetActive(false);
        spawner.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            pressText.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                anim.enabled = true;
                spawner.SetActive(true);
                Destroy(pressText);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            pressText.SetActive(false);
        }
    }
}
