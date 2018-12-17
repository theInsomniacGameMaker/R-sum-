using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewindTime : MonoBehaviour
{
    public struct PointInTime
    {
        public Vector2 position;
        public Sprite sprite;

        public PointInTime(Vector2 _position, Sprite _sprite)
        {
            position = _position;
            sprite = _sprite;
        }
    }

    private bool isRewinding = false;
    private bool intialKinematicState = false;
    private List<PointInTime> pointsInTime = new List<PointInTime>();
    private SpriteRenderer selfSpriteRenderer;
    private Animator selfAnimator;

    private void Awake()
    {
        selfSpriteRenderer = GetComponent<SpriteRenderer>();
        selfAnimator = GetComponent<Animator>();
    }

    private void Start()
    {
        PlayerScript.onPlayerDeath += StartRewind;
    }

    private void Update()
    {
    }

    private void FixedUpdate()
    {
        if (isRewinding)
        {
            Rewind();
        }
        else
        {
            Record();
        }
    }


    private void StartRewind()
    {
        isRewinding = true;

        if (GetComponent<Rigidbody2D>())
        {
            intialKinematicState = GetComponent<Rigidbody2D>().isKinematic;
            GetComponent<Rigidbody2D>().isKinematic = true;
        }
    }

    private void StopRewind()
    {
        isRewinding = false;

        if (GetComponent<Rigidbody2D>())
        {
            GetComponent<Rigidbody2D>().isKinematic = intialKinematicState;
        }
    }

    private void Rewind()
    {
        if (pointsInTime.Count > 0)
        {
            selfAnimator.enabled = false;
            transform.position = pointsInTime[0].position;
            selfSpriteRenderer.sprite = pointsInTime[0].sprite;
            pointsInTime.RemoveAt(0);
        }
        else
        {
            StopRewind();
        }
    }

    private void Record()
    {
        if (pointsInTime.Count <= 640)
        {
            pointsInTime.Insert(0, new PointInTime(transform.position, selfSpriteRenderer.sprite));
        }
    }

    private void OnDisable()
    {
        PlayerScript.onPlayerDeath -= StartRewind;
    }


}

