using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{

    public string itemType;

    public GameObject player;

    public PlayerController playerController;

    public InventoryManager inventoryManager;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();
        inventoryManager = GameObject.Find("Inventory Manager").GetComponent<InventoryManager>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance < 1f && Input.GetKeyDown(playerController.pickUpItem))
        {
            inventoryManager.addItem(itemType, transform.name);
            gameObject.SetActive(false);
        }
    }
}
