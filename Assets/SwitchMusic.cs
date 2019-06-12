using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchMusic : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip clipBegin;
    [SerializeField] private AudioClip clipMiddle;
    [SerializeField] private AudioClip clipFull;

    [Range(0, 1)]
    [SerializeField] private float minVolume;
    [Range(0, 1)]
    [SerializeField] private float maxVolume;
    [SerializeField] private float durationSwitch;


    // Start is called before the first frame update
    void Start()
    {
        source.clip = clipBegin;
        source.volume = minVolume;
        source.loop = true;
        source.Play();
    }

    public void Switch()
    {
        source.clip = clipMiddle;
        //source.loop = false;
        source.Play();
        source.volume = Mathf.Lerp(minVolume, maxVolume, durationSwitch * Time.deltaTime);
    }
}
