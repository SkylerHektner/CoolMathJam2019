using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(SkullMovement))]
[RequireComponent(typeof(Rigidbody))]
public class Possess : MonoBehaviour
{
    public float possessRange = 3f;
    public float jumpDuration = 1f;
    public float jumpHeight = 1f;

    private List<IPossessable> possessables = new List<IPossessable>();
    private IPossessable curCandidate;
    private bool possessing = false;
    private SkullMovement skullMovement;
    private Rigidbody rb;
    private Quaternion originalRot;

    private Coroutine curMoveToPosCoroutine;

    private void Start()
    {
        IEnumerable<IPossessable> temp = FindObjectsOfType<MonoBehaviour>().OfType<IPossessable>();
        foreach (IPossessable p in temp)
        {
            possessables.Add(p);
        }
        skullMovement = GetComponent<SkullMovement>();
        rb = GetComponent<Rigidbody>();
        originalRot = transform.rotation;
    }

    private void Update()
    {
        if (!possessing)
        {
            // search for in range possessable to become a candidate
            if (curCandidate == null)
            {
                foreach (IPossessable possessable in possessables)
                {
                    if (possessable.IsInRange(possessRange, transform.position))
                    {
                        Debug.Log("FOUND A CANDIDATE TO POSSESS");
                        curCandidate = possessable;
                        curCandidate.OnBecomeCandidate();
                        break;
                    }
                }
            }
            // if we have an in range candidate
            else
            {
                // check if they went out of range
                if (!curCandidate.IsInRange(possessRange, transform.position))
                {
                    Debug.Log("CURRENT CANDIDATE TO POSSESS LOST");
                    curCandidate.OnNoLongerCandidate();
                    curCandidate = null;
                }
                // otherwise check if the player tried to possess them
                else if (Input.GetButtonDown("Possess"))
                {
                    Debug.Log("POSSESSING");
                    curMoveToPosCoroutine = StartCoroutine(MoveToPosition((int)(jumpDuration * 60), curCandidate.getMountTransform()));
                    skullMovement.CanMove = false;
                    possessing = true;
                    rb.useGravity = false;
                    rb.isKinematic = true;
                    rb.detectCollisions = false;
                }
            }
        }
        else
        {
            // check if the player is trying to end possession
            if (Input.GetButtonDown("Possess"))
            {
                Debug.Log("ENDING POSSESSION OF CANDIDATE");
                if (curMoveToPosCoroutine != null)
                {
                    StopCoroutine(curMoveToPosCoroutine);
                }
                transform.parent = null;
                curCandidate.OnDePossess();
                possessing = false;
                skullMovement.CanMove = true;
                rb.useGravity = true;
                rb.isKinematic = false;
                rb.detectCollisions = true;
                StartCoroutine(RotateHead(10, originalRot));
            }
        }
    }

    private IEnumerator MoveToPosition(int numFrames, Transform newParent)
    {
        Vector3 startPos = transform.position;
        Quaternion originalRot = transform.rotation;
        for (int i = 0; i < numFrames; i++)
        {
            float percentageDone = (float)i / (float)numFrames;
            transform.position = Parabola.SampleParabola(
                startPos, newParent.position, jumpHeight, percentageDone);
            transform.rotation = Quaternion.Lerp(originalRot, newParent.rotation, percentageDone);
            yield return null;
        }
        transform.position = newParent.position;
        transform.rotation = newParent.rotation;
        transform.parent = newParent;
        curCandidate.OnPossess();
        Debug.Log("FINISHED JUMPING ONTO POSSESSION CANDIDATE");
    }

    private IEnumerator RotateHead(int numFrames, Quaternion finalRot)
    {
        Quaternion currentRot = transform.rotation;
        for (int i = 0; i < numFrames; i++)
        {
            transform.rotation = Quaternion.Lerp(currentRot, finalRot, (float)i / (float)numFrames);
            yield return null;
        }
        transform.rotation = finalRot;
    }
}


interface IPossessable
{
    // Called when a possessable becomes a candidate
    void OnBecomeCandidate();

    // Called when a possessable is no longer a candidate
    void OnNoLongerCandidate();

    // Called when a possessable becomes possessed
    void OnPossess();

    // Called when a possessable is no longer possessed
    void OnDePossess();

    // returns whether or not the skull is in range given a range
    // and the skulls current position
    bool IsInRange(float range, Vector3 position);

    // returns the transform the skull is to be mounted to
    // after a posession
    Transform getMountTransform();
}
