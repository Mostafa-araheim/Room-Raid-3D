using UnityEngine;
using UnityEngine.Pool;

public class InteractableObject : MonoBehaviour
{
    public string itemName; // The name that appears when hovering
    public bool playerInRange;
    public bool canBeStored = false;
    public string GetItemName()
    {
        return itemName;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerInRange && SelectionManager.instance.onTarget && canBeStored)
        {
            if (!InventorySystem.Instance.CheckIfFull())
            {
                InventorySystem.Instance.AddToInventory(itemName);
                Debug.Log("added to inventory");
                Destroy(gameObject);
            }
            else
            {
                Debug.Log("inventory is full");
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") )
            playerInRange = true;
        if (this.transform.CompareTag("CanBeStored"))
          canBeStored = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInRange = false;
    }
}