using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour, IAction
{
    GameObject target;
    public float interactionRange = 1.5f;

    void Update()
    {
        if (target == null) return;

        if (!IsInRange())
        {
            GetComponent<Mover>().MoveTo(target.transform.position);
        }
        else
        {
            GetComponent<Mover>().Cancel();
            MakeInteraction();
        }
    }

    private void MakeInteraction()
    {
        GetComponent<ActionScheduler>().StartAction(this);
        target.GetComponent<IInteraction>().Interact();
        GetComponent<ActionScheduler>().CancelCurrentAction();
        Cancel();
    }

    public void Interact(GameObject interactable) {
        target = interactable;
    }

    bool IsInRange()
    {
        return Vector3.Distance(transform.position, target.transform.position) < interactionRange;
    }

    public void Cancel()
    {
        target = null;
    }
}
