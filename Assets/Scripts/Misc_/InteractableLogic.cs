using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableLogic : MonoBehaviour
{
    GameObject player;
    public float interactionDistance;
    public Renderer mesh;
    public float pulseInterval;
    public bool inRange;
    bool canInteract;

    Material material;
    public AnimationCurve curve;
    float transparency;
    float timer;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>().gameObject;
        material = mesh.materials[1];
        transparency = 0;

        canInteract = true;
        curve = new AnimationCurve();
        curve.AddKey(0, 0);
        curve.AddKey(pulseInterval / 2, .5f);
        curve.AddKey(pulseInterval, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < interactionDistance)
            inRange = true;
        else
            inRange = false;

        if (inRange && canInteract)
        {
            transparency = curve.Evaluate(timer);
            material.SetFloat("_Transparency", transparency);
            if (timer < pulseInterval)
                timer += Time.deltaTime;
            else
                timer = 0;
        }
        else
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
                transparency = curve.Evaluate(timer);
                material.SetFloat("_Transparency", transparency);
            }
        }
    }

    public void SetInteracting(bool canInteract)
    {
        this.canInteract = canInteract;
    }
}
