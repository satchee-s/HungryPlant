using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotShifting : MonoBehaviour
{
    public Transform leftNeighbor;

    public Vector2 leftShift, selfShift;
    AnimationCurve leftCurve, selfCurve;
    Vector3 position;

    // Start is called before the first frame update
    void Start()
    {
        position = transform.position;

        leftCurve = new AnimationCurve();
        leftCurve.AddKey(new Keyframe(.5f, leftShift.x));
        leftCurve.AddKey(new Keyframe(1, leftShift.y));

        selfCurve = new AnimationCurve();
        selfCurve.AddKey(new Keyframe(.5f, selfShift.y));
        selfCurve.AddKey(new Keyframe(1, selfShift.x));
    }

    // Update is called once per frame
    void Update()
    {
        float leftScale = leftNeighbor.localScale.x;
        float leftOffset = leftCurve.Evaluate(leftScale);

        float selfScale = transform.localScale.x;
        float selfOffset = selfCurve.Evaluate(selfScale);

        Vector3 newPos = new Vector3(leftNeighbor.position.x + leftOffset + selfOffset, leftNeighbor.position.y, leftNeighbor.position.z);
        transform.position = newPos;
    }
}
