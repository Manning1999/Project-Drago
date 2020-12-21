using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{

    //create singleton
    public static InventoryController instance;
    private static InventoryController _instance;

    public static InventoryController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<InventoryController>();
            }

            return _instance;
        }
    }



    [SerializeField]
    private List<InventoryItem> itemsInInventory = new List<InventoryItem>();

    [SerializeField]
    [Tooltip("Set to -1 to make carry weight unlimited")]
    private int maxCarryWeight = 100;

    [SerializeField]
    private int currentCarryWeight = 0;


    /// <summary>
    /// This will add an item to the inventory
    /// </summary>
    /// <param name="itemToAdd"></param>
    public void AddItemToInventory(InventoryItem itemToAdd)
    {
        itemsInInventory.Add(itemToAdd);
    }


    /// <summary>
    /// This will remove an item from the inventory
    /// </summary>
    /// <param name="itemToRemove"></param>
    public void RemoveItemFromInventory(InventoryItem itemToRemove)
    {
        itemsInInventory.Remove(itemToRemove);
    }

    public void DestroyItem(InventoryItem itemToDestroy)
    {
        RemoveItemFromInventory(itemToDestroy);
        Destroy(itemToDestroy);
    }

    [ContextMenu("Equip bow")]
    public void test()
    {
        itemsInInventory[0].gameObject.SetActive(true);
       itemsInInventory[0].Equip(PlayerController.Instance._righthand);
    }
}
