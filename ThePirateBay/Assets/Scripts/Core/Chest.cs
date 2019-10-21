using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IInteraction
{
    public Transform lidPivot;
    bool isOpen = false;

    void IInteraction.Interact()
    {
        if (isOpen) return;
        Open();
    }

    void IInteraction.StopInteraction()
    {
        
    }

    void Open() {
        lidPivot.Rotate(new Vector3(-120f, 0, 0));
        isOpen = true;
    }

    void Close() {

    }
}
