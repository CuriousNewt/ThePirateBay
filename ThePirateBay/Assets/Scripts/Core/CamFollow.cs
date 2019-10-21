using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public GameObject targetToFollow;

    private void LateUpdate()
    {
        transform.position = targetToFollow.transform.position;
    }
}
