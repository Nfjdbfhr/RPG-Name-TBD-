using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{

    public Dictionary<string, List<string>> inventory = new Dictionary<string, List<string>>();

    // Start is called before the first frame update
    void Start()
    {
        inventory.Add("equipped", new List<string>());
        inventory.Add("weapons", new List<string>());
        inventory.Add("shields", new List<string>());
        inventory.Add("armor", new List<string>());
        inventory.Add("misc", new List<string>());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addItem(string itemType, string itemName)
    {
        if (inventory.ContainsKey(itemType))
        {
            inventory[itemType].Add(itemName);
        }
        else
        {
            Debug.LogWarning("Item type '" + itemType + "' does not exist in the inventory.");
        }

        for (int i = 0; i < inventory[itemType].Count; i++)
        {
            Debug.Log(inventory[itemType][i]);
        }
    }
}
