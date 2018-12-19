using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static byte scrollSpeedMultiplier = 1;
    public static bool tutorialOver = false;

    [SerializeField]
    private GameObject skillRetrievedText;

    private AudioSource selfAudioSource;

    private void Start()
    {
        PlayerScript.scrollCollected += ShowText;
        selfAudioSource = GetComponent<AudioSource>();
        PlayerScript.startRewind += StopBackgroundMusic;
        PlayerScript.normalGameStarted += StartBackgroundMusic;

    }

    private void Update()
    {
        selfAudioSource.pitch = Time.timeScale;
    }

    private void ShowText()
    {
        skillRetrievedText.SetActive(true);

        skillRetrievedText.GetComponent<Animator>().SetTrigger("Up");
    }

    private void StartBackgroundMusic()
    {
        selfAudioSource.Play();
    }

    private void StopBackgroundMusic()
    {
        selfAudioSource.Stop();
    }

    private void OnDisable()
    {
        PlayerScript.startRewind -= StopBackgroundMusic;
        PlayerScript.normalGameStarted -= StartBackgroundMusic;
    }

}
