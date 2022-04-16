using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToInitialState : MonoBehaviour
{
    public Vector3 targetPosition;
    public Quaternion targetRotation;
    Rigidbody body;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
        targetRotation = transform.rotation;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        body.MovePosition(transform.TransformPoint(targetPosition));
        body.MoveRotation(targetRotation);
    }
}
