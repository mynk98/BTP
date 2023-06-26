using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemInventory : MonoBehaviour
{
    public string itemName;
    public Sprite itemImage;
    public int exit;

    public CellWasteType itemType;
    public int machineSrNo;

    public void SetItem(string name, Sprite image, int exits,CellWasteType itemType,int machineNo)
    {
        this.exit=exits;
        itemImage = image;
        itemName = name;
        GetComponent<Image>().sprite = image;
        GetComponentInChildren<TMP_Text>().text = name;
        this.itemType = itemType;
        machineSrNo = machineNo;

    }

}
