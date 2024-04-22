using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public int tab = 0;

    public GameObject[] tabButtons = new GameObject[5];
    public GameObject[] tabContents = new GameObject[5];
    public GameObject selectedTabIndicator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void swapTab(int tabToSwitchTo)
    {
        if (tabToSwitchTo != -1)
        {
            tab = tabToSwitchTo;
        }

        for (int i = 0; i < tabContents.Length; i++)
        {
            tabContents[i].SetActive(false);
        }

        tabContents[tab].SetActive(true);

        // selectedTabIndicator.transform.position = tabButtons[tab].transform.position;
    }
}
