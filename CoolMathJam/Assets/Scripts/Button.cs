using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public GameObject[] Triggerables;
    private List<ITriggerable> triggerables = new List<ITriggerable>();

    private bool pressed = false;

    private void Start()
    {
        foreach (GameObject g in Triggerables)
        {
            triggerables.Add(g.GetComponent<ITriggerable>());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!pressed && other.tag == "Skeleton" || other.tag == "Skull")
        {
            foreach (ITriggerable t in triggerables)
            {
                t.OnTriggerOn();
            }
            pressed = true;
        }
    }
}
