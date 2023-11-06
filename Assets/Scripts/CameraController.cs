using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public BallController target;
    private float offset;
    private void Awake()
    {
        offset = target.transform.position.y;
    }


    void Update()
    {
        Vector3 vector3 = transform.position;
        vector3.y = target.transform.position.y + offset;
        transform.position = vector3;
    }
}
