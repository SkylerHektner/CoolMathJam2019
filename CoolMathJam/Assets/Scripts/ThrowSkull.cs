using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowSkull : MonoBehaviour
{
    public bool canThrow = false;

    [SerializeField] private float throwForce = 100f;
    // default to adding an upwards angle of 10 degrees to the throw
    [SerializeField] private Vector3 angleMod = new Vector3(0, 0.1f, 0);

    private GameObject skull;

    private void Start()
    {
        skull = GameObject.FindGameObjectWithTag("Skull");
    }

    private void Update()
    {
        if (canThrow && Input.GetButtonDown("Action"))
        {
            skull.GetComponent<Possess>().tryEndPossession();
            skull.GetComponent<Rigidbody>().AddForce((transform.GetChild(0).right + angleMod) * throwForce, ForceMode.Impulse);
        }
    }
}
