using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    [SerializeField]
    private AudioClip buttonClick;

    public void PlaySound()
    {
        AudioSource.PlayClipAtPoint(buttonClick, Vector2.zero);
    }
}
