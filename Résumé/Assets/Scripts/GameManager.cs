using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static byte scrollSpeedMultiplier = 1;

    [SerializeField]
    private GameObject skillRetrievedText;


    private void Start()
    {
        PlayerScript.scrollCollected += ShowText;
    }

    private void ShowText()
    {
        Debug.Log("This was called");
        skillRetrievedText.SetActive(true);
        //Debug.Break();
        skillRetrievedText.GetComponent<Animator>().SetTrigger("Up");
    }
}
