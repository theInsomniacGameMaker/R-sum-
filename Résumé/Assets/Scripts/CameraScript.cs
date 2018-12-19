using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private OLDTVFilter3 filter;
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
        filter.enabled = true;
    }

    private void SwitchOffFilter()
    {
        filter.enabled = false;
    }

    private void OnDisable()
    {
        PlayerScript.startRewind -= SwitchOnFilter;
        PlayerScript.normalGameStarted -= SwitchOffFilter;

    }
}
