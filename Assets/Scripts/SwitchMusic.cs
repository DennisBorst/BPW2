using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SwitchMusic : MonoBehaviour
{
    [SerializeField] private ParticleSystem ring;

    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip clipBegin;
    [SerializeField] private AudioClip clipMiddle;

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
        ParticleManager.instance.SpawnParticle(ParticleManager.instance.startRingParticle, transform.position - transform.up * 2f, transform.rotation);
        Camera.main.transform.DOComplete();
        Camera.main.transform.DOShakePosition(2f, 1f, 10, 90, false, true);
        source.clip = clipMiddle;
        source.Play();
        source.volume = maxVolume;
    }
}
