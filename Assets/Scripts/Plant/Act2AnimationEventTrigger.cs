using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Act2AnimationEventTrigger : MonoBehaviour
{

    public GreenhouseDoorTrigger eventHolder;

    public LookAtConstraint lookAtConstraint;
    public Transform target;
    ConstraintSource constraintSource;
    [Range(0, 1)] public float weight;
    public float lookSpeed;
    bool looking;    

    private void Awake()
    {
        constraintSource = new ConstraintSource();
        constraintSource.weight = 1;
        constraintSource.sourceTransform = target;
    }

    public void SlowTime()
    {
        eventHolder.SlowTime();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void EnableLookAt()
    {
        if (!looking)
        {
            StartCoroutine(LookAtPlant(true));
            looking = true;
        }
    }

    public void DisableLookAt()
    {
        if (looking)
        {
            StartCoroutine(LookAtPlant(false));
            looking = false;
        }
    }

    IEnumerator LookAtPlant(bool look)
    {
        if (look)
        {
            lookAtConstraint.SetSource(0, constraintSource);
            lookAtConstraint.constraintActive = true;
            while (weight < 1)
            {
                weight += Time.unscaledDeltaTime * lookSpeed;
                lookAtConstraint.weight = weight;
                yield return null;
            }
            lookAtConstraint.GetComponent<MouseMovement>().enabled = false;
        }
        else
        {
            while (weight > 0)
            {
                weight -= Time.unscaledDeltaTime * lookSpeed;
                lookAtConstraint.weight = weight;
                yield return null;
            }
            lookAtConstraint.constraintActive = false;
            lookAtConstraint.GetComponent<MouseMovement>().enabled = true;
        }
    }
}
