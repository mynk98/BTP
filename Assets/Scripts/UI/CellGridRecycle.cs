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
        GridRecycle.selectedCell = GetComponent<Button>();

        if (InventoryRecycle.selectedItem == null)
        {
            cell.itemImage = GetComponentInParent<GridRecycle>().defaultImage;
            cell.itemName = "";
            cell.exit = 0;
        }
        else if(InventoryRecycle.selectedItem.transform.parent.gameObject.tag=="buttons panel")
        {
            GetComponentInParent<GridRecycle>().ButtonPressed();
        }
        else
        {
            transform.rotation = Quaternion.identity;
            ItemInventory item = InventoryRecycle.selectedItem.GetComponent<ItemInventory>();
            cell.itemImage = item.itemImage;
            cell.itemName = item.itemName;
            cell.exit = item.exit;

            GetComponent<Image>().sprite = cell.itemImage;
            GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1f);
        }

        

        print("cell: "+cell.exit);
        //print("item: "+item.exit);

        

    }
}
