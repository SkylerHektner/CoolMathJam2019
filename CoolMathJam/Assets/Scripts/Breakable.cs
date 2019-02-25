using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    // call this to break the breakable object
    public void Break()
    {
        Destroy(gameObject);
    }
}
