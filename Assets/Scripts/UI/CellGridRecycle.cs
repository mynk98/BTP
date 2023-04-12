using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class CellGridRecycle : MonoBehaviour
{
    public struct Cell
    {
        public string itemName;
        public Sprite itemImage;
        public int exit;
    }

    public Cell cell;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(SetCell);
    }

    public void SetCell()
    {

        transform.rotation = Quaternion.identity;

        ItemInventory item;
        if (InventoryRecycle.selectedItem == null)
        {
            cell.itemImage = GetComponentInParent<GridRecycle>().defaultImage;
            cell.itemName = "";
            cell.exit = 0;
        }
        else
        {
            item= InventoryRecycle.selectedItem.GetComponent<ItemInventory>();
            cell.itemImage = item.itemImage;
            cell.itemName = item.itemName;
            cell.exit = (item.exit);
        }
        
        
        print("cell: "+cell.exit);
        //print("item: "+item.exit);

        GetComponent<Image>().sprite = cell.itemImage;
        GridRecycle.selectedCell = GetComponent<Button>();
    }
}
