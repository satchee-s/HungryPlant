using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleRoomLighting : MonoBehaviour
{
    public bool startEnabled;
    bool lightsOn;
    Light[] roomLights;

    public bool hallLights;
    public GameObject player;
    public float toggleDistance;

    private void Start()
    {
        roomLights = GetComponentsInChildren<Light>();

        if (!startEnabled)
        {
            for (int i = 0; i < roomLights.Length; i++)
                roomLights[i].enabled = false;
        }        
    }

    private void Update()
    {
        if (hallLights && player != null)
        {
            if (lightsOn)
            {
                for (int i = 0; i < roomLights.Length; i++)
                {
                    float dist = Vector3.Distance(roomLights[i].transform.position, player.transform.position);
                    if (dist < toggleDistance)
                    {
                        roomLights[i].enabled = true;
                    }
                    else
                    {
                        roomLights[i].enabled = false;
                    }
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && !hallLights)
        {
            if (!lightsOn)
            {
                lightsOn = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && hallLights)
        {
            Debug.Log("NearHall");
            if (!lightsOn)
            {
                for (int i = 0; i < roomLights.Length; i++)
                    roomLights[i].enabled = true;
                lightsOn = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            if (lightsOn)
            {
                for (int i = 0; i < roomLights.Length; i++)
                    roomLights[i].enabled = false;
                lightsOn = false;
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (player != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(player.transform.position, toggleDistance);
        }
        
    }
}
