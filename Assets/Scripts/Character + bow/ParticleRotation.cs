using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ParticleRotation : MonoBehaviour
{
    public float speed = 10;
    public float shieldRotationSpeed = 0;

    void Update()
    {
        transform.eulerAngles += new Vector3(0, shieldRotationSpeed, speed) * Time.deltaTime;
    }
}
