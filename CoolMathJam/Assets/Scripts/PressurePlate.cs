using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public GameObject[] Triggerables;
    private List<ITriggerable> triggerables = new List<ITriggerable>();

    private int onCount = 0;

    private void Start()
    {
        foreach (GameObject g in Triggerables)
        {
            triggerables.Add(g.GetComponent<ITriggerable>());
        }
    }

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
