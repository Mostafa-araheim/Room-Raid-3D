using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{

    public static InventorySystem Instance { get; set; }
    public GameObject inventoryScreenUI;
    public List<GameObject> slotList = new List<GameObject>();
    public List<string> itemList = new List<string>();
    private GameObject itemToAdd;
    private GameObject slotToEquip;
    public bool isOpen;
    //public bool isFull;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }


    void Start()
    {
        isOpen = false;
        PopulateSlotList();
    }
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.I) && !isOpen)
        {
            Cursor.lockState = CursorLockMode.None;

            inventoryScreenUI.SetActive(true);
            isOpen = true;
        }
        else if (Input.GetKeyDown(KeyCode.I) && isOpen)
        {
            inventoryScreenUI.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            isOpen = false;
        }
    }
    public void AddToInventory(string itemName)
    {
        slotToEquip = FindNextEmptySlot();
        try
        {
            itemToAdd = Instantiate(Resources.Load<GameObject>(itemName)
                , slotToEquip.transform.position
                , slotToEquip.transform.rotation);
        }
        catch { }
        if (itemToAdd is not null)
        {
            itemToAdd.transform.SetParent(slotToEquip.transform);
            itemList.Add(itemName);
        }
    }

    public bool CheckIfFull()
    {
        int counter = 0;
        foreach (GameObject slot in slotList)
        {
            if (slot.transform.childCount > 0)
                counter++;
        }
        if (counter == 4)
            return true;
        else
            return false;
    }

    private GameObject FindNextEmptySlot()
    {
        foreach (GameObject slot in slotList)
        {
            if (slot.transform.childCount == 0)
            {
                return slot;
            }

        }
        return new GameObject();
    }

    private void PopulateSlotList()
    {
        foreach (Transform child in inventoryScreenUI.transform)
        {
            if (child.CompareTag("InventorySlot"))
            {
                slotList.Add(child.gameObject);
            }
        }
    }


}