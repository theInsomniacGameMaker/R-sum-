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

    private const ushort MaxRewinds = 256;

    private bool isRewinding = false;
    private bool intialKinematicState = false;
    private ushort rewindCount = 0;
    private List<PointInTime> pointsInTime = new List<PointInTime>();
    private SpriteRenderer selfSpriteRenderer;
    private Animator selfAnimator;

    public delegate void RewindCompleted();
    public event RewindCompleted rewindCompleted;

    private void Awake()
    {
        selfSpriteRenderer = GetComponent<SpriteRenderer>();
        selfAnimator = GetComponent<Animator>();
    }

    private void Start()
    {
        PlayerScript.startRewind += StartRewind;
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
        rewindCount = 0;
        if (GetComponent<Rigidbody2D>())
        {
            intialKinematicState = GetComponent<Rigidbody2D>().isKinematic;
            GetComponent<Rigidbody2D>().isKinematic = true;
        }
    }

    private void StopRewind()
    {
        isRewinding = false;
        rewindCount = 0;

        if (GetComponent<Rigidbody2D>())
        {
            GetComponent<Rigidbody2D>().isKinematic = intialKinematicState;
        }

        //if (selfAnimator)
        //{
        //    selfAnimator.enabled = true;
        //}

        if (rewindCompleted != null)
        {
            rewindCompleted();
        }
    }

    private void Rewind()
    {
        if (rewindCount < MaxRewinds)
        {
            if (pointsInTime.Count > 4)
            {
                if (selfAnimator)
                {
                    selfAnimator.StopPlayback();
                    selfAnimator.enabled = false;
                }
                transform.position = pointsInTime[0].position;
                selfSpriteRenderer.sprite = pointsInTime[0].sprite;
                pointsInTime.RemoveAt(0);
                pointsInTime.RemoveAt(1);
                pointsInTime.RemoveAt(2);
            }
            rewindCount+=3;
        }
        else
        {
            StopRewind();
        }
    }

    private void Record()
    {
        if (pointsInTime.Count <= MaxRewinds)
        {
            pointsInTime.Insert(0, new PointInTime(transform.position, selfSpriteRenderer.sprite));
        }
    }

    private void OnDisable()
    {
        PlayerScript.startRewind -= StartRewind;
    }
}

