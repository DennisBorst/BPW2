using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIdirection : MonoBehaviour
{
    [HideInInspector]
    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        player = Character.Instance.transform;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.LookAt(player);
    }
}
