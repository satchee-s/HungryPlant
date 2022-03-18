using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastController : MonoBehaviour
{
    RaycastHit hit;
    Camera cam;
    [SerializeField] float minDoorDistance;

    private void Start()
    {
        cam = Camera.main;
    }
    private void Update()
    {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.gameObject.tag == "Door")
            {
                Door(hit.collider.gameObject);                
            }
        }
    }

    void Door(GameObject obj)
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Vector3.Distance(this.transform.position, obj.transform.position) <= minDoorDistance)
            {
                obj.GetComponent<DoorController>().PlayAnimation();
            }
        }
            
    }
}
