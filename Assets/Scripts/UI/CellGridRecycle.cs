using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CellGridRecycle : MonoBehaviour
{
    public struct Cell
    {
        public string itemName;
        public Sprite itemImage;
        public List<int> exits;
    }

    public Cell cell;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(SetCell);
        cell.exits = new List<int>() { };
    }

    public void SetCell()
    {

        transform.rotation = Quaternion.identity;
        cell.itemImage = InventoryRecycle.selectedItem.itemImage;
        cell.itemName = InventoryRecycle.selectedItem.itemName;

        int exitsCount = InventoryRecycle.selectedItem.exits.Count;

        for(int i = 0; i < exitsCount; i++)
        {
            cell.exits.Add(InventoryRecycle.selectedItem.exits[i]);
        }

        GetComponent<Image>().sprite = InventoryRecycle.selectedItem.itemImage;
        GridRecycle.selectedCell = GetComponent<Button>();
    }
}
