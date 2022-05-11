using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VineIKSolver : MonoBehaviour
{

    public LayerMask levelLayer = default;
    public Transform raycastPoint;
    public Transform body;
    public VineIKSolver oppositeVine;
    public VineIKSolver linkedVine;

    public float speed = 1;
    public float stepDistance = 4;
    public float stepLength = 4;
    public float stepHeight;
    public Vector3 tipOffset;
    public Color targetColor;

    float tipSpacing;
    Vector3 oldPos, curPos, newPos;
    [SerializeField]Transform target;
    Vector3 oldNormal, curNormal, newNormal;
    float lerp;

    public AudioClip[] steps;
    AudioSource source;
    bool triggerStep;

    // Start is called before the first frame update
    void Start()
    {
        tipSpacing = transform.localPosition.x;
        oldPos = transform.position;
        newPos = transform.position;
        curPos = transform.position;

        oldNormal = transform.up;
        newNormal = transform.up;
        curNormal = transform.up;
        lerp = 1;

        source = GetComponent<AudioSource>();

        Ray ray = new Ray(raycastPoint.position, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, 10, levelLayer))
        {
            target.position = hit.point;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = curPos;
        transform.up = curNormal;

        Ray ray = new Ray(raycastPoint.position, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, 10, levelLayer))
        {
            Vector3 temp = target.position;
            temp.y = hit.point.y;
            target.position = temp;
            target.forward = body.forward;

            if (Vector3.Distance(newPos, target.position) > stepDistance && !oppositeVine.IsMoving() && lerp >= 1)
            {
                linkedVine.LinkedTrigger();
                triggerStep = true;
                lerp = 0;
                int dir = body.InverseTransformPoint(target.position).z > body.InverseTransformPoint(newPos).z ? 1 : -1;
                newPos = target.position + (body.forward * stepLength * dir);
                newNormal = hit.normal;
            }
        }

        if (lerp < 1)
        {
            Vector3 temp = Vector3.Lerp(oldPos, newPos, lerp);
            temp.y += Mathf.Sin(lerp * Mathf.PI) * stepHeight;

            curPos = temp;
            curNormal = Vector3.Lerp(oldNormal, newNormal, lerp);
            lerp += Time.deltaTime * speed;
        }
        else
        {
            oldPos = newPos;
            oldNormal = newNormal;

            if (triggerStep)
            {
                int index = Random.Range(0, steps.Length);
                source.PlayOneShot(steps[index]);
                triggerStep = false;
            }
        }
    }

    public bool IsMoving()
    {
        if (lerp < 1)
            return true;
        else 
            return false;
    }

    public void LinkedTrigger()
    {
        Ray ray = new Ray(raycastPoint.position, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, 10, levelLayer))
        {
            lerp = 0;
            int dir = body.InverseTransformPoint(target.position).z > body.InverseTransformPoint(newPos).z ? 1 : -1;
            newPos = target.position + (body.forward * stepLength * dir);
            newNormal = hit.normal;
        }            
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = targetColor;
        if (raycastPoint != null)
            Gizmos.DrawWireCube(raycastPoint.position, Vector3.one * .1f);
        Gizmos.DrawSphere(target.position, .1f);
        Gizmos.DrawLine(target.position, target.position + target.forward);

        if (Vector3.Distance(transform.position, target.position) > stepDistance)
            Gizmos.color = Color.red;
        else
            Gizmos.color = Color.blue;
        if (Vector3.Distance(transform.position, target.position) < stepDistance * 1.5f)
            Gizmos.DrawLine(transform.position, target.position);
    }
}