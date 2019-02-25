using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smash : MonoBehaviour
{
    public bool canSmash = false;
    [SerializeField] private float smashRange = 0.25f;

    private int smashablesMask;

    private void Start()
    {
        smashablesMask = 1 << LayerMask.NameToLayer("Breakable");
    }

    private void Update()
    {
        if (canSmash && Input.GetButtonDown("Action"))
        {
            smash();
        }
    }

    // becke lemme smash
    private void smash()
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(transform.position, transform.GetChild(0).right, out hitInfo, smashRange, smashablesMask))
        {
            hitInfo.collider.gameObject.GetComponent<Breakable>().Break();
        }
    }
}
