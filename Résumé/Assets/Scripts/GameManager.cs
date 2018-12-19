﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static byte scrollSpeedMultiplier = 1;
    public static bool tutorialOver = false;

    [SerializeField]
    private GameObject skillRetrievedText;


    private void Start()
    {
        PlayerScript.scrollCollected += ShowText;
    }

    private void ShowText()
    {
        skillRetrievedText.SetActive(true);

        skillRetrievedText.GetComponent<Animator>().SetTrigger("Up");
    }
}
