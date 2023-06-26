using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineButton : MonoBehaviour
{
    public InventoryRecycle inventoryRecycle;
    public AllMachines allMachines;

    // Start is called before the first frame update
    void Start()
    {
        inventoryRecycle = InventoryRecycle.get;
        allMachines = AllMachines.get;
        
    }


    public void OnButtonClick()
    {

        inventoryRecycle.items.Add(allMachines.items[transform.GetSiblingIndex() - 1]);//remove this 1 if inactive prefab deleted
        gameObject.SetActive(false);
        print(transform.GetSiblingIndex());
    }
}
