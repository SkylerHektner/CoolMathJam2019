using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour, IPossessable
{
    public Transform neckTransform;
    public Material ValidCandidateMat;

    private Renderer renderer;
    private Material originalMat;

    private void Start()
    {
        renderer = GetComponentInChildren<Renderer>();
        originalMat = renderer.material;
    }

    public Transform getMountTransform()
    {
        return neckTransform;
    }

    public bool IsInRange(float range, Vector3 position)
    {
        return (transform.position - position).sqrMagnitude < range * range;
    }

    public void OnBecomeCandidate()
    {
        renderer.material = ValidCandidateMat;
    }

    public void OnDePossess()
    {
        renderer.material = ValidCandidateMat;
        GetComponent<JumperSkeletonMovement>().CanMove = false;
    }

    public void OnNoLongerCandidate()
    {
        renderer.material = originalMat;
    }

    public void OnPossess()
    {
        renderer.material = originalMat;
        GetComponent<JumperSkeletonMovement>().CanMove = true;
    }
}
