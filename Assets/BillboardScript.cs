using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardScript : MonoBehaviour
{
    public Transform cam;
    public Transform origin;

    private void LateUpdate()
    {
        transform.LookAt(transform.position + cam.forward);
        transform.position = (origin.transform.position + cam.transform.position) / 2;
    }
}
