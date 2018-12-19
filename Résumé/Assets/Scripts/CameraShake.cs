using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private AudioSource selfAudioSource;

    private void Start()
    {
        selfAudioSource = GetComponent<AudioSource>();
    }

    public void StartCameraShake(float magnitude, float duration)
    {
        StartCoroutine(ShakeCamera(magnitude, duration));
    }

    private IEnumerator ShakeCamera(float magnitude, float duration)
    {
        selfAudioSource.Play();
        //Debug.Log("Camera Shake function called");
        Vector3 originalPosition = transform.localPosition;
        
        float elapsedTime = 0.0f;

        while (elapsedTime < duration)
        {
            float x = Random.Range(-1.0f, 1.0f) * magnitude;
            float y = Random.Range(-1.0f, 1.0f) * magnitude;

            transform.localPosition = new Vector3(x, y, originalPosition.z);
            elapsedTime += Time.deltaTime;
            //Debug.Break();
            yield return null;
        }

        transform.localPosition = new Vector3(0, 0, originalPosition.z);
    }
}
