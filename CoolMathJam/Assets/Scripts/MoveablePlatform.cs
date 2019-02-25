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
        originalPos = transform.localPosition;
    }

    private void Update()
    {
        if (GoToMovePos && transform.localPosition != moveToPos)
        {
            transform.localPosition += (moveToPos - transform.localPosition).normalized * Time.deltaTime * moveSpeed;
            if ((moveToPos - transform.localPosition).sqrMagnitude < 0.01f)
            {
                transform.localPosition = moveToPos;
            }
        }
        else if (!GoToMovePos && transform.localPosition != originalPos)
        {
            transform.localPosition += (originalPos - transform.localPosition).normalized * Time.deltaTime * moveSpeed;
            if ((originalPos - transform.localPosition).sqrMagnitude < 0.01f)
            {
                transform.localPosition = originalPos;
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
