using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public ITriggerable[] triggerables;

    private int onCount = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Skeleton" || other.tag == "Skull")
        {
            onCount++;
        }
        if (onCount == 1)
        {
            foreach(ITriggerable t in triggerables)
            {
                t.OnTriggerOn();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Skeleton" || other.tag == "Skull")
        {
            onCount--;
        }
        if (onCount == 0)
        {
            foreach (ITriggerable t in triggerables)
            {
                t.OnTriggerOff();
            }
        }
    }
}
