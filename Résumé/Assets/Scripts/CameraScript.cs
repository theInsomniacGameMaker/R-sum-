using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private OLDTVFilter3 filter;

    [SerializeField]
    private AudioSource rewindSFX;

    private void Awake()
    {
        filter = GetComponent<OLDTVFilter3>();
    }

    private void Start()
    {
        filter.enabled = false;
        PlayerScript.startRewind += SwitchOnFilter;
        PlayerScript.normalGameStarted += SwitchOffFilter;
    }

    private void SwitchOnFilter()
    {
        rewindSFX.pitch = 1.5f;
        filter.enabled = true;
        rewindSFX.Play();
    }

    private void SwitchOffFilter()
    {
        filter.enabled = false;
        rewindSFX.Stop();
    }

    private void OnDisable()
    {
        PlayerScript.startRewind -= SwitchOnFilter;
        PlayerScript.normalGameStarted -= SwitchOffFilter;

    }
}
