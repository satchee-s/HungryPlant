using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public Rigidbody rbd;
    public Sprite inventoryImage;
    void Start()
    {
        rbd = GetComponent<Rigidbody>();
    }

}
