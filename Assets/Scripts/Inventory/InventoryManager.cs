using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class InventoryManager : MonoBehaviour
{
    public InventoryObject inventory;
    //public Text pickUpText;
    public bool pickUpAllowed = false;
    Item item;
    GameObject itemObject;
    private void Awake()
    {
        //pickUpText.gameObject.SetActive(false);

    }
    private void Update()
    {
        if(pickUpAllowed && Input.GetKeyDown(KeyCode.Z))
        {
            PickUp();
        }
    }

    private void PickUp()
    {
        if (item)
        {
            if (inventory.AddItem(item.item, itemObject))
            {
                itemObject.SetActive(false);
                DontDestroyOnLoad(itemObject);
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Item"))
        {
            pickUpAllowed = true;
            item = other.GetComponent<Item>();
            itemObject = other.gameObject;
            //pickUpText.gameObject.SetActive(true);
        }
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Item"))
        {
            pickUpAllowed = false;
            //pickUpText.gameObject.SetActive(false);
        }
    } 
    public void OnApplicationQuit()
    {
        inventory.Container.Clear();
    }
}
