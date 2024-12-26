using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogBehavior : MonoBehaviour
{
    [SerializeField] private Animator m_animator;
    private Vector3 targetPosition;
    private float timer;
    public float speed = 2f;

    private bool isWalking;
    private bool isLookRight;



    void Start()
    {
        timer = 2f;
    }

    void Update()
    {
        if (timer <= 0)
        {
            SetTargetPosition();
            timer = Random.Range(5.0f, 15.0f);
        }
        else
        {
            timer -= Time.deltaTime;
        }

        if (isWalking)
        {
            Move();
        }
    }
    private void SetTargetPosition()
    {
        float x = Random.Range(-9f, 10f);
        float y = Random.Range(-3.5f, 3.5f);
        targetPosition = new Vector3(x, y);
        targetPosition.z = transform.position.z;

        isWalking = true;
        m_animator.Play("DogWalk");

        if (transform.position.x > targetPosition.x && isLookRight) Flip();
        if (transform.position.x < targetPosition.x && !isLookRight) Flip();
    }
    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        if(transform.position == targetPosition)
        {
            isWalking = false;
            m_animator.Play("DogIdle");
        }
    }

    private void Flip()
    {
        isLookRight = !isLookRight;
        transform.Rotate(0f, 180f, 0f);
    }
}
