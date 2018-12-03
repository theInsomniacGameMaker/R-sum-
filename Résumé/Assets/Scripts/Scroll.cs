using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour
{

    [SerializeField]
    private float speed;

    [SerializeField]
    private Vector2 direction;

    void Start ()
    {
        direction.Normalize();
	}
	
	void Update ()
    {
        if (speed == 0.0f)
            Debug.Log("Speed is set to 0 for " + gameObject.name);

        transform.Translate(direction * speed * Time.deltaTime);
    }
}
