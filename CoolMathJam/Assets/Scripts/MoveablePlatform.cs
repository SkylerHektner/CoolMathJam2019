using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveablePlatform : MonoBehaviour, ITriggerable
{
    [SerializeField] private Vector3 moveToPos;
    [SerializeField] private float moveSpeed;
    private Vector3 originalPos;

    private bool GoToMovePos = false;

    private void Start()
    {
        originalPos = transform.position;
    }

    private void Update()
    {
        if (GoToMovePos && transform.position != moveToPos)
        {
            transform.position += (moveToPos - transform.position).normalized * Time.deltaTime * moveSpeed;
            if ((moveToPos - transform.position).sqrMagnitude < 0.01f)
            {
                transform.position = moveToPos;
            }
        }
        else if (!GoToMovePos && transform.position != originalPos)
        {
            transform.position += (originalPos - transform.position).normalized * Time.deltaTime * moveSpeed;
            if ((originalPos - transform.position).sqrMagnitude < 0.01f)
            {
                transform.position = originalPos;
            }
        }
    }

    public void OnTriggerOff()
    {
        GoToMovePos = false;
    }

    public void OnTriggerOn()
    {
        GoToMovePos = true;
    }
}
